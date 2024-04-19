namespace ProjectManagementAPI.Repositories;

public interface IGenericRepository<T>
{
    public Task<IEnumerable<T>> GetAll();

    public Task<T?> GetById(int id);

    public Task<T?> Delete(int id);
}