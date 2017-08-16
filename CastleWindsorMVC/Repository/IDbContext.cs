using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace CastleWindsorMVC.Repository
{
    public interface IDbContext : IDisposable
    {
        Database Db { get; }
        DbEntityEntry Entry(object entity);
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
    } 
}