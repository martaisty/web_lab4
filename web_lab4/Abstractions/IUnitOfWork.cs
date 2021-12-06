using System.Threading.Tasks;

namespace web_lab4.Abstractions
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        Task<int> SaveAsync();
    }
}