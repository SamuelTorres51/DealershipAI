namespace DearlershipAI.API.Models.Entities;

public class Car {
    public long Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal Price { get; set; }
    public string Fuel { get; set; } = string.Empty;
    public string Transmissao { get; set; } = string.Empty;
    public int Mileage { get; set; }
    public string? Image_url { get; set; } = string.Empty;

}
