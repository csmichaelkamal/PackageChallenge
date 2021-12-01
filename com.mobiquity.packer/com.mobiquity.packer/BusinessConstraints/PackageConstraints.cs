using System;
using System.Collections.Generic;
using System.Text;

namespace com.mobiquity.packer.BusinessConstraints
{
    public static class PackageConstraints
    {
        /// <summary>
        /// The max weight for a package
        /// </summary>
        public static int MaxWeight = 100; // Read it from configuration file or environment variable

        /// <summary>
        /// The max number of items that a package can hold
        /// </summary>
        public static int MaxItems = 15; // Read it from configuration file or environment variable

    }
}
