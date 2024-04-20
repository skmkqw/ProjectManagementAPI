namespace ProjectManagementAPI.Repositories;

public interface IGenericRepository<T>
{
    public Task<IEnumerable<T>> GetAll();

    public Task<T?> GetById(Guid id);

    public Task<T?> Delete(Guid id);
}