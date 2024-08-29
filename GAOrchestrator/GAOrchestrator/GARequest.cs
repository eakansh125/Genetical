namespace GAOrchestrator
{
    public class GARequest
    {
        public int populationSize { get; set; }
        public int geneLength { get; set; } 
        public int generations { get; set; } 
        public double mutationRate { get; set; }
        public int algo { get; set; }
    }
}
