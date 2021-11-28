using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services;
using com.mobiquity.packer.Services.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;

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
        [Category("ProcessFileLineItem")]
        public void ProcessFileLineItem_WhenPassCorrectData_ShouldReturnPackageItem()
        {
            var packageItem = _packageFileHandler.ProcessFileLineItem("(1,53.38,€45)");

            Assert.IsInstanceOf(typeof(PackageItem), packageItem);

            Assert.AreEqual(packageItem.Index, 1);
            Assert.AreEqual(packageItem.Weight, 53.38, 0.5);
            Assert.AreEqual(packageItem.Value, 45);
        }

        [Test]
        [Category("ProcessFileLineItem")]
        public void ProcessFileLineItem_WhenMissingCurlyBraces_ShouldThrowException()
        {
            Assert.Throws(typeof(APIException), () => _packageFileHandler.ProcessFileLineItem("1,53.38,€45)"));
        }

        [Test]
        [Category("ProcessFileLine")]
        public void ProcessFileLine_WhenPassEmptyString_ShouldThrowException()
        {
            Assert.Throws(typeof(APIException), () => _packageFileHandler.ProcessFileLine(string.Empty));
        }

        [Test]
        [Category("ProcessFileLine")]
        public void ProcessFileLine_WhenPassCorrectData_ShouldReturnListOfPackageItems()
        {
            var packageLine = "(1,33.5,50) (2,77,86) (3,56,98)";
            var packageItems = _packageFileHandler.ProcessFileLine(packageLine);

            Assert.That(packageItems.Count == 3);

            Assert.AreEqual(packageItems[0].Index, 1);
            Assert.AreEqual(packageItems[0].Weight, 33.5);
            Assert.AreEqual(packageItems[0].Value, 50);

            Assert.AreEqual(packageItems[1].Index, 2);
            Assert.AreEqual(packageItems[1].Weight, 77);
            Assert.AreEqual(packageItems[1].Value, 86);

            Assert.AreEqual(packageItems[2].Index, 3);
            Assert.AreEqual(packageItems[2].Weight, 56);
            Assert.AreEqual(packageItems[2].Value, 98);
        }

        [Test]
        [Category("ProcessFileLines")]
        public void ProcessFileLines_WhenPassCorrectFileLines_ShouldReturnListOfPackages()
        {
            var input = new List<string> {
            "81 : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)",
            "8 : (1,15.3,€34)"
            };

            var packages = _packageFileHandler.ProcessFileLines(input);

            Assert.That(packages.Count == 2);
        }

        #endregion
    }
}