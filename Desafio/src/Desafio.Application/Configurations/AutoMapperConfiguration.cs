﻿using AutoMapper;
using Desafio.Application;
using Desafio.Domain;

namespace Desafio.API;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        #region Unit
        CreateMap<Unit, CreateUnitRequest>().ReverseMap();
        CreateMap<Unit, CreateUnitResponse>().ReverseMap();
        CreateMap<Unit, UnitResponse>().ReverseMap();
        CreateMap<Unit, UpdateUnitRequest>().ReverseMap();
        #endregion

        #region User
        CreateMap<User, LoginUserRequest>().ReverseMap();
        CreateMap<User, LoginUserResponse>().ReverseMap();
        CreateMap<User, CreateUserRequest>().ReverseMap();
        CreateMap<User, CreateUserResponse>().ReverseMap();
        CreateMap<User, GetUserResponse>().ReverseMap();
        CreateMap<User, GetUserRequest>().ReverseMap();
        CreateMap<User, UserResponse>().ReverseMap();
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
