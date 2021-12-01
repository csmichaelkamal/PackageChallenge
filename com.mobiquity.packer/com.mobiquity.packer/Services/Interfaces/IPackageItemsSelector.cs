using com.mobiquity.packer.Models;
using System.Collections.Generic;

namespace com.mobiquity.packer.Services.Interfaces
{
    public interface IPackageItemSelector
    {
        List<string> Select(List<Package> packages);
    }
}
