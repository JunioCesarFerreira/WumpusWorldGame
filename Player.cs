namespace WumpusWorld
{
    internal class Player
    {
        public Point Position = new(0, 0);
        public string Direction = "down";
        public bool HaveGold = false;
        public bool HaveArrow = true;
        public bool HeardScream = false;
        public int Score = 0;
    }
}
