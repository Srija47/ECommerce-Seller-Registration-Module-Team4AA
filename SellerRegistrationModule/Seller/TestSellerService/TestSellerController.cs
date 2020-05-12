
using Moq;
using NUnit.Framework;
using SellerService.Manager;

namespace TestSellerService
{
    class TestSellerController
    {
        private SellerController _sellerController;
        Mock<ISellerManager> _sellerManager;
        [SetUp]
        public void SetUp()
        {
            var logger = new Mock<ILogger<SellerController>>();
            _sellerManager = new Mock<ISellerManager>();
            _sellerController = new SellerController(_sellerManager.Object, logger.Object);
        }
        public void TearDown()
        {
            _accountController = null;

        }
    }
}
