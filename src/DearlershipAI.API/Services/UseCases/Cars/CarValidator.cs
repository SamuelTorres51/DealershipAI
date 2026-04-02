using DearlershipAI.API.Models.DTOs.Requests;
using FluentValidation;

namespace DearlershipAI.API.Services.UseCases.Cars;

public class CarValidator : AbstractValidator<RequestCarJson>{
    public CarValidator() {
        RuleFor(car => car.Brand).NotEmpty().WithMessage("Brand is required.").
            MaximumLength(80).WithMessage("Brand must be at most 80 characters long.");
        RuleFor(car => car.Model).NotEmpty().WithMessage("Model is required.").
            MaximumLength(120).WithMessage("Model must be at most 120 characters long.");
        RuleFor(car => car.Version).MaximumLength(120).WithMessage("Version must be at most 120 characters long.");
        RuleFor(car => car.Year).NotEmpty().WithMessage("Year is required.").
            InclusiveBetween(1886, DateTime.Now.Year).WithMessage($"Year must be between 1886 and {DateTime.Now.Year}.");
        RuleFor(car => car.Price).NotEmpty().WithMessage("Price is required.").
            GreaterThan(0).WithMessage("Price must be greater than 0.");
        RuleFor(car => car.Mileage).GreaterThanOrEqualTo(0).WithMessage("Mileage must be greater than or equal to 0.");
        RuleFor(car => car.Fuel).MaximumLength(30).WithMessage("Fuel must be at most 30 characters long.");
        RuleFor(car => car.Transmissao).MaximumLength(30).WithMessage("Transmission must be at most 30 characters long.");
        RuleFor(car => car.Image_url).MaximumLength(2048).WithMessage("Image URL must be at most 2048 characters long.");

    }
}
