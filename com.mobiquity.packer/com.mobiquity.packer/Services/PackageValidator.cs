using com.mobiquity.packer.BusinessConstraints;
using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.mobiquity.packer.Services
{
    public class PackageValidator : IPackageValidator
    {
        public List<Package> Validate(List<Package> packages)
        {
            if (packages == null || !packages.Any())
            {
                throw new APIException($"{nameof(packages)} cannot be null or empty");
            }

            var validPackages = new List<Package>();

            foreach (var package in packages)
            {
                if (IsPackageValid(package))
                {
                    validPackages.Add(package);
                }
            }

            return validPackages;
        }

        private bool IsPackageValid(Package package)
        {
            // Validate the package itself first
            if (package.MaxWeight > PackageConstraints.MaxWeight)
            {
                throw new APIException($"package maximum weight cannot exceed {PackageConstraints.MaxWeight}");
            }

            if (package.PackageItems.Count > PackageConstraints.MaxItems)
            {
                throw new APIException($"package maximum items cannot exceed {PackageConstraints.MaxItems}");
            }

            var isAllPackageItemsValid = package.PackageItems.All(pkg => pkg.Weight <= PackageItemConstraints.MaxWeight
            && pkg.Value <= PackageItemConstraints.MaxCost);

            // Check if any package item fail to pass the constrain tests
            if (!isAllPackageItemsValid)
            {
                throw new APIException("No valid package items found in that file");
            }

            return true;
        }
    }
}
