namespace WumpusWorld
{
    internal class SmartAgent
    {
        private readonly Player _player;
        private readonly Board _board;
        private readonly HandlerInterfaceBoard _handler;

        private readonly HazardProbabilityDistribution _wumpusPd;
        private readonly HazardProbabilityDistribution _pitPd;

        private readonly bool[,] _visited;

        private Stack<Point> path = new();

        private enum WumpusHuntState {
            None,
            Hunting,
            Shooting,
            Finished
        };
        private WumpusHuntState huntingWumpus = WumpusHuntState.None;
        private Point? wumpusPosition = null;
        private List<Point> possibleWumpusPositions = [];
        private int targetIndex = 0;

        public SmartAgent() 
        {
            // Construtor vazio apenas para Visual Studio parar de emitir warnnigs
            _player = new Player();
            _board = new Board();
            _handler = new HandlerInterfaceBoard(new Button[1,1]);
            _wumpusPd = new HazardProbabilityDistribution(new Button[1, 1], "",0);
            _pitPd = new HazardProbabilityDistribution(new Button[1, 1], "", 0); ;
            _visited = new bool[0,0];
        }

        public SmartAgent(Player player, Board board, HandlerInterfaceBoard handler, HazardProbabilityDistribution w, HazardProbabilityDistribution p) 
        { 
            _player = player;
            _board = board;
            _handler = handler;
            _wumpusPd = w;
            _pitPd = p;
            _visited = new bool[handler.DimX, handler.DimY];
        }

        private static void Redirect(Point current, Point target)
        {
            if (current.Y == target.Y)
            {
                if (current.X > target.X)
                    SendKeys.Send("{Left}");
                else
                    SendKeys.Send("{Right}");
            }
            if (current.X == target.X)
            {
                if (target.Y > current.Y)
                    SendKeys.Send("{Up}");
                else
                    SendKeys.Send("{Down}");
            }
        }

        /// <summary>
        /// Realiza um passo utilizando lógica do agente inteligênte
        /// </summary>
        /// <param name="message">Mensagens de retorno ao finalizar</param>
        /// <returns>true se ainda tem passos. false se o processo terminou, idependente do sucesso.</returns>
        public bool Step(out string message)
        {
            bool resultStep = true;
            message = "";
            // Marca como visitado
            _visited[_player.Position.X, _player.Position.Y] = true;

            // Atualiza distribuições de probabilidades
            UpdateProbDist();

            // Se está na posição do tesouro e ainda não o pegou
            if (_player.Position == _board.Gold && !_player.HaveGold)
            {
                CollectGold();
            }
            else if (_player.HaveGold) // Se já pegou o tesouro
            {
                return FollowPathToExit(out message);
            }
            else // Procura adjacência segura para explorar ou caça o Wumpus
            {
                return ExploreOrHuntWumpus(out message);
            }
            return resultStep;
        }

        private void CollectGold()
        {
            SendKeys.Send(" "); // Pega o tesouro
            // Prepara caminho de retorno para saída
            path = PathFinder.FindShortestPath(_visited, _player.Position, new Point(0, 0));
            path.Pop(); // Remove o topo pois é posição atual
        }

        private bool FollowPathToExit(out string message)
        {
            message = "";
            if (path.Count > 0)
            {
                Redirect(_player.Position, path.Pop());
                SendKeys.Send("{Enter}");
            }
            else
            {
                message = "Completed successfully!";
                SendKeys.Send("{Down}");
                return false;
            }
            return true;
        }


        private bool ExploreOrHuntWumpus(out string message)
        {
            message = "";
            Point? nextMove = FindUnexploredSafeCell();
            if (nextMove.HasValue)
            {
                Redirect(_player.Position, nextMove.Value);
                SendKeys.Send("{Enter}");
            }
            else if (!_board.WumpusIsDead)
            {
                if (!WumpusHuntStateMachine())
                {
                    if (TryFindPossibleWumpusPositions())
                    {
                        var safePositions = GetSafePositionsForHunt();
                        if (safePositions.Count > 0)
                        {
                            return PreparePathToHunt(safePositions);
                        }
                    }
                    message = "There are no more moves without the risk of death.";
                    return false;
                }
            }
            else
            {
                message = "There are no more moves without the risk of death.";
                return false;
            }
            return true;
        }

        private List<Point> GetSafePositionsForHunt()
        {
            var adj1 = GetAdjacentCells(possibleWumpusPositions[0]);
            var adj2 = GetAdjacentCells(possibleWumpusPositions[1]);
            return adj1.Intersect(adj2).Where(p => _visited[p.X, p.Y]).ToList();
        }

        private bool TryFindPossibleWumpusPositions()
        {
            possibleWumpusPositions.Clear();
            if (!_board.WumpusIsDead && _player.HaveArrow)
            {
                for (int i = 0; i < _handler.DimX; i++)
                {
                    for (int j = 0; j < _handler.DimY; j++)
                    {
                        if (_wumpusPd.ProbDist[i, j] == 0.5)
                        {
                            possibleWumpusPositions.Add(new Point(i, j));
                        }
                    }
                }
            }
            return possibleWumpusPositions.Count == 2;
        }

        private Point? FindUnexploredSafeCell()
        {
            var adj = GetAdjacentCells(_player.Position);

            foreach (var cell in adj)
            {
                if (IsSafeAndUnvisited(cell)) return cell;
            }

            var unexploredSafeCells = new List<Point>();
            for (int i = 0; i < _handler.DimX; i++)
            {
                for (int j = 0; j < _handler.DimY; j++)
                {
                    if (IsSafeAndUnvisited(new Point(i, j)))
                        unexploredSafeCells.Add(new Point(i, j));
                }
            }

            if (unexploredSafeCells.Count > 0)
                return FindClosestUnexploredSafeCell(unexploredSafeCells);

            return null;
        }

        private bool IsSafeAndUnvisited(Point cell)
        {
            return (_wumpusPd.ProbDist[cell.X, cell.Y] + _pitPd.ProbDist[cell.X, cell.Y] == 0 ||
                    (wumpusPosition.HasValue && cell == wumpusPosition && _board.WumpusIsDead)) &&
                    !_visited[cell.X, cell.Y];
        }

        private Point? FindClosestUnexploredSafeCell(List<Point> unexploredSafeCells)
        {
            foreach (var cell in unexploredSafeCells)
            {
                _visited[cell.X, cell.Y] = true;
                var path = PathFinder.FindShortestPath(_visited, _player.Position, cell);
                _visited[cell.X, cell.Y] = false;
                if (path.Count >= 2)
                {
                    path.Pop();
                    return path.Pop();
                }
            }
            return null;
        }

        private bool PreparePathToHunt(List<Point> dest)
        {
            path = PathFinder.FindShortestPath(_visited, _player.Position, dest);
            path.Pop(); // Remove o topo pois é posição atual
            if (path.Count == 0)
            {
                huntingWumpus = WumpusHuntState.Shooting;
                return AimToShoot();
            }
            else
            {
                huntingWumpus = WumpusHuntState.Hunting;
                Redirect(_player.Position, path.Pop());
                SendKeys.Send("{Enter}");
            }
            return true;
        }

        private bool AimToShoot()
        {
            if (wumpusPosition.HasValue)
                Redirect(_player.Position, wumpusPosition.Value);
            else if (possibleWumpusPositions.Count == 2)
            {
                targetIndex = new Random().Next(0, 1);
                Redirect(_player.Position, possibleWumpusPositions[targetIndex]);
            }
            else return false;
            return true;
        }

        private bool WumpusHuntStateMachine()
        {
            bool isRunning = true; 
            switch (huntingWumpus)
            {
                case WumpusHuntState.None:
                    wumpusPosition = SearchesWumpus();
                    if (wumpusPosition.HasValue)
                    {
                        var adj = GetAdjacentCells(wumpusPosition.Value);
                        isRunning = PreparePathToHunt(adj);
                    }
                    else isRunning = false;
                    break;
                case WumpusHuntState.Hunting:
                    if (path.Count == 0)
                    {
                        huntingWumpus = WumpusHuntState.Shooting;
                        isRunning = AimToShoot();
                    }
                    else
                    {
                        Redirect(_player.Position, path.Pop());
                        SendKeys.Send("{Enter}");
                    }
                    break;
                case WumpusHuntState.Shooting:
                    SendKeys.Send("a"); // Atira a flecha
                    huntingWumpus = WumpusHuntState.Finished;
                    if (possibleWumpusPositions.Count == 2)
                    {
                        if (_board.WumpusIsDead)
                        {
                            _wumpusPd.UpdateSafety(possibleWumpusPositions[targetIndex == 0 ? 1 : 0]);
                        }
                        else
                        {
                            _wumpusPd.UpdateSafety(possibleWumpusPositions[targetIndex]);
                        }
                    }
                    break;
                case WumpusHuntState.Finished:
                    isRunning = false;
                    break;
            }
            return isRunning;
        }

        private void UpdateProbDist()
        {
            if (_board.WumpusIsDead) _wumpusPd.ClearProbabilityDistribution();
            else _wumpusPd.CalculateProbabilities();

            _pitPd.CalculateProbabilities();
        }

        private Point? SearchesWumpus()
        {
            if (!_board.WumpusIsDead && _player.HaveArrow)
            {
                for (int i = 0; i < _handler.DimX; i++)
                    for (int j = 0; j < _handler.DimY; j++)
                        if (_wumpusPd.ProbDist[i, j]==1)
                            return new Point(i, j);
            }
            return null;
        }

        private List<Point> GetAdjacentCells(Point p)
        {
            var adj = new List<Point>();
            if (p.X > 0) adj.Add(new(p.X - 1, p.Y));
            if (p.Y > 0) adj.Add(new(p.X, p.Y - 1));
            if (p.X < _handler.DimX - 1) adj.Add(new(p.X + 1, p.Y));
            if (p.Y < _handler.DimY - 1) adj.Add(new(p.X, p.Y + 1));
            return adj;
        }
    }
}
