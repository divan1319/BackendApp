using BackendApp.DTOs;
using BackendApp.Models;
using BackendApp.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {


        private IValidator<CreateBeerDto> _createBeerValidator;
        private IValidator<UpdateBeerDto> _updateBeerValidator;
        private ICommonService<BeerDto, CreateBeerDto, UpdateBeerDto> _beerService;

        public BeerController(
            StoreContext context,
            IValidator<CreateBeerDto> createBeerValidator,
            IValidator<UpdateBeerDto> updateBeerValidator,
            [FromKeyedServices("BeerService")] ICommonService<BeerDto,CreateBeerDto,UpdateBeerDto> beerService
            )
        {

            _createBeerValidator = createBeerValidator;
            _updateBeerValidator = updateBeerValidator;
            _beerService = beerService;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() => await _beerService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beerDto = await _beerService.GetById(id);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(CreateBeerDto createBeerDto)
        {
            var validationResult = await _createBeerValidator.ValidateAsync(createBeerDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beerDto = await _beerService.Add(createBeerDto);

            return CreatedAtAction(nameof(GetById),new {id=beerDto.Id},beerDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, UpdateBeerDto updateBeerDto)
        {
            var validationResult = await _updateBeerValidator.ValidateAsync(updateBeerDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beerDto = _beerService.Update(id,updateBeerDto);

            

            return beerDto == null ? NotFound():Ok(beerDto) ;
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> Delete(int id)
        {
            var beerDto = await _beerService.Delete(id);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }
    }
}
