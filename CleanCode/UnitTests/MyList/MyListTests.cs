using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class MyListTests
    {
        [TestMethod]
        public void Add_DidNotReachEndOfRow()
        {
            var myList = new MyList(1, 2);
            myList.Add(3);

            Assert.AreEqual(3, myList.Get(0));
        }

        [TestMethod]
        public void Add_DidReachEndOfRow()
        {
            var myList = new MyList(2, 1);
            myList.Add(3);
            myList.Add(4);

            Assert.AreEqual(4, myList.Get(1));
        }

        [TestMethod]
        public void Add_DidReachEndOfCapacity_DropValue()
        {
            var myList = new MyList(1, 1);
            myList.Add(3);
            myList.Add(4);
            myList.Add(5);

            Assert.AreEqual(3, myList.Get(0));
        }
    }
}
