using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace com.mobiquity.packer.Services
{
    public class PackageFileHandler : IPackageFileHandler
    {
        public List<Package> HandlePackageFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public List<PackageItem> ProcessFileLine(string fileLinePackageItems)
        {
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

            packageItemSb = packageItemSb.Remove(0, 1);

            var packageItemLength = packageItemSb.Length;

            packageItemSb = packageItemSb.Remove(packageItemLength - 1, 1);

            var packageItem = packageItemSb.ToString();

            var packageItemDetails = packageItem.Split(',');

            if (packageItemDetails.Length != 3)
            {
                throw new APIException("Input wasn't in the correct format, correct format is (index, weight, value)");
            }

            var packageItemIndex = GetNumber(packageItemDetails[0]);
            var packageItemWeight = GetFloatNumber(packageItemDetails[1]);
            var packageItemPrice = GetCurrency(packageItemDetails[2]);

            var package = new PackageItem(packageItemIndex, packageItemWeight, packageItemPrice);

            return package;
        }

        public int GetNumber(string numberAsString)
        {
            if (int.TryParse(numberAsString, out int convertedNumber))
            {
                return convertedNumber;
            }

            throw new APIException($"{numberAsString} is not correct number");
        }

        public float GetCurrency(string numberAsString)
        {
            if (float.TryParse(numberAsString, NumberStyles.Currency, new CultureInfo("fr-FR"), out float value))
            {
                return value;
            }

            throw new APIException("The supplied value cannot be converted to currency");
        }

        public float GetFloatNumber(string numberAsString)
        {
            if (float.TryParse(numberAsString, out float convertedNumber))
            {
                return convertedNumber;
            }

            throw new APIException($"{numberAsString} is not correct number");
        }
    }
}
