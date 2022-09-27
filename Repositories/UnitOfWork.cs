using Avalanche.Data;

namespace Avalanche.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly snowboardingContext _context;

        public UnitOfWork(snowboardingContext context)
        {
            _context = context;
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
