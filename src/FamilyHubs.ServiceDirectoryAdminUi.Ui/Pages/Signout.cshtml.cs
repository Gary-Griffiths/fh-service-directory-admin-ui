using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectoryAdminUi.Ui.Pages
{
    public class SignoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            foreach (var cookie in HttpContext.Request.Cookies)
            {
                System.Diagnostics.Debug.WriteLine(cookie.Key);
                if (cookie.Key == ".AspNetCore.Identity.Application")
                    Response.Cookies.Delete(cookie.Key);
            }

            return SignOut("Cookies", "oidc");
        }
    }
}
