using System;
using System.Diagnostics;

namespace UnityEngine.Assertions.Must
{
	// Token: 0x0200046E RID: 1134
	[DebuggerStepThrough]
	[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
	public static class MustExtensions
	{
		// Token: 0x06002CC4 RID: 11460 RVA: 0x00054C3C File Offset: 0x00052E3C
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		[Conditional("UNITY_ASSERTIONS")]
		public static void MustBeTrue(this bool value)
		{
			Assert.IsTrue(value);
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x00054C46 File Offset: 0x00052E46
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustBeTrue(this bool value, string message)
		{
			Assert.IsTrue(value, message);
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x00054C51 File Offset: 0x00052E51
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustBeFalse(this bool value)
		{
			Assert.IsFalse(value);
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x00054C5B File Offset: 0x00052E5B
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustBeFalse(this bool value, string message)
		{
			Assert.IsFalse(value, message);
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x00054C66 File Offset: 0x00052E66
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustBeApproximatelyEqual(this float actual, float expected)
		{
			Assert.AreApproximatelyEqual(actual, expected);
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x00054C71 File Offset: 0x00052E71
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustBeApproximatelyEqual(this float actual, float expected, string message)
		{
			Assert.AreApproximatelyEqual(actual, expected, message);
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x00054C7D File Offset: 0x00052E7D
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustBeApproximatelyEqual(this float actual, float expected, float tolerance)
		{
			Assert.AreApproximatelyEqual(actual, expected, tolerance);
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x00054C89 File Offset: 0x00052E89
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustBeApproximatelyEqual(this float actual, float expected, float tolerance, string message)
		{
			Assert.AreApproximatelyEqual(expected, actual, tolerance, message);
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x00054C96 File Offset: 0x00052E96
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustNotBeApproximatelyEqual(this float actual, float expected)
		{
			Assert.AreNotApproximatelyEqual(expected, actual);
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x00054CA1 File Offset: 0x00052EA1
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustNotBeApproximatelyEqual(this float actual, float expected, string message)
		{
			Assert.AreNotApproximatelyEqual(expected, actual, message);
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x00054CAD File Offset: 0x00052EAD
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		[Conditional("UNITY_ASSERTIONS")]
		public static void MustNotBeApproximatelyEqual(this float actual, float expected, float tolerance)
		{
			Assert.AreNotApproximatelyEqual(expected, actual, tolerance);
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x00054CB9 File Offset: 0x00052EB9
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustNotBeApproximatelyEqual(this float actual, float expected, float tolerance, string message)
		{
			Assert.AreNotApproximatelyEqual(expected, actual, tolerance, message);
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x00054CC6 File Offset: 0x00052EC6
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		[Conditional("UNITY_ASSERTIONS")]
		public static void MustBeEqual<T>(this T actual, T expected)
		{
			Assert.AreEqual<T>(actual, expected);
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x00054CD1 File Offset: 0x00052ED1
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustBeEqual<T>(this T actual, T expected, string message)
		{
			Assert.AreEqual<T>(expected, actual, message);
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x00054CDD File Offset: 0x00052EDD
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustNotBeEqual<T>(this T actual, T expected)
		{
			Assert.AreNotEqual<T>(actual, expected);
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x00054CE8 File Offset: 0x00052EE8
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		[Conditional("UNITY_ASSERTIONS")]
		public static void MustNotBeEqual<T>(this T actual, T expected, string message)
		{
			Assert.AreNotEqual<T>(expected, actual, message);
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x00054CF4 File Offset: 0x00052EF4
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustBeNull<T>(this T expected) where T : class
		{
			Assert.IsNull<T>(expected);
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x00054CFE File Offset: 0x00052EFE
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustBeNull<T>(this T expected, string message) where T : class
		{
			Assert.IsNull<T>(expected, message);
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x00054D09 File Offset: 0x00052F09
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		[Conditional("UNITY_ASSERTIONS")]
		public static void MustNotBeNull<T>(this T expected) where T : class
		{
			Assert.IsNotNull<T>(expected);
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x00054D13 File Offset: 0x00052F13
		[Conditional("UNITY_ASSERTIONS")]
		[Obsolete("Must extensions are deprecated. Use UnityEngine.Assertions.Assert instead")]
		public static void MustNotBeNull<T>(this T expected, string message) where T : class
		{
			Assert.IsNotNull<T>(expected, message);
		}
	}
}
