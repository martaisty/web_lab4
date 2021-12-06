using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using web_lab4.Abstractions;
using web_lab4.Models;

namespace web_lab4.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private readonly IServiceProvider _serviceProvider;

        private IUserRepository _userRepository;

        public UnitOfWork(DatabaseContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public IUserRepository Users =>
            _userRepository ??= _serviceProvider.GetService<IUserRepository>();

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}