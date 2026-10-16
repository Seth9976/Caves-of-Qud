using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200003C RID: 60
	[NullableContext(1)]
	[Nullable(0)]
	internal static class PathUtilities
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00006E1C File Offset: 0x0000501C
		private static string PlatformSpecificDirectorySeparator
		{
			get
			{
				if (PathUtilities.s_platformSpecificDirectorySeparator == null)
				{
					PathUtilities.s_platformSpecificDirectorySeparator = ((Array.IndexOf<char>(Path.GetInvalidFileNameChars(), '*') >= 0) ? '\\' : '/').ToString();
				}
				return PathUtilities.s_platformSpecificDirectorySeparator;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00006E58 File Offset: 0x00005058
		internal static int IndexOfFileName(string path)
		{
			if (path == null)
			{
				return -1;
			}
			for (int i = path.Length - 1; i >= 0; i--)
			{
				char c = path.get_Chars(i);
				if (c == '\\' || c == '/' || c == ':')
				{
					return i + 1;
				}
			}
			return 0;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00006E9C File Offset: 0x0000509C
		internal static string GetFileName(string path, bool includeExtension = true)
		{
			int num = PathUtilities.IndexOfFileName(path);
			if (num > 0)
			{
				return path.Substring(num);
			}
			return path;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00006EC0 File Offset: 0x000050C0
		internal static string CombinePathWithRelativePath(string root, string relativePath)
		{
			if (root.Length == 0)
			{
				return relativePath;
			}
			char c = root.get_Chars(root.Length - 1);
			if (c == '\\' || c == '/' || c == ':')
			{
				return root + relativePath;
			}
			return root + PathUtilities.PlatformSpecificDirectorySeparator + relativePath;
		}

		// Token: 0x04000195 RID: 405
		private const char DirectorySeparatorChar = '\\';

		// Token: 0x04000196 RID: 406
		private const char AltDirectorySeparatorChar = '/';

		// Token: 0x04000197 RID: 407
		private const char VolumeSeparatorChar = ':';

		// Token: 0x04000198 RID: 408
		private static string s_platformSpecificDirectorySeparator;
	}
}
