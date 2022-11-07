using Avalanche.Data;

namespace Avalanche.Repositories
{
    /// <summary>
    /// Implementierung des UnitOfWork-Patterns.
    /// Definition: https://de.wikipedia.org/wiki/Unit_of_Work
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        private readonly snowboardingContext _context;
        public SnowboarderRepository Snowboarder { get; }
        public BergRepository Berg { get; }
        public Repository<Gebirge> Gebirge { get; }
        public Repository<Schwierigkeit> Schwierigkeit { get; }
        public Repository<Profi> Profi { get; }
        public Repository<Trick> Trick { get; }
        public Repository<Sponsor> Sponsor { get; }
        public Repository<Sponsoring> Sponsoring { get; }
        public WettkampfRepository Wettkampf { get; }
        public Repository<Vertragsart> Vertragsart { get; }

        public UnitOfWork(snowboardingContext context)
        {
            _context = context;
            Snowboarder = new SnowboarderRepository(_context);
            Berg = new BergRepository(_context);
            Gebirge = new Repository<Gebirge>(_context);
            Schwierigkeit = new Repository<Schwierigkeit>(_context);
            Profi = new Repository<Profi>(_context);
            Trick = new Repository<Trick>(_context);
            Sponsor = new Repository<Sponsor>(_context);
            Wettkampf = new WettkampfRepository(_context);
            Sponsoring = new Repository<Sponsoring>(_context);
            Vertragsart = new Repository<Vertragsart>(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
