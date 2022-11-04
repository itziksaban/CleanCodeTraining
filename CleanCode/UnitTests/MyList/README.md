
## MyList - an effective dynamic List

MyList is a List that tries to support intensive `Add` operations. For that, it is implemented as follows:
It holds, internally, a list of 1000 lists of integers (kind of a matrix). The user of `MyList` decides what will be the length of each of these 1000 lists. So for example, if the user decides that the length will be 2000, we will have a list of 1000 lists, each with the length of 2000 integers. In other words, it will be a matrix of 1000 rows X 2000 cols.

At first, all 1000 lists are empty. When we call `MyList.Add` for the first time, the first list is initiated and the value is added to it. As long as this list has not reached its max length (decided by the user - 2000 in our example), we will keep adding values to it every time `MyList.Add` is being called. Once this list had reached its max length - we initiate the next list and start adding to it and so forth.  

Your job is to write unit tests to this class that will cover 100% of its code.
