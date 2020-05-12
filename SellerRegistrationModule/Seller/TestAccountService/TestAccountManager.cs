
using AccountService.Entities;
using AccountService.Manager;
using AccountService.Models;
using AccountService.Repositories;
using Moq;
using NUnit.Framework;
using System;

namespace TestAccountService
{
    [TestFixture]
    class TestAccountManager
    {
        private IAccountManager AccountManager;
        [SetUp]
        public void SetUp()
        {
            AccountManager = new AccountManager(new AccountRepository(new SellerDBContext()));
        }
        [TearDown]
        public void TearDown()
        {
            AccountManager = null;

        }
        /// <summary>
        /// Testing register seller functionality for a new seller
        /// </summary>
        [Test]
        [TestCase( "parnitha", "parnitha@", "6475JH6754", "virtusa", "good", "bangalore", "www.virtusa.com", "parnitha@gmail.com", "9123479543")]
        //[TestCase("aarush", "aarush!", "tcs", "good", "chennai", "www.tcs.com", "aarush@gmail.com", "9973473256")]
        [Description("Test for SellerRegistration Success")]
        public void TestSellerRegister_Success(string username, string password, string gst, string companyname, string aboutcmpy, string address, string website, string email, string mobileno)
        {
            try
            {
                var seller = new SellerRegister { Username = username, Password = password, Gst = gst, Companyname = companyname, Briefaboutcompany = aboutcmpy, Postaladdress = address, Website = website, Emailid = email, Contactnumber = mobileno };
                var mock = new Mock<IAccountRepository>();
                mock.Setup(x => x.SellerRegisterAsync(seller)).ReturnsAsync(true);
                AccountManager accountManager = new AccountManager(mock.Object);
                var result = accountManager.SellerRegister(seller);
                Assert.IsNotNull(result, "test method failed SellerRegister method is null");
                Assert.AreEqual(result.Result, true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [Test]
        [TestCase("parnitha", "parnitha@", "6475JH6754", "virtusa", "good", "bangalore", "www.virtusa.com", "parnitha@gmail.com", "9123479543")]
        // [TestCase(65544, "renu", "renu777", "virtusa", 34, "good", "bangalore", "www.virtusa.com", "parnitha@gmail.com", "9123479543")]
        [Description("Test for SellerRegistration Success")]
        public void TestSellerRegister_UnSuccess(string username, string password, string gst, string companyname, string aboutcmpy, string address, string website, string email, string mobileno)
        {
            try
            {
                var seller = new SellerRegister { Username = username, Password = password, Gst = gst, Companyname = companyname, Briefaboutcompany = aboutcmpy, Postaladdress = address, Website = website, Emailid = email, Contactnumber = mobileno };
                var mock = new Mock<IAccountRepository>();
                mock.Setup(x => x.SellerRegisterAsync(seller)).ReturnsAsync(false) ;
                AccountManager accountManager = new AccountManager(mock.Object);
                var result = accountManager.SellerRegister(seller);
                Assert.IsNotNull(result, "test method failed SellerRegister method is null");
                Assert.AreEqual(result.Result, false);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }
        [Test]
        // <summary>
        /// Service should return seller if correct usename and password is supplied
        /// </summary>

        [Description("Seller Login Success")]
        public void SellerLoginByRightUserLoginTest()
        {
            try
            {
                var seller = new SellerLogin { Username = "rahul", Password = "rahul123" };
                var mock = new Mock<IAccountRepository>();
                mock.Setup(x => x.ValidateSellerAsync("rahul", "rahul123")).ReturnsAsync(seller);
                AccountManager accountManager = new AccountManager(mock.Object);
                var result = accountManager.ValidateSeller("rahul", "rahul123");
                Assert.IsNotNull(result, "test method failed SellerRegister method is null");
                Assert.AreEqual(result.Result, true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [Test]
        [Description("Seller Login UnSuccess")]
        public void SellerLoginByWrongUserLoginTest()
        {
            try
            {
                var seller = new SellerLogin { Username = "rahul", Password = "rahul123" };
                var mock = new Mock<IAccountRepository>();
                mock.Setup(x => x.ValidateSellerAsync("pramod", "pramod32@")).ReturnsAsync(seller);
                AccountManager accountManager = new AccountManager(mock.Object);
                var result = accountManager.ValidateSeller("pramod", "pramod32@");
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
