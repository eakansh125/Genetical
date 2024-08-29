using SelectionService.Interfaces;

namespace SelectionService.Services
{
    public class SelectionTournament : ISelection
    {
        private readonly int _tournamentSize;

        public SelectionTournament(int tournamentSize = 3)
        {
            _tournamentSize = tournamentSize;
        }

        public List<Chromosome> Select(List<Chromosome> population, int selectionCount)
        {
            var selected = new List<Chromosome>();
            var rand = new Random();

            for (int i = 0; i < selectionCount; i++)
            {
                var tournament = new List<Chromosome>();

                for (int j = 0; j < _tournamentSize; j++)
                {
                    var randomIndex = rand.Next(population.Count);
                    tournament.Add(population[randomIndex]);
                }

                var bestInTournament = tournament.OrderByDescending(c => c.Fitness).First();
                selected.Add(bestInTournament);
            }

            return selected;
        }
    }
}
