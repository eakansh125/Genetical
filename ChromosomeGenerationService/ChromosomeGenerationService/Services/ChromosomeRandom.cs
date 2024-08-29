using ChromosomeGenerationService.Interfaces;
using ChromosomeGenerationService.Models;

namespace ChromosomeGenerationService.Services
{
    public class ChromosomeRandom : IChromosome
    {
        public Chromosome Generate(int geneLength)
        {
            var random = new Random();
            var genes = new List<int>();

            // Binary genes (0 or 1)
            for (int i = 0; i < geneLength; i++)
            {
                genes.Add(random.Next(0, 2));
            }

            var chromosome = new Chromosome { Genes = genes };
            return chromosome;
        }
    }
}
