namespace WumpusWorld
{
    public partial class FormGame : Form
    {
        private readonly Button[,] _matrix;
        private readonly Dictionary<string, Image> _images;

        private Point index = new(0, 0);
        private Point[] pits = { new(0, 3), new(2, 0), new(3, 2) };
        private Point wumpus = new(1, 2);
        private Point gold = new(3, 3);

        private Button scopeButton = new();

        private bool wumpusIsDead = false;
        private int playerScore = 0;

        private readonly ToolTip _toolTip;

        private readonly Color _closedColor = Color.FromArgb(64, 40, 32);
        private readonly Color _openedColor = Color.FromArgb(64, 64, 64);
        private readonly Color _pitColor = Color.FromArgb(0, 0, 0);
        private readonly Color _goldColor = Color.FromArgb(64, 64, 16);

        class Player
        {
            public string Direction = "down";
            public bool HaveGold = false;
            public bool HaveArrow = true;
            public bool HeardScream = false;
        }
        private Player player = new();


        public FormGame()
        {
            InitializeComponent();

            _toolTip = new ToolTip();

            string[] pathImgs = Directory.GetFiles("img");
            _images = new Dictionary<string, Image>(6);
            foreach (string path in pathImgs)
            {
                string name = Path.GetFileNameWithoutExtension(path);
                _images[name] = Image.FromFile(path);
            }
            _matrix = new Button[4, 4]
            {
                { button1, button5, button9, button13 },
                { button2, button6, button10, button14 },
                { button3, button7, button11, button15 },
                { button4, button8, button12, button16 },
            };

            _toolTip.SetToolTip(button_left, "Move Left (Arrow Left)");
            _toolTip.SetToolTip(button_right, "Move Right (Arrow Right)");
            _toolTip.SetToolTip(button_up, "Move Up (Arrow Up)");
            _toolTip.SetToolTip(button_down, "Move Down (Arrow Down)");
            _toolTip.SetToolTip(button_go, "Go (Enter)");
            _toolTip.SetToolTip(button_get, "Get Gold (Space)");
            _toolTip.SetToolTip(button_arrow, "Shoot Arrow (A)");

            StartBoard();
        }

        private void StartBoard()
        {
            foreach (Button button in _matrix)
            {
                button.Text = "";
                button.ForeColor = _closedColor;
                button.BackColor = _closedColor;
                button.BackgroundImage = null;
                button.Image = null;
                button.BackgroundImageLayout = ImageLayout.Stretch;
                button.FlatAppearance.MouseOverBackColor = _closedColor;
                button.FlatAppearance.MouseDownBackColor = _closedColor;
                button.Enabled = true;
            }
            playerScore = 0;
            player = new Player();
            index = new(0, 0);
            wumpusIsDead = false;
            button_arrow.Enabled = true;
            scopeButton = _matrix[index.X, index.Y];
            scopeButton.Image = _images["player_down"];
            PaintButton(index, scopeButton);
            Tagging();
            label_scream.Visible = false;
        }


        private void Button_New_Game_MouseClick(object sender, MouseEventArgs e)
        {
            StartBoard();
        }

        private void Button_Show_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    Point p = new(i, j);
                    PaintButton(p, _matrix[i, j]);
                    BackgroundImageButton(p, _matrix[i, j]);
                }
            }
        }

        private void Directions_Click(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                player.Direction = button.Name.Split("_")[1];
                scopeButton.Image = _images["player_" + player.Direction];
            }
        }

        private void Button_Go_Click(object sender, EventArgs e)
        {
            switch (player.Direction)
            {
                case "up":
                    if (index.Y < game_grid.RowCount - 1)
                    {
                        index.Y++;
                        Go();
                    }
                    break;
                case "down":
                    if (index.Y > 0)
                    {
                        index.Y--;
                        Go();
                    }
                    break;
                case "left":
                    if (index.X > 0)
                    {
                        index.X--;
                        Go();
                    }
                    break;
                case "right":
                    if (index.X < game_grid.RowCount - 1)
                    {
                        index.X++;
                        Go();
                    }
                    break;
            }
        }

        private void Button_Get_Click(object sender, EventArgs e)
        {
            if (index == gold)
            {
                UpdateScore(1000);
                scopeButton.BackgroundImage = null;
                scopeButton.BackColor = _openedColor;
                scopeButton.FlatAppearance.MouseOverBackColor = _openedColor;
                player.HaveGold = true;
            }
        }

        private void Button_Arrow_Click(object sender, EventArgs e)
        {
            if (player.HaveArrow)
            {
                UpdateScore(-10);
                player.HaveArrow = false;
                button_arrow.Enabled = false;
                switch (player.Direction)
                {
                    case "up":
                        if (index.X == wumpus.X && index.Y + 1 == wumpus.Y)
                            KillWumpus();
                        break;
                    case "down":
                        if (index.X == wumpus.X && index.Y - 1 == wumpus.Y)
                            KillWumpus();
                        break;
                    case "left":
                        if (index.X - 1 == wumpus.X && index.Y == wumpus.Y)
                            KillWumpus();
                        break;
                    case "right":
                        if (index.X + 1 == wumpus.X && index.Y == wumpus.Y)
                            KillWumpus();
                        break;
                }
            }
        }


        private void Go()
        {
            UpdateScore(-1);
            _matrix[index.X, index.Y].Image = scopeButton.Image;
            scopeButton.Image = null;
            scopeButton = _matrix[index.X, index.Y];
            PaintButton(index, scopeButton);
            BackgroundImageButton(index, scopeButton);
            WorstHappened();
        }

        private void WorstHappened()
        {
            if ((index == wumpus && !wumpusIsDead))
            {
                UpdateScore(-1000);
                MessageBox.Show("you were devoured by the Wumpus!");
                foreach (var b in _matrix) b.Enabled = false;
            }
            if (pits.Where(p => p == index).Any())
            {
                UpdateScore(-1000);
                MessageBox.Show("you fell into the pit and died!");
                foreach (var b in _matrix) b.Enabled = false;
            }
        }

        private void PaintButton(Point point, Button button)
        {
            button.BackColor = _openedColor;
            button.FlatAppearance.MouseOverBackColor = _openedColor;
            button.ForeColor = Color.White;
            if (point == wumpus)
            {
                button.BackColor = _openedColor;
                button.FlatAppearance.MouseOverBackColor = _openedColor;
            }
            if (pits.Where(p => p == point).Any())
            {
                button.BackColor = _pitColor;
                button.FlatAppearance.MouseOverBackColor = _pitColor;
            }
            if (point == gold)
            {
                button.BackColor = _goldColor;
                button.FlatAppearance.MouseOverBackColor = _goldColor;
            }
        }

        private bool IsWumpus(int i, int j)
        {
            return i == wumpus.X && j == wumpus.Y;
        }

        private bool IsPit(int i, int j)
        {
            return pits.Where(p => i == p.X && j == p.Y).Any();
        }

        private void TagStench(int i, int j)
        {
            if (!_matrix[i, j].Text.Contains("Stench"))
            {
                _matrix[i, j].Text += "Stench\n";
            }
        }

        private void TagBreeze(int i, int j)
        {
            if (!_matrix[i, j].Text.Contains("Breeze"))
            {
                _matrix[i, j].Text += "Breeze\n";
            }
        }

        private void Tagging()
        {
            int maxX = _matrix.GetLength(0) - 1;
            int maxY = _matrix.GetLength(1) - 1;
            for (int i = 0; i <= maxX; i++)
            {
                for (int j = 0; j <= maxY; j++)
                {
                    if (i + 1 <= maxX)
                    {
                        if (IsWumpus(i + 1, j)) TagStench(i, j);
                        if (IsPit(i + 1, j)) TagBreeze(i, j);
                    }
                    if (j + 1 <= maxY)
                    {
                        if (IsWumpus(i, j + 1)) TagStench(i, j);
                        if (IsPit(i, j + 1)) TagBreeze(i, j);
                    }
                    if (i - 1 >= 0)
                    {
                        if (IsWumpus(i - 1, j)) TagStench(i, j);
                        if (IsPit(i - 1, j)) TagBreeze(i, j);
                    }
                    if (j - 1 >= 0)
                    {
                        if (IsWumpus(i, j - 1)) TagStench(i, j);
                        if (IsPit(i, j - 1)) TagBreeze(i, j);
                    }
                }
            }
        }

        private void BackgroundImageButton(Point point, Button button)
        {
            if (point == wumpus)
            {
                button.BackgroundImage = wumpusIsDead ? _images["dead_wumpus"] : _images["wumpus"];
            }
            if (point == gold)
            {
                button.BackgroundImage = _images["gold"];
            }
        }

        private void KillWumpus()
        {
            wumpusIsDead = true;
            player.HeardScream = true;
            label_scream.Visible = true;
            foreach (var b in _matrix)
            {
                if (b.BackgroundImage != null)
                {
                    if (b.BackgroundImage == _images["wumpus"])
                    {
                        b.BackgroundImage = _images["dead_wumpus"];
                        break;
                    }
                }
            }
        }

        private void UpdateScore(int value)
        {
            playerScore += value;
            label_score.Text = playerScore.ToString();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    button_go.PerformClick();
                    break;
                case Keys.Space:
                    button_get.PerformClick();
                    break;
                case Keys.Left:
                    Directions_Click(button_left, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
                case Keys.Right:
                    Directions_Click(button_right, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
                case Keys.Up:
                    Directions_Click(button_up, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
                case Keys.Down:
                    Directions_Click(button_down, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
                case Keys.A:
                    button_arrow.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}