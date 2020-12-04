using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using Time_TimePeriod;

namespace Time_UnitTest
{
    [TestClass]
    public class UnitTestsTimeandTimePeriodConstructors
    {
        private static byte defaultTime = 0;

        private void AssertTime(Time t, byte expectedH, byte expectedM, byte expectedS)
        {
            Assert.AreEqual(expectedH, t.Hours);
            Assert.AreEqual(expectedM, t.Minutes);
            Assert.AreEqual(expectedS, t.Seconds);
        }

        private void AssertTimePeriod(TimePeriod tP, long expectedH, long expectedM, long expectedS)
        {
            Assert.AreEqual(expectedH, tP.Hours);
            Assert.AreEqual(expectedM, tP.Minutes);
            Assert.AreEqual(expectedS, tP.Seconds);
        }

        #region Constuctor tests
        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default_Time()
        {
            Time t = new Time();

            Assert.AreEqual(defaultTime, t.Hours);
            Assert.AreEqual(defaultTime, t.Minutes);
            Assert.AreEqual(defaultTime, t.Minutes);
        }

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default_TimePeriod()
        {
            TimePeriod tP = new TimePeriod();

            Assert.AreEqual(defaultTime, tP.Hours);
            Assert.AreEqual(defaultTime, tP.Minutes);
            Assert.AreEqual(defaultTime, tP.Minutes);
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
        [DataRow((long)100, (long)1, (long)1, (long)100, (long)1, (long)1)]
        [DataRow((long)8, (long)23, (long)59, (long)8, (long)23, (long)59)]
        [DataRow((long)12, (long)7, (long)9, (long)12, (long)7, (long)9)]
        [DataRow((long)23, (long)40, (long)54, (long)23, (long)40, (long)54)]
        public void Constructor_3param_TimePeriod(long h, long m, long s, long expectedH, long expectedM, long expectedS)
        {
            TimePeriod tP = new TimePeriod(h, m, s);

            AssertTimePeriod(tP, expectedH, expectedM, expectedS);
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
        [DataRow((long)100, (long)1, (long)100, (long)1)]
        [DataRow((long)8, (long)23, (long)8, (long)23)]
        [DataRow((long)12, (long)7, (long)12, (long)7)]
        [DataRow((long)23, (long)40, (long)23, (long)40)]
        public void Constructor_2param_TimePeriod(long h, long m, long expectedH, long expectedM)
        {
            TimePeriod tP = new TimePeriod(h, m);

            AssertTimePeriod(tP, expectedH, expectedM, expectedS: 0);
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
        [DataRow((long)100, (long)100)]
        [DataRow((long)8, (long)8)]
        [DataRow((long)12, (long)12)]
        [DataRow((long)23, (long)23)]
        public void Constructor_1param_TimePeriod(long h, long expectedH)
        {
            TimePeriod tP = new TimePeriod(h);

            AssertTimePeriod(tP, expectedH, expectedM: 0, expectedS: 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)23, (byte)9, (byte)40, "23:09:40")]
        [DataRow((byte)23, (byte)0, (byte)0, "23:00:00")]
        [DataRow((byte)9, (byte)39, (byte)8, "09:39:08")]
        public void Constructor_ToString(byte h, byte m, byte s, string time)
        {
            Time t = new Time(h, m, s);


            Assert.AreEqual(time, t.ToString());

        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((long)100, (long)1, (long)1, "100:01:01")]
        [DataRow((long)8, (long)23, (long)59, "8:23:59")]
        [DataRow((long)12, (long)7, (long)9, "12:07:09")]
        [DataRow((long)23, (long)40, (long)54, "23:40:54")]
        public void Constructor_ToString_TimePeriod(long  h, long m, long s, string timePeriod)
        {
            TimePeriod tP = new TimePeriod(h, m ,s);


            Assert.AreEqual(timePeriod, tP.ToString());
        }


        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("23:09:40", (byte)23, (byte)9, (byte)40)]
        [DataRow("23:00:00", (byte)23, (byte)0, (byte)0)]
        [DataRow("09:39:08", (byte)9, (byte)39, (byte)8)]
        public void Constructor_StringParam(string time, byte expectedH, byte expectedM, byte expectedS)
        {
            Time t = new Time(time);


            AssertTime(t, expectedH, expectedM, expectedS);

        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("100:01:01", (long)360000,(long)60, (long)1)]
        [DataRow("08:23:59", (long)28800, (long)1380, (long)59)]
        [DataRow("12:07:09", (long)43200, (long)420, (long)9)]
        [DataRow("23:40:54",(long)82800, (long)2400, (long)54)]
        public void Constructor_StringParam_TimePeriod(string timePeriod, long expectedH, long expectedM, long expectedS)
        {
            TimePeriod tP = new TimePeriod(timePeriod);

            AssertTimePeriod(tP,expectedH, expectedM, expectedS);
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
            new Time(h, m, s);
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
            new Time(h, m);
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
            new Time(h);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)38)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_DefaultMeters_ArgumentOutOfRangeException(byte h)
        {
            new Time(h);
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

        #region Arithmetic operations

        [DataTestMethod, TestCategory("Time arithmetic operations")]
        [DataRow((byte)12, (byte)30, (byte)30, (byte)14, (byte)40, (byte)40, (byte)3, (byte)11, (byte)10)]
        [DataRow((byte)2, (byte)35, (byte)30, (byte)14, (byte)10, (byte)10, (byte)16, (byte)45, (byte)40)]
        [DataRow((byte)10, (byte)55, (byte)43, (byte)17, (byte)30, (byte)20, (byte)4, (byte)26, (byte)3)]
        [DataRow((byte)11, (byte)23, (byte)6, (byte)13, (byte)18, (byte)30, (byte)0, (byte)41, (byte)36)]
        public void TimeOne_Plus_TimeTwo_Operation(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2, byte expectedH, byte expectedM, byte expectedS)
        {
            Time t1 = new Time(h1, m1, s1);
            Time t2 = new Time(h2, m2, s2);
            Time t3 = new Time(expectedH, expectedM, expectedS);

            Assert.AreEqual(t3, t1 + t2);
        }

        [DataTestMethod, TestCategory("Time arithmetic operations")]
        [DataRow((byte)12, (byte)30, (byte)20, (byte)14, (byte)35, (byte)30, (byte)21, (byte)54, (byte)50)]
        [DataRow((byte)2, (byte)35, (byte)30, (byte)14, (byte)10, (byte)10, (byte)12, (byte)25, (byte)20)]
        [DataRow((byte)10, (byte)55, (byte)43, (byte)17, (byte)30, (byte)20, (byte)17, (byte)25, (byte)23)]
        [DataRow((byte)2, (byte)5, (byte)30, (byte)14, (byte)10, (byte)10, (byte)11, (byte)55, (byte)20)]
        [DataRow((byte)11, (byte)23, (byte)6, (byte)13, (byte)18, (byte)30, (byte)22, (byte)4, (byte)36)]
        public void TimeOne_Minus_TimeTwo_Operation(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2, byte expectedH, byte expectedM, byte expectedS)
        {
            Time t1 = new Time(h1, m1, s1);
            Time t2 = new Time(h2, m2, s2);
            Time t3 = new Time(expectedH, expectedM, expectedS);

            Assert.AreEqual(t3, t1 - t2);
        }
        #endregion
    }
}
