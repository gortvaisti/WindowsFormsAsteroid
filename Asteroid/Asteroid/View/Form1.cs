using Asteroid.Model;
using Asteroid.Persistence;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Asteroid
{
    public partial class GameForm : Form
    {
        private AsteroidGameModel _model = null!;
        private Timer _timer = null!;

        public GameForm()
        {
            InitializeComponent();

            IAsteroidDataAccess _dataAccess = new AsteroidFileDataAccess();

            //Buttons Visibility
            buttonPause.Visible = true;
            buttonNewGame.Visible = false;
            buttonSaveGame.Visible = false;
            buttonLoadGame.Visible = false;

            //Timer
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += Timer_Tick;
            _timer.Start();

            _model = new AsteroidGameModel(_dataAccess);
            _model.ShipMoved += Game_ShipMoved;
            _model.AsteroidsMoved += Game_AsteroidsMoved;
            _model.GameOver += Game_GameOver;
            SetupTable();
        }

        private void Timer_Tick(Object? sender, EventArgs e)
        {
            _model.Table.Time++;
            timeLabel.Text = "Time: " + TimeSpan.FromSeconds(_model.Table.Time).ToString();

            _model.MoveAsteroids();
            _model.GenerateAsteroid();
            RefreshTable();
        }

        private void SetupTable()
        {
            gamePanel.Controls.Clear();

            int cellSize = _model.Table.GetCellSize();

            for (int row = 0; row < _model.Table.Rows; row++)
            {
                for (int col = 0; col < _model.Table.Cols; col++)
                {
                    Panel cell = new Panel();
                    cell.Size = new Size(cellSize, cellSize);

                    
                    if (_model.Table.GameBoard[row, col] == 1)
                    {
                        cell.BackColor = Color.Blue;  // �rhaj�
                    }
                    else if (_model.Table.GameBoard[row, col] == 2)
                    {
                        cell.BackColor = Color.Gray;  // Aszteroida
                    }
                    else
                    {
                        cell.BackColor = Color.Black;  // �res t�r
                    }

                    cell.Location = new Point(col * cellSize, row * cellSize);
                    gamePanel.Controls.Add(cell); 
                }
            }
        }

        private void RefreshTable()
        {
            for (int row = 0; row < _model.Table.Rows; row++)
            {
                for (int col = 0; col < _model.Table.Cols; col++)
                {
                    Panel cell = (Panel)gamePanel.Controls[row * _model.Table.Cols + col];

                    if (_model.Table.GameBoard[row, col] == 1)
                    {
                        cell.BackColor = Color.Blue;  // �rhaj�
                    }
                    else if (_model.Table.GameBoard[row, col] == 2)
                    {
                        cell.BackColor = Color.Gray;  // Aszteroida
                    }
                    else
                    {
                        cell.BackColor = Color.Black;  // �res t�r
                    }
                }
            }
        }



        private void Game_ShipMoved(object? sender, EventArgs e)
        {
            RefreshTable();  
        }

        private void Game_AsteroidsMoved(object? sender, EventArgs e)
        {
            RefreshTable();
        }

        private void ButtonPause_Click(object sender, EventArgs e)
        {
            if (_timer.Enabled)
            {
                _timer.Stop();
                buttonPause.Text = "Start";
                buttonPause.BackColor = Color.Green;
                buttonNewGame.Visible = true;
                buttonSaveGame.Visible = true;
                buttonLoadGame.Visible = true;
            }
            else
            {
                _timer.Start();
                buttonPause.Text = "Pause";
                buttonPause.BackColor = Color.Maroon;
                buttonNewGame.Visible = false;
                buttonSaveGame.Visible = false;
                buttonLoadGame.Visible = false;
            }
            
        }

        private void ButtonNewGame_Clicked(object sender, EventArgs e)
        {
            _model.NewGame();
            _timer.Start();
            buttonPause.Text = "Pause";
            buttonPause.BackColor = Color.Maroon;
            buttonPause.Visible = true;
            buttonNewGame.Visible = false;
            buttonSaveGame.Visible = false;
            buttonLoadGame.Visible = false;
        }

        private async void ButtonSaveGame_Clicked(object sender, EventArgs e)
        {
            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _model.SaveGameAsync(_saveFileDialog.FileName);

                }
                catch (AsteroidDataException)
                {
                    MessageBox.Show("J�t�k ment�se sikertelen!" + Environment.NewLine + "Hib�s az el�r�si �t, vagy a k�nyvt�r nem �rhat�.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void ButtonLoadGame_Clicked(object sender, EventArgs e)
        {
            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _model.LoadGameAsync(_openFileDialog.FileName);
                    buttonPause.Text = "Start";
                    buttonPause.BackColor = Color.Green;
                    buttonPause.Visible = true;
                    buttonNewGame.Visible = true;
                    buttonSaveGame.Visible = true;
                    buttonLoadGame.Visible = true;
                }
                catch (AsteroidDataException)
                {
                    MessageBox.Show("J�t�k bet�lt�se sikertelen!" + Environment.NewLine + "Hib�s az el�r�si �t, vagy a f�jlform�tum.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                SetupTable();
            }
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (_timer.Enabled)
            {
                if (e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                {
                    _model.MoveShip(e);
                }
            }

        }

        private void Game_GameOver(object? sender, int time)
        {
            RefreshTable();
            _timer.Stop();
            TimeSpan gameTime = TimeSpan.FromSeconds(time);
            MessageBox.Show($"Game Over! Az �rhaj�t eltal�lta egy aszteroida.\n\nT�l�lt Id�: {gameTime}", "Game Over");
            buttonNewGame.Visible = true;
            buttonLoadGame.Visible = true;
            buttonPause.Visible = false;
        }
    }
}
