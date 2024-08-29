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
        public ActionResult<double> EvaluateFitness([FromBody] Chromosome chromosome, int fitnessType)
        {
            if (fitnessType == 1)
            {
                IFitnessFunction fitnessFunctionGeneral = new FitnessFunctionGeneral();
                return Ok(fitnessFunctionGeneral.CalculateFitness(chromosome));
            }
            else if (fitnessType == 2)
            {
                IFitnessFunction fitnessFunctionNormalized = new FitnessFunctionNormalized(0.3, 0.8);
                return Ok(fitnessFunctionNormalized.CalculateFitness(chromosome));
            }
            
            
            //var fitness = fitnessFunctionNormalized.CalculateFitness(chromosome);
            return BadRequest("Invalid fitness selection, please enter a valid fitness function type");
        }
    }
}
