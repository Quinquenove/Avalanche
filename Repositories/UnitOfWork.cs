using Avalanche.Data;

namespace Avalanche.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly snowboardingContext _context;
        public SnowboarderRepository Snowboarder { get; }
        public BergRepository Berg { get; }

        public UnitOfWork(snowboardingContext context)
        {
            _context = context;
            Snowboarder = new SnowboarderRepository(_context);
            Berg = new BergRepository(_context);
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
