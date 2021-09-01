using Core.Entities;
using Core.Enums;
using System;
using Xunit;

namespace UnitTests
{
    public class SertiveRequestTest
    {
        [Fact]
        public void NewServiceRequest()
        {
            //Arrange
            var sRequest = new SRequest();
            sRequest.buildingCode = "123156161";
            sRequest.createdDate = DateTime.Now;
            sRequest.currentStatus = (int)CurrentStatusEnum.Created;
            sRequest.description = "example for testing";

            //Act
            sRequest.SetPrice(100);

            //Assert
            Assert.Equal(100, sRequest.price);
        }
    }
}
