namespace MutationService.Interfaces
{
    public interface IMutation
    {
        Chromosome Mutate(Chromosome chromosome, double mutationRate);
        //Chromosome Mutate(Chromosome chromosome, double mutationRate, int initial);
    }
}
