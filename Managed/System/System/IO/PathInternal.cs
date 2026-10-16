using System;
using System.Runtime.CompilerServices;

namespace System.IO
{
	// Token: 0x020004F8 RID: 1272
	internal static class PathInternal
	{
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002984 RID: 10628 RVA: 0x0008EB1D File Offset: 0x0008CD1D
		internal static StringComparison StringComparison
		{
			get
			{
				if (!PathInternal.s_isCaseSensitive)
				{
					return StringComparison.OrdinalIgnoreCase;
				}
				return StringComparison.Ordinal;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002985 RID: 10629 RVA: 0x0008EB29 File Offset: 0x0008CD29
		internal static bool IsCaseSensitive
		{
			get
			{
				return PathInternal.s_isCaseSensitive;
			}
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x0008EB30 File Offset: 0x0008CD30
		private static bool GetIsCaseSensitive()
		{
			bool flag;
			try
			{
				string text = Path.Combine(Path.GetTempPath(), "CASESENSITIVETEST" + Guid.NewGuid().ToString("N"));
				using (new FileStream(text, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.DeleteOnClose))
				{
					flag = !File.Exists(text.ToLowerInvariant());
				}
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x0006E747 File Offset: 0x0006C947
		internal static bool IsValidDriveChar(char value)
		{
			return (value >= 'A' && value <= 'Z') || (value >= 'a' && value <= 'z');
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x0008EBB8 File Offset: 0x0008CDB8
		private static bool EndsWithPeriodOrSpace(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return false;
			}
			char c = path[path.Length - 1];
			return c == ' ' || c == '.';
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x0008EBEA File Offset: 0x0008CDEA
		internal static string EnsureExtendedPrefixIfNeeded(string path)
		{
			if (path != null && (path.Length >= 260 || PathInternal.EndsWithPeriodOrSpace(path)))
			{
				return PathInternal.EnsureExtendedPrefix(path);
			}
			return path;
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x0008EC0C File Offset: 0x0008CE0C
		internal static string EnsureExtendedPrefix(string path)
		{
			if (PathInternal.IsPartiallyQualified(path) || PathInternal.IsDevice(path))
			{
				return path;
			}
			if (path.StartsWith("\\\\", StringComparison.OrdinalIgnoreCase))
			{
				return path.Insert(2, "?\\UNC\\");
			}
			return "\\\\?\\" + path;
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x0008EC48 File Offset: 0x0008CE48
		internal static bool IsDevice(string path)
		{
			return PathInternal.IsExtended(path) || (path.Length >= 4 && PathInternal.IsDirectorySeparator(path[0]) && PathInternal.IsDirectorySeparator(path[1]) && (path[2] == '.' || path[2] == '?') && PathInternal.IsDirectorySeparator(path[3]));
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x0008ECA8 File Offset: 0x0008CEA8
		internal static bool IsExtended(string path)
		{
			return path.Length >= 4 && path[0] == '\\' && (path[1] == '\\' || path[1] == '?') && path[2] == '?' && path[3] == '\\';
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x0008ECF8 File Offset: 0x0008CEF8
		internal unsafe static int GetRootLength(ReadOnlySpan<char> path)
		{
			int i = 0;
			int num = 2;
			int num2 = 2;
			bool flag = path.StartsWith<char>("\\\\?\\");
			bool flag2 = path.StartsWith<char>("\\\\?\\UNC\\");
			if (flag)
			{
				if (flag2)
				{
					num2 = "\\\\?\\UNC\\".Length;
				}
				else
				{
					num += "\\\\?\\".Length;
				}
			}
			if ((!flag || flag2) && path.Length > 0 && PathInternal.IsDirectorySeparator((char)(*path[0])))
			{
				i = 1;
				if (flag2 || (path.Length > 1 && PathInternal.IsDirectorySeparator((char)(*path[1]))))
				{
					i = num2;
					int num3 = 2;
					while (i < path.Length)
					{
						if (PathInternal.IsDirectorySeparator((char)(*path[i])) && --num3 <= 0)
						{
							break;
						}
						i++;
					}
				}
			}
			else if (path.Length >= num && *path[num - 1] == (ushort)Path.VolumeSeparatorChar)
			{
				i = num;
				if (path.Length >= num + 1 && PathInternal.IsDirectorySeparator((char)(*path[num])))
				{
					i++;
				}
			}
			return i;
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x0008EDFC File Offset: 0x0008CFFC
		internal static bool IsPartiallyQualified(string path)
		{
			if (path.Length < 2)
			{
				return true;
			}
			if (PathInternal.IsDirectorySeparator(path[0]))
			{
				return path[1] != '?' && !PathInternal.IsDirectorySeparator(path[1]);
			}
			return path.Length < 3 || path[1] != Path.VolumeSeparatorChar || !PathInternal.IsDirectorySeparator(path[2]) || !PathInternal.IsValidDriveChar(path[0]);
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x0008EE75 File Offset: 0x0008D075
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool IsDirectorySeparator(char c)
		{
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
		}

		// Token: 0x04001610 RID: 5648
		private static readonly bool s_isCaseSensitive = PathInternal.GetIsCaseSensitive();

		// Token: 0x04001611 RID: 5649
		internal const string ExtendedDevicePathPrefix = "\\\\?\\";

		// Token: 0x04001612 RID: 5650
		internal const string UncPathPrefix = "\\\\";

		// Token: 0x04001613 RID: 5651
		internal const string UncDevicePrefixToInsert = "?\\UNC\\";

		// Token: 0x04001614 RID: 5652
		internal const string UncExtendedPathPrefix = "\\\\?\\UNC\\";

		// Token: 0x04001615 RID: 5653
		internal const string DevicePathPrefix = "\\\\.\\";

		// Token: 0x04001616 RID: 5654
		internal const int MaxShortPath = 260;

		// Token: 0x04001617 RID: 5655
		internal const int DevicePrefixLength = 4;
	}
}
