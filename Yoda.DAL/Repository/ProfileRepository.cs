﻿using Yoda.DAL.Interface;
using Yoda.Domain.Model;

namespace Yoda.DAL.Repository
{
	public class ProfileRepository : IBaseRepository<Profile>
	{
		private readonly AppDbContext _dbContext;

		public ProfileRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Create(Profile entity)
		{
			await _dbContext.Profiles.AddAsync(entity);
			await _dbContext.SaveChangesAsync();
		}

		public IQueryable<Profile> GetAll()
		{
			return _dbContext.Profiles;
		}

		public async Task Delete(Profile entity)
		{
			_dbContext.Profiles.Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<Profile> Update(Profile entity)
		{
			_dbContext.Profiles.Update(entity);
			await _dbContext.SaveChangesAsync();
			return entity;
		}
	}
}
