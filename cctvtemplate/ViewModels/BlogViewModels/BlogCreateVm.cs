using cctvtemplate.Models;
using System.ComponentModel.DataAnnotations;

namespace cctvtemplate.ViewModels.BlogViewModels
{
    public class BlogCreateVm
    {
        [Required]
        [MaxLength(256)]
        [MinLength(3)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public DateTime PostedDate { get; set; }
        public int TagId { get; set; }
    }
}
