using cctvtemplate.Models.Common;

namespace cctvtemplate.Models
{
    public class Tag:BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Blog> Blogs { get; set; } = [];
    }
}
