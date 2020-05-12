
using AccountService.Controllers;
using AccountService.Manager;
using AccountService.Models;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;

namespace TestAccountService
{
    class TestAccountController
    {
        private AccountController _accountController;
        Mock<IAccountManager> _accountManager;
        [SetUp]
        public void SetUp()
        {
            var logger = new Mock<ILogger<AccountController>>();
            _accountManager = new Mock<IAccountManager>();
            _accountController = new AccountController(_accountManager.Object, logger.Object);
        }
        public void TearDown()
        {
            _accountController = null;

        }
        /// <summary>
        /// Testing register seller functionality for a new seller
        /// </summary>
        [Test]
        [TestCase("prethi", "prethi88", "virtusa", "324JH78", "good", "bangalore", "www.virtusa.com", "parnitha@gmail.com", "9123479543")]
        [Description("Test for SellerRegistration Success")]
        public void TestSellerRegister_Success(string username, string password, string companyname, string gst, string aboutcmpy, string address, string website, string email, string mobileno)
        {
            try
            {
                var mock = new Mock<IAccountManager>();
                var seller = new SellerRegister { Username = username, Password = password, Gst = gst, Companyname = companyname, Briefaboutcompany = aboutcmpy, Postaladdress = address, Website = website, Emailid = email, Contactnumber = mobileno };
                mock.Setup(x => x.SellerRegister(seller)).ReturnsAsync(true);
                var result = _accountController.SellerRegister(seller);
                Assert.IsNotNull(result, "test method failed SellerRegister method is null");
                Assert.AreNotEqual(mock.Object, result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }
        [Test]
        [TestCase("renu", "renu777", "virtusa", "34", "good", "bangalore", "www.virtusa.com", "parnitha@gmail.com", "9123479543")]
        [Description("Test for SellerRegistration Success")]
        public void TestSellerRegister_UnSuccess(string username, string password, string gst, string companyname, string aboutcmpy, string address, string website, string email, string mobileno)
        {
            try
            {
                var mock = new Mock<IAccountManager>();
                var seller = new SellerRegister { Username = username, Password = password, Gst = gst, Companyname = companyname, Briefaboutcompany = aboutcmpy, Postaladdress = address, Website = website, Emailid = email, Contactnumber = mobileno };
                mock.Setup(x => x.SellerRegister(seller)).ReturnsAsync(false);
                var result = _accountController.SellerRegister(seller);
                Assert.IsNotNull(result, "test method failed SellerRegister method is null");
                Assert.AreNotEqual(mock.Object, result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }
        [Test]
        //[TestCase("rahul", "rahul123")]
        [Description("Seller Login Success")]
        public void SellerLoginByRightUserLoginTest()
        {
            try
            {
                var seller = new SellerLogin { Username = "rahul", Password = "rahul123" };
                var mock = new Mock<IAccountManager>();
                mock.Setup(x => x.ValidateSeller("rahul", "rahul123")).ReturnsAsync(seller);
                var result = _accountController.SellerLogin("rahul", "rahul123");
                Assert.IsNotNull(result, "test method failed SellerRegister method is null");
                Assert.AreNotEqual(mock.Object, result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [Test]
        [Description("Seller Login UnSuccess")]
        public void Test_Controller_SellerLogin_UnSuccess()
        {
            try
            {
                var seller = new SellerLogin { Username = "pramod", Password = "pramod123" };
                var mock = new Mock<IAccountManager>();
                mock.Setup(x => x.ValidateSeller("pramod", "pramod123")).ReturnsAsync(seller);
                var result = _accountController.SellerLogin("pramod", "pramod123");
                Assert.IsNotNull(result, "test method failed SellerRegister method is null");
                Assert.AreNotEqual(mock.Object, result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }


        }

    }
}

