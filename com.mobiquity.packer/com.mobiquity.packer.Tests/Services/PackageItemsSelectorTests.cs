using com.mobiquity.packer.Services;
using NUnit.Framework;

namespace com.mobiquity.packer.Tests.Services
{
    public class PackageItemsSelectorTests
    {
        #region Private Members

        private PackageItemsSelector packageItemsSelector;

        #endregion

        #region Setup

        [SetUp]
        public void Setup()
        {
            packageItemsSelector = new PackageItemsSelector();
        }

        #endregion

        #region Tests

        [Test]
        [Category("PackageItemsSelector")]
        public void PackageItemsSelectorSelect_WhenPassCorrectData_ShouldReturnPackageItem()
        {

        }

        #endregion
    }
}