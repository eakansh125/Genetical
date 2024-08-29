using Microsoft.AspNetCore.Mvc;
using ChromosomeGenerationService.Models;
using System.Collections.Generic;
using ChromosomeGenerationService.Interfaces;
using ChromosomeGenerationService.Services;

namespace ChromosomeGenerationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChromosomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Chromosome> GenerateChromosome(int geneLength, int generationType)
        {
            if (generationType == 1)
            {
                IChromosome chromosomeRandom = new ChromosomeRandom();
                return Ok(chromosomeRandom.Generate(geneLength));
            }
            else if (generationType == 2)
            {
                IChromosome chromosomeHeuristic = new ChromosomeHeuristic();
                return Ok(chromosomeHeuristic.Generate(geneLength));
            }
            
            return Ok("Invalid generation selection, please enter correct generation type");
        
        }
    }
}
