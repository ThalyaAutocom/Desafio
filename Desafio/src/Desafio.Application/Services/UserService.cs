using AutoMapper;
using Desafio.Application;
using Desafio.Domain;
using Desafio.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Desafio.Identity;

public class UserService : IUserService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly JwtOptions _jwtOptions;
    private readonly IMapper _mapper;

    public UserService(SignInManager<User> signInManager,
                           UserManager<User> userManager,
                           IOptions<JwtOptions> jwtOptions,
                           IMapper mapper)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
        _mapper = mapper;
    }
    #region Controller Methods
    public async Task<LoginUserResponse> LoginAsync(LoginUserRequest loginUserRequest)
    {
        if (loginUserRequest == null) throw new CustomException("The request was not provided.");

        var email = _userManager.Users.FirstOrDefaultAsync(x => x.NickName == loginUserRequest.NickName).Result?.Email;
        if(email == null) throw new CustomException("The User was not found.", statusCode: System.Net.HttpStatusCode.NotFound);

        var result = await _signInManager.PasswordSignInAsync(email, loginUserRequest.Password, false, true);
        if (result.Succeeded) return await GenerateToken(email);

        throw new CustomException("Incorrect e-mail or password", statusCode: System.Net.HttpStatusCode.Unauthorized);
    }

    public async Task<CreateUserResponse> InsertUserAsync(CreateUserRequest registerUserRequest)
    {
        if (registerUserRequest == null) throw new CustomException("The request was not provided.");

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
            throw new CustomException(errorMessage);
        }

        //desbloquear usuário já que não terá e-mail de confirmação
        await _userManager.SetLockoutEnabledAsync(identityUser, false);

        string roleDescription = registerUserRequest.UserLevel.ToString();
        await _userManager.AddToRoleAsync(identityUser, roleDescription);

        CreateUserResponse userRegisterResponse = _mapper.Map<CreateUserResponse>(identityUser);

        return userRegisterResponse;
    }

    public async Task<IEnumerable<GetAllUserResponse>> GetAllAsync()
    {
        var result = await _userManager.Users.ToListAsync();
        
        if (result == null || result.Count() == 0)
        {
            throw new CustomException("No users were found.");
        }

        var response = _mapper.Map<List<GetAllUserResponse>>(result);
        return response;
    }

    public async Task<UserResponse> GetByShortIdAsync(string shortId)
    {
        if (shortId == null) throw new CustomException("The short id was not provided.");

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.ShortId == shortId);

        if (user == null)
        {
            throw new CustomException("No user was found.");
        }

        return _mapper.Map<UserResponse>(user);
    }

    public async Task<bool> UpdateAsync(UpdateUserRequest userRequest)
    {
        if (userRequest == null) throw new CustomException("The request was not provided.");

        var existingUser = await _userManager.FindByEmailAsync(userRequest.Email);

        if (existingUser == null)
        {
            throw new CustomException("User was not found");
        }

        if (existingUser.Email == "admin@admin.com")
        {
            throw new CustomException("Initial e-mail cannot be updated.");
        }

        var existingRole = await _userManager.GetRolesAsync(existingUser);

        _mapper.Map(userRequest, existingUser);

        await _userManager.UpdateAsync(existingUser);

        string newRole = userRequest.UserLevel.ToString();
        await _userManager.RemoveFromRoleAsync(existingUser, existingRole.FirstOrDefault());
        await _userManager.AddToRoleAsync(existingUser, newRole);

        return true;

    }

    public async Task<bool> UpdateAsync(UpdateLoginUserRequest userRequest)
    {
        if (userRequest == null) throw new CustomException("The request was not provided.");

        var existingUser = _userManager.Users.FirstOrDefaultAsync(x => x.NickName == userRequest.NickName).Result;

        if (existingUser == null)
        {
            throw new CustomException("The user was not found.");
        }

        if (existingUser.Email == "admin@admin.com")
        {
            throw new CustomException("Initial e-mail cannot be updated.");
        }

        var result = await _userManager.ChangePasswordAsync(existingUser, userRequest.CurrentPassword, userRequest.NewPassword);
        if (!result.Succeeded) return false;

        return true;
    }

    public async Task<bool> RemoveAsync(string shortId)
    {
        if (shortId == null) throw new CustomException("The short id was not provided.");

        var user = _userManager.Users.FirstOrDefault(x => x.ShortId == shortId);

        if (user == null)
        {
            throw new CustomException("Email was not found.");
        }

        if (user.Email == "admin@admin.com")
        {
            throw new CustomException("Initial e-mail cannot be removed.");
        }

        await _userManager.DeleteAsync(user);

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
            Expiration = $"{TimeSpan.FromHours(_jwtOptions.ExpirationHour).TotalMilliseconds}",
            Name = user.Name
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

    public async Task<bool> EmailAlreadyUsed(UpdateUserRequest userRequest)
    {
        return await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == userRequest.Email && x.Id != userRequest.Id) != null;
    }

    public async Task<bool> DocumentAlreadyUsed(string document)
    {
        return await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Document == document) != null;
    }

    public async Task<bool> DocumentAlreadyUsed(UpdateUserRequest userRequest)
    {
        return await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Document == userRequest.Document && x.Id != userRequest.Id) != null;
    }

    public async Task<bool> NickNameAlreadyUsed(string nickName)
    {
        return await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.NickName == nickName) != null;
    }

    public async Task<bool> NickNameAlreadyUsed(UpdateUserRequest userRequest)
    {
        return await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.NickName == userRequest.NickName && x.Id != userRequest.Id) != null;
    }

    public async Task<bool> CorrectPassword(UpdateLoginUserRequest request)
    {
        var existingUser = _userManager.Users.FirstOrDefaultAsync(x => x.NickName == request.NickName).Result;

        if (existingUser == null)
        {
            throw new CustomException("The user was not found.");
        }

        var result = await _signInManager.PasswordSignInAsync(existingUser.Email, request.CurrentPassword, false, true);
        return result.Succeeded;
    }
    #endregion
}
