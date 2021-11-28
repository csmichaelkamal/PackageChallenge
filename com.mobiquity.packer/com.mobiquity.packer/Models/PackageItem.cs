namespace com.mobiquity.packer.Models
{
    /// <summary>
    /// This class contains package item details for each package that we can / cannot select it.
    /// </summary>
    public class PackageItem
    {
        public PackageItem(int index, float weight, float value)
        {
            Index = index;
            Weight = weight;
            Value = value;
        }

        public int Index { get; private set; }

        public float Weight { get; private set; }

        public float Value { get; private set; }
    }
}
