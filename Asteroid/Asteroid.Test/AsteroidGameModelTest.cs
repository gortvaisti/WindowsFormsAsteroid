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
            Assert.IsTrue(asteroidFound, "Az aszteroidának meg kellett volna jelennie az elsõ sorban.");
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
                        Assert.AreEqual(1, _model.Table.GameBoard[row, col], "Az ûrhajónak a megfelelõ helyen kell lennie.");
                    }
                    else
                    {
                        Assert.AreEqual(0, _model.Table.GameBoard[row, col], "Az összes többi cellának üresnek kell lennie.");
                    }
                }
            }
        }

        [TestMethod]
        public void MoveAsteroids_MovesAsteroidsDown()
        {
            _model.Table.GameBoard[0, 0] = 2;
            _model.MoveAsteroids();
            Assert.AreEqual(0, _model.Table.GameBoard[0, 0], "Az aszteroida nem mozdult el az eredeti helyérõl.");
            Assert.AreEqual(2, _model.Table.GameBoard[1, 0], "Az aszteroida nem került lejjebb a második sorba.");
        }

        [TestMethod]
        public void MoveShip_MovesShipLeft()
        {
            _model.NewGame();
            var keyArgs = new KeyEventArgs(System.Windows.Forms.Keys.A);
            _model.MoveShip(keyArgs);

            int shipRow = _model.Table.Rows - 1;
            int expectedCol = (_model.Table.Cols / 2) - 1;
            Assert.AreEqual(1, _model.Table.GameBoard[shipRow, expectedCol], "Az ûrhajónak egy oszloppal balra kellett volna mozdulnia.");
            Assert.AreEqual(0, _model.Table.GameBoard[shipRow, expectedCol + 1], "Az elõzõ pozíciónak üresnek kell lennie.");
        }

        [TestMethod]
        public void MoveShip_MovesShipRight()
        {
            _model.NewGame();
            var keyArgs = new KeyEventArgs(System.Windows.Forms.Keys.D);
            _model.MoveShip(keyArgs);

            int shipRow = _model.Table.Rows - 1;
            int expectedCol = (_model.Table.Cols / 2) + 1;
            Assert.AreEqual(1, _model.Table.GameBoard[shipRow, expectedCol], "Az ûrhajónak egy oszloppal jobbra kellett volna mozdulnia.");
            Assert.AreEqual(0, _model.Table.GameBoard[shipRow, expectedCol - 1], "Az elõzõ pozíciónak üresnek kell lennie.");
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

            Assert.IsTrue(gameOverTriggered, "A GameOver eseménynek ki kellett volna váltódnia.");
        }

        [TestMethod]
        public void MoveShip_CannotMoveRightOutOfBounds()
        {
            _model.NewGame();
            for (int i = 0; i < 15; i++)  // Az ûrhajót a jobbra 15ször
            {
                _model.MoveShip(new KeyEventArgs(System.Windows.Forms.Keys.D));
            }

            int shipRow = _model.Table.Rows - 1;
            Assert.AreEqual(1, _model.Table.GameBoard[shipRow, _model.Table.Cols - 1], "Az ûrhajónak a jobb szélen kellett volna maradnia.");
        }

        [TestMethod]
        public void MoveShip_CannotMoveLeftOutOfBounds()
        {
            _model.NewGame();
            for (int i = 0; i < 15; i++)  // Az ûrhajót a jobbra 15ször
            {
                _model.MoveShip(new KeyEventArgs(System.Windows.Forms.Keys.A));
            }

            int shipRow = _model.Table.Rows - 1;
            Assert.AreEqual(1, _model.Table.GameBoard[shipRow, 0], "Az ûrhajónak a bal szélen kellett volna maradnia.");
        }

    }
}