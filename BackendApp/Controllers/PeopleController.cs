using BackendApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {

        private IPeopleService _peopleService;

        public PeopleController()
        {
            _peopleService = new PeopleService();
        }

        [HttpGet("all")]
        public List<People> GetPeople() => Repository.People;

        [HttpGet("{id}")]
        public ActionResult<People> Get(int id) {
            var people = Repository.People.FirstOrDefault(p => p.Id == id);

            if(people == null)
            {
                return NotFound();
            }
            
            return Ok(people);
         }

        [HttpGet("search/{search}")]
        public List<People> Get(string search) =>
            Repository.People.Where(p => p.Name.ToUpper().Contains(search.ToUpper())).ToList();

        [HttpPost]
        public IActionResult Add(People people)
        {
            if (!_peopleService.Validate(people))
            {
                return BadRequest();
            }

            Repository.People.Add(people);

            return NoContent();
        }

    }

    public class Repository
    {
        public static List<People> People = new List<People>
        {
            new People()
            {
                Id = 1,Name = "John Doe", Birthdate = new DateTime(1987,10,3)
            },
            new People()
            {
                Id = 3,Name = "Karla Maria", Birthdate = new DateTime(1991,12,3)
            },
            new People()
            {
                Id = 2,Name = "Mrs beats", Birthdate = new DateTime(1990,12,3)
            },
            new People()
            {
                Id = 4,Name = "jhony deep", Birthdate = new DateTime(1998,09,2)
            },
        };
    }
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
