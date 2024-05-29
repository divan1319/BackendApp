using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BackendApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult GetSync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            stopwatch.Start();
            Thread.Sleep(1000);
            Console.WriteLine("bd connected");

            Thread.Sleep(1000);
            Console.WriteLine("email finish");

            Console.WriteLine("Todo ha terminado");
            stopwatch.Stop();
            
            return Ok(stopwatch.Elapsed);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            var task1 = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("bd connected");
                return 1;
            });

            var task2 = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("email was send");
                return 2;
            });

            task1.Start();
            task2.Start();

            Console.WriteLine("hago otra cosa");

            var result1 = await task1;
            var result2 = await task2;

            Console.WriteLine("todo ha terminado");

            stopwatch.Stop();

            return Ok(result1 +" "+result2+" "+stopwatch.Elapsed);
        }
    }
}
