using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;
using XRL.Core;

namespace Platform.IO
{
	// Token: 0x0200033F RID: 831
	public class Path
	{
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x00098A32 File Offset: 0x00096C32
		public static char DirectorySeparatorChar
		{
			get
			{
				return Path.DirectorySeparatorChar;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001F32 RID: 7986 RVA: 0x00098A39 File Offset: 0x00096C39
		public static char PathSeparator
		{
			get
			{
				return Path.PathSeparator;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x00098A40 File Offset: 0x00096C40
		public static char AltDirectorySeparatorChar
		{
			get
			{
				return Path.AltDirectorySeparatorChar;
			}
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x00098A47 File Offset: 0x00096C47
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Combine(params string[] paths)
		{
			return Path.Combine(paths);
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x00098A4F File Offset: 0x00096C4F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetDirectoryName(string path)
		{
			return Path.GetDirectoryName(path);
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00098A57 File Offset: 0x00096C57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetFileName(string path)
		{
			return Path.GetFileName(path);
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x00098A5F File Offset: 0x00096C5F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetRelativePath(string relativeTo, string path)
		{
			return Path.GetRelativePath(relativeTo, path);
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x00098A68 File Offset: 0x00096C68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetFileNameWithoutExtension(string path)
		{
			return Path.GetFileNameWithoutExtension(path);
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x00098A70 File Offset: 0x00096C70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetExtension(string path)
		{
			return Path.GetExtension(path);
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x00098A78 File Offset: 0x00096C78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasExtension(string path)
		{
			return Path.HasExtension(path);
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x00098A80 File Offset: 0x00096C80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string ChangeExtension(string path, string extension)
		{
			return Path.ChangeExtension(path, extension);
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x00098A89 File Offset: 0x00096C89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetFullPath(string path)
		{
			return Path.GetFullPath(path);
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00098A91 File Offset: 0x00096C91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static char[] GetInvalidFileNameChars()
		{
			return Path.GetInvalidFileNameChars();
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x00098A98 File Offset: 0x00096C98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static char[] GetInvalidPathChars()
		{
			return Path.GetInvalidPathChars();
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00098A9F File Offset: 0x00096C9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetRootPathRelative(string relativeTo, string path)
		{
			return Path.GetRelativePath(relativeTo, path);
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x00098AA8 File Offset: 0x00096CA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Join(string path1, string path2)
		{
			return Path.Join(path1, path2);
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x00098ABB File Offset: 0x00096CBB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsPathRootedToMount(string path)
		{
			return State.GetStorage().IsPathRootedToMount(path);
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x00098AC8 File Offset: 0x00096CC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetPathMount(string path)
		{
			return State.GetStorage().GetPathMount(path);
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x00098AD5 File Offset: 0x00096CD5
		public static string PersistentDataPath()
		{
			return State.GetStorage().RootPath;
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00098AE1 File Offset: 0x00096CE1
		public static bool IsROM(string path)
		{
			return path.Contains(Application.streamingAssetsPath);
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x00098AEE File Offset: 0x00096CEE
		[Obsolete("Path handling moved back to DataManager and made absolute. Please use DataManager.SyncedPath.")]
		public static string RWSynced(string path)
		{
			return Path.Combine(new string[]
			{
				XRLCore.SyncedPath,
				path
			});
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x00098B07 File Offset: 0x00096D07
		[Obsolete("Path handling moved back to DataManager and made absolute. Please use DataManager.LocalPath.")]
		public static string RWLocal(string path)
		{
			return Path.Combine(new string[]
			{
				XRLCore.LocalPath,
				path
			});
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x00098B20 File Offset: 0x00096D20
		[Obsolete("Path handling moved back to DataManager and made absolute. Please use DataManager.SavePath.")]
		public static string RW(string path)
		{
			return path;
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x00098B24 File Offset: 0x00096D24
		public static bool IsMatch(string path, string searchPattern)
		{
			if (string.IsNullOrEmpty(searchPattern))
			{
				return string.IsNullOrEmpty(path);
			}
			if (string.IsNullOrEmpty(path) && searchPattern != "*")
			{
				return false;
			}
			int i = 0;
			int num = 0;
			int num2 = -1;
			int num3 = -1;
			while (i < path.Length)
			{
				if (num < searchPattern.Length && (searchPattern[num] == '?' || searchPattern[num] == path[i]))
				{
					i++;
					num++;
				}
				else if (num < searchPattern.Length && searchPattern[num] == '*')
				{
					num2 = num;
					num3 = i;
					num++;
				}
				else
				{
					if (num2 == -1)
					{
						return false;
					}
					num = num2 + 1;
					num3++;
					i = num3;
				}
			}
			while (num < searchPattern.Length && searchPattern[num] == '*')
			{
				num++;
			}
			return num == searchPattern.Length;
		}

		// Token: 0x04001012 RID: 4114
		private static Regex sm_rootedToDriveRegex = new Regex("^(?<mountName>[^@:|\\\\]{1,15}):", RegexOptions.Compiled);
	}
}
