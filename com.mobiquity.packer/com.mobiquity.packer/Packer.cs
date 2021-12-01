using com.mobiquity.packer.Services.Interfaces;
using com.mobiquity.packer.Services;
using System.Linq;

namespace com.mobiquity.packer
{
    public class Packer
    {
        public static string Pack(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new APIException($"{nameof(filePath)} cannot be null");
            }

            IPackageFileHandler fileHandler = new PackageFileHandler();
            IPackageValidator packageValidator = new PackageValidator();
            IPackageItemSelector itemsSelector = new PackageItemsSelector();

            // The program flow is as following:
            // First, we need to read the file from the given filePath (this method's parameter)
            // Second, validate the package and its items against the constraints
            // Thirs, we pass it to the file handler
            // Fourth, we pass the output from the file handler (list of packages) to the package items
            // selector to select the items that maximize the profit (the value)

            // Pass the file to the file handler
            var packages = fileHandler.HandlePackageFile(filePath);

            if (packages == null || !packages.Any())
            {
                throw new APIException("No valid packages has been found in the file your provided");
            }

            // validate the pacgake and its items against our business constraints
            var validPackages = packageValidator.Validate(packages);
            if (validPackages == null || !validPackages.Any())
            {
                throw new APIException("No valid packacge found");
            }

            // Finally, pass the valid packages to the selector
            var selectedPackages = itemsSelector.Select(packages);

            if (selectedPackages == null || !selectedPackages.Any())
            {
                throw new APIException("No packages has been selected, please make sure from the input file");
            }

            var finalOutput = string.Join(System.Environment.NewLine, selectedPackages);

            return finalOutput;
        }
    }
}
