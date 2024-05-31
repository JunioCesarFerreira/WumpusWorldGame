namespace WumpusWorld
{
    internal class SmartAgent
    {
        private readonly Player _player;
        private readonly Board _board;
        private readonly HandlerInterfaceBoard _handler;

        private readonly HazardProbabilityDistribution _wumpusPd;
        private readonly HazardProbabilityDistribution _pitPd;

        private Stack<Point> path = new();

        private readonly bool[,] _visited;
        private enum HuntingWumpus {
            None,
            Hunting,
            Shooting,
            Finished
        };
        private HuntingWumpus huntingWumpus = HuntingWumpus.None;
        private Point? wumpusPosition = null;

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
                SendKeys.Send(" "); // Pega o tesouro
                // Prepara caminho de retorno para saída
                path = PathFinder.FindShortestPath(_visited, _player.Position, new Point(0, 0));
                path.Pop(); // Remove o topo pois é posição atual
            }
            else if (_player.HaveGold) // Se já pegou o tesouro
            {
                if (path.Count > 0)
                {
                    Redirect(_player.Position, path.Pop());
                    SendKeys.Send("{Enter}");
                }
                else
                {
                    resultStep = false;
                    message = "Completed successfully!";
                    SendKeys.Send("{Down}");
                }
            }
            else // Procura adjacência segura para explorar
            {
                Point? point = SearchesForUnexploredSafeCells();
                if (point.HasValue)
                {
                    Redirect(_player.Position, point.Value);
                    SendKeys.Send("{Enter}");
                }
                else if (!_board.WumpusIsDead)
                {
                    if (!HuntingWumpusStateMachine())
                    {
                        resultStep = false;
                        message = "There are no more moves without the risk of death.";
                    }
                }
                else
                {
                    resultStep = false;
                    message = "There are no more moves without the risk of death.";
                }
            }
            return resultStep;
        }

        private bool HuntingWumpusStateMachine()
        {
            bool running = true; 
            switch (huntingWumpus)
            {
                case HuntingWumpus.None:
                    wumpusPosition = SearchesWumpus();
                    if (wumpusPosition.HasValue)
                    {
                        var adj = Adjacency(wumpusPosition.Value);
                        path = PathFinder.FindShortestPath(_visited, _player.Position, adj);
                        path.Pop(); // Remove o topo pois é posição atual
                        if (path.Count == 0)
                        {
                            huntingWumpus = HuntingWumpus.Shooting;
                            if (wumpusPosition.HasValue)
                                Redirect(_player.Position, wumpusPosition.Value);
                            else running = false;
                        }
                        else
                        {
                            huntingWumpus = HuntingWumpus.Hunting;
                            Redirect(_player.Position, path.Pop());
                            SendKeys.Send("{Enter}");
                        }
                    }
                    else running = false;
                    break;
                case HuntingWumpus.Hunting:
                    if (path.Count == 0)
                    {
                        huntingWumpus = HuntingWumpus.Shooting;
                        if (wumpusPosition.HasValue)
                            Redirect(_player.Position, wumpusPosition.Value);
                        else running = false;
                    }
                    else
                    {
                        Redirect(_player.Position, path.Pop());
                        SendKeys.Send("{Enter}");
                    }
                    break;
                case HuntingWumpus.Shooting:
                    SendKeys.Send("a"); // Atira a flecha
                    huntingWumpus = HuntingWumpus.Finished;
                    break;
                case HuntingWumpus.Finished:
                    running = false;
                    break;
            }
            return running;
        }

        private static void Redirect(Point pos, Point dest)
        {
            if (pos.Y == dest.Y)
            {
                if (pos.X > dest.X)
                    SendKeys.Send("{Left}");
                else
                    SendKeys.Send("{Right}");
            }
            if (pos.X == dest.X)
            {
                if (dest.Y > pos.Y)
                    SendKeys.Send("{Up}");
                else
                    SendKeys.Send("{Down}");
            }
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

        private List<Point> Adjacency(Point p)
        {
            var adj = new List<Point>();
            if (p.X > 0) adj.Add(new(p.X - 1, p.Y));
            if (p.Y > 0) adj.Add(new(p.X, p.Y - 1));
            if (p.X < _handler.DimX - 1) adj.Add(new(p.X + 1, p.Y));
            if (p.Y < _handler.DimY - 1) adj.Add(new(p.X, p.Y + 1));
            return adj;
        }

        private Point? SearchesForUnexploredSafeCells()
        {
            var adj = Adjacency(_player.Position);

            Point? dest = null;
            // Procura por células adjacentes seguras e inexploradas
            foreach (Point pt in adj)
            {
                float sum = _wumpusPd.ProbDist[pt.X, pt.Y] + _pitPd.ProbDist[pt.X, pt.Y];
                bool v = _visited[pt.X, pt.Y];
                if ((sum == 0 || (pt==wumpusPosition && _board.WumpusIsDead)) && !v)
                {
                    dest = pt;
                    break;
                }
            }
            // Procura por outras células seguras e inexplorados
            if (dest is null)
            {
                var list = new List<Point>();
                for (int i=0; i < _handler.DimX; i++)
                {
                    for (int j=0; j< _handler.DimY; j++)
                    {
                        float sum = _wumpusPd.ProbDist[i, j] + _pitPd.ProbDist[i, j];
                        if (sum == 0 && !_visited[i,j])
                        {
                            list.Add(new Point(i, j));
                        }
                    }
                }
                if (list.Count > 0)
                {
                    foreach (Point pt in list)
                    {
                        _visited[pt.X, pt.Y] = true;
                        var tmp = PathFinder.FindShortestPath(_visited, _player.Position, pt);
                        _visited[pt.X, pt.Y] = false;
                        if (tmp.Count >= 2)
                        {
                            tmp.Pop();
                            dest = tmp.Pop();
                            break;
                        }
                    }
                }
            }
            return dest;
        }
    }
}
