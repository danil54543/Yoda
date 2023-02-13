using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoda.DAL.Interface;
using Yoda.Domain.Model;

namespace Yoda.DAL.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        /// <summary>
		/// Database context instance.
		/// </summary>
		private readonly AppDbContext db;

        public ProfileRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task Create(Profile entity)
        {
            await db.Profiles.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(Profile entity)
        {
            db.Profiles.Remove(entity);
            await db.SaveChangesAsync();
        }

        public IQueryable<Profile> GetAll()
        {
            return db.Profiles;
        }

        public async Task<Profile> Update(Profile entity)
        {
            db.Profiles.Update(entity);
            await db.SaveChangesAsync();
            return entity;
        }
    }
}
