using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.DataAccess.Models;

namespace TestTask.DataAccess.Repositories
{
    public class GenericRepository<TEntity> where TEntity:BaseEntity
    {
        protected readonly ApplicationContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(ApplicationContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetEntitiesAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<TEntity> GetEntityAsync(int id)
        {
            return await DbSet.FirstOrDefaultAsync(ent => ent.Id == id);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            Context.SaveChanges();
            return entity;
        }

        public async Task<int> UpdateAsync(TEntity entity, int id)
        {
            var oldEntity = await DbSet.FirstOrDefaultAsync(ent => ent.Id == id);
            oldEntity = (TEntity)entity.MapFrom(oldEntity);
            return Context.SaveChanges();
        }
       
        public async Task<int> DeleteAsync(int id)
        {
            var entity = await DbSet.FirstOrDefaultAsync(ent => ent.Id == id);
            DbSet.Remove(entity);
            return Context.SaveChanges();
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
