using MutationService.Interfaces;

namespace MutationService.Services
{
    public class MutationBitflip : IMutation
    {
        public Chromosome Mutate(Chromosome chromosome, double mutationRate)
        {
            var random = new Random();

            for (int i = 0; i < chromosome.Genes.Count; i++)
            {
                if (random.NextDouble() < mutationRate)
                {
                    chromosome.Genes[i] = 1 - chromosome.Genes[i]; // Flip bit
                }
            }
            return chromosome;
        }
    }
}
