using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LIcensesPO.DbConfig;
using LIcensesPO.Models;
using Microsoft.EntityFrameworkCore;

namespace LIcensesPO.Services;

public interface BaseService<T> where T: BaseEntity
{
    public IEnumerable<T> GetAll()
    {
        using (var dbContext = new AppDbContext())
        {
            return dbContext.Set<T>().ToList();
        }
    }

    public  T GetById(long id)
    {
        using (var dbContext = new AppDbContext())
        {
            return dbContext.Set<T>().Find(id);
        }
    }

    public void Add(T entity)
    {
        using (var dbContext = new AppDbContext())
        {
            dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();
        }
    }

    public void Update(T entity)
    {
        using (var dbContext = new AppDbContext())
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }

    public  void Delete(long id)
    {
        using (var dbContext = new AppDbContext())
        {
            var entity = dbContext.Set<T>().Find(id);
            if (entity != null)
            {
                dbContext.Set<T>().Remove(entity);
                dbContext.SaveChanges();
            }
        }
    }
    
    public  IEnumerable<T> Filter(Expression<Func<T, bool>> filter)
    {
        using (var dbContext = new AppDbContext())
        {
            return dbContext.Set<T>().Where(filter).ToList();
        }
    }
}