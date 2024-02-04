using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace resturant.Pages
{
    public class About_UsModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public About_UsModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
