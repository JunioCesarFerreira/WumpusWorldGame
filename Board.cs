namespace WumpusWorld
{
    internal class Board
    {
        // Pontos de localização dos elementos do jogo
        private Point[] pits = { new(0, 3), new(1, 2), new(3, 2) };//{ new(0, 3), new(2, 0), new(3, 2) };
        private Point wumpus = new(2, 2);//new(1, 2);
        private Point gold = new(3, 3);

        public Point Wumpus { get => wumpus; }
        public Point Gold { get => gold; }

        // Indicares do jogo
        public bool WumpusIsDead = false;

        public void NewGame(int dimX, int dimY)
        {
            var rand = new Random();
            var positions = new List<Point>();

            while (positions.Count < 5)
            {
                int x = rand.Next(0, dimX);
                int y = rand.Next(0, dimY);

                var p = new Point(x, y);

                if (p != new Point(0, 0) && !positions.Contains(p))
                {
                    positions.Add(p);
                }
            }

            gold = positions[0];
            pits[0] = positions[1];
            pits[1] = positions[2];
            pits[2] = positions[3];
            wumpus = positions[4];
            WumpusIsDead = false;
        }

        public bool IsWumpus(int i, int j)
        {
            return i == wumpus.X && j == wumpus.Y;
        }

        public bool IsPit(int i, int j)
        {
            return pits.Where(p => i == p.X && j == p.Y).Any();
        }

        public bool IsPit(Point pos)
        {
            return pits.Where(p => pos==p).Any();
        }

        public void PlayerShootsArrow(Player player, Action KillWumpus)
        {
            player.HaveArrow = false;
            switch (player.Direction)
            {
                case "up":
                    if (player.Position.X == wumpus.X && player.Position.Y + 1 == wumpus.Y)
                        KillWumpus();
                    break;
                case "down":
                    if (player.Position.X == wumpus.X && player.Position.Y - 1 == wumpus.Y)
                        KillWumpus();
                    break;
                case "left":
                    if (player.Position.X - 1 == wumpus.X && player.Position.Y == wumpus.Y)
                        KillWumpus();
                    break;
                case "right":
                    if (player.Position.X + 1 == wumpus.X && player.Position.Y == wumpus.Y)
                        KillWumpus();
                    break;
            }

        }
    }
}
