using StroyToday.Core.Helpers;

namespace StroyToday.Core.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedDateDisplay => CreatedOn.DisplayOnlyDate();
        public string CreatedTimeDisplay => CreatedOn.DisplayTimeOnly();
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
