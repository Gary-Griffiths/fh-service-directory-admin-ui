using FamilyHubs.ServiceDirectory.Shared.Models.Api.OpenReferralServices;
using FamilyHubs.ServiceDirectoryAdminUi.Ui.Models;
using FamilyHubs.ServiceDirectoryAdminUi.Ui.Services;
using FamilyHubs.ServiceDirectoryAdminUi.Ui.Services.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FamilyHubs.ServiceDirectoryAdminUi.Ui.Pages.OrganisationAdmin;

//[Authorize(Policy = "ApiScope")]
[Authorize]
public class WelcomeModel : PageModel
{
    [BindProperty]
    public OrganisationViewModel OrganisationViewModel { get; set; } = new OrganisationViewModel();
    //public string? StrOrganisationViewModel { get; private set; }

    private readonly ILocalOfferClientService _localOfferClientService;
    private readonly ISessionService _session;

    public List<OpenReferralServiceDto> Services { get; private set; } = default!;

    public WelcomeModel(ILocalOfferClientService localOfferClientService, ISessionService sessionService)
    {
        _localOfferClientService = localOfferClientService;
        _session = sessionService;
    }

    public async Task OnGet(string strOrganisationViewModel)
    {
        /*** Using Session storage as a service ***/
        
        OrganisationViewModel = _session.RetrieveService(HttpContext) ?? new OrganisationViewModel();
        if (OrganisationViewModel != null && OrganisationViewModel.Id == Guid.Empty)
        {
            OrganisationViewModel organisationViewModel = new()
            {
                Id = new Guid("72e653e8-1d05-4821-84e9-9177571a6013")
            };

            organisationViewModel.Name = "Bristol City Council";

            _session.StoreService(HttpContext, organisationViewModel);

            OrganisationViewModel = organisationViewModel;
        }

        if (OrganisationViewModel != null && OrganisationViewModel.Id != Guid.Empty)
            Services = await _localOfferClientService.GetServicesByOrganisationId(OrganisationViewModel.Id.ToString());
        else
            Services = new List<OpenReferralServiceDto>();



        //if (strOrganisationViewModel != null)
        //    OrganisationViewModel = JsonConvert.DeserializeObject<OrganisationViewModel>(strOrganisationViewModel) ?? new OrganisationViewModel();

        //StrOrganisationViewModel = strOrganisationViewModel;

        //if (OrganisationViewModel != null)
        //    Services = await _localOfferClientService.GetServicesByOrganisationId(OrganisationViewModel.Id.ToString());
        //else
        //    Services = new List<OpenReferralServiceDto>();
    }
}
