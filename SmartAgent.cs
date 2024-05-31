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


        public SmartAgent(Player player, Board board, HandlerInterfaceBoard handler, HazardProbabilityDistribution w, HazardProbabilityDistribution p) 
        { 
            _player = player;
            _board = board;
            _handler = handler;
            _wumpusPd = w;
            _pitPd = p;
            _visited = new bool[handler.DimX, handler.DimY];
        }

        public void Step()
        {
            // Marca como visitado
            _visited[_player.Position.X, _player.Position.Y] = true;

            // Se está na posição do tesouro e ainda não o pegou
            if (_player.Position == _board.Gold && !_player.HaveGold)
            {
                path = PathFinder.FindShortestPath(_visited, _player.Position, new Point(0, 0));
                path.Pop(); // Remove o topo pois é posição atual
                SendKeys.Send(" ");
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
                    MessageBox.Show("Finished!");
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
            }
        }

        public static void Redirect(Point pos, Point dest)
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
                {
                    for (int j = 0; j < _handler.DimY; j++)
                    {
                        if (_wumpusPd.ProbDist[i, j]==1)
                        {
                            return new Point(i, j);
                        }
                    }
                }
            }
            return null;
        }

        private Point? SearchesForUnexploredSafeCells()
        {
            var adj = new List<Point>();
            Point p = _player.Position;
            if (p.X > 0) adj.Add(new(p.X - 1, p.Y));
            if (p.Y > 0) adj.Add(new(p.X, p.Y - 1));
            if (p.X < _handler.DimX-1) adj.Add(new(p.X + 1, p.Y));
            if (p.Y < _handler.DimY-1) adj.Add(new(p.X, p.Y + 1));

            if (adj.Count == 0) return null;

            UpdateProbDist();

            Point? dest = null;
            foreach (Point pt in adj)
            {
                float sum = _wumpusPd.ProbDist[pt.X, pt.Y] + _pitPd.ProbDist[pt.X, pt.Y];
                bool v = _visited[pt.X, pt.Y];
                if (sum == 0 && !v)
                {
                    dest = pt;
                    break;
                }
            }
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
            if (dest is null)
            {
                foreach (Point pt in adj)
                {
                    float sum = _wumpusPd.ProbDist[pt.X, pt.Y] + _pitPd.ProbDist[pt.X, pt.Y];
                    if (sum == 0)
                    {
                        dest = pt;
                        break;
                    }
                }
            }
            return dest;
        }
    }
}
