using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid.Persistence
{
    public class AsteroidTable
    {
        public int[,] GameBoard { get; private set; }
        public int Rows { get; private set; } = 20;  
        public int Cols { get; private set; } = 20;
        public int BoardSize { get; private set; } = 500;
        public int Time { get; set; } = 0;

        public AsteroidTable()
        {
            GameBoard = new int[Rows, Cols];
        }

        public int GetCellSize()
        {
            return BoardSize / Rows;
        }
    }
}
