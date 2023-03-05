using DeliveryManager.Core.Interfaces;
using DeliveryManager.Core.Model;

namespace DeliveryManager.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            PackageRepository = new GenericRepository<Package>(_dbContext);
            RecipientRepository = new GenericRepository<Recipient>(_dbContext);
        }

        public IGenericRepository<Package> PackageRepository { get; }
        public IGenericRepository<Recipient> RecipientRepository { get; }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}