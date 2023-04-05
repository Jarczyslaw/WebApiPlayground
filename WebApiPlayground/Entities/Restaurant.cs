namespace WebApiPlayground.Entities
{
    public class Restaurant
    {
        public virtual Address Address { get; set; }

        public int AddressId { get; set; }

        public string Category { get; set; }

        public string ContactEmail { get; set; }

        public string ContactNumber { get; set; }

        public virtual User CreatedUser { get; set; }

        public int? CreatedUserId { get; set; }

        public string Description { get; set; }

        public virtual List<Dish> Dishes { get; set; }

        public bool HasDelivery { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}