using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicProgram.SqlHelper;
using HelperTest;

namespace UnitTestingHelper
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestSqlHelper test1 = new TestSqlHelper();
            Assert.IsNotNull(test1.ToString(), "测试失败");
        }
    }
}
