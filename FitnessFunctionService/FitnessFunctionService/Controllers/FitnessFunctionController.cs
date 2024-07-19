using FitnessFunctionService.Interfaces;
using FitnessFunctionService.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessFunctionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FitnessController : ControllerBase
    {
        //private readonly IFitnessFunction _fitnessFunction;

        //public FitnessController(IFitnessFunction fitnessFunction)
        //{
        //    _fitnessFunction = fitnessFunction;
        //}
        
        [HttpPost]
        public ActionResult<double> EvaluateFitness([FromBody] Chromosome chromosome)
        {
            IFitnessFunction fitnessFunctionGeneral = new FitnessFunctionGeneral(); 
            var fitness = fitnessFunctionGeneral.CalculateFitness(chromosome);
            return Ok(fitness);
        }
    }
}
