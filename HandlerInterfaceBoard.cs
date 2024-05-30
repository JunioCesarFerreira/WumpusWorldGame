﻿using System.Numerics;

namespace WumpusWorld
{
    internal class HandlerInterfaceBoard
    {
        private readonly Button[,] _buttons; // Matriz de botões que compõe o tabuleiro

        private readonly Dictionary<string, Image> _images;

        // Cores utilizadas nas células do tabuleiro 
        private readonly Color _closedColor = Color.FromArgb(64, 40, 32);
        private readonly Color _openedColor = Color.FromArgb(64, 64, 64);
        private readonly Color _pitColor = Color.FromArgb(0, 0, 0);
        private readonly Color _goldColor = Color.FromArgb(64, 64, 16);

        public int DimX { get => _buttons.GetLength(0); }
        public int DimY { get => _buttons.GetLength(1); }

        // Posição atual do jogador
        public Button ScopeButton = new();

        public HandlerInterfaceBoard(Button[,] buttons)
        {
            _buttons = buttons;
            string[] pathImgs = Directory.GetFiles("img");
            _images = new Dictionary<string, Image>(6);
            foreach (string path in pathImgs)
            {
                string name = Path.GetFileNameWithoutExtension(path);
                _images[name] = Image.FromFile(path);
            }
        }

        public void DisableAll()
        {
            foreach (var b in _buttons) b.Enabled = false;
        }

        public void StartBoard(int xInit=0, int yInit=0)
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
            ScopeButton = _buttons[xInit, yInit];
            ScopeButton.Image = _images["player_down"];
            ScopeButton.BackColor = _openedColor;
            ScopeButton.FlatAppearance.MouseOverBackColor = _openedColor;
            ScopeButton.ForeColor = Color.White;
        }

        public void MovePlayer(Player player, Board board)
        {
            _buttons[player.Position.X, player.Position.Y].Image = ScopeButton.Image;
            ScopeButton.Image = null;
            ScopeButton = _buttons[player.Position.X, player.Position.Y];
            PaintButton(player.Position, board, player);
            BackgroundImageButton(player.Position, board, player);
        }

        public void PaintButton(Point point, Board board, Player player)
        {
            _buttons[point.X,point.Y].BackColor = _openedColor;
            _buttons[point.X, point.Y].FlatAppearance.MouseOverBackColor = _openedColor;
            _buttons[point.X, point.Y].ForeColor = Color.White;
            if (point == board.Wumpus)
            {
                _buttons[point.X, point.Y].BackColor = _openedColor;
                _buttons[point.X, point.Y].FlatAppearance.MouseOverBackColor = _openedColor;
            }
            if (board.Pits.Where(p => p == point).Any())
            {
                _buttons[point.X, point.Y].BackColor = _pitColor;
                _buttons[point.X, point.Y].FlatAppearance.MouseOverBackColor = _pitColor;
            }
            if (point == board.Gold && !player.HaveGold)
            {
                _buttons[point.X, point.Y].BackColor = _goldColor;
                _buttons[point.X, point.Y].FlatAppearance.MouseOverBackColor = _goldColor;
            }
        }

        public void BackgroundImageButton(Point point, Board board, Player player)
        {
            if (point == board.Wumpus)
            {
                _buttons[point.X, point.Y].BackgroundImage = board.WumpusIsDead ? _images["dead_wumpus"] : _images["wumpus"];
            }
            if (point == board.Gold && !player.HaveGold)
            {
                _buttons[point.X, point.Y].BackgroundImage = _images["gold"];
            }
        }

        public void UpdatePlayerDirection(Player player)
        {
            ScopeButton.Image = _images["player_" + player.Direction];
        }

        public void RemoveGold()
        {
            ScopeButton.BackgroundImage = null;
            ScopeButton.BackColor = _openedColor;
            ScopeButton.FlatAppearance.MouseOverBackColor = _openedColor;
        }

        public void UpdateDeadWumpus()
        {
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

        public void Tagging(Board board)
        {
            int maxX = _buttons.GetLength(0) - 1;
            int maxY = _buttons.GetLength(1) - 1;
            for (int i = 0; i <= maxX; i++)
            {
                for (int j = 0; j <= maxY; j++)
                {
                    if (i + 1 <= maxX)
                    {
                        if (board.IsWumpus(i + 1, j)) TagStench(i, j);
                        if (board.IsPit(i + 1, j)) TagBreeze(i, j);
                    }
                    if (j + 1 <= maxY)
                    {
                        if (board.IsWumpus(i, j + 1)) TagStench(i, j);
                        if (board.IsPit(i, j + 1)) TagBreeze(i, j);
                    }
                    if (i - 1 >= 0)
                    {
                        if (board.IsWumpus(i - 1, j)) TagStench(i, j);
                        if (board.IsPit(i - 1, j)) TagBreeze(i, j);
                    }
                    if (j - 1 >= 0)
                    {
                        if (board.IsWumpus(i, j - 1)) TagStench(i, j);
                        if (board.IsPit(i, j - 1)) TagBreeze(i, j);
                    }
                }
            }
        }
    }
}
