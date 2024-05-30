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
            _visited[_player.Position.X, _player.Position.Y] = true;
            if (_player.Position == _board.Gold && !_player.HaveGold)
            {
                path = PathFinder.FindShortestPath(_visited, _player.Position, new Point(0, 0));
                path.Pop(); // Remove o topo pois é posição atual
                SendKeys.Send(" ");
            }
            else if (_player.HaveGold)
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
            else
            {
                Point? point = SeeksBetterAdj();
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

        private Point? SeeksBetterAdj()
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
