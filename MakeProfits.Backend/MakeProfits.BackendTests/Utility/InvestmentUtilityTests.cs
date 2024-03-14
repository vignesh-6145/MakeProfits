using Castle.Core.Logging;
using MakeProfits.Backend.Repository;
using MakeProfits.Backend.Utillity;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NSubstitute;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MakeProfits.BackendTests.Utility
{
    public class InvestmentUtilityTests
    {
        [Fact]
        public async Task MakeGet_ShouldReturnSuccess_WhenUrlIsValid()
        {
            //Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK});
            var client = new HttpClient(mockHttpMessageHandler.Object);

            var logger = Substitute.For<ILogger<InvestmentsUtility>>();
            var dataAccess = Substitute.For<IInvestmentDataAccess>();
            var utility = new InvestmentsUtility(logger,dataAccess);
            var SomeValidUrl = "https://thehub.incedoinc.com"; // Can be replace any valid url

            //Act
            var response = await utility.MakeGet(SomeValidUrl);

            //Asesert

            Assert.NotNull(response);
            //logger.Received().LogInformation("Request to make an {ActionMethod} for resource at {URL}","GET",SomeValidUrl);


        }
    }
}
