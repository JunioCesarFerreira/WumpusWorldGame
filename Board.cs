namespace WumpusWorld
{
    internal class Board
    {
        // Pontos de localização dos elementos do jogo
        public Point[] Pits = { new(0, 3), new(1, 2), new(3, 2) };//{ new(0, 3), new(2, 0), new(3, 2) };
        public Point Wumpus = new(2, 2);//new(1, 2);
        public Point Gold = new(3, 3);

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

            Gold = positions[0];
            Pits[0] = positions[1];
            Pits[1] = positions[2];
            Pits[2] = positions[3];
            Wumpus = positions[4];
            WumpusIsDead = false;
        }

        public bool IsWumpus(int i, int j)
        {
            return i == Wumpus.X && j == Wumpus.Y;
        }

        public bool IsPit(int i, int j)
        {
            return Pits.Where(p => i == p.X && j == p.Y).Any();
        }

        public void PlayerShootsArrow(Player player, Action KillWumpus)
        {
            player.HaveArrow = false;
            switch (player.Direction)
            {
                case "up":
                    if (player.Position.X == Wumpus.X && player.Position.Y + 1 == Wumpus.Y)
                        KillWumpus();
                    break;
                case "down":
                    if (player.Position.X == Wumpus.X && player.Position.Y - 1 == Wumpus.Y)
                        KillWumpus();
                    break;
                case "left":
                    if (player.Position.X - 1 == Wumpus.X && player.Position.Y == Wumpus.Y)
                        KillWumpus();
                    break;
                case "right":
                    if (player.Position.X + 1 == Wumpus.X && player.Position.Y == Wumpus.Y)
                        KillWumpus();
                    break;
            }

        }
    }
}
