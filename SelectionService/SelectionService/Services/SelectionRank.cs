using SelectionService.Interfaces;

namespace SelectionService.Services
{
    public class SelectionRank: ISelection
    {
        public List<Chromosome> Select(List<Chromosome> population, int selectionCount)
        {
            var selectedChromosomes = population.OrderByDescending(c => c.Fitness).Take(selectionCount).ToList();
            return selectedChromosomes;
        }
    }
}
