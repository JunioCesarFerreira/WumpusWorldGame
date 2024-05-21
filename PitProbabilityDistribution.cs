namespace WumpusWorld
{
    internal class PitProbabilityDistribution
    {
        private readonly int _dim;
        private readonly string _tag;
        private readonly Button[,] _board;
        private readonly Dictionary<(int,int),float> _probDist;
        private readonly bool[,] _safe;
        private readonly bool[,] _visited;
        private readonly List<(int, int)> _found = new List<(int, int)>();

        public Dictionary<(int, int), float> ProbDist { get { return _probDist; } }

        public PitProbabilityDistribution(Button[,] board, string tag)
        {
            _board = board;
            _dim = board.GetLength(0);
            _tag = tag;
            _probDist = new Dictionary<(int, int), float>(_dim*_dim);
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
            CheckSafety(startPosition, false);
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
            foreach (var p in GetPositionsWhere(_visited, true))
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
                foreach (var list in sets)
                {
                    float prob = (float)1 / list.Count;
                    foreach (var e in list)
                        _probDist[(e.Item1, e.Item2)] = prob;
                }
            }
            else
            {
                var @unsafe = GetPositionsWhere(_safe, false);
                foreach (var p in @unsafe)
                {
                    _probDist[(p.Item1, p.Item2)] = (float)3 / @unsafe.Count;
                }
            }
            var greaters = _probDist.Where(e => e.Value > 1).ToArray();
            foreach (var e in greaters)
            {
                _probDist[e.Key] = e.Value/greaters.Length;
                if (_probDist[e.Key] > 1)
                {
                    float tmp = _probDist[e.Key] - 1;
                    _probDist[e.Key] = 1;
                    foreach (var others in _probDist)
                    {
                        if (others.Value == 0 && !_safe[others.Key.Item1, others.Key.Item2] && !_visited[others.Key.Item1, others.Key.Item2])
                        {
                            _probDist[others.Key] = tmp / GetPositionsWhere(_safe, false).Count;
                        }
                    }
                }
            }
            foreach (var e in _probDist)
            {
                if (_probDist[e.Key] == 1 && !_found.Contains(e.Key))
                {
                    _found.Add(e.Key);
                }
            }
            if (_found.Count > 0)
            {
                foreach (var k in _found)
                {
                    _probDist[k] = 1;
                }
            }
            if (_found.Count == 3)
            {
                _probDist
                    .Where(e => e.Value != 1)
                    .ToList()
                    .ForEach(e => _probDist[e.Key] = 0 );
            }
        }

        // Adiciona à lista se não for seguro
        private void AddIfUnsafe(int x, int y, ref List<(int, int)> list)
        {
            if (!_safe[x, y] && !_visited[x, y]) list.Add((x, y));
        }

        private void Clear()
        {
            for (int i = 0; i < _dim; i++)
            {
                for (int j = 0; j < _dim; j++)
                {
                    _probDist[(i, j)] = 0;
                }
            }
        }

        private static List<(int, int)> GetPositionsWhere(bool[,] forMatrix, bool whereValue)
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
