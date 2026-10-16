using System;
using System.ComponentModel;
using System.IO;
using NUnit.Framework.Constraints;

namespace NUnit.Framework
{
	// Token: 0x02000050 RID: 80
	public static class FileAssert
	{
		// Token: 0x06000275 RID: 629 RVA: 0x000065C2 File Offset: 0x000047C2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new static bool Equals(object a, object b)
		{
			throw new InvalidOperationException("FileAssert.Equals should not be used for Assertions");
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000065CE File Offset: 0x000047CE
		public new static void ReferenceEquals(object a, object b)
		{
			throw new InvalidOperationException("FileAssert.ReferenceEquals should not be used for Assertions");
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000065DA File Offset: 0x000047DA
		public static void AreEqual(Stream expected, Stream actual, string message, params object[] args)
		{
			Assert.That<Stream>(actual, Is.EqualTo(expected), message, args);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000065EA File Offset: 0x000047EA
		public static void AreEqual(Stream expected, Stream actual)
		{
			FileAssert.AreEqual(expected, actual, string.Empty, null);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000065FC File Offset: 0x000047FC
		public static void AreEqual(FileInfo expected, FileInfo actual, string message, params object[] args)
		{
			using (FileStream fileStream = expected.OpenRead())
			{
				using (FileStream fileStream2 = actual.OpenRead())
				{
					FileAssert.AreEqual(fileStream, fileStream2, message, args);
				}
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00006654 File Offset: 0x00004854
		public static void AreEqual(FileInfo expected, FileInfo actual)
		{
			FileAssert.AreEqual(expected, actual, string.Empty, null);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00006664 File Offset: 0x00004864
		public static void AreEqual(string expected, string actual, string message, params object[] args)
		{
			using (FileStream fileStream = File.OpenRead(expected))
			{
				using (FileStream fileStream2 = File.OpenRead(actual))
				{
					FileAssert.AreEqual(fileStream, fileStream2, message, args);
				}
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000066BC File Offset: 0x000048BC
		public static void AreEqual(string expected, string actual)
		{
			FileAssert.AreEqual(expected, actual, string.Empty, null);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000066CB File Offset: 0x000048CB
		public static void AreNotEqual(Stream expected, Stream actual, string message, params object[] args)
		{
			Assert.That<Stream>(actual, Is.Not.EqualTo(expected), message, args);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000066E0 File Offset: 0x000048E0
		public static void AreNotEqual(Stream expected, Stream actual)
		{
			FileAssert.AreNotEqual(expected, actual, string.Empty, null);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000066F0 File Offset: 0x000048F0
		public static void AreNotEqual(FileInfo expected, FileInfo actual, string message, params object[] args)
		{
			using (FileStream fileStream = expected.OpenRead())
			{
				using (FileStream fileStream2 = actual.OpenRead())
				{
					FileAssert.AreNotEqual(fileStream, fileStream2, message, args);
				}
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00006748 File Offset: 0x00004948
		public static void AreNotEqual(FileInfo expected, FileInfo actual)
		{
			FileAssert.AreNotEqual(expected, actual, string.Empty, null);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00006758 File Offset: 0x00004958
		public static void AreNotEqual(string expected, string actual, string message, params object[] args)
		{
			using (FileStream fileStream = File.OpenRead(expected))
			{
				using (FileStream fileStream2 = File.OpenRead(actual))
				{
					FileAssert.AreNotEqual(fileStream, fileStream2, message, args);
				}
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000067B0 File Offset: 0x000049B0
		public static void AreNotEqual(string expected, string actual)
		{
			FileAssert.AreNotEqual(expected, actual, string.Empty, null);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000067BF File Offset: 0x000049BF
		public static void Exists(FileInfo actual, string message, params object[] args)
		{
			Assert.That<FileInfo>(actual, new FileOrDirectoryExistsConstraint().IgnoreDirectories, message, args);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000067D3 File Offset: 0x000049D3
		public static void Exists(FileInfo actual)
		{
			FileAssert.Exists(actual, string.Empty, null);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000067E1 File Offset: 0x000049E1
		public static void Exists(string actual, string message, params object[] args)
		{
			Assert.That<string>(actual, new FileOrDirectoryExistsConstraint().IgnoreDirectories, message, args);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000067F5 File Offset: 0x000049F5
		public static void Exists(string actual)
		{
			FileAssert.Exists(actual, string.Empty, null);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00006803 File Offset: 0x00004A03
		public static void DoesNotExist(FileInfo actual, string message, params object[] args)
		{
			Assert.That<FileInfo>(actual, new NotConstraint(new FileOrDirectoryExistsConstraint().IgnoreDirectories), message, args);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000681C File Offset: 0x00004A1C
		public static void DoesNotExist(FileInfo actual)
		{
			FileAssert.DoesNotExist(actual, string.Empty, null);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000682A File Offset: 0x00004A2A
		public static void DoesNotExist(string actual, string message, params object[] args)
		{
			Assert.That<string>(actual, new NotConstraint(new FileOrDirectoryExistsConstraint().IgnoreDirectories), message, args);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00006843 File Offset: 0x00004A43
		public static void DoesNotExist(string actual)
		{
			FileAssert.DoesNotExist(actual, string.Empty, null);
		}
	}
}
