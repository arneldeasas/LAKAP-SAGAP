namespace LAKAPSAGAP.Services.Repositories;

public interface ICommonRepository<T>
{
	public Task<T> Create(T item);
	public Task<T> Update(T item);
	public Task<T> Delete(T item);
	public Task<T> GetById(string Id);
	public Task<List<T>> GetAll();
}
