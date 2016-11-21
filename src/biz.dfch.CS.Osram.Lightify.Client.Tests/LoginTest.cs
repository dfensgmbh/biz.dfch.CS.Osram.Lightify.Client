using System;
using System.Collections.Generic;
using biz.dfch.CS.Osram.Lightify.Client.Model;
using biz.dfch.CS.Testing.Attributes;
using biz.dfch.CS.Web.Utilities.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    [TestClass]
    public class LoginTest
    {
        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+userName")]
        public void GetTokenWithNullUsernameThrowsContractException()
        {
            var sut = new Login();

            var userName = default(string);
            var password = default(string);
            var serialNumber = default(string);

            sut.GetToken(userName, password, serialNumber);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+password")]
        public void GetTokenWithNullPasswordThrowsContractException()
        {
            var sut = new Login();

            var userName = "ArbitraryUser";
            var password = default(string);
            var serialNumber = default(string);

            sut.GetToken(userName, password, serialNumber);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+serialNumber")]
        public void GetTokenWithNullSerialNumberThrowsContractException()
        {
            var sut = new Login();

            var userName = "ArbitraryUser";
            var password = "ArbitraryPassword";
            var serialNumber = default(string);

            sut.GetToken(userName, password, serialNumber);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+uri")]
        public void GetTokenWithEmptyUriThrowsContractException()
        {
            var sut = new Login();

            var userName = "ArbitraryUser";
            var password = "ArbitraryPassword";
            var serialNumber = "ArbitrarySerialNumber";

            var result = sut.GetToken(userName, password, serialNumber);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void GetTokenSucceeds()
        {
            var userId = "arbitraryUser";
            var securityToken = "validSecurityToken";
            var input = new SessionResponse
            {
                UserId = userId,
                SecurityToken = securityToken
            };
            var json = input.SerializeObject();
            
            var client = Mock.Create<RestCallExecutor>();
            Mock.Arrange(() => client.Invoke(Arg.IsAny<HttpMethod>(), Arg.IsAny<string>(), Arg.IsAny<IDictionary<string, string>>(), Arg.IsAny<string>()))
                .Returns(json);
            var uri = new Uri("https://eu.lightify-api.org.example.com/lightify/services/session");
            var sut = new Login(uri);

            var userName = "ArbitraryUser";
            var password = "ArbitraryPassword";
            var serialNumber = "ArbitrarySerialNumber";

            var result = sut.GetToken(userName, password, serialNumber);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void GetTokenSucceeds2()
        {
            var uri = new Uri("https://eu.lightify-api.org/lightify/services/session");
            var sut = new Login(uri);

            var userName = "ArbitraryUser";
            var password = "ArbitraryPassword";
            var serialNumber = "ArbitrarySerialNumber";

            var result = sut.GetToken(userName, password, serialNumber);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }
    }
}
