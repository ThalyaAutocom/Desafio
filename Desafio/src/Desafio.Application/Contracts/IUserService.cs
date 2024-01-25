using Desafio.Domain;
using System.Security.Claims;

namespace Desafio.Application;

public interface IUserService
{
    Task<CreateUserResponse> InsertUserAsync(CreateUserRequest registerUserRequest);
    Task<LoginUserResponse> LoginAsync(LoginUserRequest loginUserRequest);
    Task<IEnumerable<UserResponse>> GetAllAsync();
    Task<UserResponse> GetByShortIdAsync(string shortId);
    Task<bool> UpdateAsync(UpdateUserRequest userRequest);
    Task<bool> UpdateAsync(UpdateLoginUserRequest userRequest);
    Task<bool> RemoveAsync(string shortId);
    Task<bool> EmailAlreadyUsed(string email);
    Task<bool> EmailAlreadyUsed(UpdateUserRequest userRequest);
    Task<bool> DocumentAlreadyUsed(string document);
    Task<bool> DocumentAlreadyUsed(UpdateUserRequest userRequest);
    Task<bool> NickNameAlreadyUsed(string nickName);
    Task<bool> NickNameAlreadyUsed(UpdateUserRequest userRequest);
    Task<bool> CorrectPassword(UpdateLoginUserRequest request);
}

