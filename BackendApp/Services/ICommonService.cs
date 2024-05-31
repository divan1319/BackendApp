using BackendApp.DTOs;

namespace BackendApp.Services
{
    public interface ICommonService<T,TI,TU>
    {
        Task<IEnumerable<T>> Get();

        Task<T> GetById(int id);

        Task<T> Add(TI createBeerDto);

        Task<T> Update(int id, TU updateBeerDto);

        Task<T> Delete(int id);

    }
}
