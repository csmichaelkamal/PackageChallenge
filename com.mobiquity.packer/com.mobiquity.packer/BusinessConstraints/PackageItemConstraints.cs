using System;
using System.Collections.Generic;
using System.Text;

namespace com.mobiquity.packer.BusinessConstraints
{
    public static class PackageItemConstraints
    {
        /// <summary>
        /// The max weight for an item in a package
        /// </summary>
        public static int MaxWeight = 100; // Read it from configuration file or environment variable

        /// <summary>
        /// The max cost for an item in a package
        /// </summary>
        public static int MaxCost = 100; // Read it from configuration file or environment variable
    }
}
