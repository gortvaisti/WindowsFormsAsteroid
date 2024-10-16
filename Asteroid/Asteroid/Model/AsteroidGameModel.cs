using Asteroid.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Asteroid.Model
{
   
    internal class AsteroidGameModel
    {

        public event EventHandler? ShipMoved;
        public event EventHandler? AsteroidsMoved;
        public event EventHandler<int>? GameOver;

        private AsteroidTable _table = null!;
        private IAsteroidDataAccess _dataAccess; // adatelérés
        private Random _random = new Random();
        private int _moreAsteroidsTime = 15; //hány másodpercenként legyen több aszteroida

        public AsteroidGameModel(IAsteroidDataAccess dataAccess) 
        {
            _dataAccess = dataAccess;
            _table = new AsteroidTable();
            NewGame();
        }
        public AsteroidTable Table { get => _table; set => _table = value; }

        public void NewGame()
        {
            InitializeBoard();
            _table.Time = 0;
        }

        public void InitializeBoard()
        {
            for (int row = 0; row < _table.Rows; row++)
            {
                for (int col = 0; col < _table.Cols; col++)
                {
                    _table.GameBoard[row, col] = 0;
                }
            }

            int shipRow = _table.Rows - 1;
            int shipCol = _table.Cols / 2;
            _table.GameBoard[shipRow, shipCol] = 1;
        }

        public void MoveShip(KeyEventArgs e)
        {
            int shipRow = Table.Rows - 1;

            for (int col = 0; col < Table.Cols; col++)
            {
                if (Table.GameBoard[shipRow, col] == 1)
                {
                    if ((e.KeyCode == Keys.A || e.KeyCode == Keys.Left) && col > 0)
                    {
                        if(Table.GameBoard[shipRow, col - 1] != 2)
                        {
                            Table.GameBoard[shipRow, col] = 0;
                            Table.GameBoard[shipRow, col - 1] = 1;
                        }
                        else
                        {
                            Table.GameBoard[shipRow, col] = 0;
                            GameOver?.Invoke(this, _table.Time);
                        }
                        
                    }
                    else if ((e.KeyCode == Keys.D || e.KeyCode == Keys.Right) && col < Table.Cols - 1)
                    {
                        if (Table.GameBoard[shipRow, col + 1] != 2)
                        {
                            Table.GameBoard[shipRow, col] = 0;
                            Table.GameBoard[shipRow, col + 1] = 1;
                        }
                        else
                        {
                            Table.GameBoard[shipRow, col] = 0;
                            GameOver?.Invoke(this, _table.Time);
                        }
                    }

                    Debug.WriteLine(col);
                    ShipMoved?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        public void GenerateAsteroid()
        {
            int asteroidCount = Math.Min((_table.Time / _moreAsteroidsTime) + 1, Table.Cols);

            HashSet<int> usedColumns = new HashSet<int>();
            for (int i = 0; i < asteroidCount; i++)
            {
                int randomCol;
                do
                {
                    randomCol = _random.Next(0, Table.Cols);
                } while (usedColumns.Contains(randomCol));

                usedColumns.Add(randomCol);
                Table.GameBoard[0, randomCol] = 2;
            }
        }

        public void MoveAsteroids()
        {
            for (int row = Table.Rows - 1; row >= 0; row--)
            {
                for (int col = 0; col < Table.Cols; col++)
                {
                    if (Table.GameBoard[row, col] == 2)
                    {
                        Table.GameBoard[row, col] = 0;

                        if (row + 1 < Table.Rows && Table.GameBoard[row + 1, col] == 1)
                        {
                            Table.GameBoard[row + 1, col] = 2;
                            GameOver?.Invoke(this, _table.Time);
                            return;
                        }

                        else if (row + 1 < Table.Rows)
                        {
                            Table.GameBoard[row + 1, col] = 2;
                        }   
                    }
                }
            }

            AsteroidsMoved?.Invoke(this, EventArgs.Empty);
        }

        public async Task SaveGameAsync(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            await _dataAccess.SaveAsync(path, _table);
        }

        public async Task LoadGameAsync(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            _table = await _dataAccess.LoadAsync(path);

        }
    }
}
