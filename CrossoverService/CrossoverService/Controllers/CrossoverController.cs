namespace CrossoverService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    namespace CrossoverService.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class CrossoverController : ControllerBase
        {
            [HttpPost]
            public ActionResult<List<Chromosome>> CrossoverChromosomes([FromBody] List<Chromosome> parents)
            {
                if (parents.Count < 2)
                {
                    return BadRequest("At least two parents are required.");
                }

                var random = new Random();
                int crossoverPoint = random.Next(1, parents[0].Genes.Count - 1);

                var offspring1 = new Chromosome
                {
                    Id = parents[0].Id,
                    Genes = parents[0].Genes.Take(crossoverPoint).Concat(parents[1].Genes.Skip(crossoverPoint)).ToList(),
                    //Fitness = parents[0].Fitness
                };

                var offspring2 = new Chromosome
                {
                    Id = parents[1].Id,
                    Genes = parents[1].Genes.Take(crossoverPoint).Concat(parents[0].Genes.Skip(crossoverPoint)).ToList(),
                    //Fitness = parents[1].Fitness
                };

                return Ok(new List<Chromosome> { offspring1, offspring2 });
            }
        }
    }

}
