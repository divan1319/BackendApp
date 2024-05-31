using AutoMapper;
using BackendApp.DTOs;
using BackendApp.Models;
using BackendApp.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackendApp.Services
{
    public class BeerService : ICommonService<BeerDto, CreateBeerDto, UpdateBeerDto>
    {
 
        private IRepository<Beer> _beerRepository;
        private IMapper _mapper;
        public BeerService(IRepository<Beer> beerRepository,IMapper mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }
        public async Task<BeerDto> Add(CreateBeerDto createBeerDto)
        {
            var beer = _mapper.Map<Beer>(createBeerDto);

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

            var beerDto = _mapper.Map<BeerDto>(beer);

            return beerDto;
        }

        public async Task<BeerDto> Delete(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = _mapper.Map<BeerDto>(beer);

                _beerRepository.Delete(beer);
                await _beerRepository.Save();

                return beerDto;
            }

            return null;
        }

        public async Task<IEnumerable<BeerDto>> Get()
        {
            var beers = await _beerRepository.Get();

            return beers.Select(b => _mapper.Map<BeerDto>(b));
        }
            

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = _mapper.Map<BeerDto>(beer);

                return beerDto;
            }

            return null;
        }

        public async Task<BeerDto> Update(int id, UpdateBeerDto updateBeerDto)
        {
            try
            {
                Console.WriteLine(id);
                Console.WriteLine(updateBeerDto);
                var beer = await _beerRepository.GetById(id);
                Console.WriteLine(beer);

                if (beer != null)
                {
                    //beer = _mapper.Map<UpdateBeerDto,Beer>(updateBeerDto);

                    _beerRepository.Update(beer);
                    await _beerRepository.Save();

                    var beerDto = _mapper.Map<BeerDto>(beer);

                    return beerDto;
                }
                return null;
            }
            catch(Exception e)
            {
                Console.WriteLine("hubo un error:"+e.ToString());
                return null;
            }


            
        }
    }
}
