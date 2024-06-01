namespace WumpusWorld
{
    internal class PathFinder
    {
        private static readonly int[] rowOffsets = [-1, 1, 0, 0];
        private static readonly int[] colOffsets = [0, 0, -1, 1];

        private class Node
        {
            public int X { get; }
            public int Y { get; }
            public Node Parent { get; }

            public Node(int x, int y, Node parent)
            {
                X = x;
                Y = y;
                Parent = parent;
            }
        }

        public static Stack<Point> FindShortestPath(bool[,] visited, Point start, Point end)
        {
            int rows = visited.GetLength(0);
            int cols = visited.GetLength(1);

            // Verificando se os dados de entrada são válidos
            if (!IsValid(start.X, start.Y, visited) || !IsValid(end.X, end.Y, visited))
            {
                return [];
            }

            bool[,] visitedNodes = new bool[rows, cols];
            var queue = new Queue<Node>();
            queue.Enqueue(new Node(start.X, start.Y, null));
            visitedNodes[start.X, start.Y] = true;

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();

                if (current.X == end.X && current.Y == end.Y)
                {
                    // Construção do caminnho
                    return ConstructPath(current);
                }

                for (int i = 0; i < 4; i++)
                {
                    int newRow = current.X + rowOffsets[i];
                    int newCol = current.Y + colOffsets[i];

                    if (IsValid(newRow, newCol, visited) && !visitedNodes[newRow, newCol])
                    {
                        queue.Enqueue(new Node(newRow, newCol, current));
                        visitedNodes[newRow, newCol] = true;
                    }
                }
            }

            return []; 
        }


        public static Stack<Point> FindShortestPath(bool[,] visited, Point start, List<Point> ends)
        {
            int rows = visited.GetLength(0);
            int cols = visited.GetLength(1);

            // Verificando se os dados de entrada são válidos
            if (!IsValid(start.X, start.Y, visited))
            {
                return new Stack<Point>();
            }

            bool[,] visitedNodes = new bool[rows, cols];
            var queue = new Queue<Node>();
            queue.Enqueue(new Node(start.X, start.Y, null));
            visitedNodes[start.X, start.Y] = true;

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();

                if (ends.Exists(end => current.X == end.X && current.Y == end.Y))
                {
                    // Construção do caminho
                    return ConstructPath(current);
                }

                for (int i = 0; i < 4; i++)
                {
                    int newRow = current.X + rowOffsets[i];
                    int newCol = current.Y + colOffsets[i];

                    if (IsValid(newRow, newCol, visited) && !visitedNodes[newRow, newCol])
                    {
                        queue.Enqueue(new Node(newRow, newCol, current));
                        visitedNodes[newRow, newCol] = true;
                    }
                }
            }

            return new Stack<Point>(); // Nenhum caminho encontrado
        }

        private static bool IsValid(int row, int col, bool[,] visited)
        {
            return row >= 0 
                && row < visited.GetLength(0) 
                && col >= 0 
                && col < visited.GetLength(1) 
                && visited[row, col];
        }

        private static Stack<Point> ConstructPath(Node endNode)
        {
            var path = new Stack<Point>();
            Node current = endNode;

            while (current != null)
            {
                path.Push(new Point(current.X, current.Y));
                current = current.Parent;
            }

            return path;
        }
    }
}
