using cctvtemplate.Models.Common;

namespace cctvtemplate.Models
{
    public class Blog : BaseEntity
    {
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public DateTime PostedDate { get; set; }
        public int TagId { get; set; }
        public Tag? Tag { get; set; }
    }
}
