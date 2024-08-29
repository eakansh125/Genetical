using Microsoft.AspNetCore.Mvc;
using MutationService.Interfaces;
using MutationService.Services;

namespace MutationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MutationController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Chromosome> MutateChromosome([FromBody] Chromosome chromosome, double mutationRate, int mutationType)
        {
            IMutation mutationBitflip = new MutationBitflip();

            var mutation = mutationBitflip.Mutate(chromosome, mutationRate);
            
            return Ok(chromosome);
        }
    }
}
