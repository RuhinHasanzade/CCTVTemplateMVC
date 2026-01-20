using cctvtemplate.Models;

namespace cctvtemplate.ViewModels.BlogViewModels
{
    public class BlogGetVm
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime PostedDate { get; set; }
        public string TagName { get; set; }
    }
}
