using Avalanche.Data;
using Microsoft.EntityFrameworkCore;

namespace Avalanche.Repositories
{
    /// <summary>
    /// Speziefisches Repository für die Wettkampf Tabelle.
    /// Überschreibt Methoden der Basis Klasse um Beziehungen zu anderen Tabellen zu berücksichtigen.
    /// </summary>
    public class WettkampfRepository : Repository<Wettkampf>
    {
        public WettkampfRepository(DbContext Context) : base(Context)
        {
        }

        public override IEnumerable<Wettkampf> GetAll()
        {
            return base._entities.
                        Include(x => x.Sponsor)
                        .Include(x => x.Berg)
                        .ThenInclude(x => x.Schwierigkeit)
                        .Include(x => x.Berg)
                        .ThenInclude(x => x.Gebirge)
                        .ToList();
        }

        public override Wettkampf GetById(long id)
        {
            return base._entities.
                        Where(x => x.Id == id)
                        .Include(x => x.Sponsor)
                        .Include(x => x.Berg)
                        .ThenInclude(x => x.Schwierigkeit)
                        .Include(x => x.Berg)
                        .ThenInclude(x => x.Gebirge)
                        .Include(x => x.Snowboarders)
                        .First();
        }
    }
}
