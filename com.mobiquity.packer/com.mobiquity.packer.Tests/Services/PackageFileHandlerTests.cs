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
            var packgaeItem = _packageFileHandler.ProcessFileLineItem("(1,53.38,€45)");

            Assert.IsInstanceOf(typeof(PackageItem), packgaeItem);

            Assert.AreEqual(packgaeItem.Index, 1);
            Assert.AreEqual(packgaeItem.Weight, 53.38, 0.5);
            Assert.AreEqual(packgaeItem.Value, 45);
        }

        #endregion
    }
}