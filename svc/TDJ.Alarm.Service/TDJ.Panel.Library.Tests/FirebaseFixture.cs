using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDJ.Panel.Library;

namespace TDJ.Panel.Library.Tests
{
    [TestClass]
    public class FirebaseClientFixture
    {
        [ClassInitialize]
        public static void Init(TestContext context)
        {
        }

        [TestMethod]
        public void FirebaseClientTest()
        {
            var f = new FirebaseClient();
        }
    }
}
