using FitnessFunctionService.Interfaces;

namespace FitnessFunctionService.Services
{
    public class FitnessFunctionGeneral : IFitnessFunction
    {
        public double CalculateFitness(Chromosome chromosome)
        {
            double fitness = chromosome.Genes.Sum();
            return fitness;
        }
    }
}
