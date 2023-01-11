﻿namespace WebApiPlayground.Entities
{
    public class Address
    {
        public string City { get; set; }

        public int Id { get; set; }

        public string PostalCode { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public int RestaurantId { get; set; }

        public string Street { get; set; }
    }
}