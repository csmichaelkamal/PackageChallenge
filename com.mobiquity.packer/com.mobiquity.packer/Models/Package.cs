using System.Collections.Generic;

namespace com.mobiquity.packer.Models
{
    /// <summary>
    /// This class is the main package class that holds Maximum weight for each package and 
    /// the possible package items to be included
    /// </summary>
    public class Package
    {
        /// <summary>
        /// Max Weight for each package, given in the input file (No specific weight unit)
        /// </summary>
        public int MaxWeight { get; set; }

        /// <summary>
        /// List of the Package Items (e.g. (1,44,$38))
        /// </summary>
        public List<PackageItem> PackageItems { get; set; }
    }
}
