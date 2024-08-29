using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GAOrchestrator;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<ActionResult> RunGA([FromBody] GARequest gaRequest) //, int elitismCount = 1
        {
            //var digits = gaRequest.algo.Split(',');
            List<int> digits = gaRequest.algo.ToString().Select(digit => int.Parse(digit.ToString())).ToList();
            var population = new List<Chromosome>();
            //var bestSolutions = new List<Chromosome>();
            var generationFitnessList = new List<GenerationFitness>();

            // Generate initial population
            for (int i = 0; i < gaRequest.populationSize; i++)
            {
                //var request = new RestRequest("http://localhost:5285/Chromosome", Method.Get);
                var request = new RestRequest("https://chromosomegenerationservice-b4f0agh6hsfnauex.ukwest-01.azurewebsites.net/Chromosome", Method.Get);
                request.AddParameter("geneLength", gaRequest.geneLength);
                request.AddParameter("generationType", digits[0]);
                var response = await _client.ExecuteAsync<Chromosome>(request);
                population.Add(response.Data);
            }

            // Evaluate fitness
            foreach (var chromosome in population)
            {
                var request = new RestRequest("https://localhost:7149/Fitness", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(chromosome);
                request.AddParameter("fitnessType", digits[1], ParameterType.QueryString);
                var response = await _client.ExecuteAsync<double>(request);
                chromosome.Fitness = response.Data;
            }

            for (int generation = 0; generation < gaRequest.generations; generation++)
            {
                

                // Store the best solution of the current generation
                var bestSolution = population.OrderByDescending(c => c.Fitness).First();
                var averageFitness = population.Average(c => c.Fitness);
                generationFitnessList.Add(new GenerationFitness
                {
                    Generation = generation,
                    BestFitness = bestSolution.Fitness,
                    AverageFitness = averageFitness,
                    BestGenes = bestSolution.Genes
                });

                // Selection
                var selectionRequest = new RestRequest("https://localhost:7187/Selection", Method.Post);
                selectionRequest.AddHeader("Content-Type", "application/json");
                selectionRequest.AddJsonBody(population);
                selectionRequest.AddParameter("selectionCount", gaRequest.populationSize / 2, ParameterType.QueryString);
                selectionRequest.AddParameter("selectionType", digits[2], ParameterType.QueryString);
                var selectionResponse = await _client.ExecuteAsync<List<Chromosome>>(selectionRequest);
                var selectedPopulation = selectionResponse.Data;


                // Crossover
                var newPopulation = new List<Chromosome>();
                while (newPopulation.Count < gaRequest.populationSize) //- elitismCount
                {
                    var parents = selectedPopulation.OrderBy(x => Guid.NewGuid()).Take(2).ToList();
                    var crossoverRequest = new RestRequest("https://localhost:7192/Crossover", Method.Post);
                    crossoverRequest.AddHeader("Content-Type", "application/json");
                    crossoverRequest.AddJsonBody(parents);
                    crossoverRequest.AddParameter("crossoverType", digits[3], ParameterType.QueryString);
                    var crossoverResponse = await _client.ExecuteAsync<List<Chromosome>>(crossoverRequest);
                    newPopulation.AddRange(crossoverResponse.Data);
                }

                // Mutation
                var mutatedPopulation = new List<Chromosome>();
                foreach (var chromosome in newPopulation)
                {
                    var mutationRequest = new RestRequest("https://localhost:7191/Mutation", Method.Post);
                    mutationRequest.AddHeader("Content-Type", "application/json");
                    mutationRequest.AddJsonBody(chromosome);
                    mutationRequest.AddParameter("mutationRate", gaRequest.mutationRate, ParameterType.QueryString);
                    mutationRequest.AddParameter("mutationType", digits[4], ParameterType.QueryString);
                    var mutationResponse = await _client.ExecuteAsync<Chromosome>(mutationRequest);
                    mutatedPopulation.Add(mutationResponse.Data);
                }

                // Evaluate fitness
                foreach (var chromosome in mutatedPopulation)
                {
                    var request = new RestRequest("https://localhost:7149/Fitness", Method.Post);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddJsonBody(chromosome);
                    request.AddParameter("fitnessType", digits[1], ParameterType.QueryString);
                    var response = await _client.ExecuteAsync<double>(request);
                    chromosome.Fitness = response.Data;
                }

                // Add elitism: carry forward the best solutions to the next generation
                //var elite = population.OrderByDescending(c => c.Fitness).Take(elitismCount).ToList();
                //mutatedPopulation.AddRange(elite);

                //population = mutatedPopulation;
                //population = mutatedPopulation.OrderByDescending(c => c.Fitness).Take(populationSize - 1).ToList();
                //population.Add(bestSolution);
                // Replacement: Replace the least fit individuals with new offspring
                var combinedPopulation = population.Concat(mutatedPopulation).ToList();
                population = combinedPopulation.OrderByDescending(c => c.Fitness).Take(gaRequest.populationSize).ToList();
            }

            // Return the best solution
            //var bestSolution = population.OrderByDescending(c => c.Fitness).First();
            return Ok(generationFitnessList);
        }
    }
}
