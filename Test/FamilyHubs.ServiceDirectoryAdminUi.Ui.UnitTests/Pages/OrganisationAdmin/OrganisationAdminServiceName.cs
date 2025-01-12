using FamilyHubs.ServiceDirectoryAdminUi.Ui.Pages.OrganisationAdmin;
using FamilyHubs.ServiceDirectoryAdminUi.Ui.Services.Api;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FamilyHubs.ServiceDirectoryAdminUi.Ui.UnitTests.Pages.OrganisationAdmin
{
    public class OrganisationAdminServiceName
    {
        private ServiceNameModel serviceName;

        public OrganisationAdminServiceName()
        {
            var mock = new Mock<IOpenReferralOrganisationAdminClientService>();
            serviceName = new ServiceNameModel(mock.Object);
        }


        [Fact]
        public void NullServiceName()
        {
            // Act
            var result = serviceName.OnPost() as RedirectToPageResult;

            // Assert
            serviceName.ValidationValid.Should().BeFalse();
        }

        [Fact]
        public void EmptyServiceName()
        {
            // Arrange
            serviceName.ServiceName = "";

            // Act
            var result = serviceName.OnPost() as RedirectToPageResult;

            // Assert
            serviceName.ValidationValid.Should().BeFalse();
        }

        [Fact]
        public void MoreThan255CharServiceName()
        {
            // Arrange
            serviceName.ServiceName = "ABCSDFGHJKLMNOPQRSTUVWXYZ ABCSDFGHJKLMNOPQRSTUVWXYZ ABCSDFGHJKLMNOPQRSTUVWXYZ ABCSDFGHJKLMNOPQRSTUVWXYZ" +
                "ABCSDFGHJKLMNOPQRSTUVWXYZ ABCSDFGHJKLMNOPQRSTUVWXYZ ABCSDFGHJKLMNOPQRSTUVWXYZ ABCSDFGHJKLMNOPQRSTUVWXYZ ABCSDFGHJKLMNOPQRSTUVWXYZ" +
                "ABCSDFGHJKLMNOPQRSTUVWXYZ ABCSDFGHJKLMNOPQRSTUVWXYZ";

            // Act
            var result = serviceName.OnPost() as RedirectToPageResult;

            // Assert
            serviceName.ValidationValid.Should().BeFalse();
        }

        [Fact]
        public void ValidServiceName()
        {
            // Arrange
            serviceName.ServiceName = "ASDFGHJKLMNOPQRSTUVWXYZ";

            // Act
            var result = serviceName.OnPost() as ActionResult;

            // Assert
            serviceName.ValidationValid.Should().BeTrue();
        }
    }
}
