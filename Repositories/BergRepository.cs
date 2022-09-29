using Avalanche.Data;
using Microsoft.EntityFrameworkCore;

namespace Avalanche.Repositories
{
    public class BergRepository : Repository<Berg>
    {
        public BergRepository(DbContext Context) : base(Context)
        {
        }
    }
}
