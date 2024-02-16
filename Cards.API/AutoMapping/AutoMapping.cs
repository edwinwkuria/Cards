﻿using AutoMapper;
using Cards.BindingModels.CardsController;
using Cards.BindingModels.UsersController;
using Cards.Infrastructure.Entities;

namespace Cards.API.AutoMapping;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<CardBindingModel, Card>();
        CreateMap<LoginBindingModel, User>();
    }
    
}