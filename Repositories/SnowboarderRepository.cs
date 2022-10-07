using Avalanche.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Avalanche.Repositories
{
    public class SnowboarderRepository : Repository<Snowboarder>, IRepository<Snowboarder>
    {
        public SnowboarderRepository(DbContext Context) : base(Context)
        {
        }

        public override IEnumerable<Snowboarder> Find(Expression<Func<Snowboarder, bool>> predicate)
        {
            return base._entities.Include(x => x.HausBerg)
                                 .ThenInclude(x => x.Gebirge)
                                 .Include(x => x.HausBerg)
                                 .ThenInclude(x => x.Schwierigkeit)
                                 .Include(x => x.Profi)
                                 .ThenInclude(x => x.BestTrick)
                                 .Include(x => x.Sponsorings)
                                 .ThenInclude(x => x.SponsorNavigation)
                                 .Include(x => x.Sponsorings)
                                 .ThenInclude(x => x.VertragsartNavigation)
                                 .Where(predicate);
        }
    }
}
