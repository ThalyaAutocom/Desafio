using AutoMapper;
using Desafio.Application;
using Desafio.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;

namespace Desafio.Identity;

public class UserService : ServiceBase, IUserService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly JwtOptions _jwtOptions;
    private readonly IMapper _mapper;

    public UserService(SignInManager<User> signInManager,
                           UserManager<User> userManager,
                           IOptions<JwtOptions> jwtOptions,
                           IMapper mapper,
                           IError error) : base(error)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
        _mapper = mapper;
    }
    #region Controller Methods
    public async Task<LoginUserResponse> LoginAsync(LoginUserRequest loginUserRequest)
    {
        var result = await _signInManager.PasswordSignInAsync(loginUserRequest.Email, loginUserRequest.Password, false, true);
        if (result.Succeeded) return await GenerateToken(loginUserRequest.Email);

        throw new ValidationException("Incorrect e-mail ou password");
    }

    public async Task<CreateUserResponse> InsertUserAsync(CreateUserRequest registerUserRequest)
    {
        var identityUser = _mapper.Map<User>(registerUserRequest);

        identityUser.EmailConfirmed = true;
        var result = await _userManager.CreateAsync(identityUser, registerUserRequest.Password);

        if (!result.Succeeded)
        {
            string errorMessage = string.Empty;
            foreach (var error in result.Errors)
            {
                errorMessage += $"{error.Description}\r\n";
            }
            throw new ValidationException(errorMessage);
        }

        //desbloquear usuário já que não terá e-mail de confirmação
        await _userManager.SetLockoutEnabledAsync(identityUser, false);

        string roleDescription = registerUserRequest.UserLevel.ToString();
        await _userManager.AddToRoleAsync(identityUser, roleDescription);

        CreateUserResponse userRegisterResponse = _mapper.Map<CreateUserResponse>(identityUser);

        return userRegisterResponse;
    }

    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        var result = await _userManager.Users.ToListAsync();
        var response = _mapper.Map<List<UserResponse>>(result);
        return response;
    }

    public async Task<UserResponse> GetByShortIdAsync(string shortId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.ShortId == shortId);

        if (user == null)
        {
            throw new Exception("No user was found.");
        }

        return _mapper.Map<UserResponse>(user);
    }

    public async Task<UserResponse> GetByIdAsync(string id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            throw new Exception("No user was found.");
        }

        return _mapper.Map<UserResponse>(user);
    }

    public async Task<bool> UpdateAsync(UpdateUserRequest userRequest)
    {
        var existingUser = await _userManager.FindByEmailAsync(userRequest.Email);

        if (existingUser == null)
        {
            throw new Exception("User was not found");
        }

        var existingRole = await _userManager.GetRolesAsync(existingUser);

        _mapper.Map(userRequest, existingUser);

        await _userManager.UpdateAsync(existingUser);

        string newRole = userRequest.UserLevel.ToString();
        await _userManager.RemoveFromRoleAsync(existingUser, existingRole.FirstOrDefault());
        await _userManager.AddToRoleAsync(existingUser, newRole);

        var userResponse = _mapper.Map<GetUserResponse>(existingUser);

        // userResponse.Roles.Add(newRole);

        return true;

    }

    public async Task<bool> UpdateAsync(UpdateLoginUserRequest userRequest)
    {
        var existingUser = await _userManager.FindByEmailAsync(userRequest.Email);

        if (existingUser == null)
        {
            throw new Exception("The user was not found.");
        }

        var result = await _userManager.ChangePasswordAsync(existingUser, userRequest.CurrentPassword, userRequest.NewPassword);
        if (!result.Succeeded) return false;

        return true;
    }

    public async Task<bool> RemoveAsync(string email)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser == null)
        {
            throw new Exception("Email was not found.");
        }
        await _userManager.DeleteAsync(existingUser);

        return true;
    }
    #endregion

    #region Helper Methods 
    private async Task<LoginUserResponse> GenerateToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); //jwt ID
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString())); //criação
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)); //emissão

        foreach (var role in roles)
            claims.Add(new Claim("role", role));

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
        var expiration = DateTime.UtcNow.AddHours(_jwtOptions.ExpirationHour);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _jwtOptions.Sender,
            Audience = _jwtOptions.ValidIn,
            Subject = identityClaims,
            Expires = expiration,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encodedToken = tokenHandler.WriteToken(token);

        return new LoginUserResponse
        {
            Token = encodedToken,
            Expiration = TimeSpan.FromHours(_jwtOptions.ExpirationHour).TotalMinutes,
        };
    }

    //Converter corretamente os segundos da data
    private static long ToUnixEpochDate(DateTime date)
    {
        //Retorna os segundos da data passada
        return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
    #endregion

    #region Validations Methods
    public async Task<bool> EmailAlreadyUsed(string email)
    {
        return await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email) != null;
    }

    public async Task<bool> DocumentAlreadyUsed(string document)
    {
        return await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Document == document) != null;
    }

    public async Task<bool> NickNameAlreadyUsed(string nickName)
    {
        return await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Document == nickName) != null;
    }

    public async Task<bool> CorrectPassword(UpdateLoginUserRequest request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser == null)
        {
            throw new Exception("The user was not found.");
        }

        var result = await _signInManager.PasswordSignInAsync(request.Email, request.CurrentPassword, false, true);
        return result.Succeeded;
    }

    public async Task<bool> DocumentAlreadyUsed(UpdateUserRequest userRequest)
    {
        return await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Document == userRequest.Document && x.Id != userRequest.Id) != null;
    }

    public async Task<bool> NickNameAlreadyUsed(UpdateUserRequest userRequest)
    {
        return await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Document == userRequest.NickName && x.Id != userRequest.Id) != null;
    }
    #endregion
}
