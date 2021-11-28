using com.mobiquity.packer.Models;
using System.Collections.Generic;

namespace com.mobiquity.packer.Services.Interfaces
{
    public interface IPackageFileHandler
    {
        List<Package> HandlePackageFile(string filePath);
    }
}
