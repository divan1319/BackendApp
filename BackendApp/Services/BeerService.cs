using BackendApp.DTOs;
using BackendApp.Models;
using BackendApp.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackendApp.Services
{
    public class BeerService : ICommonService<BeerDto, CreateBeerDto, UpdateBeerDto>
    {
 
        private IRepository<Beer> _beerRepository;

        public BeerService(IRepository<Beer> beerRepository)
        {
            _beerRepository = beerRepository;
        }
        public async Task<BeerDto> Add(CreateBeerDto createBeerDto)
        {
            var beer = new Beer()
            {
                Name = createBeerDto.Name,
                BrandId = createBeerDto.BrandId,
                Alcohol = createBeerDto.Alcohol,
            };

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

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
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandId = beer.BrandId
                };

                _beerRepository.Delete(beer);
                await _beerRepository.Save();

                return beerDto;
            }

            return null;
        }

        public async Task<IEnumerable<BeerDto>> Get()
        {
            var beers = await _beerRepository.Get();

            return beers.Select(b => new BeerDto
            {
                Id = b.BeerId,
                Name = b.Name,
                Alcohol = b.Alcohol,
                BrandId = b.BrandId
            });
        }
            

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

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
            var beer = await _beerRepository.GetById(id);

            if(beer != null)
            {
                beer.Name = updateBeerDto.Name;
                beer.Alcohol = updateBeerDto.Alcohol;
                beer.BrandId = updateBeerDto.BrandId;

                _beerRepository.Update(beer);
                await _beerRepository.Save();

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
