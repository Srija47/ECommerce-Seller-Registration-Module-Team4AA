using System;
using System.Threading.Tasks;
using AccountService.Models;
using AccountService.Repositories;
using Moq;
using NUnit.Framework;

namespace UnitTestProject
{
        [TestFixture]
        public class TestAccountRepository
        {
            IAccountRepository AccountRepository;
            [SetUp]
            public void SetUp()
            {
                AccountRepository = new AccountRepository(new SellerDBContext());
            }
            [TearDown]
            public void TearDown()
            {
                Console.WriteLine("Test Case run sucessfully");

            }

            /// <summary>
            /// Testing register seller
            /// </summary>
            [Test]
            [TestCase(9567, "parnitha", "parnitha@", "virtusa", "good", "bangalore", "www.virtusa.com", "parnitha@gmail.com", "9123479543")]
            [TestCase(3578, "aarush", "aarush!", "tcs",  "good", "chennai", "www.tcs.com", "aarush@gmail.com", "9973473256")]
            [Description("Test Register()")]
            public void TestRegister(int sid, string username, string password, string companyname,string briefaboutcompany, string address, string website, string email, string mobileno)
            {
                var Datetime = System.DateTime.Now;
                var seller = new Seller { Sellerid = sid, Username = username, Password = password, Companyname = companyname, Briefaboutcompany = briefaboutcompany, Postaladdress = address, Website = website, Emailid = email, Contactnumber = mobileno };
                AccountRepository.SellerRegister(seller);
                var mock = new Mock<IAccountRepository>();
                mock.Setup(x => x.SellerRegister(seller));
            }
            [Test]
            [Description("Test SellerLogin()_ForSuccess cases")]
            public void TestSellerLogin_ForSuccess()
            {
            //var result = AccountRepository.ValidateSeller("suma", "bade123");
            //Assert.NotNull(result);
            try
            {
                Task<Seller> result=AccountRepository.ValidateSeller("rahul", "rahul123");
                Seller newseller = new Seller();
                newseller = result.Result;
                Assert.NotNull(newseller.Username);
            }
            catch(Exception e)
            {
                Assert.Fail(e.Message);
            }
            }
            [Test]
            [Description("Test SellerLogin_For Failure cases")]
            public void TestSellerLogin_ForFailure()
            {
            try
            {
                Task<Seller> result = AccountRepository.ValidateSeller("keshab", "keshab234");
                Seller new_seller = result.Result;
                Assert.NotNull(new_seller.Username);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}

