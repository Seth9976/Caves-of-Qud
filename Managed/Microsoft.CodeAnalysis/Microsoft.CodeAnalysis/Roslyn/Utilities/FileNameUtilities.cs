using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000043 RID: 67
	[NullableContext(2)]
	[Nullable(0)]
	internal static class FileNameUtilities
	{
		// Token: 0x06000157 RID: 343 RVA: 0x00005724 File Offset: 0x00003924
		internal static bool IsFileName([NotNullWhen(true)] string path)
		{
			return FileNameUtilities.IndexOfFileName(path) == 0;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005730 File Offset: 0x00003930
		private static int IndexOfExtension(string path)
		{
			if (path == null)
			{
				return -1;
			}
			int length = path.Length;
			int num = length;
			while (--num >= 0)
			{
				char c = path.get_Chars(num);
				if (c == '.')
				{
					if (num != length - 1)
					{
						return num;
					}
					return -1;
				}
				else if (c == '\\' || c == '/' || c == ':')
				{
					break;
				}
			}
			return -1;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000577C File Offset: 0x0000397C
		[return: NotNullIfNotNull("path")]
		internal static string GetExtension(string path)
		{
			if (path == null)
			{
				return null;
			}
			int num = FileNameUtilities.IndexOfExtension(path);
			if (num < 0)
			{
				return string.Empty;
			}
			return path.Substring(num);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000057A8 File Offset: 0x000039A8
		[return: NotNullIfNotNull("path")]
		private static string RemoveExtension(string path)
		{
			if (path == null)
			{
				return null;
			}
			int num = FileNameUtilities.IndexOfExtension(path);
			if (num >= 0)
			{
				return path.Substring(0, num);
			}
			if (path.Length > 0 && path.get_Chars(path.Length - 1) == '.')
			{
				return path.Substring(0, path.Length - 1);
			}
			return path;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000057FC File Offset: 0x000039FC
		[return: NotNullIfNotNull("path")]
		internal static string ChangeExtension(string path, string extension)
		{
			if (path == null)
			{
				return null;
			}
			string text = FileNameUtilities.RemoveExtension(path);
			if (extension == null || path.Length == 0)
			{
				return text;
			}
			if (extension.Length == 0 || extension.get_Chars(0) != '.')
			{
				return text + "." + extension;
			}
			return text + extension;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000584C File Offset: 0x00003A4C
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

		// Token: 0x0600015D RID: 349 RVA: 0x00005890 File Offset: 0x00003A90
		[return: NotNullIfNotNull("path")]
		internal static string GetFileName(string path, bool includeExtension = true)
		{
			int num = FileNameUtilities.IndexOfFileName(path);
			string text = ((num <= 0) ? path : path.Substring(num));
			if (!includeExtension)
			{
				return FileNameUtilities.RemoveExtension(text);
			}
			return text;
		}

		// Token: 0x0400006B RID: 107
		internal const char DirectorySeparatorChar = '\\';

		// Token: 0x0400006C RID: 108
		internal const char AltDirectorySeparatorChar = '/';

		// Token: 0x0400006D RID: 109
		internal const char VolumeSeparatorChar = ':';
	}
}
