using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services;
using com.mobiquity.packer.Services.Interfaces;
using NUnit.Framework;

namespace com.mobiquity.packer.Tests.Services
{
    public class PackageFileHandlerTests
    {
        #region Private Members

        private PackageFileHandler _packageFileHandler;

        #endregion

        #region Setup

        [SetUp]
        public void Setup()
        {
            _packageFileHandler = new PackageFileHandler();
        }

        #endregion

        #region Tests

        [Test]
        public void ProcessFileLineItem_WhenPassCorrectData_ShouldReturnPackageItem()
        {
            var packageItem = _packageFileHandler.ProcessFileLineItem("(1,53.38,€45)");

            Assert.IsInstanceOf(typeof(PackageItem), packageItem);

            Assert.AreEqual(packageItem.Index, 1);
            Assert.AreEqual(packageItem.Weight, 53.38, 0.5);
            Assert.AreEqual(packageItem.Value, 45);
        }

        [Test]
        public void ProcessFileLineItem_WhenMissingCurlyBraces_ShouldThrowException()
        {
            Assert.Throws(typeof(APIException), () => _packageFileHandler.ProcessFileLineItem("1,53.38,€45)"));
        }

        #endregion
    }
}