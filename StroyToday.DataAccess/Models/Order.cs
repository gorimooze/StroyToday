namespace StroyToday.DataAccess.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
