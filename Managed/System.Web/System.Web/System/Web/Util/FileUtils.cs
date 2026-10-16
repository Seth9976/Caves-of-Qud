using System;
using System.Diagnostics;
using System.IO;

namespace System.Web.Util
{
	// Token: 0x02000139 RID: 313
	internal sealed class FileUtils
	{
		// Token: 0x06000E53 RID: 3667 RVA: 0x00026C91 File Offset: 0x00024E91
		internal static object CreateTemporaryFile(string tempdir, FileUtils.CreateTempFile createFile)
		{
			return FileUtils.CreateTemporaryFile(tempdir, null, null, createFile);
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00026C9C File Offset: 0x00024E9C
		internal static object CreateTemporaryFile(string tempdir, string extension, FileUtils.CreateTempFile createFile)
		{
			return FileUtils.CreateTemporaryFile(tempdir, null, extension, createFile);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00026CA8 File Offset: 0x00024EA8
		internal static object CreateTemporaryFile(string tempdir, string prefix, string extension, FileUtils.CreateTempFile createFile)
		{
			if (tempdir == null || tempdir.Length == 0)
			{
				return null;
			}
			if (createFile == null)
			{
				return null;
			}
			object obj = null;
			do
			{
				Random random = FileUtils.rnd;
				int num;
				lock (random)
				{
					num = FileUtils.rnd.Next();
				}
				string text = Path.Combine(tempdir, string.Format("{0}{1}{2}", (prefix != null) ? (prefix + ".") : "", num.ToString("x", Helpers.InvariantCulture), (extension != null) ? ("." + extension) : ""));
				try
				{
					obj = createFile(text);
				}
				catch (IOException)
				{
				}
				catch
				{
					throw;
				}
			}
			while (obj == null);
			return obj;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DEVEL")]
		public static void WriteLineLog(string logFilePath, string format, params object[] parms)
		{
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00026D80 File Offset: 0x00024F80
		[Conditional("DEVEL")]
		public static void WriteLog(string logFilePath, string format, params object[] parms)
		{
			using (TextWriter textWriter = new StreamWriter((logFilePath != null && logFilePath.Length > 0) ? logFilePath : Path.Combine(Path.GetTempPath(), "System.Web.log"), true))
			{
				if (parms != null && parms.Length != 0)
				{
					textWriter.Write(format, parms);
				}
				else
				{
					textWriter.Write(format);
				}
			}
		}

		// Token: 0x040011FB RID: 4603
		private static Random rnd = new Random();

		// Token: 0x0200013A RID: 314
		// (Invoke) Token: 0x06000E5B RID: 3675
		internal delegate object CreateTempFile(string path);
	}
}
