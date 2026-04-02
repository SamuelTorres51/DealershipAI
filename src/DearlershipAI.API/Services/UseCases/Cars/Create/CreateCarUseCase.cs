using AutoMapper;
using DearlershipAI.API.Models.DTOs.Requests;
using DearlershipAI.API.Models.Entities;
using DearlershipAI.API.Models.Repositories;
using DearlershipAI.API.Models.Repositories.Cars;

namespace DearlershipAI.API.Services.UseCases.Cars.Create;

public class CreateCarUseCase {
    private readonly ICarWriteOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnityOfWork _unityOfWork;

    public CreateCarUseCase(ICarWriteOnlyRepository repository, IMapper mapper, IUnityOfWork unityOfWork) {
        _repository = repository;
        _mapper = mapper;
        _unityOfWork = unityOfWork;
    }

    public async Task<Car> Execute(RequestCarJson request) {
        Validate(request);
        var car = _mapper.Map<Car>(request);
        await _repository.Add(car);
        await _unityOfWork.Commit();
        return car;
    }

    private void Validate(RequestCarJson request) {
        var validator = new CarValidator();
        var validationResult = validator.Validate(request);

        if (validationResult.IsValid == false) {
            var errorsMessage = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new Exception();
        }
    }
}
