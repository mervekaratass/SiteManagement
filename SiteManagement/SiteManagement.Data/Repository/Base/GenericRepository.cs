using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Data.Repository;

public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
{
    private readonly SiteDbContext dbContext;

    public GenericRepository(SiteDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Delete(Entity entity)
    {
        dbContext.Set<Entity>().Remove(entity);
    }

    public void DeleteById(int id)
    {
        var entity = dbContext.Set<Entity>().Find(id);
        Delete(entity);
    }

    public List<Entity> GetAll()
    {
        return dbContext.Set<Entity>().ToList();
    }

    public IQueryable<Entity> GetAllAsQueryable()
    {
        return dbContext.Set<Entity>().AsQueryable();
    }

    public List<Entity> GetbyFilter(Expression<Func<Entity, bool>> filter)
    {
        return dbContext.Set<Entity>().Where(filter).ToList();
    }

    public Entity GetById(int id)
    {
        var entity= dbContext.Set<Entity>().Find(id);
        return entity;
    }

    public void Insert(Entity entity)
    {
        dbContext.Set<Entity>().Add(entity);
      
    }

    public void Save()
    {
        dbContext.SaveChanges();
    }

    public void Update(Entity entity)
    {
        dbContext.Set<Entity>().Update(entity);
      
      
    }
}
