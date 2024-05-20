namespace WumpusWorld
{
    partial class FormGame
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGame));
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            button_left = new Button();
            button_down = new Button();
            button_up = new Button();
            button_right = new Button();
            tableLayoutPanel5 = new TableLayoutPanel();
            button_get = new Button();
            button_arrow = new Button();
            button_go = new Button();
            label_scream = new Label();
            tableLayoutPanel6 = new TableLayoutPanel();
            label16 = new Label();
            label15 = new Label();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            label3 = new Label();
            label5 = new Label();
            label7 = new Label();
            label6 = new Label();
            game_grid = new TableLayoutPanel();
            button16 = new Button();
            button15 = new Button();
            button14 = new Button();
            button13 = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            button10 = new Button();
            button11 = new Button();
            button12 = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            label_score = new Label();
            button_new_game = new Button();
            button_show = new Button();
            label0 = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            game_grid.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 2);
            tableLayoutPanel1.Controls.Add(game_grid, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 65.916954F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 34.083046F));
            tableLayoutPanel1.Size = new Size(711, 625);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 0);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel5, 2, 0);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel6, 1, 0);
            tableLayoutPanel3.Location = new Point(3, 431);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(705, 191);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tableLayoutPanel4.ColumnCount = 3;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel4.Controls.Add(button_left, 0, 1);
            tableLayoutPanel4.Controls.Add(button_down, 1, 2);
            tableLayoutPanel4.Controls.Add(button_up, 1, 0);
            tableLayoutPanel4.Controls.Add(button_right, 2, 1);
            tableLayoutPanel4.Location = new Point(66, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel4.Size = new Size(166, 166);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // button_left
            // 
            button_left.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button_left.BackColor = Color.FromArgb(64, 64, 64);
            button_left.FlatAppearance.BorderSize = 0;
            button_left.FlatStyle = FlatStyle.Flat;
            button_left.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_left.ForeColor = Color.White;
            button_left.Location = new Point(3, 58);
            button_left.Name = "button_left";
            button_left.Size = new Size(49, 49);
            button_left.TabIndex = 1;
            button_left.Text = "o";
            button_left.TextAlign = ContentAlignment.MiddleLeft;
            button_left.UseVisualStyleBackColor = false;
            button_left.MouseClick += Directions_Click;
            // 
            // button_down
            // 
            button_down.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button_down.BackColor = Color.FromArgb(64, 64, 64);
            button_down.FlatAppearance.BorderSize = 0;
            button_down.FlatStyle = FlatStyle.Flat;
            button_down.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_down.ForeColor = Color.White;
            button_down.Location = new Point(58, 113);
            button_down.Name = "button_down";
            button_down.Size = new Size(49, 50);
            button_down.TabIndex = 2;
            button_down.Text = "o";
            button_down.TextAlign = ContentAlignment.BottomCenter;
            button_down.UseVisualStyleBackColor = false;
            button_down.MouseClick += Directions_Click;
            // 
            // button_up
            // 
            button_up.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button_up.BackColor = Color.FromArgb(64, 64, 64);
            button_up.FlatAppearance.BorderSize = 0;
            button_up.FlatStyle = FlatStyle.Flat;
            button_up.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_up.ForeColor = Color.White;
            button_up.Location = new Point(58, 3);
            button_up.Name = "button_up";
            button_up.Size = new Size(49, 49);
            button_up.TabIndex = 4;
            button_up.Text = "o";
            button_up.TextAlign = ContentAlignment.TopCenter;
            button_up.UseVisualStyleBackColor = false;
            button_up.MouseClick += Directions_Click;
            // 
            // button_right
            // 
            button_right.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button_right.BackColor = Color.FromArgb(64, 64, 64);
            button_right.FlatAppearance.BorderSize = 0;
            button_right.FlatStyle = FlatStyle.Flat;
            button_right.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_right.ForeColor = Color.White;
            button_right.Location = new Point(113, 58);
            button_right.Name = "button_right";
            button_right.Size = new Size(50, 49);
            button_right.TabIndex = 5;
            button_right.Text = "o";
            button_right.TextAlign = ContentAlignment.MiddleRight;
            button_right.UseVisualStyleBackColor = false;
            button_right.MouseClick += Directions_Click;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 3;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel5.Controls.Add(button_go, 0, 0);
            tableLayoutPanel5.Controls.Add(button_get, 0, 1);
            tableLayoutPanel5.Controls.Add(button_arrow, 0, 2);
            tableLayoutPanel5.Controls.Add(label_scream, 1, 0);
            tableLayoutPanel5.Location = new Point(473, 3);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 3;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel5.Size = new Size(184, 166);
            tableLayoutPanel5.TabIndex = 1;
            // 
            // button_get
            // 
            button_get.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button_get.BackColor = Color.FromArgb(64, 64, 64);
            button_get.FlatAppearance.BorderSize = 0;
            button_get.FlatStyle = FlatStyle.Flat;
            button_get.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_get.ForeColor = Color.White;
            button_get.Location = new Point(3, 58);
            button_get.Name = "button_get";
            button_get.Size = new Size(55, 49);
            button_get.TabIndex = 1;
            button_get.Text = "get";
            button_get.UseVisualStyleBackColor = false;
            button_get.Click += Button_Get_Click;
            // 
            // button_arrow
            // 
            button_arrow.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button_arrow.BackColor = Color.FromArgb(64, 64, 64);
            button_arrow.FlatAppearance.BorderSize = 0;
            button_arrow.FlatStyle = FlatStyle.Flat;
            button_arrow.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_arrow.ForeColor = Color.White;
            button_arrow.Location = new Point(3, 113);
            button_arrow.Name = "button_arrow";
            button_arrow.Size = new Size(55, 50);
            button_arrow.TabIndex = 4;
            button_arrow.Text = "arrow";
            button_arrow.UseVisualStyleBackColor = false;
            button_arrow.Click += Button_Arrow_Click;
            // 
            // button_go
            // 
            button_go.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button_go.BackColor = Color.FromArgb(64, 64, 64);
            button_go.FlatAppearance.BorderSize = 0;
            button_go.FlatStyle = FlatStyle.Flat;
            button_go.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_go.ForeColor = Color.White;
            button_go.Location = new Point(3, 3);
            button_go.Name = "button_go";
            button_go.Size = new Size(55, 49);
            button_go.TabIndex = 5;
            button_go.Text = "go";
            button_go.UseVisualStyleBackColor = false;
            button_go.Click += Button_Go_Click;
            // 
            // label_scream
            // 
            label_scream.Anchor = AnchorStyles.Right;
            label_scream.AutoSize = true;
            label_scream.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_scream.ForeColor = Color.White;
            label_scream.Location = new Point(68, 19);
            label_scream.Name = "label_scream";
            label_scream.Size = new Size(51, 17);
            label_scream.TabIndex = 6;
            label_scream.Text = "scream";
            label_scream.Visible = false;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel6.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel6.ColumnCount = 4;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel6.Controls.Add(label16, 3, 0);
            tableLayoutPanel6.Controls.Add(label15, 3, 1);
            tableLayoutPanel6.Controls.Add(label14, 3, 2);
            tableLayoutPanel6.Controls.Add(label13, 3, 3);
            tableLayoutPanel6.Controls.Add(label12, 2, 0);
            tableLayoutPanel6.Controls.Add(label11, 2, 1);
            tableLayoutPanel6.Controls.Add(label10, 2, 2);
            tableLayoutPanel6.Controls.Add(label9, 2, 3);
            tableLayoutPanel6.Controls.Add(label8, 1, 0);
            tableLayoutPanel6.Controls.Add(label1, 0, 3);
            tableLayoutPanel6.Controls.Add(label2, 0, 2);
            tableLayoutPanel6.Controls.Add(label4, 0, 0);
            tableLayoutPanel6.Controls.Add(label3, 0, 1);
            tableLayoutPanel6.Controls.Add(label5, 1, 3);
            tableLayoutPanel6.Controls.Add(label7, 1, 1);
            tableLayoutPanel6.Controls.Add(label6, 1, 2);
            tableLayoutPanel6.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tableLayoutPanel6.Location = new Point(238, 3);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 4;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel6.Size = new Size(229, 185);
            tableLayoutPanel6.TabIndex = 6;
            // 
            // label16
            // 
            label16.Anchor = AnchorStyles.None;
            label16.AutoSize = true;
            label16.ForeColor = Color.White;
            label16.Location = new Point(178, 17);
            label16.Name = "label16";
            label16.Size = new Size(44, 13);
            label16.TabIndex = 1;
            label16.Text = "label16";
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.None;
            label15.AutoSize = true;
            label15.ForeColor = Color.White;
            label15.Location = new Point(178, 63);
            label15.Name = "label15";
            label15.Size = new Size(44, 13);
            label15.TabIndex = 1;
            label15.Text = "label15";
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.None;
            label14.AutoSize = true;
            label14.ForeColor = Color.White;
            label14.Location = new Point(178, 109);
            label14.Name = "label14";
            label14.Size = new Size(44, 13);
            label14.TabIndex = 1;
            label14.Text = "label14";
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.None;
            label13.AutoSize = true;
            label13.ForeColor = Color.White;
            label13.Location = new Point(178, 155);
            label13.Name = "label13";
            label13.Size = new Size(44, 13);
            label13.TabIndex = 1;
            label13.Text = "label13";
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.None;
            label12.AutoSize = true;
            label12.ForeColor = Color.White;
            label12.Location = new Point(121, 17);
            label12.Name = "label12";
            label12.Size = new Size(44, 13);
            label12.TabIndex = 1;
            label12.Text = "label12";
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.None;
            label11.AutoSize = true;
            label11.ForeColor = Color.White;
            label11.Location = new Point(121, 63);
            label11.Name = "label11";
            label11.Size = new Size(44, 13);
            label11.TabIndex = 1;
            label11.Text = "label11";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.None;
            label10.AutoSize = true;
            label10.ForeColor = Color.White;
            label10.Location = new Point(121, 109);
            label10.Name = "label10";
            label10.Size = new Size(44, 13);
            label10.TabIndex = 1;
            label10.Text = "label10";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.None;
            label9.AutoSize = true;
            label9.ForeColor = Color.White;
            label9.Location = new Point(124, 155);
            label9.Name = "label9";
            label9.Size = new Size(38, 13);
            label9.TabIndex = 1;
            label9.Text = "label9";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.None;
            label8.AutoSize = true;
            label8.ForeColor = Color.White;
            label8.Location = new Point(67, 17);
            label8.Name = "label8";
            label8.Size = new Size(38, 13);
            label8.TabIndex = 1;
            label8.Text = "label8";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(10, 155);
            label1.Name = "label1";
            label1.Size = new Size(38, 13);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(10, 109);
            label2.Name = "label2";
            label2.Size = new Size(38, 13);
            label2.TabIndex = 1;
            label2.Text = "label2";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(10, 17);
            label4.Name = "label4";
            label4.Size = new Size(38, 13);
            label4.TabIndex = 3;
            label4.Text = "label4";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(10, 63);
            label3.Name = "label3";
            label3.Size = new Size(38, 13);
            label3.TabIndex = 2;
            label3.Text = "label3";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(67, 155);
            label5.Name = "label5";
            label5.Size = new Size(38, 13);
            label5.TabIndex = 4;
            label5.Text = "label5";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.None;
            label7.AutoSize = true;
            label7.ForeColor = Color.White;
            label7.Location = new Point(67, 63);
            label7.Name = "label7";
            label7.Size = new Size(38, 13);
            label7.TabIndex = 6;
            label7.Text = "label7";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.ForeColor = Color.White;
            label6.Location = new Point(67, 109);
            label6.Name = "label6";
            label6.Size = new Size(38, 13);
            label6.TabIndex = 5;
            label6.Text = "label6";
            // 
            // game_grid
            // 
            game_grid.Anchor = AnchorStyles.None;
            game_grid.ColumnCount = 4;
            game_grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            game_grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            game_grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            game_grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            game_grid.Controls.Add(button16, 3, 0);
            game_grid.Controls.Add(button15, 2, 0);
            game_grid.Controls.Add(button14, 1, 0);
            game_grid.Controls.Add(button13, 0, 0);
            game_grid.Controls.Add(button1, 0, 3);
            game_grid.Controls.Add(button2, 1, 3);
            game_grid.Controls.Add(button3, 2, 3);
            game_grid.Controls.Add(button4, 3, 3);
            game_grid.Controls.Add(button5, 0, 2);
            game_grid.Controls.Add(button6, 1, 2);
            game_grid.Controls.Add(button7, 2, 2);
            game_grid.Controls.Add(button8, 3, 2);
            game_grid.Controls.Add(button9, 0, 1);
            game_grid.Controls.Add(button10, 1, 1);
            game_grid.Controls.Add(button11, 2, 1);
            game_grid.Controls.Add(button12, 3, 1);
            game_grid.Location = new Point(180, 62);
            game_grid.Name = "game_grid";
            game_grid.RowCount = 4;
            game_grid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            game_grid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            game_grid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            game_grid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            game_grid.Size = new Size(350, 350);
            game_grid.TabIndex = 0;
            // 
            // button16
            // 
            button16.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button16.BackColor = Color.FromArgb(64, 64, 64);
            button16.FlatAppearance.BorderSize = 0;
            button16.FlatStyle = FlatStyle.Flat;
            button16.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button16.ForeColor = Color.White;
            button16.Location = new Point(264, 3);
            button16.Name = "button16";
            button16.Size = new Size(83, 81);
            button16.TabIndex = 15;
            button16.Text = "teste";
            button16.TextAlign = ContentAlignment.TopLeft;
            button16.UseVisualStyleBackColor = false;
            // 
            // button15
            // 
            button15.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button15.BackColor = Color.FromArgb(64, 64, 64);
            button15.FlatAppearance.BorderSize = 0;
            button15.FlatStyle = FlatStyle.Flat;
            button15.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button15.ForeColor = Color.White;
            button15.Location = new Point(177, 3);
            button15.Name = "button15";
            button15.Size = new Size(81, 81);
            button15.TabIndex = 14;
            button15.Text = "teste";
            button15.TextAlign = ContentAlignment.TopLeft;
            button15.UseVisualStyleBackColor = false;
            // 
            // button14
            // 
            button14.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button14.BackColor = Color.FromArgb(64, 64, 64);
            button14.FlatAppearance.BorderSize = 0;
            button14.FlatStyle = FlatStyle.Flat;
            button14.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button14.ForeColor = Color.White;
            button14.Location = new Point(90, 3);
            button14.Name = "button14";
            button14.Size = new Size(81, 81);
            button14.TabIndex = 13;
            button14.Text = "teste";
            button14.TextAlign = ContentAlignment.TopLeft;
            button14.UseVisualStyleBackColor = false;
            // 
            // button13
            // 
            button13.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button13.BackColor = Color.FromArgb(64, 64, 64);
            button13.FlatAppearance.BorderSize = 0;
            button13.FlatStyle = FlatStyle.Flat;
            button13.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button13.ForeColor = Color.White;
            button13.Location = new Point(3, 3);
            button13.Name = "button13";
            button13.Size = new Size(81, 81);
            button13.TabIndex = 12;
            button13.Text = "teste";
            button13.TextAlign = ContentAlignment.TopLeft;
            button13.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button1.BackColor = Color.FromArgb(64, 64, 64);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(3, 264);
            button1.Name = "button1";
            button1.Size = new Size(81, 83);
            button1.TabIndex = 0;
            button1.Text = "teste";
            button1.TextAlign = ContentAlignment.TopLeft;
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button2.BackColor = Color.FromArgb(64, 64, 64);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.White;
            button2.Location = new Point(90, 264);
            button2.Name = "button2";
            button2.Size = new Size(81, 83);
            button2.TabIndex = 1;
            button2.Text = "teste";
            button2.TextAlign = ContentAlignment.TopLeft;
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button3.BackColor = Color.FromArgb(64, 64, 64);
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.White;
            button3.Location = new Point(177, 264);
            button3.Name = "button3";
            button3.Size = new Size(81, 83);
            button3.TabIndex = 2;
            button3.Text = "teste";
            button3.TextAlign = ContentAlignment.TopLeft;
            button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button4.BackColor = Color.FromArgb(64, 64, 64);
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button4.ForeColor = Color.White;
            button4.Location = new Point(264, 264);
            button4.Name = "button4";
            button4.Size = new Size(83, 83);
            button4.TabIndex = 3;
            button4.Text = "teste";
            button4.TextAlign = ContentAlignment.TopLeft;
            button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            button5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button5.BackColor = Color.FromArgb(64, 64, 64);
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button5.ForeColor = Color.White;
            button5.Location = new Point(3, 177);
            button5.Name = "button5";
            button5.Size = new Size(81, 81);
            button5.TabIndex = 4;
            button5.Text = "teste";
            button5.TextAlign = ContentAlignment.TopLeft;
            button5.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            button6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button6.BackColor = Color.FromArgb(64, 64, 64);
            button6.BackgroundImageLayout = ImageLayout.Stretch;
            button6.FlatAppearance.BorderSize = 0;
            button6.FlatStyle = FlatStyle.Flat;
            button6.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button6.ForeColor = Color.White;
            button6.Location = new Point(90, 177);
            button6.Name = "button6";
            button6.Size = new Size(81, 81);
            button6.TabIndex = 5;
            button6.Text = "teste";
            button6.TextAlign = ContentAlignment.TopLeft;
            button6.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            button7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button7.BackColor = Color.FromArgb(64, 64, 64);
            button7.FlatAppearance.BorderSize = 0;
            button7.FlatStyle = FlatStyle.Flat;
            button7.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button7.ForeColor = Color.White;
            button7.Location = new Point(177, 177);
            button7.Name = "button7";
            button7.Size = new Size(81, 81);
            button7.TabIndex = 6;
            button7.Text = "teste";
            button7.TextAlign = ContentAlignment.TopLeft;
            button7.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            button8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button8.BackColor = Color.FromArgb(64, 64, 64);
            button8.FlatAppearance.BorderSize = 0;
            button8.FlatStyle = FlatStyle.Flat;
            button8.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button8.ForeColor = Color.White;
            button8.Location = new Point(264, 177);
            button8.Name = "button8";
            button8.Size = new Size(83, 81);
            button8.TabIndex = 7;
            button8.Text = "teste";
            button8.TextAlign = ContentAlignment.TopLeft;
            button8.UseVisualStyleBackColor = false;
            // 
            // button9
            // 
            button9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button9.BackColor = Color.FromArgb(64, 64, 64);
            button9.FlatAppearance.BorderSize = 0;
            button9.FlatStyle = FlatStyle.Flat;
            button9.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button9.ForeColor = Color.White;
            button9.Location = new Point(3, 90);
            button9.Name = "button9";
            button9.Size = new Size(81, 81);
            button9.TabIndex = 8;
            button9.Text = "teste";
            button9.TextAlign = ContentAlignment.TopLeft;
            button9.UseVisualStyleBackColor = false;
            // 
            // button10
            // 
            button10.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button10.BackColor = Color.FromArgb(64, 64, 64);
            button10.FlatAppearance.BorderSize = 0;
            button10.FlatStyle = FlatStyle.Flat;
            button10.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button10.ForeColor = Color.White;
            button10.Location = new Point(90, 90);
            button10.Name = "button10";
            button10.Size = new Size(81, 81);
            button10.TabIndex = 9;
            button10.Text = "teste";
            button10.TextAlign = ContentAlignment.TopLeft;
            button10.UseVisualStyleBackColor = false;
            // 
            // button11
            // 
            button11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button11.BackColor = Color.FromArgb(64, 64, 64);
            button11.FlatAppearance.BorderSize = 0;
            button11.FlatStyle = FlatStyle.Flat;
            button11.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button11.ForeColor = Color.White;
            button11.Location = new Point(177, 90);
            button11.Name = "button11";
            button11.Size = new Size(81, 81);
            button11.TabIndex = 10;
            button11.Text = "teste";
            button11.TextAlign = ContentAlignment.TopLeft;
            button11.UseVisualStyleBackColor = false;
            // 
            // button12
            // 
            button12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button12.BackColor = Color.FromArgb(64, 64, 64);
            button12.FlatAppearance.BorderSize = 0;
            button12.FlatStyle = FlatStyle.Flat;
            button12.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button12.ForeColor = Color.White;
            button12.Location = new Point(264, 90);
            button12.Name = "button12";
            button12.Size = new Size(83, 81);
            button12.TabIndex = 11;
            button12.Text = "teste";
            button12.TextAlign = ContentAlignment.TopLeft;
            button12.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top;
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Controls.Add(label_score, 3, 0);
            tableLayoutPanel2.Controls.Add(button_new_game, 0, 0);
            tableLayoutPanel2.Controls.Add(button_show, 1, 0);
            tableLayoutPanel2.Controls.Add(label0, 2, 0);
            tableLayoutPanel2.Location = new Point(15, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(681, 41);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // label_score
            // 
            label_score.Anchor = AnchorStyles.Left;
            label_score.AutoSize = true;
            label_score.Font = new Font("Segoe UI", 10F);
            label_score.ForeColor = Color.White;
            label_score.Location = new Point(513, 11);
            label_score.Name = "label_score";
            label_score.Size = new Size(17, 19);
            label_score.TabIndex = 7;
            label_score.Text = "0";
            // 
            // button_new_game
            // 
            button_new_game.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button_new_game.BackColor = Color.FromArgb(64, 64, 64);
            button_new_game.FlatAppearance.BorderSize = 0;
            button_new_game.FlatStyle = FlatStyle.Flat;
            button_new_game.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_new_game.ForeColor = Color.White;
            button_new_game.Location = new Point(3, 3);
            button_new_game.Name = "button_new_game";
            button_new_game.Size = new Size(164, 35);
            button_new_game.TabIndex = 5;
            button_new_game.Text = "new game";
            button_new_game.UseVisualStyleBackColor = false;
            button_new_game.MouseClick += Button_New_Game_MouseClick;
            // 
            // button_show
            // 
            button_show.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button_show.BackColor = Color.FromArgb(64, 64, 64);
            button_show.FlatAppearance.BorderSize = 0;
            button_show.FlatStyle = FlatStyle.Flat;
            button_show.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_show.ForeColor = Color.White;
            button_show.Location = new Point(173, 3);
            button_show.Name = "button_show";
            button_show.Size = new Size(164, 35);
            button_show.TabIndex = 1;
            button_show.Text = "show";
            button_show.UseVisualStyleBackColor = false;
            button_show.MouseClick += Button_Show_MouseClick;
            // 
            // label0
            // 
            label0.Anchor = AnchorStyles.Right;
            label0.AutoSize = true;
            label0.Font = new Font("Segoe UI", 10F);
            label0.ForeColor = Color.White;
            label0.Location = new Point(466, 11);
            label0.Name = "label0";
            label0.Size = new Size(41, 19);
            label0.TabIndex = 6;
            label0.Text = "score";
            // 
            // FormGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 32, 32);
            ClientSize = new Size(711, 625);
            Controls.Add(tableLayoutPanel1);
            Name = "FormGame";
            Text = "Wumpus World Game";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            game_grid.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel game_grid;
        private Button button1;
        private Button button16;
        private Button button15;
        private Button button14;
        private Button button13;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Button button10;
        private Button button11;
        private Button button12;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private Button button_left;
        private Button button_down;
        private Button button_up;
        private Button button_right;
        private TableLayoutPanel tableLayoutPanel5;
        private Button button_get;
        private Button button_arrow;
        private Button button_go;
        private TableLayoutPanel tableLayoutPanel2;
        private Button button_show;
        private Button button_new_game;
        private Label label_score;
        private Label label0;
        private Label label_scream;
        private TableLayoutPanel tableLayoutPanel6;
        private Label label1;
        private Label label2;
        private Label label4;
        private Label label3;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label5;
        private Label label7;
        private Label label6;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label16;
    }
}
