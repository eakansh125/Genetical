using Microsoft.AspNetCore.Mvc;
using SelectionService.Interfaces;
using SelectionService.Services;

namespace SelectionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SelectionController : ControllerBase
    {
        [HttpPost]
        public ActionResult<List<Chromosome>> SelectChromosomes([FromBody] List<Chromosome> population, 
            int selectionCount, int selectionType)
        {
            if (selectionType == 1)
            {
                ISelection selectionRank = new SelectionRank();
                return Ok(selectionRank.Select(population,selectionCount));
            }
            else if (selectionType == 2)
            {
                ISelection selectionTournament = new SelectionTournament();
                return Ok(selectionTournament.Select(population, selectionCount));
            }
            return BadRequest("Invalid Selection, enter a valid selecion type");
        }
    }
}
