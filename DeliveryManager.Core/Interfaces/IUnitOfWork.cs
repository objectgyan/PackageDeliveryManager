using DeliveryManager.Core.Model;

namespace DeliveryManager.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Package> PackageRepository { get; }
        IGenericRepository<Recipient> RecipientRepository { get; }

        Task SaveChangesAsync();
    }
}