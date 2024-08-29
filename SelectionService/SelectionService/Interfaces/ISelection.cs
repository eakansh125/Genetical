namespace SelectionService.Interfaces
{
    public interface ISelection
    {
        List<Chromosome> Select(List<Chromosome> population, int selectionCount);
    }
}
