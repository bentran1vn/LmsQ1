using System.ComponentModel.DataAnnotations;

namespace Q1.BO.Services.SilverJewelry;

public static class Request
{
    public record Create()
    {
        [Required]
        public string SilverJewelryId { get; set; } = null!;

        [Required]
        [RegularExpression(@"^([A-Z][a-z0-9]*\s?)+$", ErrorMessage = "Each word in SilverJewelryName must start with a capital letter and may contain letters, spaces, and digits.")]
        public string SilverJewelryName { get; set; } = null!;

        public string? SilverJewelryDescription { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0.")]
        public decimal Price { get; set; }

        public decimal? MetalWeight { get; set; }

        [Required]
        [Range(1900, int.MaxValue, ErrorMessage = "ProductionYear must be greater than or equal to 1900.")]
        public int ProductionYear { get; set; }

        [Required]
        public string CategoryId { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}