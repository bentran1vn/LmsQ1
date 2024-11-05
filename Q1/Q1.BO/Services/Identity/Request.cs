using System.ComponentModel.DataAnnotations;

namespace Q1.BO.Services.Identity;

public static class Request
{
    public record Login([Required]string Email, [Required]string Password);
}