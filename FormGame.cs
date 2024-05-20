using System.Collections;
using System.Collections.Generic;

namespace WumpusWorld
{
    public partial class FormGame : Form
    {
        private const int dim = 4;
        private readonly Button[,] _buttons;
        private readonly Label[,] _labels;
        private readonly Dictionary<string, Image> _images;

        private readonly ToolTip _toolTip;

        private readonly Color _closedColor = Color.FromArgb(64, 40, 32);
        private readonly Color _openedColor = Color.FromArgb(64, 64, 64);
        private readonly Color _pitColor = Color.FromArgb(0, 0, 0);
        private readonly Color _goldColor = Color.FromArgb(64, 64, 16);

        private Point index = new(0, 0);
        private Point[] pits = { new(0, 3), new(2, 0), new(3, 2) };
        private Point wumpus = new(1, 2);
        private Point gold = new(3, 3);

        private Button scopeButton = new();

        private bool wumpusIsDead = false;
        private int playerScore = 0;

        private float[,] wumpusProb = new float[dim, dim];
        private float[,] pitProb = new float[dim, dim];
        private bool[,] safe = new bool[dim, dim];
        private bool[,] visited = new bool[dim, dim];

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

            StartBoard();
        }

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
            float p = (float)1 / 5; // Probabilidade inicial dos poços
            pitProb = new float[,]
            {
                { 0, p, p, p },
                { p, p, p, p },
                { p, p, p, p },
                { p, p, p, p }
            };
            safe = new bool[,]
            {
                {true, false, false, false },
                {false, false, false, false },
                {false, false, false, false },
                {false, false, false, false }
            };
            visited = new bool[,]
            {
                {true, false, false, false },
                {false, false, false, false },
                {false, false, false, false },
                {false, false, false, false }
            };
            CheckIsSafe();
            ChangeWumpusProb(); // Probabilidade inicial do Wumpus
            UpdateUIProb();
        }

        private void CheckIfUnsafe(int x, int y, ref List<(int, int)> list)
        {
            if (!safe[x, y])
            {
                list.Add((x, y));
            }
        }

        private List<(int, int)> GetWhere(bool[,] forMatrix, bool whereValue)
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

        private void ClearWumpusProb()
        {
            for (int i = 0; i < wumpusProb.GetLength(0); i++)
            {
                for (int j = 0; j < wumpusProb.GetLength(1); j++)
                {
                    wumpusProb[i, j] = 0;
                }
            }
        }

        private void UpdateUIProb()
        {
            for (int i = wumpusProb.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < wumpusProb.GetLength(1); j++)
                {
                    string wumpusText;
                    if (wumpusProb[j, i] % 1 == 0)
                        wumpusText = wumpusProb[j, i].ToString("F0");
                    else
                        wumpusText = wumpusProb[j, i].ToString("F2");

                    _labels[j, i].Text = $"W={wumpusText}";
                }
            }
        }

        private void ChangeWumpusProb()
        {
            // Prepara modelos de onde está o Wumpus
            var sets = new List<List<(int, int)>>();
            foreach (var p in GetWhere(visited, true))
            {
                int x = p.Item1;
                int y = p.Item2;
                if (_buttons[x, y].Text.Contains("stench", StringComparison.CurrentCultureIgnoreCase))
                {
                    var list = new List<(int, int)>(4);
                    if (x - 1 >= 0)
                        CheckIfUnsafe(x - 1, y, ref list);

                    if (x + 1 < _buttons.GetLength(0))
                        CheckIfUnsafe(x + 1, y, ref list);

                    if (y - 1 >= 0)
                        CheckIfUnsafe(x, y - 1, ref list);

                    if (y + 1 < _buttons.GetLength(1))
                        CheckIfUnsafe(x, y + 1, ref list);

                    if (list.Count > 0)
                    {
                        sets.Add(list);
                    }
                }
            }

            ClearWumpusProb();

            if (sets.Count > 0)
            {
                // Inicializa a interseção com a primeira lista
                var intersection = new HashSet<(int, int)>(sets[0]);

                // Realiza a interseção com as outras listas
                foreach (var list in sets.Skip(1))
                    intersection.IntersectWith(list);

                float prob = (float)1 / intersection.Count;
                foreach (var e in intersection)
                    wumpusProb[e.Item1, e.Item2] = prob;
            }
            else
            {
                var @unsafe = GetWhere(safe, false);
                foreach (var p in @unsafe)
                    wumpusProb[p.Item1, p.Item2] = (float)1 / @unsafe.Count;
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


        private void Go()
        {
            UpdateScore(-1);
            _buttons[index.X, index.Y].Image = scopeButton.Image;
            scopeButton.Image = null;
            visited[index.X, index.Y] = true;
            safe[index.X, index.Y] = true;
            scopeButton = _buttons[index.X, index.Y];
            PaintButton(index, scopeButton);
            BackgroundImageButton(index, scopeButton);
            WorstHappened();
            CheckIsSafe();
            ChangeWumpusProb();
            UpdateUIProb();
        }


        private void CheckIsSafe()
        {
            string text = scopeButton.Text.ToLower();
            if (!text.Contains("stench") || wumpusIsDead)
            {
                if (index.X - 1 >= 0)
                    safe[index.X - 1, index.Y] = true;

                if (index.X + 1 < _buttons.GetLength(0))
                    safe[index.X + 1, index.Y] = true;

                if (index.Y - 1 >= 0)
                    safe[index.X, index.Y - 1] = true;

                if (index.Y + 1 < _buttons.GetLength(1))
                    safe[index.X, index.Y + 1] = true;
            }
        }

        private void WorstHappened()
        {
            safe[index.X, index.Y] = true;
            if ((index == wumpus && !wumpusIsDead))
            {
                safe[index.X, index.Y] = false;
                UpdateScore(-1000);
                MessageBox.Show("you were devoured by the Wumpus!");
                foreach (var b in _buttons) b.Enabled = false;
            }
            if (pits.Where(p => p == index).Any())
            {
                safe[index.X, index.Y] = false;
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