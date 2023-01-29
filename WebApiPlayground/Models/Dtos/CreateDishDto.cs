using System.ComponentModel.DataAnnotations;

namespace WebApiPlayground.Models.Dtos
{
    public class CreateDishDto
    {
        public string Description { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}