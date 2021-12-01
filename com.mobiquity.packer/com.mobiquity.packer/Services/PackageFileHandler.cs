using com.mobiquity.packer.Extensions;
using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace com.mobiquity.packer.Services
{
    public class PackageFileHandler : IPackageFileHandler
    {
        public List<Package> HandlePackageFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new APIException($"{nameof(filePath)} is null or empty");
            }

            var fileLines = File.ReadAllLines(filePath);

            if (fileLines.Length < 1)
            {
                throw new APIException("Empty File Content");
            }

            var packages = ProcessFileLines(fileLines.ToList());

            return packages;
        }

        public List<Package> ProcessFileLines(List<string> fileLines)
        {
            if (fileLines == null || fileLines.Count == 0)
            {
                return null;
            }

            var packages = new List<Package>();

            foreach (var line in fileLines)
            {
                if (!line.Contains(":"))
                {
                    throw new APIException("Invalid File Line Format, " +
                        "Line Should contains \":\" between Package Max Capacity and Package Items");
                }

                var lineParts = line.Split(':');

                if (lineParts.Length > 2)
                {
                    throw new APIException("Invalid File Line Format, Line Should Contain ONLY one \":\"");
                }

                // Parse the first Part of the Line, the Maximum Capacity of the Package
                var maxPackageWeight = lineParts[0].GetNumber();

                // Parse the Package Items that might be Included / Selected in the Package
                var packageItems = ProcessFileLine(lineParts[1].Trim());

                // Construct Package and return the List

                packages.Add(new Package
                {
                    MaxWeight = maxPackageWeight,
                    PackageItems = packageItems
                });
            }

            return packages;
        }

        public List<PackageItem> ProcessFileLine(string fileLinePackageItems)
        {
            if (string.IsNullOrEmpty(fileLinePackageItems))
            {
                throw new APIException($"{nameof(fileLinePackageItems)} is empty");
            }

            var packageItems = fileLinePackageItems.Split(' ');

            if (packageItems == null || packageItems.Length < 1)
            {
                throw new APIException($"{nameof(packageItems)} is null");
            }

            var processedFileLines = new List<PackageItem>();

            for (int i = 0; i < packageItems.Length; i++)
            {
                var package = ProcessFileLineItem(packageItems[i]);

                processedFileLines.Add(package);
            }

            return processedFileLines;
        }

        public PackageItem ProcessFileLineItem(string fileLineItem)
        {
            if (!fileLineItem.StartsWith("(") || !fileLineItem.EndsWith(")"))
            {
                throw new APIException("Package Details malformatted, Should start with \"(\" and End with \")\" ");
            }

            var packageItemSb = new StringBuilder(fileLineItem);

            // Remove the First Curly Brace
            packageItemSb = packageItemSb.Remove(0, 1);

            var packageItemLength = packageItemSb.Length;

            // Remove the Last Curly Brace
            packageItemSb = packageItemSb.Remove(packageItemLength - 1, 1);

            var packageItem = packageItemSb.ToString();

            // Split Package Item into Index, Weight and Value (Price or Profit)
            var packageItemDetails = packageItem.Split(',');

            // Check if the Package item is in the correct format or not
            if (packageItemDetails.Length != 3)
            {
                throw new APIException("Input wasn't in the correct format, correct format is (index, weight, value)");
            }

            var packageItemIndex = packageItemDetails[0].GetNumber();
            var packageItemWeight = packageItemDetails[1].GetFloatNumber();
            var packageItemPrice = packageItemDetails[2].GetCurrency();

            var package = new PackageItem(packageItemIndex, packageItemWeight, packageItemPrice);

            return package;
        }
    }
}
