using System.Security.Claims;
using Q1.BO.Abstract;
using Q1.DAO.Abstract;
using Q1.DAO.Models;

namespace Q1.BO.Services.Identity;

public class IdentityService : IIdentityServices
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRepositoryBase<BranchAccount> _branchAccountRepository;

    public IdentityService(IJwtTokenService jwtTokenService, IRepositoryBase<BranchAccount> branchAccountRepository)
    {
        _jwtTokenService = jwtTokenService;
        _branchAccountRepository = branchAccountRepository;
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _branchAccountRepository.FindSingleAsync(x => x.EmailAddress.Equals(email));

        if (user is null)
        {
            throw new Exception("Null");
        }

        if (!user.AccountPassword.Equals(password))
        {
            throw new Exception("Forbidden");
        }
        
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.EmailAddress ?? "default"),
            new Claim(ClaimTypes.Role, user.Role.ToString()!),
            new Claim("Role", user.Role.ToString()!),
            new Claim("UserId", user.AccountId.ToString()!),
            new Claim(ClaimTypes.Name, user.EmailAddress ?? "default"),
            new Claim(ClaimTypes.Expired, TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow.AddMinutes(120), vietnamTimeZone).ToString())
        };
        
        var accessToken = _jwtTokenService.GenerateAccessToken(claims);

        return accessToken;
    }
}