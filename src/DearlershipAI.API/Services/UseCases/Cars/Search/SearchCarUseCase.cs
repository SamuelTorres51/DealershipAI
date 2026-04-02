using DearlershipAI.API.Models.DTOs.Requests;
using DearlershipAI.API.Models.DTOs.Responses;
using DearlershipAI.API.Models.Repositories.Cars;
using Google.GenAI;
using Google.GenAI.Types;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace DearlershipAI.API.Services.UseCases.Cars.Search;

public class SearchCarUseCase : ISearchCarUseCase {
    private readonly ICarReadOnlyRepository _repository;
    private readonly IConfiguration _configuration;

    public SearchCarUseCase(ICarReadOnlyRepository repository, IConfiguration configuration) {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task<ResponseCarsJson?> Execute(RequestSearchCarJson request) {
        var API_KEY = _configuration["ConnectionStrings:API_KEY"];
        var client = new Client(apiKey: API_KEY);
        var userMessage = request.Search;

        var prompt = $@"
        Você é um extrator de dados para busca de veículos em um sistema de concessionária.

        Sua tarefa é analisar a mensagem do usuário e retornar um JSON com os seguintes campos:

        - brand: fabricante do veículo (ex: Volkswagen, Fiat, BMW)
        - model: modelo do veículo (ex: Gol, Civic, Corolla)
        - version: versão, motor ou variante (ex: 1.0, 1.4 TSI, Comfortline, Touring)

        REGRAS IMPORTANTES:
        - NÃO invente informações
        - Só preencha um campo se ele estiver claramente presente na mensagem
        - Se um campo não estiver presente, retorne null
        - NÃO deduza valores implícitos
        - NÃO adicione nenhum texto fora do JSON
        - Retorne APENAS JSON válido

        EXEMPLOS:

        Entrada: ""Tem Gol 1.4?""
        Saída:
        {{ ""brand"": null, ""model"": ""Gol"", ""version"": ""1.4"" }}

        Entrada: ""Vocês têm BMW?""
        Saída:
        {{ ""brand"": ""BMW"", ""model"": null, ""version"": null }}

        Entrada: ""Tem Civic Touring?""
        Saída:
        {{ ""brand"": null, ""model"": ""Civic"", ""version"": ""Touring"" }}

        Entrada: ""Quais carros vocês têm?""
        Saída:
        {{ ""brand"": null, ""model"": null, ""version"": null }}

        Agora analise:

        Entrada: ""{userMessage}""
        Saída:
        ";

        var config = new GenerateContentConfig {
            ResponseMimeType = "application/json"
        };

        var response = await client.Models.GenerateContentAsync(
            model: "gemini-3.1-flash-lite-preview", contents: prompt, config: config);

        var text = response.Text?.Trim();

        SearchCarFilters? filters;

        if (string.IsNullOrWhiteSpace(text)) {
            return null;
        }

        try {
            filters = JsonSerializer.Deserialize<SearchCarFilters>(text, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });
        } catch {
            
            return null;
        }

        var cars = await _repository.Search(filters!);
        var final = new ResponseCarsJson {
            Cars = cars
        };

        return final;
    }
}
