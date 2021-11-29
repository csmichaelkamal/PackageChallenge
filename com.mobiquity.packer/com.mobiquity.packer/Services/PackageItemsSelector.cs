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
            throw new NotImplementedException();
        }
    }
}
