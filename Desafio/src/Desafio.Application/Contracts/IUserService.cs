using Desafio.Domain;
using System.Security.Claims;

namespace Desafio.Application;

public interface IUserService
{
    Task<CreateUserResponse> InsertUserAsync(CreateUserRequest registerUserRequest);
    Task<LoginUserResponse> LoginAsync(LoginUserRequest loginUserRequest);
    Task<IEnumerable<User>> GetAllAsync();
    Task<IEnumerable<GetUserResponse>> GetAllUsersByRoleAsync(string role);
    Task<GetUserResponse> GetByShortIdAsync(string shortId);
    Task<GetUserResponse> UpdateAsync(UpdateUserRequest userRequest);
    Task<GetUserResponse> UpdateAsync(UpdateLoginUserRequest userRequest);
    Task<GetUserResponse> RemoveAsync(string email);
    Task<bool> EmailAlreadyUsed(string email);
    Task<bool> DocumentAlreadyUsed(string document);
    Task<bool> NickNameAlreadyUsed(string nickName);
}

