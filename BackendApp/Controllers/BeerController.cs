using BackendApp.DTOs;
using BackendApp.Models;
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
        private StoreContext _context;
        private IValidator<CreateBeerDto> _createBeerValidator;
        private IValidator<UpdateBeerDto> _updateBeerValidator;

        public BeerController(StoreContext context, IValidator<CreateBeerDto> createBeerValidator, IValidator<UpdateBeerDto> updateBeerValidator)
        {
            _context = context;
            _createBeerValidator = createBeerValidator;
            _updateBeerValidator = updateBeerValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() => await _context.Beers.Select(b => new BeerDto
        {
            Id = b.BeerId,
            Name = b.Name,
            Alcohol = b.Alcohol,
            BrandId = b.BrandId,

        }).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            var beerDto = new BeerDto
            {
                Id = beer.BrandId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandId = beer.BrandId,
            };

            return Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(CreateBeerDto createBeerDto)
        {
            var validationResult = await _createBeerValidator.ValidateAsync(createBeerDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beer = new Beer()
            {
                Name = createBeerDto.Name,
                BrandId = createBeerDto.BrandId,
                Alcohol = createBeerDto.Alcohol,
            };

            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandId = beer.BrandId
            };

            return CreatedAtAction(nameof(GetById),new {id=beer.BeerId},beerDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, UpdateBeerDto updateBeerDto)
        {
            var validationResult = await _updateBeerValidator.ValidateAsync(updateBeerDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var beer = await _context.Beers.FindAsync(id);

            beer.Name = updateBeerDto.Name;
            beer.Alcohol = updateBeerDto.Alcohol;
            beer.BrandId = updateBeerDto.BrandId;

            await _context.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandId = beer.BrandId
            };

            return Ok(beerDto);
            
        }
    }
}
