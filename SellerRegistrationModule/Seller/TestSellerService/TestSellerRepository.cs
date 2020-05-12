using Moq;
using NUnit.Framework;
using SellerService.Entities;
using SellerService.Models;
using SellerService.Repositories;
using System;

namespace TestSellerService
{
    [TestFixture]
    class TestSellerRepository
    {
        private ISellerRepository SellerRepository;
        [SetUp]
        public void SetUp()
        {
            SellerRepository = new SellerRepository(new SellerDBContext());
        }
        [TearDown]
        public void TearDown()
        {
            SellerRepository = null;

        }
        /// <summary>
        /// Testing View seller profile functionality for a existing seller
        /// </summary>
        [Test]
        [TestCase(1)]
        [Description("Test for ViewSellerProfile Success")]
        public void Test_ViewSellerProfile_Success()
        {
            try
            {
                var seller = new SellerProfile { Sellerid=1,Username = "rahul", Password = "rahul123" };
                var mock = new Mock<ISellerRepository>();
                mock.Setup(x => x.ViewSellerProfile(1)).ReturnsAsync(seller);
                var result = SellerRepository.ViewSellerProfile(1);
                Assert.IsNotNull(result, "test method failed ValidateSeller method is null");
                Assert.AreEqual(mock.Object, result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.InnerException.Message);
            }
        }
        /// <summary>
        /// Testing View seller profile functionality for a existing seller not found
        /// </summary>
        [Test]
        [TestCase(9)]
        [Description("Test for ViewSellerProfile UnSuccess")]
        public void Test_ViewSellerProfile_UnSuccess()
        {
            try
            {
                var seller = new SellerProfile { Sellerid = 1, Username = "rahul", Password = "rahul123" };
                var mock = new Mock<ISellerRepository>();
                mock.Setup(x => x.ViewSellerProfile(1)).ReturnsAsync(seller);
                var result = SellerRepository.ViewSellerProfile(1);
                Assert.IsNotNull(result, "test method failed ValidateSeller method is null");
                Assert.AreEqual(mock.Object, result);
            }
            catch (Exception e)
            {
                Assert.Fail(e.InnerException.Message);
            }
        }
        /// <summary>
        /// Testing update seller profile functionality for a existing seller
        /// </summary>
        [Test]
        [TestCase("parnitha", "parnitha@", "6475JH6754", "virtusa", "good", "bangalore", "www.virtusa.com", "parnitha@gmail.com", "9123479543")]
        //[TestCase(3578, "aarush", "aarush!", "tcs", "good", "chennai", "www.tcs.com", "aarush@gmail.com", "9973473256")]
        [Description("Test for UpdateSellerProfile Success")]
        public void TestUpdateSellerProfile_Success()
        {
            try
            {
                SellerProfile seller1 = new SellerProfile();
                Seller seller = new Seller { Sellerid = 2, Username = "parnitha@", Gst = "6475JH6754", Companyname = "virtusa", Briefaboutcompany = "good" };
                seller1.Username = seller.Username;
                seller1.Password = seller.Password;
                seller1.Companyname = seller.Companyname;
                seller1.Briefaboutcompany = seller.Briefaboutcompany;
                seller1.Postaladdress = seller.Postaladdress;
                seller1.Website = seller.Website;
                seller1.Emailid = seller.Emailid;
                seller1.Contactnumber = seller.Contactnumber;
                var mock = new Mock<ISellerRepository>();
                mock.Setup(x => x.UpdateSellerProfile(seller1)).ReturnsAsync(true);
                //SellerRepository sellerRepository = new SellerRepository(mock.Object);
                var result = SellerRepository.UpdateSellerProfile(seller1);
                Assert.IsNotNull(result, "test method failed SellerRegister method is null");
                //Assert.AreEqual(result, true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }



    }

}
