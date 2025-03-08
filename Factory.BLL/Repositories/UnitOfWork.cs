using Factory.BLL.Interfaces;
using Factory.BLL.InterFaces;
using Factory.BLL.Repositories;
using Factory.DAL;

namespace Factory.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FactDdContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(FactDdContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as IRepository<TEntity>
                    ?? throw new InvalidOperationException($"Repository for {typeof(TEntity).Name} not found.");
            }

            var repository = new Repository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
