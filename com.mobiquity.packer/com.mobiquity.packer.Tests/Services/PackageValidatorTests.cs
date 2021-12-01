using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace com.mobiquity.packer.Tests.Services
{
    public class PackageValidatorTests
    {
        #region Private Members

        private PackageValidator packageValidator;

        #endregion

        #region Setup

        [SetUp]
        public void Setup()
        {
            packageValidator = new PackageValidator();
        }

        #endregion

        #region Tests

        [Test]
        [Category("ValidatePackage")]
        public void PackageValidator_ValidatePackage_WhenPassMaxWeightGreaterThan100_ShouldThrowException()
        {
            var input = new List<Package>
            { new Package
                    {
                        MaxWeight = 101
                    }
            };

            Assert.Throws(typeof(APIException), () => packageValidator.Validate(input));
        }

        [Test]
        [Category("ValidatePackage")]
        public void PackageValidator_ValidatePackage_WhenPassItemsGreaterThan15_ShouldThrowException()
        {
            var input = new List<Package>
            {
                new Package
                    {
                        MaxWeight = 100,
                        PackageItems = new List<PackageItem>
                        {
                            new PackageItem(1,22,33),
                            new PackageItem(2,33,44),
                            new PackageItem(3,44,55),
                            new PackageItem(4,55,66),
                            new PackageItem(5,66,77),
                            new PackageItem(6,77,88),
                            new PackageItem(7,88,99),
                            new PackageItem(8,99,32),
                            new PackageItem(9,12,33),
                            new PackageItem(10,13,34),
                            new PackageItem(11,14,35),
                            new PackageItem(12,15,36),
                            new PackageItem(13,16,37),
                            new PackageItem(14,17,38),
                            new PackageItem(15,18,39),
                            new PackageItem(16,19,40)
                        }
                    }
            };

            Assert.Throws(typeof(APIException), () => packageValidator.Validate(input));
        }

        [Test]
        [Category("ValidatePackage")]
        public void PackageValidator_ValidatePackage_WhenPassItemsLessThanOrEqual15_ShouldPass()
        {
            var input = new List<Package>
            {
                new Package
                    {
                        MaxWeight = 100,
                        PackageItems = new List<PackageItem>
                        {
                            new PackageItem(1,22,33),
                            new PackageItem(2,33,44),
                            new PackageItem(3,44,55),
                            new PackageItem(4,55,66),
                            new PackageItem(5,66,77),
                            new PackageItem(6,77,88),
                            new PackageItem(7,88,99),
                            new PackageItem(8,99,32),
                            new PackageItem(9,12,33),
                            new PackageItem(10,13,34),
                            new PackageItem(11,14,35),
                            new PackageItem(12,15,36),
                            new PackageItem(13,16,37),
                            new PackageItem(14,17,38),
                            new PackageItem(15,18,39)
                        }
                    }
            };

            var validPackages = packageValidator.Validate(input);

            // As we might have only one valid package
            Assert.AreEqual(validPackages.Count, 15, 14);
        }

        [Test]
        [Category("ValidatePackageItems")]
        public void PackageValidator_ValidatePackageItems_WhenPassItemsWithWeightMoreThan100_ShouldThrowException()
        {
            var input = new List<Package>
            {
                new Package
                    {
                        MaxWeight = 100,
                        PackageItems = new List<PackageItem>
                        {
                            new PackageItem(1,22,33),
                            new PackageItem(2,101,44),
                            new PackageItem(3,44,55)
                        }
                    }
            };

            // As we might have only one valid package
            Assert.Throws(typeof(APIException), () => packageValidator.Validate(input));
        }

        [Test]
        [Category("ValidatePackageItems")]
        public void PackageValidator_ValidatePackageItems_WhenPassItemsWithValuetMoreThan100_ShouldThrowException()
        {
            var input = new List<Package>
            {
                new Package
                    {
                        MaxWeight = 100,
                        PackageItems = new List<PackageItem>
                        {
                            new PackageItem(1,22,33),
                            new PackageItem(2,33,101),
                            new PackageItem(3,44,55)
                        }
                    }
            };

            // As we might have only one valid package
            Assert.Throws(typeof(APIException), () => packageValidator.Validate(input));
        }

        #endregion
    }
}