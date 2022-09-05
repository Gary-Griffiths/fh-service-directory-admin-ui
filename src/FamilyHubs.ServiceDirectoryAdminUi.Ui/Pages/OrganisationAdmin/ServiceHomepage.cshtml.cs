using FamilyHubs.ServiceDirectoryAdminUi.Ui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FamilyHubs.ServiceDirectoryAdminUi.Ui.Pages.OrganisationAdmin;

public class ServiceHomepageModel : PageModel
{
    [BindProperty]
    public OrganisationViewModel OrganisationViewModel { get; set; } = new OrganisationViewModel();
    public string? StrOrganisationViewModel { get; private set; }

    public void OnGet(string strOrganisationViewModel)
    {
        if (strOrganisationViewModel != null)
            OrganisationViewModel = JsonConvert.DeserializeObject<OrganisationViewModel>(strOrganisationViewModel) ?? new OrganisationViewModel();

        StrOrganisationViewModel = strOrganisationViewModel;
    }
}
