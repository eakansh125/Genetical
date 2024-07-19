using Microsoft.AspNetCore.Mvc;

namespace SelectionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SelectionController : ControllerBase
    {
        [HttpPost]
        public ActionResult<List<Chromosome>> SelectChromosomes([FromBody] List<Chromosome> population, int selectionCount)
        {
            var selectedChromosomes = population.OrderByDescending(c => c.Fitness).Take(selectionCount).ToList();
            return Ok(selectedChromosomes);
        }
    }
}
