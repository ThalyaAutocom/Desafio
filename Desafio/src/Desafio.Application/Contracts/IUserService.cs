using Desafio.Domain;
using System.Security.Claims;

namespace Desafio.Application;

public interface IUserService
{
    Task<CreateUserResponse> InsertUserAsync(CreateUserRequest registerUserRequest);
    Task<LoginUserResponse> LoginAsync(LoginUserRequest loginUserRequest);
    Task<IEnumerable<UserResponse>> GetAllAsync();
    Task<UserResponse> GetByShortIdAsync(string shortId);
    Task<UserResponse> GetByIdAsync(string id);
    Task<bool> UpdateAsync(UpdateUserRequest userRequest);
    Task<bool> UpdateAsync(UpdateLoginUserRequest userRequest);
    Task<bool> RemoveAsync(string email);
    Task<bool> EmailAlreadyUsed(string email);
    Task<bool> DocumentAlreadyUsed(string document);
    Task<bool> NickNameAlreadyUsed(string nickName);
}

