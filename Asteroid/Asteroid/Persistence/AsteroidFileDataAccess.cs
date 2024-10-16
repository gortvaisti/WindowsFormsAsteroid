using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid.Persistence
{
    public class AsteroidFileDataAccess : IAsteroidDataAccess
    {
        public async Task<AsteroidTable> LoadAsync(String path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    String line = await reader.ReadLineAsync() ?? String.Empty;
                    int time = Int32.Parse(line); // idő beolvasása
                    AsteroidTable table = new AsteroidTable();
                    table.Time = time;

                    for (int i = 0; i < table.Rows; i++)
                    {
                        line = await reader.ReadLineAsync() ?? String.Empty;
                        String[] values = line.Split(' ');

                        for (int j = 0; j < table.Cols; j++)
                        {
                            table.GameBoard[i, j] = Int32.Parse(values[j]);
                        }
                    }

                    return table;
                }
            }
            catch
            {
                throw new AsteroidDataException();
            }
        }

        public async Task SaveAsync(String path, AsteroidTable table)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine(table.Time); // idő kiírása
                    for (int i = 0; i < table.Rows; i++)
                    {
                        for (int j = 0; j < table.Cols; j++)
                        {
                            await writer.WriteAsync(table.GameBoard[i, j] + " ");
                        }
                        await writer.WriteLineAsync();
                    }
                }
            }
            catch
            {
                throw new AsteroidDataException();
            }
        }
    }
}
