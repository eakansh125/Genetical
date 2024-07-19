using Microsoft.AspNetCore.Mvc;

namespace MutationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MutationController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Chromosome> MutateChromosome([FromBody] Chromosome chromosome, double mutationRate)
        {
            var random = new Random();

            for (int i = 0; i < chromosome.Genes.Count; i++)
            {
                if (random.NextDouble() < mutationRate)
                {
                    chromosome.Genes[i] = 1 - chromosome.Genes[i]; // Flip bit
                }
            }

            return Ok(chromosome);
        }
    }
}
