using AutoMapper;
using Desafio.Application;
using Desafio.Domain;

namespace Desafio.API;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        #region Unit
        CreateMap<Unit, CreateUnitResponse>().ReverseMap();
        CreateMap<Unit, UnitResponse>().ReverseMap();
        CreateMap<Unit, CreateUnitRequest>().ReverseMap();
        CreateMap<Unit, UpdateUnitRequest>().ReverseMap();
        #endregion

        #region User
        CreateMap<User, UpdateUserRequest>().ReverseMap();
        CreateMap<User, CreateUserRequest>().ReverseMap();
        CreateMap<User, CreateUserResponse>().ReverseMap();
        CreateMap<User, UserResponse>().ReverseMap();
        CreateMap<User, GetAllUserResponse>().ReverseMap();

        #endregion

        #region Product
        CreateMap<Product, CreateProductRequest>().ReverseMap();
        CreateMap<Product, CreateProductResponse>().ReverseMap();
        CreateMap<Product, ProductResponse>().ReverseMap();
        CreateMap<Product, UpdateProductRequest>().ReverseMap();
        #endregion

        #region Person
        CreateMap<Person, PersonResponse>().ReverseMap();
        CreateMap<Person, CreatePersonRequest>().ReverseMap();
        CreateMap<Person, CreatePersonResponse>().ReverseMap();
        CreateMap<Person, UpdatePersonRequest>().ReverseMap();
        #endregion
    }
}
