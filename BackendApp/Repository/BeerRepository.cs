using BackendApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApp.Repository
{
    public class BeerRepository : IRepository<Beer>
    {
        private StoreContext _storeContext;

        public BeerRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task Add(Beer entity) => await _storeContext.Beers.AddAsync(entity);

        public void Delete(Beer entity) => _storeContext.Beers.Remove(entity);

        public async Task<IEnumerable<Beer>> Get() => await _storeContext.Beers.ToListAsync();

        public async Task<Beer> GetById(int id) => await _storeContext.Beers.FindAsync(id);

        public async Task Save() => await _storeContext.SaveChangesAsync();

        public void Update(Beer entity)
        {
            _storeContext.Beers.Attach(entity);
            _storeContext.Beers.Entry(entity).State = EntityState.Modified;
        }
    }
}
