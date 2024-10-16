namespace Asteroid
{
    partial class GameForm
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
            timeLabel = new Label();
            buttonPause = new Button();
            buttonNewGame = new Button();
            buttonSaveGame = new Button();
            buttonLoadGame = new Button();
            gamePanel = new Panel();
            label1 = new Label();
            label2 = new Label();
            _openFileDialog = new OpenFileDialog();
            _saveFileDialog = new SaveFileDialog();
            SuspendLayout();
            // 
            // timeLabel
            // 
            timeLabel.AutoSize = true;
            timeLabel.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            timeLabel.Location = new Point(12, 612);
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new Size(191, 32);
            timeLabel.TabIndex = 1;
            timeLabel.Text = "Time: 00:00:00";
            // 
            // buttonPause
            // 
            buttonPause.BackColor = Color.Maroon;
            buttonPause.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 238);
            buttonPause.Location = new Point(40, 12);
            buttonPause.Name = "buttonPause";
            buttonPause.Size = new Size(150, 40);
            buttonPause.TabIndex = 2;
            buttonPause.TabStop = false;
            buttonPause.Text = "Pause";
            buttonPause.UseVisualStyleBackColor = false;
            buttonPause.Click += ButtonPause_Click;
            // 
            // buttonNewGame
            // 
            buttonNewGame.BackColor = Color.SkyBlue;
            buttonNewGame.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 238);
            buttonNewGame.Location = new Point(225, 12);
            buttonNewGame.Name = "buttonNewGame";
            buttonNewGame.Size = new Size(150, 40);
            buttonNewGame.TabIndex = 3;
            buttonNewGame.TabStop = false;
            buttonNewGame.Text = "New Game";
            buttonNewGame.UseVisualStyleBackColor = false;
            buttonNewGame.Click += ButtonNewGame_Clicked;
            // 
            // buttonSaveGame
            // 
            buttonSaveGame.BackColor = Color.SkyBlue;
            buttonSaveGame.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 238);
            buttonSaveGame.Location = new Point(411, 12);
            buttonSaveGame.Name = "buttonSaveGame";
            buttonSaveGame.Size = new Size(150, 40);
            buttonSaveGame.TabIndex = 4;
            buttonSaveGame.TabStop = false;
            buttonSaveGame.Text = "Save Game";
            buttonSaveGame.UseVisualStyleBackColor = false;
            buttonSaveGame.Click += ButtonSaveGame_Clicked;
            // 
            // buttonLoadGame
            // 
            buttonLoadGame.BackColor = Color.SkyBlue;
            buttonLoadGame.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 238);
            buttonLoadGame.Location = new Point(604, 12);
            buttonLoadGame.Name = "buttonLoadGame";
            buttonLoadGame.Size = new Size(150, 40);
            buttonLoadGame.TabIndex = 5;
            buttonLoadGame.TabStop = false;
            buttonLoadGame.Text = "Load Game";
            buttonLoadGame.UseVisualStyleBackColor = false;
            buttonLoadGame.Click += ButtonLoadGame_Clicked;
            // 
            // gamePanel
            // 
            gamePanel.BackColor = SystemColors.Control;
            gamePanel.Location = new Point(133, 78);
            gamePanel.Margin = new Padding(0);
            gamePanel.Name = "gamePanel";
            gamePanel.Size = new Size(500, 500);
            gamePanel.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label1.Location = new Point(495, 587);
            label1.Name = "label1";
            label1.Size = new Size(66, 25);
            label1.TabIndex = 6;
            label1.Text = "Press";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label2.Location = new Point(465, 612);
            label2.Name = "label2";
            label2.Size = new Size(133, 25);
            label2.TabIndex = 7;
            label2.Text = "<- A      D ->";
            // 
            // _openFileDialog
            // 
            _openFileDialog.Filter = "Aszteroida tábla (*.atl)|*.atl";
            _openFileDialog.Title = "Aszteroida játék betöltése";
            // 
            // _saveFileDialog
            // 
            _saveFileDialog.Filter = "Aszteroida tábla (*.atl)|*.atl";
            _saveFileDialog.Title = "Aszteroida játék betöltése";
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 653);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(gamePanel);
            Controls.Add(buttonLoadGame);
            Controls.Add(buttonSaveGame);
            Controls.Add(buttonNewGame);
            Controls.Add(buttonPause);
            Controls.Add(timeLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MinimizeBox = false;
            Name = "GameForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Asteroid";
            TransparencyKey = Color.Transparent;
            KeyDown += GameForm_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label timeLabel;
        private Button buttonPause;
        private Button buttonNewGame;
        private Button buttonSaveGame;
        private Button buttonLoadGame;
        private Panel gamePanel;
        private Label label1;
        private Label label2;
        private OpenFileDialog _openFileDialog;
        private SaveFileDialog _saveFileDialog;
    }
}
