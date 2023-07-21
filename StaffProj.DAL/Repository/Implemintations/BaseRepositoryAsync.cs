using Microsoft.EntityFrameworkCore;
using StaffProj.DAL.Repository.Interfaces;
using StaffProj.DAL.SqlServer;
using StaffProj.Domain.Models.Abstractions.BaseEntities;
using StaffProj.Domain.Models.TestsModels;
using StaffProj.ValidationHelper;

namespace StaffProj.DAL.Repository.Implemintations
{
    public class BaseRepositoryAsync<T> : IBaseRepositoryAsync<T>
        where T : BaseEntity
    {
        protected readonly PgDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseRepositoryAsync(PgDbContext context)
        {
            if (_dbSet == default(DbSet<T>)
                && typeof(T) != typeof(TestEntity) 
                || context == null)
                throw new ArgumentNullException(nameof(DbSet<T>));

            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Create(T entity)
        {
            ObjectValidator<T>.CheckIsNotNullObject(entity);

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> ReadAll()
        {
            return _dbSet;
        }

        public async Task<IQueryable<T>> ReadAllAsync()
        {
            return await Task.FromResult(_dbSet);
        }

        public T ReadById(Guid id)
        {
            ObjectValidator<Guid>.CheckIsNotNullObject(id);

            var entity = ReadAll().FirstOrDefault(x => x.Id == id);

            return entity == null
             ? throw new ArgumentNullException(nameof(id), $"Entity not found by id {id} in Repository")
             : entity;
        }

        public async Task<T> ReadByIdAsync(Guid id)
        {
            ObjectValidator<Guid>.CheckIsNotNullObject(id);
            var entity = await ReadAllAsync().Result.FirstOrDefaultAsync(x => x.Id == id);

            return entity == null
            ? throw new ArgumentNullException(nameof(id), $"Entity not found by id {id} in Repository")
            : entity;
        }

        public async Task UpdateAsync(T entity)
        {
            ObjectValidator<T>.CheckIsNotNullObject(entity);

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            ObjectValidator<T>.CheckIsNotNullObject(entity);

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            ObjectValidator<Guid>.CheckIsNotNullObject(id);

            var entity = await ReadByIdAsync(id);
            await DeleteAsync(entity);
        }
    }
}
