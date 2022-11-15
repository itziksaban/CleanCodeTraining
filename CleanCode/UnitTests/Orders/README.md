
## Orders Sender  

An online store allows users to place orders. An order consists of one or more items. Upon completion of an order, it is inserted into the "unsent orders" repository in the database. When all items in an order are currently available in the warehouse, the system periodically removes it from the "unsent orders" repository. This logic is inside the class `OrderSender`

Also, a discount of 5% is given to any order with more than 9 items, or to any users that is defined as VIP. This logic is inside the class `DiscountCalculator`

Your job is to write unit tests to this class that will cover 100% of its code.
