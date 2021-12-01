using System;

namespace com.mobiquity.packer.Models
{
    /// <summary>
    /// This class contains package item details for each package that we can / cannot select it.
    /// </summary>
    public class PackageItem : IComparable<PackageItem>
    {
        public PackageItem(int index, double weight, double value)
        {
            Index = index;
            Weight = weight;
            Value = value;
        }

        public int Index { get; private set; }

        public double Weight { get; private set; }

        public double Value { get; private set; }

        public int CompareTo(PackageItem other)
        {
            return Index - other.Index;
        }
    }
}
