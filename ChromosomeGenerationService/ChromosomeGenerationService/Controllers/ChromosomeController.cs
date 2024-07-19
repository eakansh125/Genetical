using Microsoft.AspNetCore.Mvc;
using ChromosomeGenerationService.Models;
using System.Collections.Generic;

namespace ChromosomeGenerationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChromosomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Chromosome> GenerateChromosome(int geneLength)
        {
            var random = new Random();
            var genes = new List<int>();

            // Binary genes (0 or 1)
            for (int i = 0; i < geneLength; i++)
            {
                genes.Add(random.Next(0, 2)); 
            }

            var chromosome = new Chromosome { Genes = genes };
            return Ok(chromosome);
        }
    }
}
