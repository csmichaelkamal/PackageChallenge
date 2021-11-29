using System.Globalization;

namespace com.mobiquity.packer.Extensions
{
    public static class StringExtensions
    {
        public static int GetNumber(this string numberAsString)
        {
            if (int.TryParse(numberAsString, out int convertedNumber))
            {
                return convertedNumber;
            }

            throw new APIException($"{numberAsString} is not correct number");
        }

        public static float GetCurrency(this string numberAsString)
        {
            if (float.TryParse(numberAsString, NumberStyles.Currency, new CultureInfo("fr-FR"), out float value))
            {
                return value;
            }

            throw new APIException("The supplied value cannot be converted to currency");
        }

        public static float GetFloatNumber(this string numberAsString)
        {
            if (float.TryParse(numberAsString, out float convertedNumber))
            {
                return convertedNumber;
            }

            throw new APIException($"{numberAsString} is not correct number");
        }
    }
}
