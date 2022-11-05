
## MyList - an effective dynamic List

MyList is a List that tries to support intensive `Add` operations. For that, it is implemented as follows:
It holds, internally, an array of 1000 arrays of integers (kind of a matrix). The user of `MyList` decides what will be the length of each of these 1000 arrays. So for example, if the user decides that the length will be 2000, we will have an array of 1000 arrays, each with the length of 2000. In other words, it will be a matrix of 1000 rows X 2000 cols.

At first, all 1000 arrays are empty exept for the first one (so actually 999 are empty). When we call `MyList.Add` for the first time, the value is added to first array. As long as this array has not reached its max length (decided by the user - 2000 in our example), we will keep adding values to it every time `MyList.Add` is being called. Once this array had reached its max length - we initiate the next array and start adding to it and so forth.  

Your job is to write unit tests to this class that will cover 100% of its code.
