using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine.Assertions.Comparers;

namespace UnityEngine.Assertions
{
	// Token: 0x0200046B RID: 1131
	[DebuggerStepThrough]
	public static class Assert
	{
		// Token: 0x06002C7A RID: 11386 RVA: 0x000541A8 File Offset: 0x000523A8
		private static void Fail(string message, string userMessage)
		{
			bool flag = !Assert.raiseExceptions;
			if (flag)
			{
				bool flag2 = message == null;
				if (flag2)
				{
					message = "Assertion has failed\n";
				}
				bool flag3 = userMessage != null;
				if (flag3)
				{
					message = userMessage + "\n" + message;
				}
				Debug.LogAssertion(message);
				return;
			}
			throw new AssertionException(message, userMessage);
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000541F9 File Offset: 0x000523F9
		[Obsolete("Assert.Equals should not be used for Assertions", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new static bool Equals(object obj1, object obj2)
		{
			throw new InvalidOperationException("Assert.Equals should not be used for Assertions");
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x00054206 File Offset: 0x00052406
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Assert.ReferenceEquals should not be used for Assertions", true)]
		public new static bool ReferenceEquals(object obj1, object obj2)
		{
			throw new InvalidOperationException("Assert.ReferenceEquals should not be used for Assertions");
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x00054214 File Offset: 0x00052414
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsTrue(bool condition)
		{
			bool flag = !condition;
			if (flag)
			{
				Assert.IsTrue(condition, null);
			}
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x00054234 File Offset: 0x00052434
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsTrue(bool condition, string message)
		{
			bool flag = !condition;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.BooleanFailureMessage(true), message);
			}
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x00054258 File Offset: 0x00052458
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsFalse(bool condition)
		{
			if (condition)
			{
				Assert.IsFalse(condition, null);
			}
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x00054274 File Offset: 0x00052474
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsFalse(bool condition, string message)
		{
			if (condition)
			{
				Assert.Fail(AssertionMessageUtil.BooleanFailureMessage(false), message);
			}
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x00054294 File Offset: 0x00052494
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual)
		{
			Assert.AreEqual<float>(expected, actual, null, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x000542A5 File Offset: 0x000524A5
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual, string message)
		{
			Assert.AreEqual<float>(expected, actual, message, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x000542B6 File Offset: 0x000524B6
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual, float tolerance)
		{
			Assert.AreApproximatelyEqual(expected, actual, tolerance, null);
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x000542C3 File Offset: 0x000524C3
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual, float tolerance, string message)
		{
			Assert.AreEqual<float>(expected, actual, message, new FloatComparer(tolerance));
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000542D5 File Offset: 0x000524D5
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual)
		{
			Assert.AreNotEqual<float>(expected, actual, null, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000542E6 File Offset: 0x000524E6
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual, string message)
		{
			Assert.AreNotEqual<float>(expected, actual, message, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000542F7 File Offset: 0x000524F7
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual, float tolerance)
		{
			Assert.AreNotApproximatelyEqual(expected, actual, tolerance, null);
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x00054304 File Offset: 0x00052504
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual, float tolerance, string message)
		{
			Assert.AreNotEqual<float>(expected, actual, message, new FloatComparer(tolerance));
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x00054316 File Offset: 0x00052516
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual)
		{
			Assert.AreEqual<T>(expected, actual, null);
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x00054322 File Offset: 0x00052522
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual, string message)
		{
			Assert.AreEqual<T>(expected, actual, message, EqualityComparer<T>.Default);
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x00054334 File Offset: 0x00052534
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual, string message, IEqualityComparer<T> comparer)
		{
			bool flag = typeof(Object).IsAssignableFrom(typeof(T));
			if (flag)
			{
				Assert.AreEqual(expected as Object, actual as Object, message);
			}
			else
			{
				bool flag2 = !comparer.Equals(actual, expected);
				if (flag2)
				{
					Assert.Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, true), message);
				}
			}
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x000543A8 File Offset: 0x000525A8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(Object expected, Object actual, string message)
		{
			bool flag = actual != expected;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, true), message);
			}
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000543D0 File Offset: 0x000525D0
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual<T>(T expected, T actual)
		{
			Assert.AreNotEqual<T>(expected, actual, null);
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000543DC File Offset: 0x000525DC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual<T>(T expected, T actual, string message)
		{
			Assert.AreNotEqual<T>(expected, actual, message, EqualityComparer<T>.Default);
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000543F0 File Offset: 0x000525F0
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual<T>(T expected, T actual, string message, IEqualityComparer<T> comparer)
		{
			bool flag = typeof(Object).IsAssignableFrom(typeof(T));
			if (flag)
			{
				Assert.AreNotEqual(expected as Object, actual as Object, message);
			}
			else
			{
				bool flag2 = comparer.Equals(actual, expected);
				if (flag2)
				{
					Assert.Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, false), message);
				}
			}
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x00054460 File Offset: 0x00052660
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(Object expected, Object actual, string message)
		{
			bool flag = actual == expected;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, false), message);
			}
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x00054488 File Offset: 0x00052688
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNull<T>(T value) where T : class
		{
			Assert.IsNull<T>(value, null);
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x00054494 File Offset: 0x00052694
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNull<T>(T value, string message) where T : class
		{
			bool flag = typeof(Object).IsAssignableFrom(typeof(T));
			if (flag)
			{
				Assert.IsNull(value as Object, message);
			}
			else
			{
				bool flag2 = value != null;
				if (flag2)
				{
					Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, true), message);
				}
			}
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x000544F8 File Offset: 0x000526F8
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNull(Object value, string message)
		{
			bool flag = value != null;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, true), message);
			}
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x0005451F File Offset: 0x0005271F
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNotNull<T>(T value) where T : class
		{
			Assert.IsNotNull<T>(value, null);
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x0005452C File Offset: 0x0005272C
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNotNull<T>(T value, string message) where T : class
		{
			bool flag = typeof(Object).IsAssignableFrom(typeof(T));
			if (flag)
			{
				Assert.IsNotNull(value as Object, message);
			}
			else
			{
				bool flag2 = value == null;
				if (flag2)
				{
					Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, false), message);
				}
			}
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x00054590 File Offset: 0x00052790
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNotNull(Object value, string message)
		{
			bool flag = value == null;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, false), message);
			}
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000545B8 File Offset: 0x000527B8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(sbyte expected, sbyte actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<sbyte>(expected, actual, null);
			}
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x000545DC File Offset: 0x000527DC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(sbyte expected, sbyte actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<sbyte>(expected, actual, message);
			}
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x00054600 File Offset: 0x00052800
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(sbyte expected, sbyte actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<sbyte>(expected, actual, null);
			}
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x00054620 File Offset: 0x00052820
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(sbyte expected, sbyte actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<sbyte>(expected, actual, message);
			}
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x00054640 File Offset: 0x00052840
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(byte expected, byte actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<byte>(expected, actual, null);
			}
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x00054664 File Offset: 0x00052864
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(byte expected, byte actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<byte>(expected, actual, message);
			}
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x00054688 File Offset: 0x00052888
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(byte expected, byte actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<byte>(expected, actual, null);
			}
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x000546A8 File Offset: 0x000528A8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(byte expected, byte actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<byte>(expected, actual, message);
			}
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x000546C8 File Offset: 0x000528C8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(char expected, char actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<char>(expected, actual, null);
			}
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000546EC File Offset: 0x000528EC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(char expected, char actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<char>(expected, actual, message);
			}
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x00054710 File Offset: 0x00052910
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(char expected, char actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<char>(expected, actual, null);
			}
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x00054730 File Offset: 0x00052930
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(char expected, char actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<char>(expected, actual, message);
			}
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x00054750 File Offset: 0x00052950
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(short expected, short actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<short>(expected, actual, null);
			}
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x00054774 File Offset: 0x00052974
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(short expected, short actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<short>(expected, actual, message);
			}
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x00054798 File Offset: 0x00052998
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(short expected, short actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<short>(expected, actual, null);
			}
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000547B8 File Offset: 0x000529B8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(short expected, short actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<short>(expected, actual, message);
			}
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000547D8 File Offset: 0x000529D8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(ushort expected, ushort actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<ushort>(expected, actual, null);
			}
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000547FC File Offset: 0x000529FC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(ushort expected, ushort actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<ushort>(expected, actual, message);
			}
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x00054820 File Offset: 0x00052A20
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(ushort expected, ushort actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<ushort>(expected, actual, null);
			}
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x00054840 File Offset: 0x00052A40
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(ushort expected, ushort actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<ushort>(expected, actual, message);
			}
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x00054860 File Offset: 0x00052A60
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(int expected, int actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<int>(expected, actual, null);
			}
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x00054884 File Offset: 0x00052A84
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(int expected, int actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<int>(expected, actual, message);
			}
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x000548A8 File Offset: 0x00052AA8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(int expected, int actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<int>(expected, actual, null);
			}
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x000548C8 File Offset: 0x00052AC8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(int expected, int actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<int>(expected, actual, message);
			}
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x000548E8 File Offset: 0x00052AE8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(uint expected, uint actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<uint>(expected, actual, null);
			}
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x0005490C File Offset: 0x00052B0C
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(uint expected, uint actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<uint>(expected, actual, message);
			}
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x00054930 File Offset: 0x00052B30
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(uint expected, uint actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<uint>(expected, actual, null);
			}
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x00054950 File Offset: 0x00052B50
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(uint expected, uint actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<uint>(expected, actual, message);
			}
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x00054970 File Offset: 0x00052B70
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(long expected, long actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<long>(expected, actual, null);
			}
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x00054994 File Offset: 0x00052B94
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(long expected, long actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<long>(expected, actual, message);
			}
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x000549B8 File Offset: 0x00052BB8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(long expected, long actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<long>(expected, actual, null);
			}
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000549D8 File Offset: 0x00052BD8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(long expected, long actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<long>(expected, actual, message);
			}
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000549F8 File Offset: 0x00052BF8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(ulong expected, ulong actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<ulong>(expected, actual, null);
			}
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x00054A1C File Offset: 0x00052C1C
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(ulong expected, ulong actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<ulong>(expected, actual, message);
			}
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x00054A40 File Offset: 0x00052C40
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(ulong expected, ulong actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<ulong>(expected, actual, null);
			}
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x00054A60 File Offset: 0x00052C60
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(ulong expected, ulong actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<ulong>(expected, actual, message);
			}
		}

		// Token: 0x04000C54 RID: 3156
		internal const string UNITY_ASSERTIONS = "UNITY_ASSERTIONS";

		// Token: 0x04000C55 RID: 3157
		[Obsolete("Future versions of Unity are expected to always throw exceptions and not have this field.")]
		public static bool raiseExceptions = true;
	}
}
