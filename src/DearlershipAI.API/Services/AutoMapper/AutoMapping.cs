using AutoMapper;
using DearlershipAI.API.Models.DTOs.Requests;
using DearlershipAI.API.Models.Entities;

namespace DearlershipAI.API.Services.AutoMapper;

public class AutoMapping : Profile{
    public AutoMapping() {
        RequestToEntity();
    }

    private void RequestToEntity() {
        CreateMap<RequestCarJson, Car>();
    }

    
}
