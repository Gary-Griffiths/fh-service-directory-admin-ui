﻿using System;

namespace FamilyHubs.ServiceDirectoryAdminUi.Ui.Models.Configuration;

public interface IHeaderConfiguration
{
    string ManageApprenticeshipsBaseUrl { get; set; }
    string EmployerCommitmentsBaseUrl { get; set; }
    string EmployerCommitmentsV2BaseUrl { get; set; }
    string EmployerFinanceBaseUrl { get; set; }
    string AuthenticationAuthorityUrl { get; set; }
    string EmployerRecruitBaseUrl { get; set; }
    Uri ChangeEmailReturnUrl { get; set; }
    Uri ChangePasswordReturnUrl { get; set; }
    Uri SignOutUrl { get; set; }
    string ClientId { get; set; }
}
