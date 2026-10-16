using System;
using System.ComponentModel;

namespace NUnit.Framework
{
	// Token: 0x0200005A RID: 90
	public class StringAssert
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x00006B30 File Offset: 0x00004D30
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new static bool Equals(object a, object b)
		{
			throw new InvalidOperationException("StringAssert.Equals should not be used for Assertions");
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00006B3C File Offset: 0x00004D3C
		public new static void ReferenceEquals(object a, object b)
		{
			throw new InvalidOperationException("StringAssert.ReferenceEquals should not be used for Assertions");
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00006B48 File Offset: 0x00004D48
		public static void Contains(string expected, string actual, string message, params object[] args)
		{
			Assert.That<string>(actual, Does.Contain(expected), message, args);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00006B58 File Offset: 0x00004D58
		public static void Contains(string expected, string actual)
		{
			StringAssert.Contains(expected, actual, string.Empty, null);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00006B67 File Offset: 0x00004D67
		public static void DoesNotContain(string expected, string actual, string message, params object[] args)
		{
			Assert.That<string>(actual, Does.Not.Contain(expected), message, args);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00006B7C File Offset: 0x00004D7C
		public static void DoesNotContain(string expected, string actual)
		{
			StringAssert.DoesNotContain(expected, actual, string.Empty, null);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00006B8B File Offset: 0x00004D8B
		public static void StartsWith(string expected, string actual, string message, params object[] args)
		{
			Assert.That<string>(actual, Does.StartWith(expected), message, args);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00006B9B File Offset: 0x00004D9B
		public static void StartsWith(string expected, string actual)
		{
			StringAssert.StartsWith(expected, actual, string.Empty, null);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00006BAA File Offset: 0x00004DAA
		public static void DoesNotStartWith(string expected, string actual, string message, params object[] args)
		{
			Assert.That<string>(actual, Does.Not.StartWith(expected), message, args);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00006BBF File Offset: 0x00004DBF
		public static void DoesNotStartWith(string expected, string actual)
		{
			StringAssert.DoesNotStartWith(expected, actual, string.Empty, null);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00006BCE File Offset: 0x00004DCE
		public static void EndsWith(string expected, string actual, string message, params object[] args)
		{
			Assert.That<string>(actual, Does.EndWith(expected), message, args);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00006BDE File Offset: 0x00004DDE
		public static void EndsWith(string expected, string actual)
		{
			StringAssert.EndsWith(expected, actual, string.Empty, null);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00006BED File Offset: 0x00004DED
		public static void DoesNotEndWith(string expected, string actual, string message, params object[] args)
		{
			Assert.That<string>(actual, Does.Not.EndWith(expected), message, args);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00006C02 File Offset: 0x00004E02
		public static void DoesNotEndWith(string expected, string actual)
		{
			StringAssert.DoesNotEndWith(expected, actual, string.Empty, null);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00006C11 File Offset: 0x00004E11
		public static void AreEqualIgnoringCase(string expected, string actual, string message, params object[] args)
		{
			Assert.That<string>(actual, Is.EqualTo(expected).IgnoreCase, message, args);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00006C26 File Offset: 0x00004E26
		public static void AreEqualIgnoringCase(string expected, string actual)
		{
			StringAssert.AreEqualIgnoringCase(expected, actual, string.Empty, null);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00006C35 File Offset: 0x00004E35
		public static void AreNotEqualIgnoringCase(string expected, string actual, string message, params object[] args)
		{
			Assert.That<string>(actual, Is.Not.EqualTo(expected).IgnoreCase, message, args);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00006C4F File Offset: 0x00004E4F
		public static void AreNotEqualIgnoringCase(string expected, string actual)
		{
			StringAssert.AreNotEqualIgnoringCase(expected, actual, string.Empty, null);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00006C5E File Offset: 0x00004E5E
		public static void IsMatch(string pattern, string actual, string message, params object[] args)
		{
			Assert.That<string>(actual, Does.Match(pattern), message, args);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00006C6E File Offset: 0x00004E6E
		public static void IsMatch(string pattern, string actual)
		{
			StringAssert.IsMatch(pattern, actual, string.Empty, null);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00006C7D File Offset: 0x00004E7D
		public static void DoesNotMatch(string pattern, string actual, string message, params object[] args)
		{
			Assert.That<string>(actual, Does.Not.Match(pattern), message, args);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00006C92 File Offset: 0x00004E92
		public static void DoesNotMatch(string pattern, string actual)
		{
			StringAssert.DoesNotMatch(pattern, actual, string.Empty, null);
		}
	}
}
