using FitnessFunctionService.Interfaces;

namespace FitnessFunctionService.Services
{
    public class FitnessFunctionNormalized : IFitnessFunction
    {
        private readonly double _min;
        private readonly double _max;

        public FitnessFunctionNormalized(double min = 0.0, double max = 1.0)
        {
            _min = min;
            _max = max;
        }

        public double CalculateFitness(Chromosome chromosome)
        {
            // Implement the normalized fitness calculation logic
            double rawFitness = chromosome.Genes.Sum();
            double normalizedFitness = Normalize(rawFitness, chromosome);
            return normalizedFitness;
        }

        private double Normalize(double rawFitness, Chromosome chromosome)
        {
            // Assuming raw fitness ranges from 0 to max possible value
            double maxPossibleFitness = chromosome.Genes.Count * chromosome.Genes.Max();
            return (_max - _min) * (rawFitness / maxPossibleFitness) + _min;
        }
    }
}
