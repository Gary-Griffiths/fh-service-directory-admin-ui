using FamilyHubs.ServiceDirectoryAdminUi.Ui.Pages.OrganisationAdmin;
using FamilyHubs.ServiceDirectoryAdminUi.Ui.Services.Api;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace FamilyHubs.ServiceDirectoryAdminUi.Ui.UnitTests.Pages.OrganisationAdmin
{
    public class OrganisationAdminInPersonWhere
    {
        private InPersonWhereModel inPersonWhereModel;

        public OrganisationAdminInPersonWhere()
        {
            var mock = new Mock<IPostcodeLocationClientService>();
            inPersonWhereModel = new InPersonWhereModel(mock.Object);
        }

        [Fact]
        public void AddressEmpty()
        {
            // Arrange
            inPersonWhereModel.Address_1 = "";
            inPersonWhereModel.City = "London";
            inPersonWhereModel.Postal_code = "TW3 2DL";

            //Act
            var result = inPersonWhereModel.OnPost() as Task<IActionResult>;

            // Assert
            inPersonWhereModel.Address1Valid.Should().BeFalse();
         }

        [Fact]
        public void CityEmpty()
        {
            // Arrange
            inPersonWhereModel.Address_1 = "ABCD";
            inPersonWhereModel.City = "";
            inPersonWhereModel.Postal_code = "TW3 2DL";

            // Act
            var result = inPersonWhereModel.OnPost() as Task<IActionResult>;

            // Assert
            inPersonWhereModel.TownCityValid.Should().BeFalse();
        }

        [Fact]
        public void PostcodeEmpty()
        {
            // Arrange
            inPersonWhereModel.Address_1 = "ABCD";
            inPersonWhereModel.City = "London";
            inPersonWhereModel.Postal_code = "";
            
            //Act
            var result = inPersonWhereModel.OnPost() as Task<IActionResult>;

            // Assert
            inPersonWhereModel.PostcodeValid.Should().BeFalse();
        }

       

        [Fact]
        public void Addressvalid()
        {
            // Arrange
            inPersonWhereModel.Address_1 = "ABCD";
            inPersonWhereModel.City = "London";
            inPersonWhereModel.Postal_code = "TW3 2DL";

            // Act
            var result = inPersonWhereModel.OnPost() as Task<IActionResult>;

            // Assert
            inPersonWhereModel.PostcodeAPIValid.Should().BeTrue();
        }
    }
}
