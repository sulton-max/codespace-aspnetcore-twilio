using System.Linq.Expressions;
using App.Api.Models.Common;
using App.Api.Models.Configurations;
using App.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace App.Api.Brokers.DataStorage;

public class EfCoreDataStorageBroker : DbContext, IDataStorageBroker
{
    public DbSet<SmsTemplate> SmsTemplates => Set<SmsTemplate>();
    public DbSet<Sms> Sms => Set<Sms>();

    private static DbContextOptions<EfCoreDataStorageBroker> GetOptions()
    {
        var optionsBuilder = new DbContextOptionsBuilder<EfCoreDataStorageBroker>();
        optionsBuilder.UseInMemoryDatabase("Tigernet");
        return optionsBuilder.Options;
    }

    [Obsolete("Only for testing purposes, use the constructor with options passed")]
    public EfCoreDataStorageBroker(IOptions<DataStorageConfiguration> dataStorageConfiguration) : this(GetOptions())
    {
        Database.EnsureCreated();
    }

    public EfCoreDataStorageBroker(DbContextOptions<EfCoreDataStorageBroker> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder);
        //
        // modelBuilder.Entity<Book>()
        //     .HasData(new List<Book>
        //     {
        //         // Write 5 bestseller books
        //         new()
        //         {
        //             Id = 1,
        //             Name = "The Great Gatsby",
        //             Author = "F. Scott Fitzgerald"
        //         },
        //         new()
        //         {
        //             Id = 2,
        //             Name = "The Catcher in the Rye",
        //             Author = "J. D. Salinger"
        //         },
        //     });
    }

    public IQueryable<TEntity> Select<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity
    {
        return Set<TEntity>().Where(expression);
    }

    public ValueTask<TEntity?> SelectByIdAsync<TEntity>(long id, CancellationToken cancellationToken = default) where TEntity : class, IEntity
    {
        return Set<TEntity>().FindAsync(new object[] { new[] { id } }, cancellationToken: cancellationToken);
    }

    public async ValueTask<bool> CheckByIdAsync<TEntity>(long id, CancellationToken cancellationToken = default) where TEntity : class, IEntity
    {
        return await Set<TEntity>().AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async ValueTask<TEntity?> InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        var entry = await Set<TEntity>().AddAsync(entity, cancellationToken);
        return entry.Entity;
    }

    public ValueTask<TEntity?> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IEntity
    {
        var entry = Set<TEntity>().Update(entity);
        return new ValueTask<TEntity?>(entry.Entity);
    }

    public ValueTask<TEntity?> DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IEntity
    {
        var entry = Set<TEntity>().Remove(entity);
        return new ValueTask<TEntity?>(entry.Entity);
    }

    public async ValueTask<bool> SaveAsync(CancellationToken cancellationToken = default)
    {
        return await SaveChangesAsync(cancellationToken) > 0;
    }
}