using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using try2.DAL.Interfaces;
using try2.DAL.Models;
using try2.Domain.Entities;
using try2.Domain.Entities.Base;
using Version = try2.DAL.Models.Version;

namespace try2.DAL.Repositories
{
    public class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly DbSet<T>? _Set;

        private readonly AirplanesDbContext _db;

        public virtual IQueryable<T> Items => _Set;


        public T Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            _db.SaveChanges();
            return item;    
        }

        public async Task<T> AddAsync(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            await _db.SaveChangesAsync();
            return item;
        }

        public T Get(int id)
        {
            return Items.FirstOrDefault(i => i.Id == id);
        }

        public async Task GetAsync(int id) => await Items.SingleOrDefaultAsync(i => i.Id == id).ConfigureAwait(false);

        public void Remove(int id)
        {
            var item = _Set.Local.FirstOrDefault(i => i.Id == id) ?? new T { Id = id };

            _db.Remove(item);

            _db.SaveChanges();
        }

        public async Task RemoveAsync(int id)
        {
            var item = await _db.Set<T>().Where(i => i.Id == id).SingleOrDefaultAsync() ?? new T { Id = id };
            _db.Remove(item);
            await _db.SaveChangesAsync();
        }

        public void Update(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (!_db.Set<T>().Local.Any(e => e.Id == item.Id))
            {
                _db.Set<T>().Attach(item);
                _db.Entry(item).State = EntityState.Modified;
            }
            _db.SaveChanges();
        }

        public async Task UpdateAsync(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public DbRepository(AirplanesDbContext db)
        {
            _db = db;
            _Set = db.Set<T>();
        }
    }

    public class ProjectRepository : DbRepository<Project>
    {

        public override IQueryable<Project> Items => base.Items.Include(item => item.Versions);

        public ProjectRepository(AirplanesDbContext db) : base(db)
        {

        }
    }

    public class VersionRepository : DbRepository<Version>
    {

        public override IQueryable<Version> Items => base.Items.Include(item => item.Project);

        public VersionRepository(AirplanesDbContext db) : base(db)
        {

        }
    }
    /*  public class UserRepository : DbRepository<User>
      {

          public override IQueryable<User> Items => base.Items.Include(item => item.Profiles);

          public UserRepository(AccountDbContext db) : base(db)
          {


          }
      }

      public class ProfileRepository : DbRepository<Profile>
      {

          public override IQueryable<Profile> Items => base.Items.Include(item => item.ThisUser);

          public ProfileRepository(AccountDbContext db) : base(db)
          {


          }
      }
    */
}
