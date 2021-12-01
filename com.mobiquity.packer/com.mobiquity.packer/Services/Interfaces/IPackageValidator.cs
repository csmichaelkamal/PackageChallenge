using com.mobiquity.packer.Models;
using System.Collections.Generic;

namespace com.mobiquity.packer.Services.Interfaces
{
    public interface IPackageValidator
    {
        List<Package> Validate(List<Package> packages);
    }
}
