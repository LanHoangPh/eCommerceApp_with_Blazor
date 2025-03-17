using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infracstructure.Data;
using eCommerceApp.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infracstructure.Repositories
{
    public class GenericRepository<TEntity>(AppDbContext context) : IGeneric<TEntity> where TEntity : class
    {
        public async Task<int> AddAsync(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
           return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id); 
            if(entity is null)
            {
                return 0;
            }
            context.Set<TEntity>().Remove(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().AsNoTracking().ToListAsync(); // AsNoTracking ở đây dùng để đọc dữ liệu và không có ý định sửa đổi(CRUD); đưng dùng nếu có ý đingj CRUD vì nó ko đc những gì bạn muốn thay đổi 
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var result = await context.Set<TEntity>().FindAsync(id);
            return result!;
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            return await context.SaveChangesAsync();
        }
    }
}
