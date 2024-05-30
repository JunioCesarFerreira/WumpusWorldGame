namespace WumpusWorld
{
    internal class HazardProbabilityDistribution
    {
        private readonly int[] _dim = new int[2]; // Dimensão do tabuleiro
        private readonly string _tag; // Marcação de alerta de perigo
        private readonly Button[,] _board; // Tabuleiro UI
        private readonly float[,] _probDist; // Distribuição de probabilidades de perigo
        private readonly bool[,] _safe; // Matriz que indica posições seguras
        private readonly bool[,] _visited; // Matriz que indica posições vizitadas
        private readonly int _numHazards; // Número de perigos ocultos

        public float[,] ProbDist { get { return _probDist; } }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="board">Tabuleiro na interface</param>
        /// <param name="tag">Marcação indicadora de perigo adjacente</param>
        /// <param name="num">Quantidade de perigos ocultos</param>
        public HazardProbabilityDistribution(Button[,] board, string tag, int num)
        {
            _numHazards = num;
            _board = board;
            _dim[0] = board.GetLength(0);
            _dim[1] = board.GetLength(1);
            _tag = tag;
            _probDist = new float[_dim[0], _dim[1]];
            _safe = new bool[_dim[0], _dim[1]];
            _visited = new bool[_dim[0], _dim[1]];
        }

        public void Initialize(Point startPosition)
        {
            for (int i = 0; i < _dim[0]; i++)
            {
                for (int j = 0; j < _dim[1]; j++)
                {
                    _safe[i, j] = false;
                    _visited[i, j] = false;
                }
            }
            _safe[startPosition.X, startPosition.Y] = true;
            _visited[startPosition.X, startPosition.Y] = true;
            ClearProbabilityDistribution();
            CheckSafety(startPosition, false);
        }

        public void CheckSafety(Point position, bool isSafe)
        {
            _visited[position.X, position.Y] = true;
            if (!_board[position.X, position.Y].Text.Contains(_tag, StringComparison.CurrentCultureIgnoreCase) || isSafe)
            {
                MarkAdjacentCellsAsSafe(position);
            }
        }

        public void CalculateProbabilities()
        {
            var unsafeSet = new List<(int, int)>();
            for (int i = 0; i < _dim[0]; i++)
            {
                for (int j = 0; j < _dim[1]; j++)
                {
                    if (!_safe[i, j] && !_visited[i, j])
                    {
                        unsafeSet.Add((i, j));
                    }
                }
            }

            var validCombinations = new List<List<(int, int)>>();

            var allCombinations = GetCombinations(unsafeSet, _numHazards);
            foreach (var combination in allCombinations)
            {
                if (IsValidCombination(combination))
                {
                    validCombinations.Add(combination);
                }
            }

            ClearProbabilityDistribution();

            foreach (var cell in unsafeSet)
            {
                int count = validCombinations
                    .Count(combination => combination.Contains(cell));

                _probDist[cell.Item1, cell.Item2] = (float)count / validCombinations.Count;
            }
        }

        private bool IsValidCombination(List<(int, int)> combination)
        {
            var neighborhoodCheck = new bool[_dim[0], _dim[1]];
            foreach (var cell in combination)
            {
                foreach (var neighbor in GetNeighbors(cell))
                {
                    neighborhoodCheck[neighbor.Item1, neighbor.Item2] = true;
                }
            }

            for (int i = 0; i < _dim[0]; i++)
            {
                for (int j = 0; j < _dim[1]; j++)
                {
                    if (_visited[i, j] && _board[i, j].Text.Contains(_tag, StringComparison.CurrentCultureIgnoreCase) && !neighborhoodCheck[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private IEnumerable<(int, int)> GetNeighbors((int, int) cell)
        {
            int i = cell.Item1;
            int j = cell.Item2;

            if (i > 0) yield return (i - 1, j);
            if (i < _dim[0] - 1) yield return (i + 1, j);
            if (j > 0) yield return (i, j - 1);
            if (j < _dim[1] - 1) yield return (i, j + 1);
        }

        private List<List<T>> GetCombinations<T>(List<T> list, int length)
        {
            if (length == 0) return [[]];
            if (list.Count == 0) return [];

            var result = new List<List<T>>();
            T head = list[0];
            var tail = list.Skip(1).ToList();

            foreach (var combination in GetCombinations(tail, length - 1))
            {
                combination.Insert(0, head);
                result.Add(combination);
            }

            result.AddRange(GetCombinations(tail, length));

            return result;
        }

        public void ClearProbabilityDistribution()
        {
            for (int i = 0; i < _dim[0]; i++)
            {
                for (int j = 0; j < _dim[1]; j++)
                {
                    _probDist[i, j] = 0;
                }
            }
        }

        private void MarkAdjacentCellsAsSafe(Point pos)
        {
            if (pos.X - 1 >= 0) _safe[pos.X - 1, pos.Y] = true;
            if (pos.X + 1 < _dim[0]) _safe[pos.X + 1, pos.Y] = true;
            if (pos.Y - 1 >= 0) _safe[pos.X, pos.Y - 1] = true;
            if (pos.Y + 1 < _dim[1]) _safe[pos.X, pos.Y + 1] = true;
        }
    }
}
