using System.ComponentModel.DataAnnotations;

namespace Q1.BO.DepencencyInjection.Options;

public class JwtOptions
{
    [Required]public string Issuer { get; set; }
    [Required]public string Audience { get; set; }
    [Required]public string SecretKey { get; set; }
    [Required]public int ExpireMinutes { get; set; }
}