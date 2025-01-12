using FamilyHubs.ServiceDirectoryAdminUi.Ui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FamilyHubs.ServiceDirectoryAdminUi.Ui.Pages.OrganisationAdmin;

public class ContactDetailsModel : PageModel
{
    [BindProperty]
    public List<string> ContactSelection { get; set; } = default!;
    [BindProperty]
    public string Email { get; set; } = default!;
    [BindProperty]
    public string Telephone { get; set; } = default!;
    [BindProperty]
    public string Website { get; set; } = default!;

    [BindProperty]
    public string? StrOrganisationViewModel { get; set; }

    public void OnGet(string strOrganisationViewModel)
    {
        StrOrganisationViewModel = strOrganisationViewModel;

        var organisationViewModel = JsonConvert.DeserializeObject<OrganisationViewModel>(StrOrganisationViewModel);
        if (organisationViewModel != null)
        {
            if (!string.IsNullOrWhiteSpace(organisationViewModel.Email))
                Email = organisationViewModel.Email;
            if (!string.IsNullOrWhiteSpace(organisationViewModel.Telephone))
                Telephone = organisationViewModel.Telephone;
            if (!string.IsNullOrWhiteSpace(organisationViewModel.Website))
                Website = organisationViewModel.Website;

            if (organisationViewModel.ContactSelection != null && organisationViewModel.ContactSelection.Any())
            {
                ContactSelection = organisationViewModel.ContactSelection;
            }
        }
    }

    public IActionResult OnPost()
    {
        //if (!ModelState.IsValid)
        //{
        //    return Page();
        //}

        if (!string.IsNullOrEmpty(StrOrganisationViewModel))
        {
            var organisationViewModel = JsonConvert.DeserializeObject<OrganisationViewModel>(StrOrganisationViewModel) ?? new OrganisationViewModel();
            organisationViewModel.Email = Email;
            organisationViewModel.Telephone = Telephone;
            organisationViewModel.Website = Website;
            organisationViewModel.ContactSelection = ContactSelection;

            StrOrganisationViewModel = JsonConvert.SerializeObject(organisationViewModel);
        }

        return RedirectToPage("/OrganisationAdmin/ServiceDescription", new
        {
            strOrganisationViewModel = StrOrganisationViewModel
        });
    }
}
