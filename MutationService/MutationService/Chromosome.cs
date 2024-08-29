namespace MutationService
{
    public class Chromosome
    {
        public int Id { get; set; }
        public List<int> Genes { get; set; }
        public double Fitness { get; set; }
    }
}
