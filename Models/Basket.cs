namespace AspaApp.Models
{
    public class Basket

    {
        public int BasketId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

    }
}
