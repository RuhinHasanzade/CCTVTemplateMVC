using System.ComponentModel.DataAnnotations;

namespace cctvtemplate.ViewModels.AuthViewModels
{
    public class AdminVm
    {
 
        public string FullName { get; set; } = string.Empty;

  
        public string Username { get; set; } = string.Empty;

        
        public string Email { get; set; } = string.Empty;

    
        public string Password { get; set; } = string.Empty;
    }
}
