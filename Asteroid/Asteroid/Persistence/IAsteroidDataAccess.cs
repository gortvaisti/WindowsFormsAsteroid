using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid.Persistence
{
    public interface IAsteroidDataAccess
    {
        Task<AsteroidTable> LoadAsync(String path);
        Task SaveAsync(String path, AsteroidTable table);
    }
}
