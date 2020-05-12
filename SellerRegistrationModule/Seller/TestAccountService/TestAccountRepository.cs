using System;
using System.Threading.Tasks;
using AccountService.Entities;
using AccountService.Models;
using AccountService.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace TestAccountService
{
    [TestFixture]
    public class TestAccountRepository
    {
        private IAccountRepository AccountRepository;
        [SetUp]
        public void SetUp()
        {
            AccountRepository = new AccountRepository(new SellerDBContext());
        }
        [TearDown]
        public void TearDown()
        {
            AccountRepository = null;

        }
        /// <summary>
        /// Testing register seller functionality for creating new seller
        /// </summary>
        [Test]
        [TestCase("parnitha", "parnitha@", "6475JH6754", "virtusa", "good", "bangalore", "www.virtusa.com", "parnitha@gmail.com", "9123479543")]
        //[TestCase("aarush", "aarush!", "1354GH465", "tcs", "good", "chennai", "www.tcs.com", "aarush@gmail.com", "9973473256")]
        [Description("Test for SellerRegistration Success")]
        public void TestSellerRegister_Success(string username, string password, string gst, string companyname, string aboutcmpy, string address, string website, string email, string mobileno)
        {
            try
            {
                var mockset = new Mock<DbSet<Seller>>();

                var mockContext = new Mock<SellerDBContext>();
                mockContext.Setup(m => m.Seller).Returns(mockset.Object);

                var seller = new SellerRegister
                {
                    Username = username,
                    Password = password,
                    Gst = gst,
                    Companyname = companyname,
                    Briefaboutcompany = aboutcmpy,
                    Postaladdress = address,
                    Website = website,
                    Emailid = email,
                    Contactnumber = mobileno
                };
                var mock = new Mock<IAccountRepository>();
                mock.Setup(x => x.SellerRegisterAsync(seller)).ReturnsAsync(true);
                AccountRepository accountRepository = new AccountRepository(mock.Object);
                var result = accountRepository.SellerRegisterAsync(seller);
                Assert.IsNotNull(result, "test method failed SellerRegister method is null");
                Assert.AreEqual(mock.Object, result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }
        [Test]
        [TestCase("parnitha", "parnitha@", "6475JH6754", "virtusa", "good", "bangalore", "www.virtusa.com", "parnitha@gmail.com", "9123479543")]
        // [TestCase(65544, "renu", "renu777", "virtusa", 34, "good", "bangalore", "www.virtusa.com", "parnitha@gmail.com", "9123479543")]
        [Description("Test for SellerRegistration UnSuccess")]
        public void TestSellerRegister_UnSuccess(string username, string password, string gst, string companyname, string aboutcmpy, string address, string website, string email, string mobileno)
        {
            try
            {
                var seller = new SellerRegister
                {
                    Username = username,
                    Password = password,
                    Gst = gst,
                    Companyname = companyname,
                    Briefaboutcompany = aboutcmpy,
                    Postaladdress = address,
                    Website = website,
                    Emailid = email,
                    Contactnumber = mobileno
                };
                var mock = new Mock<IAccountRepository>();
                mock.Setup(x => x.SellerRegisterAsync(seller)).ReturnsAsync(false);
                var result = AccountRepository.SellerRegisterAsync(seller);
                Assert.IsNotNull(result, "test method failed SellerRegister method is null");
                Assert.AreEqual(mock.Object, result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.InnerException.Message);
            }
        }
        [Test]
        // <summary>
        /// Service should return seller if correct usename and password is supplied
        /// </summary>
        [Description("Seller Login Success if ValidateSeller() success")]
        public void SellerLoginByRightUserLoginTest()
        {
            try
            {
               

                var seller = new SellerLogin { Username="rahul",Password="rahul123"};
                var mock = new Mock<IAccountRepository>();
                mock.Setup(x => x.ValidateSellerAsync("rahul", "rahul123")).ReturnsAsync(seller);
                var result = AccountRepository.ValidateSellerAsync("rahul", "rahul123");
                Assert.IsNotNull(result, "test method failed ValidateSeller method is null");
                Assert.AreEqual(mock.Object,result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.InnerException.Message);
            }
        }
        [Test]
        // <summary>
        /// Service should return seller if correct usename and password is not supplied
        /// </summary>
        [Description("Seller Login UnSuccess if it fails ValidateSeller()")]
        public void SellerLogin_UnSuccess()
        {
            try
            {
                var seller = new SellerLogin { Username = "pramod#23", Password = "pramod#23" };
                var mock = new Mock<IAccountRepository>();
                mock.Setup(x => x.ValidateSellerAsync("pramod#23", "pramod#23")).ReturnsAsync(seller);
                var result = AccountRepository.ValidateSellerAsync("pramod#23", "pramod#23");
                Assert.IsNotNull(result, "test method failed ValidateSeller method is null");
                Assert.AreEqual(mock.Object, result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.InnerException.Message);
            }

        }
    }
}

