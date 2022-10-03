namespace FamilyHubs.ServiceDirectoryAdminUi.Ui.Models;

public class BearerToken
{
    public string Token { get; set; } = default!;
    public DateTime Expiration { get; set; } = default!;
}
