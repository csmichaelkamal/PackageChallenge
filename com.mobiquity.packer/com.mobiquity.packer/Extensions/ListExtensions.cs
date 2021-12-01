using System.Collections.Generic;
using System.Linq;

namespace com.mobiquity.packer.Extensions
{
    public static class ListExtensions
    {
        public static List<int> MapIndexesList(this List<int> target, int[] source)
        {
            return target.Select(index => source[index]).ToList();
        }

        public static string GetStringRepresentation(this List<int> source)
        {
            return string.Join(",", source);
        }
    }
}
