using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GAOrchestrator;

namespace GAOrchestratorService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GAController : ControllerBase
    {
        private readonly RestClient _client;

        public GAController()
        {
            _client = new RestClient();
        }

        [HttpGet]
        public async Task<ActionResult> RunGA(int populationSize, int geneLength, int generations, double mutationRate)
        {
            var population = new List<Chromosome>();

            // Generate initial population
            for (int i = 0; i < populationSize; i++)
            {
                var request = new RestRequest("https://localhost:7189/Chromosome", Method.Get);
                request.AddParameter("geneLength", geneLength);
                var response = await _client.ExecuteAsync<Chromosome>(request);
                population.Add(response.Data);
            }

            for (int generation = 0; generation < generations; generation++)
            {
                // Evaluate fitness
                foreach (var chromosome in population)
                {
                    var request = new RestRequest("https://localhost:7149/Fitness", Method.Post);
                    request.AddJsonBody(chromosome);
                    var response = await _client.ExecuteAsync<double>(request);
                    chromosome.Fitness = response.Data;
                }

                // Selection
                var selectionRequest = new RestRequest("https://localhost:7187/Selection", Method.Post);
                selectionRequest.AddHeader("Content-Type", "application/json");
                selectionRequest.AddJsonBody(population);
                selectionRequest.AddParameter("selectionCount", populationSize / 2, ParameterType.QueryString);
                var selectionResponse = await _client.ExecuteAsync<List<Chromosome>>(selectionRequest);
                var selectedPopulation = selectionResponse.Data;


                // Crossover
                var newPopulation = new List<Chromosome>();
                while (newPopulation.Count < populationSize)
                {
                    var parents = selectedPopulation.OrderBy(x => Guid.NewGuid()).Take(2).ToList();
                    var crossoverRequest = new RestRequest("https://localhost:7192/Crossover", Method.Post);
                    crossoverRequest.AddJsonBody(parents);
                    var crossoverResponse = await _client.ExecuteAsync<List<Chromosome>>(crossoverRequest);
                    newPopulation.AddRange(crossoverResponse.Data);
                }

                // Mutation
                var mutatedPopulation = new List<Chromosome>();
                foreach (var chromosome in newPopulation)
                {
                    var mutationRequest = new RestRequest("https://localhost:7191/Mutation", Method.Post);
                    mutationRequest.AddJsonBody(chromosome);
                    mutationRequest.AddParameter("mutationRate", mutationRate, ParameterType.QueryString);
                    var mutationResponse = await _client.ExecuteAsync<Chromosome>(mutationRequest);
                    mutatedPopulation.Add(mutationResponse.Data);
                }

                population = mutatedPopulation;
            }

            // Return the best solution
            var bestSolution = population.OrderByDescending(c => c.Fitness).First();
            return Ok(bestSolution);
        }
    }
}
