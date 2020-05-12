
using Moq;
using NUnit.Framework;
using SellerService.Entities;
using SellerService.Manager;
using SellerService.Models;
using SellerService.Repositories;
using System;
using System.Threading.Tasks;

namespace TestSellerService
{
    [TestFixture]
    public class TestSellerManager
    {
        ISellerManager iSellerManager;

        [SetUp]
        public void SetUp()
        {
            iSellerManager = new SellerManager(new SellerRepository(new SellerDBContext()));
        }

        [TearDown]
        public void TearDown()
        {
            iSellerManager = null;
        }
        /// <summary>
        /// Testing seller profile
        /// </summary>
        [Test]
        [TestCase(1)]
        [TestCase(7)]
        [Description("testing seller Profile")]
        public async Task ViewSellerProfileSuccess(int sellerid)
        {
            try
            {
                SellerProfile seller = new SellerProfile();
                var mock = new Mock<ISellerRepository>();
                mock.Setup(x => x.ViewSellerProfile(sellerid)).ReturnsAsync(seller);
                SellerManager sellerManager = new SellerManager(mock.Object);
                var result = sellerManager.ViewSellerProfile(sellerid);
                //Assert.AreEqual(true, result);
                Assert.NotNull(result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// Testing buyer profile
        /// </summary>
        /// <summary>
        /// Testing seller profile
        /// </summary>
        [Test]
        [TestCase(2)]
        [TestCase(1)]
        [Description("testing seller Profile failure")]
        public async Task ViewSellerProfile_UnSuccessful(int sellerid)
        {
            try
            {
                SellerProfile seller = new SellerProfile();
                var mock = new Mock<ISellerRepository>();
                mock.Setup(x => x.ViewSellerProfile(sellerid)).ReturnsAsync(seller);
                SellerManager sellerManager = new SellerManager(mock.Object);
                var result = await sellerManager.ViewSellerProfile(sellerid);
                Assert.IsNull(result, "invalid seller");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        [Description("testing seller edit Profile")]
        public async Task UpdateSellerProfile_Success()
        {
            try
            {
                SellerProfile seller = new SellerProfile() { Sellerid = 5, Username = "manaswi", Password = "manaswi@", Companyname = "accenture", Gst = "78", Briefaboutcompany = "good", Postaladdress = "mumbai", Website = "www.accenture.com", Emailid = "manaswi@gmail.com", Contactnumber = "9478654567" };
                var mock = new Mock<ISellerRepository>();
                mock.Setup(x => x.UpdateSellerProfile(seller)).ReturnsAsync(true);
                SellerManager sellerManager = new SellerManager(mock.Object);
                var result = await sellerManager.UpdateSellerProfile(seller);
                Assert.IsNotNull(result);
                Assert.AreEqual(true, result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        [Description("testing seller edit Profile")]
        public async Task UpdateSellerProfile_UnSuccess()
        {
            try
            {
                SellerProfile seller = new SellerProfile() { Sellerid = 508, Username = "manaswi", Password = "manaswi@", Companyname = "accenture", Gst = "78", Briefaboutcompany = "good", Postaladdress = "mumbai", Website = "www.accenture.com", Emailid = "manaswi@gmail.com", Contactnumber = "9478654567" };
                var mock = new Mock<ISellerRepository>();
                mock.Setup(x => x.UpdateSellerProfile(seller)).ReturnsAsync(false);
                SellerManager sellerManager = new SellerManager(mock.Object);
                var result = await sellerManager.UpdateSellerProfile(seller);
                Assert.AreEqual(false, result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

    }
}
