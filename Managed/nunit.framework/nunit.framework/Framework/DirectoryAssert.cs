using System;
using System.ComponentModel;
using System.IO;
using NUnit.Framework.Constraints;

namespace NUnit.Framework
{
	// Token: 0x02000049 RID: 73
	public static class DirectoryAssert
	{
		// Token: 0x0600024A RID: 586 RVA: 0x0000644B File Offset: 0x0000464B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new static bool Equals(object a, object b)
		{
			throw new InvalidOperationException("DirectoryAssert.Equals should not be used for Assertions");
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00006457 File Offset: 0x00004657
		public new static void ReferenceEquals(object a, object b)
		{
			throw new InvalidOperationException("DirectoryAssert.ReferenceEquals should not be used for Assertions");
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00006463 File Offset: 0x00004663
		public static void AreEqual(DirectoryInfo expected, DirectoryInfo actual, string message, params object[] args)
		{
			Assert.AreEqual(expected, actual, message, args);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000646E File Offset: 0x0000466E
		public static void AreEqual(DirectoryInfo expected, DirectoryInfo actual)
		{
			DirectoryAssert.AreEqual(expected, actual, string.Empty, null);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000647D File Offset: 0x0000467D
		public static void AreNotEqual(DirectoryInfo expected, DirectoryInfo actual, string message, params object[] args)
		{
			Assert.AreNotEqual(expected, actual, message, args);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00006488 File Offset: 0x00004688
		public static void AreNotEqual(DirectoryInfo expected, DirectoryInfo actual)
		{
			DirectoryAssert.AreNotEqual(expected, actual, string.Empty, null);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00006497 File Offset: 0x00004697
		public static void Exists(DirectoryInfo actual, string message, params object[] args)
		{
			Assert.That<DirectoryInfo>(actual, new FileOrDirectoryExistsConstraint().IgnoreFiles, message, args);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000064AB File Offset: 0x000046AB
		public static void Exists(DirectoryInfo actual)
		{
			DirectoryAssert.Exists(actual, string.Empty, null);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000064B9 File Offset: 0x000046B9
		public static void Exists(string actual, string message, params object[] args)
		{
			Assert.That<string>(actual, new FileOrDirectoryExistsConstraint().IgnoreFiles, message, args);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000064CD File Offset: 0x000046CD
		public static void Exists(string actual)
		{
			DirectoryAssert.Exists(actual, string.Empty, null);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000064DB File Offset: 0x000046DB
		public static void DoesNotExist(DirectoryInfo actual, string message, params object[] args)
		{
			Assert.That<DirectoryInfo>(actual, new NotConstraint(new FileOrDirectoryExistsConstraint().IgnoreFiles), message, args);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000064F4 File Offset: 0x000046F4
		public static void DoesNotExist(DirectoryInfo actual)
		{
			DirectoryAssert.DoesNotExist(actual, string.Empty, null);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00006502 File Offset: 0x00004702
		public static void DoesNotExist(string actual, string message, params object[] args)
		{
			Assert.That<string>(actual, new NotConstraint(new FileOrDirectoryExistsConstraint().IgnoreFiles), message, args);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000651B File Offset: 0x0000471B
		public static void DoesNotExist(string actual)
		{
			DirectoryAssert.DoesNotExist(actual, string.Empty, null);
		}
	}
}
