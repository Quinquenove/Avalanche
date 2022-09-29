using Avalanche.Data;
using Microsoft.EntityFrameworkCore;

namespace Avalanche.Repositories
{
    public class SnowboarderRepository : Repository<Snowboarder>
    {
        public SnowboarderRepository(DbContext Context) : base(Context)
        {
            
        }
    }
}
