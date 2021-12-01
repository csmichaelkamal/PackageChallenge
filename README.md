# Package Challenge

- This is a simple implementation of the well-know problem Knapsack.
- The knapsack problem is meant to solve choosing the most valuable/profitable items to be carried in certain package(case) with maximum weight capacity
- In this problem, the solution is targeting the 0-1 algorithm, which means that we either take the item or leave it
- There are other types of the knapsack problem.

## The solution

- First, we need to read the file (passed to the entry point function (Packer))
- Second, we need to parse the file line by line
    - In each line, we need to split the package items to array or list or strings
    - Then, we can parse each package item
- Third, we pass the returned parsed packages to another service (PackageValidator) to validate both Package and its Items against the business requirements
- For simplicity, the business constraints are introduced as a static values in a static class
- Fourth, we pass the valid packages to the PackageItemsSelector to select the most profitable/valuable items that fits in the package
- Finally, we print the selected packages as by thier indexes (comma separated)

