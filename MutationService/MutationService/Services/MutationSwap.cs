using MutationService.Interfaces;

namespace MutationService.Services
{
    public class MutationSwap : IMutation
    {
        public Chromosome Mutate(Chromosome chromosome, double mutationRate, int initial)
        {
            var random = new Random();
            if(random.NextDouble()<mutationRate)
            {  
                while(true)
                {

                }
            }
            return chromosome;
        }
    }
}
