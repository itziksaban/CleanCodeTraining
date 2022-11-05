
## MyList - an effective dynamic List

MyList is a List that supports intensive `Add` operations. For that purpose, it is implemented as follows: It holds, internally, an array of 1000 arrays of integers (like a matrix). It is up to the user of MyList to decide how long each of these 1000 arrays will be. So, if the user decides that the length will be 2000, we will have an array of 1000 arrays, each with a 2000 length. Thus, it will be a matrix of 1000 rows x 2000 columns.

Initially, all 1000 arrays are empty except for the first one (so actually 999 are empty). When we call `MyList.Add` for the first time, the value is added to the first array. As long as this array does not reach its max length (2000 in our example), we will keep adding values to it every time `MyList.Add` is called. Once this array had reached its max length - we initiate the next array and start adding to it and so forth.

Your job is to write unit tests to this class that will cover 100% of its code.
