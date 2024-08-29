using ChromosomeGenerationService.Interfaces;
using ChromosomeGenerationService.Models;

namespace ChromosomeGenerationService.Services
{
    public class ChromosomeHeuristic : IChromosome
    {
        public Chromosome Generate(int geneLength)
        {
            // Implement domain-specific heuristic initialization logic here
            var genes = new List<int>();

            for (int i = 0; i < geneLength; i++)
            {
                // Example heuristic: prefer 1s over 0s, but with some randomness
                genes.Add(i % 3 == 0 ? 0 : 1);
            }

            return new Chromosome { Genes = genes };
        }
    }
}
