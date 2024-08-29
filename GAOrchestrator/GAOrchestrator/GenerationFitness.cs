namespace GAOrchestrator
{
    public class GenerationFitness
    {
        public int Generation { get; set; }
        public double BestFitness { get; set; }
        public double AverageFitness { get; set; }
        public List<int> BestGenes { get; set; }
    }
}
