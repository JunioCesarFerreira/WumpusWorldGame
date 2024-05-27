namespace WumpusWorld
{
    public partial class FormGame : Form
    {
        private const int dim = 4; // Dimens�o do problema
        
        private readonly Button[,] _buttons; // Matriz de bot�es que comp�e o tabuleiro
        private readonly Label[,] _labels;   // Matriz de labels de probabilidades

        private readonly Dictionary<string, Image> _images;  

        private readonly ToolTip _toolTip; // ToolTip dos bot�es de controle

        // Cores utilizadas nas c�lulas do tabuleiro 
        private readonly Color _closedColor = Color.FromArgb(64, 40, 32);
        private readonly Color _openedColor = Color.FromArgb(64, 64, 64);
        private readonly Color _pitColor = Color.FromArgb(0, 0, 0);
        private readonly Color _goldColor = Color.FromArgb(64, 64, 16);

        // Pontos de localiza��o dos elementos do jogo
        private Point index = new(0, 0);
        private Point[] pits = { new(0, 3), new(2, 0), new(3, 2) };
        private Point wumpus = new(1, 2);
        private Point gold = new(3, 3);

        // Posi��o atual do jogador
        private Button scopeButton = new();

        // Indicares do jogo
        private bool wumpusIsDead = false;
        private int playerScore = 0;

        // Distribui��es de probabilidades
        private readonly HazardProbabilityDistribution _wumpusPd;
        private readonly HazardProbabilityDistribution _pitPd; // Em desenvolvimento...

        // Classe Jogador
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
            _buttons = new Button[dim, dim]
            {
                { button1, button5, button9, button13 },
                { button2, button6, button10, button14 },
                { button3, button7, button11, button15 },
                { button4, button8, button12, button16 },
            };
            _labels = new Label[dim, dim]
            {
                {label1, label2, label3, label4},
                {label5, label6, label7, label8},
                {label9, label10, label11, label12},
                {label13, label14, label15, label16}
            };

            _toolTip.SetToolTip(button_left, "Move Left (Arrow Left)");
            _toolTip.SetToolTip(button_right, "Move Right (Arrow Right)");
            _toolTip.SetToolTip(button_up, "Move Up (Arrow Up)");
            _toolTip.SetToolTip(button_down, "Move Down (Arrow Down)");
            _toolTip.SetToolTip(button_go, "Go (Enter)");
            _toolTip.SetToolTip(button_get, "Get Gold (Space)");
            _toolTip.SetToolTip(button_arrow, "Shoot Arrow (A)");

            _wumpusPd = new HazardProbabilityDistribution(_buttons, "stench", 1);
            _pitPd = new HazardProbabilityDistribution(_buttons, "breeze", 3);

            StartBoard();
        }

        // Inicializa Tabuleiro
        private void StartBoard()
        {
            foreach (Button button in _buttons)
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
            label_score.Text = "0";
            player = new Player();
            index = new(0, 0);
            wumpusIsDead = false;
            button_arrow.Enabled = true;
            scopeButton = _buttons[index.X, index.Y];
            scopeButton.Image = _images["player_down"];
            PaintButton(index, scopeButton);
            Tagging();
            label_scream.Visible = false;
            _wumpusPd.Initialize(index);
            _pitPd.Initialize(index);
            UpdateProbDist();
        }

        // Atualiza tabela de distribui��es de probabilidades
        private void UpdateProbDist()
        {
            if (wumpusIsDead) _wumpusPd.ClearProbabilityDistribution();
            else _wumpusPd.CalculateProbabilities();

            _pitPd.CalculateProbabilities();

            for (int i = _wumpusPd.ProbDist.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < _wumpusPd.ProbDist.GetLength(1); j++)
                {
                    string wumpusText;
                    if (_wumpusPd.ProbDist[j, i] % 1 == 0)
                        wumpusText = _wumpusPd.ProbDist[j, i].ToString("F0");
                    else
                        wumpusText = _wumpusPd.ProbDist[j, i].ToString("F2");

                    string pitsText;
                    if (_pitPd.ProbDist[j, i] % 1 == 0)
                        pitsText = _pitPd.ProbDist[j, i].ToString("F0");
                    else
                        pitsText = _pitPd.ProbDist[j, i].ToString("F2");

                    _labels[j, i].Text = $"W={wumpusText}\nP={pitsText}";
                }
            }
        }

        private void Button_New_Game_MouseClick(object sender, MouseEventArgs e)
        {
            var rand = new Random();
            var positions = new List<Point>();

            while (positions.Count < 5)
            {
                int x = rand.Next(0, _buttons.GetLength(0));
                int y = rand.Next(0, _buttons.GetLength(1));

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

            StartBoard();
        }

        private void Button_Show_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < _buttons.GetLength(0); i++)
            {
                for (int j = 0; j < _buttons.GetLength(1); j++)
                {
                    Point p = new(i, j);
                    PaintButton(p, _buttons[i, j]);
                    BackgroundImageButton(p, _buttons[i, j]);
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
                        MovePlayer();
                    }
                    break;
                case "down":
                    if (index.Y > 0)
                    {
                        index.Y--;
                        MovePlayer();
                    }
                    break;
                case "left":
                    if (index.X > 0)
                    {
                        index.X--;
                        MovePlayer();
                    }
                    break;
                case "right":
                    if (index.X < game_grid.RowCount - 1)
                    {
                        index.X++;
                        MovePlayer();
                    }
                    break;
            }
        }

        private void Button_Get_Click(object sender, EventArgs e)
        {
            if (index == gold && !player.HaveGold)
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


        private void MovePlayer()
        {
            UpdateScore(-1);
            _buttons[index.X, index.Y].Image = scopeButton.Image;
            scopeButton.Image = null;
            scopeButton = _buttons[index.X, index.Y];
            PaintButton(index, scopeButton);
            BackgroundImageButton(index, scopeButton);
            WorstHappened();

            _wumpusPd.CheckSafety(index, wumpusIsDead);
            _pitPd.CheckSafety(index, false);

            UpdateProbDist();
        }

        private void WorstHappened()
        {;
            if ((index == wumpus && !wumpusIsDead))
            {
                UpdateScore(-1000);
                MessageBox.Show("you were devoured by the Wumpus!");
                foreach (var b in _buttons) b.Enabled = false;
            }
            if (pits.Where(p => p == index).Any())
            {;
                UpdateScore(-1000);
                MessageBox.Show("you fell into the pit and died!");
                foreach (var b in _buttons) b.Enabled = false;
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
            if (point == gold && !player.HaveGold)
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
            if (!_buttons[i, j].Text.Contains("Stench"))
            {
                _buttons[i, j].Text += "Stench\n";
            }
        }

        private void TagBreeze(int i, int j)
        {
            if (!_buttons[i, j].Text.Contains("Breeze"))
            {
                _buttons[i, j].Text += "Breeze\n";
            }
        }

        private void Tagging()
        {
            int maxX = _buttons.GetLength(0) - 1;
            int maxY = _buttons.GetLength(1) - 1;
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
            if (point == gold && !player.HaveGold)
            {
                button.BackgroundImage = _images["gold"];
            }
        }

        private void KillWumpus()
        {
            wumpusIsDead = true;
            player.HeardScream = true;
            label_scream.Visible = true;
            foreach (var b in _buttons)
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