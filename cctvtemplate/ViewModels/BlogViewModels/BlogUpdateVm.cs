using System.ComponentModel.DataAnnotations;

namespace cctvtemplate.ViewModels.BlogViewModels
{
    public class BlogUpdateVm
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        [MinLength(3)]
        public string Description { get; set; } = string.Empty;
        
        public IFormFile? Image { get; set; }
        [Required]
        public DateTime PostedDate { get; set; }
        public int TagId { get; set; }
    }
}
