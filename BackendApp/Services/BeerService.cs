using BackendApp.DTOs;
using BackendApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApp.Services
{
    public class BeerService : ICommonService<BeerDto, CreateBeerDto, UpdateBeerDto>
    {
        private StoreContext _storeContext;

        public BeerService(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<BeerDto> Add(CreateBeerDto createBeerDto)
        {
            var beer = new Beer()
            {
                Name = createBeerDto.Name,
                BrandId = createBeerDto.BrandId,
                Alcohol = createBeerDto.Alcohol,
            };

            await _storeContext.Beers.AddAsync(beer);
            await _storeContext.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandId = beer.BrandId
            };

            return beerDto;
        }

        public async Task<BeerDto> Delete(int id)
        {
            var beer = await _storeContext.Beers.FindAsync(id);

            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandId = beer.BrandId
                };

                _storeContext.Remove(beer);
                await _storeContext.SaveChangesAsync();

                return beerDto;
            }

            return null;
        }

        public async Task<IEnumerable<BeerDto>> Get() =>
            await _storeContext.Beers.Select(b => new BeerDto
            {
                Id = b.BeerId,
                Name = b.Name,
                Alcohol = b.Alcohol,
                BrandId = b.BrandId,

            }).ToListAsync();

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _storeContext.Beers.FindAsync(id);

            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BrandId,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandId = beer.BrandId,
                };

                return beerDto;
            }

            return null;
        }

        public async Task<BeerDto> Update(int id, UpdateBeerDto updateBeerDto)
        {
            var beer = await _storeContext.Beers.FindAsync(id);

            if(beer != null)
            {
                beer.Name = updateBeerDto.Name;
                beer.Alcohol = updateBeerDto.Alcohol;
                beer.BrandId = updateBeerDto.BrandId;

                await _storeContext.SaveChangesAsync();

                var beerDto = new BeerDto
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandId = beer.BrandId
                };

                return beerDto;
            }

            return null;
        }
    }
}
