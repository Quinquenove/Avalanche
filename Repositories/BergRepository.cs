using Avalanche.Data;
using Microsoft.EntityFrameworkCore;

namespace Avalanche.Repositories
{
    /// <summary>
    /// Speziefisches Repository für die Berg Tabelle.
    /// Überschreibt Methoden der Basis Klasse um Beziehungen zu anderen Tabellen zu berücksichtigen.
    /// </summary>
    public class BergRepository : Repository<Berg>, IRepository<Berg>
    {
        public BergRepository(DbContext Context) : base(Context)
        {
        }

        public override IEnumerable<Berg> GetAll()
        {
            return _entities.Include(x => x.Schwierigkeit)
                            .Include(x => x.Gebirge)
                            .ToList();
        }
    }
}
