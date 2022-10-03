using FamilyHubs.ServiceDirectoryAdminUi.Ui.Models;
using FamilyHubs.ServiceDirectoryAdminUi.Ui.Services;
using FamilyHubs.ServiceDirectoryAdminUi.Ui.Services.Api;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FamilyHubs.ServiceDirectoryAdminUi.Ui.Pages.OrganisationAdmin
{
    public class SignInModel : PageModel
    {
        private readonly ISessionService _session;
        private readonly IAuthService _authenticationService;

        [BindProperty]
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public SignInModel(ISessionService sessionService, IAuthService authenticationService)
        {
            _session = sessionService;
            _authenticationService = authenticationService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var token = await _authenticationService.Login(Email, Password);
            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token.Token);
                var claims = jwtSecurityToken.Claims.ToList();

                //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                var principal = new ClaimsPrincipal(identity);
                //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                {
                    //IsPersistent = objLoginModel.RememberLogin
                });
            }

            OrganisationViewModel organisationViewModel = new()
            {
                Id = new Guid("72e653e8-1d05-4821-84e9-9177571a6013")
            };

            organisationViewModel.Name = "Bristol City Council";

            _session.StoreOrganisationWithService(HttpContext, organisationViewModel);

            return RedirectToPage("/OrganisationAdmin/Welcome");



            //OrganisationViewModel organisationViewModel = new()
            //{
            //    Id = new Guid("72e653e8-1d05-4821-84e9-9177571a6013")
            //};

            //organisationViewModel.Name = "Bristol City Council";

            //var strOrganisationViewModel = JsonConvert.SerializeObject(organisationViewModel);

            //return RedirectToPage("/OrganisationAdmin/Welcome", new
            //{
            //    strOrganisationViewModel = strOrganisationViewModel
            //});

        }
    }
}
