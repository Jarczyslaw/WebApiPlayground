﻿using System.ComponentModel.DataAnnotations;

namespace WebApiPlayground.Models.Dtos
{
    public class CreateRestaurantDto
    {
        public string Category { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        public string ContactEmail { get; set; }

        public string ContactNumber { get; set; }

        public string Description { get; set; }

        public bool HasDelivery { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        public string PostalCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
    }
}