using AutoMapper;
using Cards.BindingModels.CardsController;
using Cards.BindingModels.UsersController;
using Cards.Infrastructure.Entities;
using Cards.Services.DTOModels;

namespace Cards.API.AutoMapping;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<CardBindingModel, Card>();
        CreateMap<LoginBindingModel, User>();
        
        CreateMap<Card, CardDTO>();
        CreateMap<User, UserDTO>();
    }
    
}