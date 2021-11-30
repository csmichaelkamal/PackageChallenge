using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace com.mobiquity.packer.Tests.Services
{
    public class PackageItemsSelectorTests
    {
        #region Private Members

        private PackageFileHandler packageFileHandler;
        private PackageItemsSelector packageItemsSelector;

        #endregion

        #region Setup

        [SetUp]
        public void Setup()
        {
            packageFileHandler = new PackageFileHandler();
            packageItemsSelector = new PackageItemsSelector(packageFileHandler);
        }

        #endregion

        #region Tests

        [Test]
        [Category("PackageItemsSelector")]
        public void PackageItemsSelectorSelect_WhenPassNull_ShouldThrowException()
        {
            Assert.Throws(typeof(APIException), () => packageItemsSelector.Select(null));
        }

        [Test]
        [Category("PackageItemsSelector")]
        public void PackageItemsSelectorSelect_WhenPassLisOfPackage_ShouldReturnString()
        {
            var packages = new List<Package>()
            {
                new Package {
                    MaxWeight = 81,
                    PackageItems = new List<PackageItem>
                                    {
                                        new PackageItem(3, 33.2, 20),
                                        new PackageItem(4, 43.2, 90),
                                        new PackageItem(1, 78.4, 30)
                                    }
                            }
            };

            var output = packageItemsSelector.Select(packages);

            Assert.AreEqual("4,3", output[0]);
        }

        #endregion
    }
}