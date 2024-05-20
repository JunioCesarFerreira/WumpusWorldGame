namespace WumpusWorld
{
    internal class ProbabilityDistribution
    {
        private readonly int _dim;
        private readonly string _tag;
        private readonly int _numberEnemies;
        private readonly Button[,] _board;
        private readonly float[,] _probDist;
        private readonly bool[,] _safe;
        private readonly bool[,] _visited;

        public float[,] ProbDist { get { return _probDist; } }

        public ProbabilityDistribution(Button[,] board, string tag, int numberEnemies)
        {
            _board = board;
            _dim = board.GetLength(0);
            _tag = tag;
            _numberEnemies = numberEnemies;
            _probDist = new float[_dim, _dim];
            _safe = new bool[_dim, _dim];
            _visited = new bool[_dim, _dim];
        }

        public void Initialize(Point startPosition)
        {
            for (int i = 0; i < _dim; i++)
            {
                for (int j = 0; j < _dim; j++)
                {
                    _safe[i, j] = false;
                    _visited[i, j] = false;
                }
            }
            _safe[startPosition.X, startPosition.Y] = true;
            _visited[startPosition.X, startPosition.Y] = true;
            Clear();
            CheckSafety(new Point(0,0), false);
        }

        public void CheckSafety(Point position, bool isSafe)
        {
            _visited[position.X, position.Y] = true;
            if (!_board[position.X, position.Y].Text.Contains(_tag, StringComparison.CurrentCultureIgnoreCase) || isSafe)
            {
                MarkAdjacentCellsAsSafe(position);
            }
            UpdateProbabilities();
        }

        // Modifica as probabilidades da localização do Wumpus
        public void UpdateProbabilities()
        {
            // Prepara modelos de onde está o Wumpus
            var sets = new List<List<(int, int)>>();
            foreach (var p in GetPositionWhere(_visited, true))
            {
                int x = p.Item1;
                int y = p.Item2;
                if (_board[x, y].Text.Contains(_tag, StringComparison.CurrentCultureIgnoreCase))
                {
                    var list = new List<(int, int)>(4);
                    if (x - 1 >= 0)
                        AddIfUnsafe(x - 1, y, ref list);

                    if (x + 1 < _board.GetLength(0))
                        AddIfUnsafe(x + 1, y, ref list);

                    if (y - 1 >= 0)
                        AddIfUnsafe(x, y - 1, ref list);

                    if (y + 1 < _board.GetLength(1))
                        AddIfUnsafe(x, y + 1, ref list);

                    if (list.Count > 0)
                    {
                        sets.Add(list);
                    }
                }
            }

            Clear();

            if (sets.Count > 0)
            {
                // Inicializa a interseção com a primeira lista
                var intersection = new HashSet<(int, int)>(sets[0]);

                // Realiza a interseção com as outras listas
                foreach (var list in sets.Skip(1))
                    intersection.IntersectWith(list);

                float prob = (float)_numberEnemies / intersection.Count;
                foreach (var e in intersection)
                    _probDist[e.Item1, e.Item2] = prob;
            }
            else
            {
                var @unsafe = GetPositionWhere(_safe, false);
                foreach (var p in @unsafe)
                    _probDist[p.Item1, p.Item2] = (float)_numberEnemies / @unsafe.Count;
            }
        }

        // Adiciona à lista se não for seguro
        private void AddIfUnsafe(int x, int y, ref List<(int, int)> list)
        {
            if (!_safe[x, y] && !_visited[x,y]) list.Add((x, y));
        }
        public void Clear()
        {
            for (int i = 0; i < _probDist.GetLength(0); i++)
            {
                for (int j = 0; j < _probDist.GetLength(1); j++)
                {
                    _probDist[i, j] = 0;
                }
            }
        }

        private static List<(int, int)> GetPositionWhere(bool[,] forMatrix, bool whereValue)
        {
            var list = new List<(int, int)>();
            for (int i = 0; i < forMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < forMatrix.GetLength(1); j++)
                {
                    if (forMatrix[i, j] == whereValue) list.Add((i, j));
                }
            }
            return list;
        }

        private void MarkAdjacentCellsAsSafe(Point pos)
        {
            if (pos.X - 1 >= 0) _safe[pos.X - 1, pos.Y] = true;
            if (pos.X + 1 < _dim) _safe[pos.X + 1, pos.Y] = true;
            if (pos.Y - 1 >= 0) _safe[pos.X, pos.Y - 1] = true;
            if (pos.Y + 1 < _dim) _safe[pos.X, pos.Y + 1] = true;
        }
    }
}
