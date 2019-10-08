using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTesting
{
    /// <summary>
    /// Summary description for LoginTesting
    /// </summary>
    [TestClass]
    public class LoginTesting
    {
        public LoginTesting()
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
        public void testPinCorrect()
        {
            Login testLogin = new Login();
            var result = testLogin.checkApi(1111);

            if (result == 1)
            {
                Assert.IsTrue(true, "Correct pin works");
            }
            else if (result == 0)
            {
                Assert.Fail("Correct pin does not work");
            }
        }

        [TestMethod]
        public void testPinWrongNum()
        {
            Login testLogin = new Login();
            var result = testLogin.checkApi(2030);

            if (result == 1)
            {
                Assert.Fail("Incorrect pin works");
            }
            else if (result == 0)
            {
                Assert.IsFalse(false, "Incorrect pin does not work");
            }
        }
    }
}
