using ChromosomeGenerationService.Models;

namespace ChromosomeGenerationService.Interfaces
{
    public interface IChromosome
    {
        Chromosome Generate(int geneLength);
    }
}
