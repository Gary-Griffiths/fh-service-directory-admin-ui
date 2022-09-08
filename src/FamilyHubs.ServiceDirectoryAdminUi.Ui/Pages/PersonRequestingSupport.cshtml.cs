using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectoryAdminUi.Ui.Pages;

public class PersonRequestingSupportModel : PageModel
{
    [BindProperty]
    public string IsTypeOfPerson { get; set; } = default!;

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (string.Compare(IsTypeOfPerson, "professional", StringComparison.OrdinalIgnoreCase) == 0)
        {
            return RedirectToPage("SignIn", new
            {

            });
        }

        return RedirectToPage("Search", new
        {
        });

        //return RedirectToPage("FindServiceFromPostcode", new
        //{
        //});

    }
}
