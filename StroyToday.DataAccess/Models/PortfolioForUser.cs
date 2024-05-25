namespace StroyToday.DataAccess.Models
{
    public class PortfolioForUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ImageName { get; set; }

        public User User { get; set; }
    }
}
