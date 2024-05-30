namespace WumpusWorld
{
    public partial class FormGame : Form
    {
        private const int dim = 4; // Dimensão do problema

        private readonly HandlerInterfaceBoard _handlerBoard; 

        private readonly ToolTip _toolTip; // ToolTip dos botões de controle

        private readonly Label[,] _labels;   // Matriz de labels de probabilidades

        // Distribuições de probabilidades
        private readonly HazardProbabilityDistribution _wumpusPd;
        private readonly HazardProbabilityDistribution _pitPd; // Em desenvolvimento...

        private readonly Board _board = new();

        private Player player = new();


        public FormGame()
        {
            InitializeComponent();

            _toolTip = new ToolTip();

            var buttons = new Button[dim, dim]
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

            _handlerBoard = new HandlerInterfaceBoard(buttons);

            _toolTip.SetToolTip(button_left, "Move Left (Arrow Left)");
            _toolTip.SetToolTip(button_right, "Move Right (Arrow Right)");
            _toolTip.SetToolTip(button_up, "Move Up (Arrow Up)");
            _toolTip.SetToolTip(button_down, "Move Down (Arrow Down)");
            _toolTip.SetToolTip(button_go, "Go (Enter)");
            _toolTip.SetToolTip(button_get, "Get Gold (Space)");
            _toolTip.SetToolTip(button_arrow, "Shoot Arrow (A)");

            _wumpusPd = new HazardProbabilityDistribution(buttons, "stench", 1);
            _pitPd = new HazardProbabilityDistribution(buttons, "breeze", 3);

            _board = new Board();
            StartBoard();
        }

        // Inicializa Tabuleiro
        private void StartBoard()
        {
            _handlerBoard.StartBoard();

            player = new Player();

            label_score.Text = _board.PlayerScore.ToString();

            button_arrow.Enabled = true;

            _handlerBoard.Tagging(_board);
            
            label_scream.Visible = false;

            _wumpusPd.Initialize(player.Position);
            _pitPd.Initialize(player.Position);

            UpdateProbDist();
        }

        // Atualiza tabela de distribuições de probabilidades
        private void UpdateProbDist()
        {
            if (_board.WumpusIsDead) _wumpusPd.ClearProbabilityDistribution();
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
            _board.NewGame(_handlerBoard.DimX, _handlerBoard.DimY);
            StartBoard();
        }

        private void Button_Show_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < _handlerBoard.DimX; i++)
            {
                for (int j = 0; j < _handlerBoard.DimY; j++)
                {
                    Point p = new(i, j);
                    _handlerBoard.PaintButton(p, _board, player);
                    _handlerBoard.BackgroundImageButton(p, _board, player);
                }
            }
        }

        private void Directions_Click(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                player.Direction = button.Name.Split("_")[1];
                _handlerBoard.UpdatePlayerDirection(player);
            }
        }

        private void Button_Go_Click(object sender, EventArgs e)
        {
            switch (player.Direction)
            {
                case "up":
                    if (player.Position.Y < game_grid.RowCount - 1)
                    {
                        player.Position.Y++;
                        MovePlayer();
                    }
                    break;
                case "down":
                    if (player.Position.Y > 0)
                    {
                        player.Position.Y--;
                        MovePlayer();
                    }
                    break;
                case "left":
                    if (player.Position.X > 0)
                    {
                        player.Position.X--;
                        MovePlayer();
                    }
                    break;
                case "right":
                    if (player.Position.X < game_grid.RowCount - 1)
                    {
                        player.Position.X++;
                        MovePlayer();
                    }
                    break;
            }
        }

        private void Button_Get_Click(object sender, EventArgs e)
        {
            if (player.Position == _board.Gold && !player.HaveGold)
            {
                UpdateScore(1000);
                _handlerBoard.RemoveGold();
                player.HaveGold = true;
            }
        }

        private void Button_Arrow_Click(object sender, EventArgs e)
        {
            if (player.HaveArrow)
            {
                UpdateScore(-10);
                button_arrow.Enabled = false;
                _board.PlayerShootsArrow(player, KillWumpus);
            }
        }


        private void MovePlayer()
        {
            UpdateScore(-1);

            _handlerBoard.MovePlayer(player, _board);

            if (!WorstHappened())
            {
                _wumpusPd.CheckSafety(player.Position, _board.WumpusIsDead);
                _pitPd.CheckSafety(player.Position, false);

                UpdateProbDist();
            }
        }

        private bool WorstHappened()
        {
            ;
            if (player.Position == _board.Wumpus && !_board.WumpusIsDead)
            {
                UpdateScore(-1000);
                MessageBox.Show("you were devoured by the Wumpus!");
                _handlerBoard.DisableAll();
                return true;
            }
            if (_board.Pits.Where(p => p == player.Position).Any())
            {
                UpdateScore(-1000);
                MessageBox.Show("you fell into the pit and died!");
                _handlerBoard.DisableAll();
                return true;
            }
            return false;
        }

        private void KillWumpus()
        {
            _board.WumpusIsDead = true;
            player.HeardScream = true;
            label_scream.Visible = true;
            _handlerBoard.UpdateDeadWumpus();
        }

        private void UpdateScore(int value)
        {
            _board.PlayerScore += value;
            label_score.Text = _board.PlayerScore.ToString();
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

        private void Button_Auto_MouseClick(object sender, MouseEventArgs e)
        {
            /*
            while (scopeButton.BackColor != _goldColor)
            {
                Point? betterMove = SearchBetterAdj();
                if (betterMove.HasValue)
                {
                    player.Direction = Redirect(player.Position, betterMove.Value);
                    scopeButton.Image = _images["player_" + player.Direction];
                    
                }
                else
                {
                    MessageBox.Show("Não foi possível determinar melhor movimento.");
                    break;
                }
            }
            */
        }
        /*
        private string Redirect(Point pos, Point dest)
        {
            string newDirection = "";
            if (pos.X == dest.X)
            {
                if (dest.Y > pos.Y)
                {
                    newDirection = "up";
                }
                else
                {
                    newDirection = "down";
                }
            }
            if (pos.Y == dest.Y)
            {
                if (pos.X > dest.X)
                {
                    newDirection = "left";
                }
                else
                {
                    newDirection = "right";
                }
            }
            return newDirection;
        }

        private Point? SearchBetterAdj()
        {
            List<Point> adj = new List<Point>();
            Point p = player.Position;
            if (p.X > 0) adj.Add(new(p.X - 1, p.Y));
            if (p.Y > 0) adj.Add(new(p.X, p.Y-1));
            if (p.X < dim) adj.Add(new(p.X+1, p.Y));
            if (p.Y < dim) adj.Add(new(p.X, p.Y + 1));
            
            if (!adj.Any()) return null;

            UpdateProbDist();

            Point? ptMin = null;
            float min = float.PositiveInfinity;
            foreach (Point pt in adj)
            {
                float sum = _wumpusPd.ProbDist[pt.X, pt.Y] + _pitPd.ProbDist[pt.X, pt.Y];
                if (sum < min)
                {
                    min = sum;
                    ptMin = pt;
                }
            }
            return ptMin;
        }
        */
    }
}