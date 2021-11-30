using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace com.mobiquity.packer.Services
{
    public class PackageItemsSelector : IPackageItemSelector
    {
        private readonly IPackageFileHandler _packageFileHandler;

        public PackageItemsSelector(IPackageFileHandler packageFileHandler)
        {
            _packageFileHandler = packageFileHandler;
        }

        public string Select(List<Package> packages)
        {
            if (packages == null || packages.Count < 1)
            {
                throw new APIException($"{nameof(packages)} cannot be null or empty, " +
                    $"it must contain at least one valid package");
            }

            foreach (var package in packages)
            {
                package.PackageItems.Sort();


            }

            return string.Empty;
        }
    }
}
