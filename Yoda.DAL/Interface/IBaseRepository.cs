namespace Yoda.DAL.Interface
{
	/// <summary>
	/// Basic repository interface for CRUD operations on project entities.
	/// </summary>
	public interface IBaseRepository<T>
	{
		/// <summary>
		/// The operation of creating a new entity instance and saving to the db.
		/// </summary>
		/// <param name="entity">New entity instance.</param>
		Task Create(T entity);


		/// <summary>
		/// Deleting an entity instance from the db.
		/// </summary>
		/// <param name="entity">Deleting entity.</param>
		Task Delete(T entity);


		/// <summary>
		/// Updating entity data.
		/// </summary>
		/// <param name="entity">Updating entity.</param>
		Task<T> Update(T entity);

		/// <summary>
		/// Getting all entities from database.
		/// </summary>
		IQueryable<T> GetAll();
	}
}
