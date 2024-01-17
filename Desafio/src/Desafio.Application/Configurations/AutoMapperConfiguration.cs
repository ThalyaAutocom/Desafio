﻿using AutoMapper;
using Desafio.Application;
using Desafio.Domain;

namespace Desafio.API;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        #region Unit
        CreateMap<Unit, UnitRequest>().ReverseMap();
        CreateMap<Unit, UnitResponse>().ReverseMap();
        #endregion

        #region User
        CreateMap<User, LoginUserRequest>().ReverseMap();
        CreateMap<User, LoginUserResponse>().ReverseMap();
        CreateMap<User, CreateUserRequest>().ReverseMap();
        CreateMap<User, CreateUserResponse>().ReverseMap();
        CreateMap<User, GetUserResponse>().ReverseMap();
        CreateMap<User, UpdateUserRequest>().ReverseMap();
        CreateMap<User, UpdateLoginUserRequest>().ReverseMap();
        #endregion

        #region Product
        CreateMap<Product, InsertProductRequest>().ReverseMap();
        CreateMap<Product, UpdateProductRequest>().ReverseMap();
        CreateMap<Product, ProductResponse>().ReverseMap();
        #endregion

        #region Person
        CreateMap<Person, PersonResponse>().ReverseMap();
        CreateMap<Person, InsertPersonRequest>().ReverseMap();
        CreateMap<Person, UpdatePersonRequest>().ReverseMap();
        #endregion
    }
}
