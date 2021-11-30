using com.mobiquity.packer.Extensions;
using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.mobiquity.packer.Services
{
    public class PackageItemsSelector : IPackageItemSelector
    {
        private readonly IPackageFileHandler _packageFileHandler;

        public PackageItemsSelector(IPackageFileHandler packageFileHandler)
        {
            _packageFileHandler = packageFileHandler;
        }

        public List<string> Select(List<Package> packages)
        {
            if (packages == null || packages.Count < 1)
            {
                throw new APIException($"{nameof(packages)} cannot be null or empty, " +
                    $"it must contain at least one valid package");
            }

            var finalOutput = new List<string>();

            foreach (var package in packages)
            {
                // we need to sort the packageItems by thier weights, and return them in new List
                var sortedPackageItems = package.PackageItems.OrderBy(p => p.Weight).ToList();

                // Here, I will implement the Knapsack algorithm for solving such kind of problems
                // In this problem, we will use the Knapsack 0-1, which means that we either 
                // take the whole item (1) or nothing (0)
                // There are other types of the knapsack problem, but we don't need them here
                // The algorithm simply keeps track of the items weights in one array
                // and another array of thier values (profits/prices)

                // We need to keep track of the original Indexes after the sort we did in the previous operation
                var originalIndexes = sortedPackageItems.Select(x => x.Index).ToArray();

                // Create two arrays for weights and values
                // We need only the numbers of the values, we don't care about the fractions
                int[] values = sortedPackageItems.Select(x => (int)x.Value).ToArray();
                int[] weights = sortedPackageItems.Select(x => (int)x.Weight).ToArray();

                // We need the maximum weight/capacity that the package can hold
                // We will use in to fill the two dimentional array (the Matrix)
                var maxWeight = package.MaxWeight;
                var packageItemsCount = sortedPackageItems.Count;

                // Here we need to fill out the two dimentional array with the data
                var selectedIndexes = KnapSack(maxWeight, weights, values, packageItemsCount);

                var selectedOriginalIndexes = selectedIndexes.MapIndexesList(originalIndexes);

                finalOutput.Add(selectedOriginalIndexes.GetStringRepresentation());
            }

            return finalOutput;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxWeight"></param>
        /// <param name="weights"></param>
        /// <param name="values"></param>
        /// <param name="packageCount"></param>
        /// <returns></returns>
        private List<int> KnapSack(int maxWeight, int[] weights, int[] values, int packageItemsCount)
        {
            int currentWeight = 0;
            int currentItem = 0;

            // Here, we create two dimensional array (matrix) with one more row and one more column
            // the extra row will be used for calculating the items in the second row (the first item)
            int[,] mat = new int[packageItemsCount + 1, maxWeight + 1];

            // Fill the first column with 0s
            for (var row = 0; row < maxWeight + 1; row++)
            {
                mat[0, row] = 0;
            }

            // Fill the first row with 0s
            for (var col = 0; col < packageItemsCount + 1; col++)
            {
                mat[col, 0] = 0;
            }

            // Main logic for building the matrix based on the previous row and the remaining package capacity
            // First, we will loop through each row, in each column in each row will calculate if the current
            // capacity can fit for the current item or not, if yes, we will add it, if not we will keep the value
            // from the previous row at that capacity
            for (currentItem = 1; currentItem <= packageItemsCount; currentItem++)
            {
                for (currentWeight = 1; currentWeight <= maxWeight; currentWeight++)
                {
                    // We need to get the previous value at the same/current capacity
                    // We can safely execute the next line because, we have an extra row (0)
                    var maxValWithoutCurr = mat[currentItem - 1, currentWeight];
                    var maxValWithCurr = 0;

                    // We use item - 1 to account for the extra row at the top (the row we added previously)
                    var weightOfCurr = weights[currentItem - 1];

                    // Check if the current capacity can hold the current item weight
                    if (currentWeight >= weightOfCurr)
                    {
                        // We check if the knapsack can fit the current item
                        // If so, maxValWithCurr is at least the value of the current item
                        maxValWithCurr = values[currentItem - 1];

                        // remainingCapacity must be at least 0
                        var remainingCapacity = currentWeight - weightOfCurr;

                        // Add the maximum value obtainable with the remaining capacity
                        // Here we are checking if the remaining capacity can hold the value 
                        // from the previous item (previous row)
                        maxValWithCurr += mat[currentItem - 1, remainingCapacity];
                    }

                    // Pick the larger of the two
                    mat[currentItem, currentWeight] = Math.Max(maxValWithoutCurr, maxValWithCurr);
                }
            }

            var maxValue = mat[packageItemsCount, maxWeight];

            // Now we need to return the selected package items by their indexes
            var selectedItemIndexes = new List<int>();

            // Reset the currentWeight after the previous loop
            currentWeight = maxWeight;

            for (currentItem = packageItemsCount; currentItem > 0 && maxValue > 0; currentItem--)
            {
                if (maxValue == mat[currentItem - 1, currentWeight])
                    continue;
                else
                {
                    // This item is included.                    
                    selectedItemIndexes.Add(currentItem - 1);

                    // Since this weight is included its
                    // value is deducted
                    maxValue -= values[currentItem - 1];
                    currentWeight -= weights[currentItem - 1];
                }
            }

            return selectedItemIndexes;
        }
    }
}
