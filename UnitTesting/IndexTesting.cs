using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTesting
{
    /// <summary>
    /// Summary description for IndexTesting
    /// </summary>
    [TestClass]
    public class IndexTesting
    {
        public IndexTesting()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        
    [TestMethod]
        public void testConnectionIndex()
        {
            Index testIndex = new Index();
            var result = testIndex.checkOutOfOrder();

            if(result == true)
            {
                Assert.IsTrue(result, "Index connection works");
            }
            else if (result == false)
            {
                Assert.Fail("Index cannot connect to db");
            }
        }

        [TestMethod]
        public void testTotalZero()
        {
            Index testIndex = new Index();
            var result = testIndex.checkOutOfOrderTotal(0);

            if (result == true)
            {
                Assert.IsTrue(result, "total is empty");
            }
            else if (result == false)
            {
                Assert.Fail("total is empty fail");
            }
        }

        [TestMethod]
        public void testTotalFive()
        {
            Index testIndex = new Index();
            var result = testIndex.checkOutOfOrderTotal(5);

            if (result == true)
            {
                Assert.Fail("total is not empty fail");
            }
            else if (result == false)
            {
                Assert.IsFalse(result, "total is not empty");
            }
        }
    }
}
