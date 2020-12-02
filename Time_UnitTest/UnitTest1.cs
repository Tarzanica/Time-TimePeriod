using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using Time_TimePeriod;

namespace Time_UnitTest
{
    [TestClass]
    public class UnitTestsTimeConstructors
    {
        private static byte defaultTime = 0;

        private void AssertTime(Time t, byte expectedH, byte expectedM, byte expectedS)
        {
            Assert.AreEqual(expectedH, t.Hours);
            Assert.AreEqual(expectedM, t.Minutes);
            Assert.AreEqual(expectedS, t.Seconds);
        }

        #region Constuctor tests
        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default()
        {
            Time t = new Time();

            Assert.AreEqual(defaultTime, t.Hours);
            Assert.AreEqual(defaultTime, t.Minutes);
            Assert.AreEqual(defaultTime, t.Minutes);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)1,(byte) 1, (byte)1, (byte)1, (byte)1, (byte)1)]
        [DataRow((byte)8, (byte)23, (byte)59, (byte)8, (byte)23, (byte)59)]
        [DataRow((byte)12, (byte)7, (byte)9, (byte)12, (byte)7, (byte)9)]
        [DataRow((byte)23, (byte)40, (byte)54, (byte)23, (byte)40, (byte)54)]
        public void Constructor_3param(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS)
        {
            Time t = new Time(h, m, s);

            AssertTime(t, expectedH, expectedM, expectedS);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)1, (byte)1, (byte)1, (byte)1)]
        [DataRow((byte)8, (byte)23, (byte)8, (byte)23)]
        [DataRow((byte)12, (byte)7, (byte)12, (byte)7)]
        [DataRow((byte)23, (byte)40, (byte)23, (byte)40)]
        public void Constructor_2param(byte h, byte m, byte expectedH, byte expectedM)
        {
            Time t = new Time(h, m);

            AssertTime(t, expectedH, expectedM, expectedS: 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)1, (byte)1)]
        [DataRow((byte)8, (byte)8)]
        [DataRow((byte)12, (byte)12)]
        [DataRow((byte)23, (byte)23)]
        public void Constructor_1param(byte h, byte expectedH)
        {
            Time t = new Time(h);

            AssertTime(t, expectedH, expectedM: 0, expectedS: 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("23:09:40", (byte)23, (byte)9, (byte)40)]
        [DataRow("23:00:00", (byte)23, (byte)0, (byte)0)]
        [DataRow("09:39:08", (byte)9, (byte)39, (byte)8)]
        public void Constructor_ToString(string time, byte expectedH, byte expectedM, byte expectedS)
        {
            Time t = new Time(time);


            AssertTime(t, expectedH, expectedM, expectedS);

        }

        // -----------

        public static IEnumerable<object[]> DataSetTime_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] { (byte)25, (byte)70, (byte)1 },
            new object[] { (byte)1, (byte)77, (byte)90 },
            new object[] { (byte)13, (byte)80, (byte)8 },
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSetTime_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3param_Default_ArgumentOutOfRangeException(byte h, byte m, byte s)
        {
            Time t = new Time(h, m, s);
        }

        public static IEnumerable<object[]> DataSetTime2param_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] { (byte)25, (byte)70 },
            new object[] { (byte)30, (byte)77 },
            new object[] { (byte)155, (byte)80 },
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSetTime2param_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2param_ArgumentOutOfRangeException(byte h, byte m)
        {
            Time t = new Time(h, m);
        }

        public static IEnumerable<object[]> DataSetTime1param_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] { (byte)245 },
            new object[] { (byte)50 },
            new object[] { (byte)75 },
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSetTime1param_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_ArgumentOutOfRangeException(byte h)
        {
            Time t = new Time(h);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)38)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_DefaultMeters_ArgumentOutOfRangeException(byte h)
        {
            Time t = new Time(h);
        }


        #endregion

        #region ToString tests

        [TestMethod, TestCategory("String representation")]
        public void ToString()
        {
            var t = new Time(4, 13);
            string expectedString = "04:13:00";

            Assert.AreEqual(expectedString, t.ToString());
        }

        #endregion

        #region Equals 
        // ToDo
        [DataTestMethod]
        [DataRow((byte)4, (byte)23, (byte)4, (byte)4, (byte)23, (byte)4, true)]
        [DataRow((byte)12, (byte)3, (byte)59, (byte)4, (byte)10, (byte)24, false)]
        public void Equals(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS, bool expectedResult)
        {
            Time t1 = new Time(h, m, s);
            Time t2 = new Time(expectedH, expectedM, expectedS);

            Assert.AreEqual(expectedResult, t1.Equals(t2));
        }

        #endregion

        #region Operators overloading

        [DataTestMethod, TestCategory("Overloading")]
        [DataRow((byte)4, (byte)23, (byte)4, (byte)4, (byte)23, (byte)4, true)]
        [DataRow((byte)12, (byte)3, (byte)59, (byte)12, (byte)10, (byte)24, false)]
        public void EqualsOperator(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS, bool expectedResult)
        {
            Time t1 = new Time(h, m, s);
            Time t2 = new Time(expectedH, expectedM, expectedS);

            Assert.AreEqual(expectedResult, t1 == t2);
        }

        [DataTestMethod, TestCategory("Overloading")]
        [DataRow((byte)4, (byte)23, (byte)2, (byte)4, (byte)23, (byte)4, true)]
        [DataRow((byte)12, (byte)3, (byte)59, (byte)4, (byte)10, (byte)24, true)]
        public void NotEqualOperator(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS, bool expectedResult)
        {
            Time t1 = new Time(h, m, s);
            Time t2 = new Time(expectedH, expectedM, expectedS);

            Assert.AreEqual(expectedResult, t1 != t2);
        }

        [DataTestMethod, TestCategory("Overloading")]
        [DataRow((byte)4, (byte)23, (byte)2, (byte)4, (byte)23, (byte)4, false)]
        [DataRow((byte)12, (byte)3, (byte)59, (byte)4, (byte)10, (byte)24, true)]
        public void MoreThanOperator(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS, bool expectedResult)
        {
            Time t1 = new Time(h, m, s);
            Time t2 = new Time(expectedH, expectedM, expectedS);

            Assert.AreEqual(expectedResult, t1 > t2);
        }

        [DataTestMethod, TestCategory("Overloading")]
        [DataRow((byte)4, (byte)23, (byte)2, (byte)4, (byte)23, (byte)4, true)]
        [DataRow((byte)12, (byte)3, (byte)59, (byte)4, (byte)10, (byte)24, false)]
        public void LessThanOperator(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS, bool expectedResult)
        {
            Time t1 = new Time(h, m, s);
            Time t2 = new Time(expectedH, expectedM, expectedS);

            Assert.AreEqual(expectedResult, t1 < t2);
        }

        [DataTestMethod, TestCategory("Overloading")]
        [DataRow((byte)4, (byte)23, (byte)4, (byte)4, (byte)23, (byte)4, true)]
        [DataRow((byte)12, (byte)3, (byte)59, (byte)4, (byte)10, (byte)24, true)]
        [DataRow((byte)3, (byte)3, (byte)59, (byte)4, (byte)10, (byte)24, false)]
        public void MoreOrEqualThanOperator(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS, bool expectedResult)
        {
            Time t1 = new Time(h, m, s);
            Time t2 = new Time(expectedH, expectedM, expectedS);

            Assert.AreEqual(expectedResult, t1 >= t2);
        }

        [DataTestMethod, TestCategory("Overloading")]
        [DataRow((byte)4, (byte)23, (byte)4, (byte)4, (byte)23, (byte)4, true)]
        [DataRow((byte)12, (byte)3, (byte)59, (byte)4, (byte)10, (byte)24, false)]
        [DataRow((byte)3, (byte)3, (byte)59, (byte)4, (byte)10, (byte)24, true)]
        public void LessOrEqualThenOperator(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS, bool expectedResult)
        {
            Time t1 = new Time(h, m, s);
            Time t2 = new Time(expectedH, expectedM, expectedS);

            Assert.AreEqual(expectedResult, t1 <= t2);
        }
        #endregion
    }
}
