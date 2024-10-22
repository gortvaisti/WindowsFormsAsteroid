using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Asteroid.Model;
using Asteroid.Persistence;
using System.Windows.Forms;

namespace Asteroid.Test
{
    [TestClass]
    public class AsteroidGameModelTest
    {
        private AsteroidGameModel _model = null!;
        private Mock<IAsteroidDataAccess> _mock = null!;

        [TestInitialize]
        public void Setup()
        {
            _mock = new Mock<IAsteroidDataAccess>();
            _model = new AsteroidGameModel(_mock.Object);
        }

        [TestMethod]
        public void GenerateAsteroid_AddsAsteroidsToFirstRow()
        {
            _model.GenerateAsteroid();

            bool asteroidFound = false;
            for (int col = 0; col < _model.Table.Cols; col++)
            {
                if (_model.Table.GameBoard[0, col] == 2)
                {
                    asteroidFound = true;
                    break;
                }
            }
            Assert.IsTrue(asteroidFound, "Az aszteroid�nak meg kellett volna jelennie az els� sorban.");
        }

        [TestMethod]
        public void NewGame_InitializesTableWithEmptyCells()
        {
            _model.NewGame();

            for (int row = 0; row < _model.Table.Rows; row++)
            {
                for (int col = 0; col < _model.Table.Cols; col++)
                {
                    if (row == _model.Table.Rows - 1 && col == _model.Table.Cols / 2)
                    {
                        Assert.AreEqual(1, _model.Table.GameBoard[row, col], "Az �rhaj�nak a megfelel� helyen kell lennie.");
                    }
                    else
                    {
                        Assert.AreEqual(0, _model.Table.GameBoard[row, col], "Az �sszes t�bbi cell�nak �resnek kell lennie.");
                    }
                }
            }
        }

        [TestMethod]
        public void MoveAsteroids_MovesAsteroidsDown()
        {
            _model.Table.GameBoard[0, 0] = 2;
            _model.MoveAsteroids();
            Assert.AreEqual(0, _model.Table.GameBoard[0, 0], "Az aszteroida nem mozdult el az eredeti hely�r�l.");
            Assert.AreEqual(2, _model.Table.GameBoard[1, 0], "Az aszteroida nem ker�lt lejjebb a m�sodik sorba.");
        }

        [TestMethod]
        public void MoveShip_MovesShipLeft()
        {
            _model.NewGame();
            var keyArgs = new KeyEventArgs(System.Windows.Forms.Keys.A);
            _model.MoveShip(keyArgs);

            int shipRow = _model.Table.Rows - 1;
            int expectedCol = (_model.Table.Cols / 2) - 1;
            Assert.AreEqual(1, _model.Table.GameBoard[shipRow, expectedCol], "Az �rhaj�nak egy oszloppal balra kellett volna mozdulnia.");
            Assert.AreEqual(0, _model.Table.GameBoard[shipRow, expectedCol + 1], "Az el�z� poz�ci�nak �resnek kell lennie.");
        }

        [TestMethod]
        public void MoveShip_MovesShipRight()
        {
            _model.NewGame();
            var keyArgs = new KeyEventArgs(System.Windows.Forms.Keys.D);
            _model.MoveShip(keyArgs);

            int shipRow = _model.Table.Rows - 1;
            int expectedCol = (_model.Table.Cols / 2) + 1;
            Assert.AreEqual(1, _model.Table.GameBoard[shipRow, expectedCol], "Az �rhaj�nak egy oszloppal jobbra kellett volna mozdulnia.");
            Assert.AreEqual(0, _model.Table.GameBoard[shipRow, expectedCol - 1], "Az el�z� poz�ci�nak �resnek kell lennie.");
        }

        [TestMethod]
        public void GameOver_TriggeredWhenAsteroidHitsShip()
        {
            _model.NewGame();
            bool gameOverTriggered = false;
            _model.GameOver += (s, e) => gameOverTriggered = true;

            int shipRow = _model.Table.Rows - 1;
            int shipCol = _model.Table.Cols / 2;
            _model.Table.GameBoard[shipRow - 1, shipCol] = 2;

            _model.MoveAsteroids();

            Assert.IsTrue(gameOverTriggered, "A GameOver esem�nynek ki kellett volna v�lt�dnia.");
        }

        [TestMethod]
        public void MoveShip_CannotMoveRightOutOfBounds()
        {
            _model.NewGame();
            for (int i = 0; i < 15; i++)  // Az �rhaj�t a jobbra 15sz�r
            {
                _model.MoveShip(new KeyEventArgs(System.Windows.Forms.Keys.D));
            }

            int shipRow = _model.Table.Rows - 1;
            Assert.AreEqual(1, _model.Table.GameBoard[shipRow, _model.Table.Cols - 1], "Az �rhaj�nak a jobb sz�len kellett volna maradnia.");
        }

        [TestMethod]
        public void MoveShip_CannotMoveLeftOutOfBounds()
        {
            _model.NewGame();
            for (int i = 0; i < 15; i++)  // Az �rhaj�t a jobbra 15sz�r
            {
                _model.MoveShip(new KeyEventArgs(System.Windows.Forms.Keys.A));
            }

            int shipRow = _model.Table.Rows - 1;
            Assert.AreEqual(1, _model.Table.GameBoard[shipRow, 0], "Az �rhaj�nak a bal sz�len kellett volna maradnia.");
        }

    }
}