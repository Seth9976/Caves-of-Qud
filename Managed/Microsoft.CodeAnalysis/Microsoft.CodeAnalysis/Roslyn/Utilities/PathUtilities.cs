using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200002D RID: 45
	[NullableContext(1)]
	[Nullable(0)]
	internal static class PathUtilities
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003856 File Offset: 0x00001A56
		internal static bool IsUnixLikePlatform
		{
			get
			{
				return PlatformInformation.IsUnix;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000385D File Offset: 0x00001A5D
		public static bool IsDirectorySeparator(char c)
		{
			return c == PathUtilities.DirectorySeparatorChar || c == '/';
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000386E File Offset: 0x00001A6E
		public static bool IsAnyDirectorySeparator(char c)
		{
			return c == '\\' || c == '/';
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000387C File Offset: 0x00001A7C
		public static string TrimTrailingSeparators(string s)
		{
			int num = s.Length;
			while (num > 0 && PathUtilities.IsDirectorySeparator(s.get_Chars(num - 1)))
			{
				num--;
			}
			if (num != s.Length)
			{
				s = s.Substring(0, num);
			}
			return s;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000038C0 File Offset: 0x00001AC0
		public static string EnsureTrailingSeparator(string s)
		{
			if (s.Length == 0 || PathUtilities.IsAnyDirectorySeparator(s.get_Chars(s.Length - 1)))
			{
				return s;
			}
			bool flag = s.IndexOf('/') >= 0;
			bool flag2 = s.IndexOf('\\') >= 0;
			if (flag && !flag2)
			{
				return s + "/";
			}
			if (!flag && flag2)
			{
				return s + "\\";
			}
			return s + PathUtilities.DirectorySeparatorChar.ToString();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003943 File Offset: 0x00001B43
		public static string GetExtension(string path)
		{
			return FileNameUtilities.GetExtension(path);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000394B File Offset: 0x00001B4B
		public static string ChangeExtension(string path, [Nullable(2)] string extension)
		{
			return FileNameUtilities.ChangeExtension(path, extension);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003954 File Offset: 0x00001B54
		public static string RemoveExtension(string path)
		{
			return FileNameUtilities.ChangeExtension(path, null);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000395D File Offset: 0x00001B5D
		[NullableContext(2)]
		[return: NotNullIfNotNull("path")]
		public static string GetFileName(string path, bool includeExtension = true)
		{
			return FileNameUtilities.GetFileName(path, includeExtension);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003966 File Offset: 0x00001B66
		[NullableContext(2)]
		public static string GetDirectoryName(string path)
		{
			return PathUtilities.GetDirectoryName(path, PathUtilities.IsUnixLikePlatform);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003974 File Offset: 0x00001B74
		[NullableContext(2)]
		internal static string GetDirectoryName(string path, bool isUnixLike)
		{
			if (path != null)
			{
				int length = PathUtilities.GetPathRoot(path, isUnixLike).Length;
				if (path.Length > length)
				{
					int i = path.Length;
					while (i > length)
					{
						i--;
						if (PathUtilities.IsDirectorySeparator(path.get_Chars(i)) && (i <= 0 || !PathUtilities.IsDirectorySeparator(path.get_Chars(i - 1))))
						{
							break;
						}
					}
					return path.Substring(0, i);
				}
			}
			return null;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000039D8 File Offset: 0x00001BD8
		internal static bool IsSameDirectoryOrChildOf(string child, string parent)
		{
			parent = PathUtilities.RemoveTrailingDirectorySeparator(parent);
			for (string text = child; text != null; text = PathUtilities.GetDirectoryName(text))
			{
				text = PathUtilities.RemoveTrailingDirectorySeparator(text);
				if (text.Equals(parent, 5))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003A0F File Offset: 0x00001C0F
		[NullableContext(2)]
		[return: NotNullIfNotNull("path")]
		public static string GetPathRoot(string path)
		{
			return PathUtilities.GetPathRoot(path, PathUtilities.IsUnixLikePlatform);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003A1C File Offset: 0x00001C1C
		[NullableContext(2)]
		[return: NotNullIfNotNull("path")]
		private static string GetPathRoot(string path, bool isUnixLike)
		{
			if (path == null)
			{
				return null;
			}
			if (isUnixLike)
			{
				return PathUtilities.GetUnixRoot(path);
			}
			return PathUtilities.GetWindowsRoot(path);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003A34 File Offset: 0x00001C34
		private static string GetWindowsRoot(string path)
		{
			int length = path.Length;
			if (length >= 1 && PathUtilities.IsDirectorySeparator(path.get_Chars(0)))
			{
				if (length < 2 || !PathUtilities.IsDirectorySeparator(path.get_Chars(1)))
				{
					return path.Substring(0, 1);
				}
				int num = 2;
				num = PathUtilities.ConsumeDirectorySeparators(path, length, num);
				bool flag = false;
				while (num != length)
				{
					if (!PathUtilities.IsDirectorySeparator(path.get_Chars(num)))
					{
						num++;
					}
					else
					{
						if (flag)
						{
							return path.Substring(0, num);
						}
						flag = true;
						num = PathUtilities.ConsumeDirectorySeparators(path, length, num);
					}
				}
				return path;
			}
			else
			{
				if (length < 2 || path.get_Chars(1) != ':')
				{
					return "";
				}
				if (length < 3 || !PathUtilities.IsDirectorySeparator(path.get_Chars(2)))
				{
					return path.Substring(0, 2);
				}
				return path.Substring(0, 3);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003AED File Offset: 0x00001CED
		private static int ConsumeDirectorySeparators(string path, int length, int i)
		{
			while (i < length && PathUtilities.IsDirectorySeparator(path.get_Chars(i)))
			{
				i++;
			}
			return i;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003B09 File Offset: 0x00001D09
		private static string GetUnixRoot(string path)
		{
			if (path.Length <= 0 || !PathUtilities.IsDirectorySeparator(path.get_Chars(0)))
			{
				return "";
			}
			return path.Substring(0, 1);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003B30 File Offset: 0x00001D30
		[NullableContext(2)]
		public static PathKind GetPathKind(string path)
		{
			if (RoslynString.IsNullOrWhiteSpace(path))
			{
				return PathKind.Empty;
			}
			if (PathUtilities.IsAbsolute(path))
			{
				return PathKind.Absolute;
			}
			if (path.Length > 0 && path.get_Chars(0) == '.')
			{
				if (path.Length == 1 || PathUtilities.IsDirectorySeparator(path.get_Chars(1)))
				{
					return PathKind.RelativeToCurrentDirectory;
				}
				if (path.get_Chars(1) == '.' && (path.Length == 2 || PathUtilities.IsDirectorySeparator(path.get_Chars(2))))
				{
					return PathKind.RelativeToCurrentParent;
				}
			}
			if (!PathUtilities.IsUnixLikePlatform)
			{
				if (path.Length >= 1 && PathUtilities.IsDirectorySeparator(path.get_Chars(0)))
				{
					return PathKind.RelativeToCurrentRoot;
				}
				if (path.Length >= 2 && path.get_Chars(1) == ':' && (path.Length <= 2 || !PathUtilities.IsDirectorySeparator(path.get_Chars(2))))
				{
					return PathKind.RelativeToDriveDirectory;
				}
			}
			return PathKind.Relative;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003BF0 File Offset: 0x00001DF0
		[NullableContext(2)]
		public static bool IsAbsolute([NotNullWhen(true)] string path)
		{
			if (RoslynString.IsNullOrEmpty(path))
			{
				return false;
			}
			if (PathUtilities.IsUnixLikePlatform)
			{
				return path.get_Chars(0) == PathUtilities.DirectorySeparatorChar;
			}
			return PathUtilities.IsDriveRootedAbsolutePath(path) || (path.Length >= 2 && PathUtilities.IsDirectorySeparator(path.get_Chars(0)) && PathUtilities.IsDirectorySeparator(path.get_Chars(1)));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003C4C File Offset: 0x00001E4C
		private static bool IsDriveRootedAbsolutePath(string path)
		{
			return path.Length >= 3 && path.get_Chars(1) == ':' && PathUtilities.IsDirectorySeparator(path.get_Chars(2));
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003C70 File Offset: 0x00001E70
		[return: Nullable(2)]
		public static string CombineAbsoluteAndRelativePaths(string root, string relativePath)
		{
			return PathUtilities.CombinePossiblyRelativeAndRelativePaths(root, relativePath);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003C7C File Offset: 0x00001E7C
		[NullableContext(2)]
		public static string CombinePossiblyRelativeAndRelativePaths(string root, string relativePath)
		{
			if (RoslynString.IsNullOrEmpty(root))
			{
				return null;
			}
			PathKind pathKind = PathUtilities.GetPathKind(relativePath);
			if (pathKind == PathKind.Empty)
			{
				return root;
			}
			if (pathKind - PathKind.RelativeToCurrentRoot > 2)
			{
				return PathUtilities.CombinePathsUnchecked(root, relativePath);
			}
			return null;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public static string CombinePathsUnchecked(string root, [Nullable(2)] string relativePath)
		{
			char c = root.get_Chars(root.Length - 1);
			if (!PathUtilities.IsDirectorySeparator(c) && c != ':')
			{
				return root + PathUtilities.DirectorySeparatorStr + relativePath;
			}
			return root + relativePath;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003CED File Offset: 0x00001EED
		[NullableContext(2)]
		[return: NotNullIfNotNull("path")]
		public static string CombinePaths(string root, string path)
		{
			if (RoslynString.IsNullOrEmpty(root))
			{
				return path;
			}
			if (RoslynString.IsNullOrEmpty(path))
			{
				return root;
			}
			if (!PathUtilities.IsAbsolute(path))
			{
				return PathUtilities.CombinePathsUnchecked(root, path);
			}
			return path;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003D14 File Offset: 0x00001F14
		private static string RemoveTrailingDirectorySeparator(string path)
		{
			if (path.Length > 0 && PathUtilities.IsDirectorySeparator(path.get_Chars(path.Length - 1)))
			{
				return path.Substring(0, path.Length - 1);
			}
			return path;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003D48 File Offset: 0x00001F48
		public static bool IsFilePath(string assemblyDisplayNameOrPath)
		{
			string extension = FileNameUtilities.GetExtension(assemblyDisplayNameOrPath);
			return string.Equals(extension, ".dll", 5) || string.Equals(extension, ".exe", 5) || assemblyDisplayNameOrPath.IndexOf(PathUtilities.DirectorySeparatorChar) != -1 || assemblyDisplayNameOrPath.IndexOf('/') != -1;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003D98 File Offset: 0x00001F98
		public static bool ContainsPathComponent([Nullable(2)] string path, string component, bool ignoreCase)
		{
			StringComparison stringComparison = (ignoreCase ? 5 : 4);
			if (path != null && path.IndexOf(component, stringComparison) >= 0)
			{
				StringComparer stringComparer = (ignoreCase ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal);
				int num = 0;
				string text = path;
				while (text != null)
				{
					string fileName = PathUtilities.GetFileName(text, true);
					if (stringComparer.Equals(fileName, component))
					{
						return true;
					}
					text = PathUtilities.GetDirectoryName(text);
					num++;
				}
			}
			return false;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003DF8 File Offset: 0x00001FF8
		public static string GetRelativePath(string directory, string fullPath)
		{
			string text = string.Empty;
			directory = PathUtilities.TrimTrailingSeparators(directory);
			fullPath = PathUtilities.TrimTrailingSeparators(fullPath);
			if (PathUtilities.IsChildPath(directory, fullPath))
			{
				return PathUtilities.GetRelativeChildPath(directory, fullPath);
			}
			string[] pathParts = PathUtilities.GetPathParts(directory);
			string[] pathParts2 = PathUtilities.GetPathParts(fullPath);
			if (pathParts.Length == 0 || pathParts2.Length == 0)
			{
				return fullPath;
			}
			int num = 0;
			while (num < pathParts.Length && PathUtilities.PathsEqual(pathParts[num], pathParts2[num]))
			{
				num++;
			}
			if (num == 0)
			{
				return fullPath;
			}
			int num2 = pathParts.Length - num;
			if (num2 > 0)
			{
				for (int i = 0; i < num2; i++)
				{
					text = text + ".." + PathUtilities.DirectorySeparatorStr;
				}
			}
			for (int j = num; j < pathParts2.Length; j++)
			{
				text = PathUtilities.CombinePathsUnchecked(text, pathParts2[j]);
			}
			return text;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003EB0 File Offset: 0x000020B0
		public static bool IsChildPath(string parentPath, string childPath)
		{
			return parentPath.Length > 0 && childPath.Length > parentPath.Length && PathUtilities.PathsEqual(childPath, parentPath, parentPath.Length) && (PathUtilities.IsDirectorySeparator(parentPath.get_Chars(parentPath.Length - 1)) || PathUtilities.IsDirectorySeparator(childPath.get_Chars(parentPath.Length)));
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003F10 File Offset: 0x00002110
		private static string GetRelativeChildPath(string parentPath, string childPath)
		{
			string text = childPath.Substring(parentPath.Length);
			int num = PathUtilities.ConsumeDirectorySeparators(text, text.Length, 0);
			if (num > 0)
			{
				text = text.Substring(num);
			}
			return text;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003F48 File Offset: 0x00002148
		private static string[] GetPathParts(string path)
		{
			string[] array = path.Split(PathUtilities.s_pathChars);
			if (array.Contains(".", null))
			{
				array = Enumerable.ToArray<string>(Enumerable.Where<string>(array, (string s) => s != "."));
			}
			return array;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003F9B File Offset: 0x0000219B
		public static bool PathsEqual(string path1, string path2)
		{
			return PathUtilities.PathsEqual(path1, path2, Math.Max(path1.Length, path2.Length));
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003FB8 File Offset: 0x000021B8
		private static bool PathsEqual(string path1, string path2, int length)
		{
			if (path1.Length < length || path2.Length < length)
			{
				return false;
			}
			for (int i = 0; i < length; i++)
			{
				if (!PathUtilities.PathCharEqual(path1.get_Chars(i), path2.get_Chars(i)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003FFD File Offset: 0x000021FD
		private static bool PathCharEqual(char x, char y)
		{
			if (PathUtilities.IsDirectorySeparator(x) && PathUtilities.IsDirectorySeparator(y))
			{
				return true;
			}
			if (!PathUtilities.IsUnixLikePlatform)
			{
				return char.ToUpperInvariant(x) == char.ToUpperInvariant(y);
			}
			return x == y;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000402C File Offset: 0x0000222C
		[NullableContext(2)]
		private static int PathHashCode(string path)
		{
			int num = 0;
			if (path != null)
			{
				for (int i = 0; i < path.Length; i++)
				{
					char c = path.get_Chars(i);
					if (!PathUtilities.IsDirectorySeparator(c))
					{
						num = Hash.Combine((int)char.ToUpperInvariant(c), num);
					}
				}
			}
			return num;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004070 File Offset: 0x00002270
		public static string NormalizePathPrefix(string filePath, [Nullable(new byte[] { 0, 0, 1, 1 })] ImmutableArray<KeyValuePair<string, string>> pathMap)
		{
			if (pathMap.IsDefaultOrEmpty)
			{
				return filePath;
			}
			foreach (KeyValuePair<string, string> keyValuePair in pathMap)
			{
				string key = keyValuePair.Key;
				if (key != null && key.Length > 0 && filePath.StartsWith(key, 4))
				{
					string value = keyValuePair.Value;
					string text = value + filePath.Substring(key.Length);
					bool flag = value.IndexOf('/') >= 0;
					bool flag2 = value.IndexOf('\\') >= 0;
					if (flag && !flag2)
					{
						return text.Replace('\\', '/');
					}
					if (!flag2 || flag)
					{
						return text;
					}
					return text.Replace('/', '\\');
				}
			}
			return filePath;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000412C File Offset: 0x0000232C
		[NullableContext(2)]
		public static bool IsValidFilePath([NotNullWhen(true)] string fullPath)
		{
			bool flag;
			try
			{
				if (RoslynString.IsNullOrEmpty(fullPath))
				{
					flag = false;
				}
				else
				{
					flag = !string.IsNullOrEmpty(new FileInfo(fullPath).Name);
				}
			}
			catch (Exception ex) when (ex is ArgumentException || ex is PathTooLongException || ex is NotSupportedException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000041A0 File Offset: 0x000023A0
		public static string NormalizeWithForwardSlash(string p)
		{
			if (PathUtilities.DirectorySeparatorChar != '/')
			{
				return p.Replace(PathUtilities.DirectorySeparatorChar, '/');
			}
			return p;
		}

		// Token: 0x0400002A RID: 42
		internal static readonly char DirectorySeparatorChar = (PlatformInformation.IsUnix ? '/' : '\\');

		// Token: 0x0400002B RID: 43
		internal const char AltDirectorySeparatorChar = '/';

		// Token: 0x0400002C RID: 44
		internal const string ParentRelativeDirectory = "..";

		// Token: 0x0400002D RID: 45
		internal const string ThisDirectory = ".";

		// Token: 0x0400002E RID: 46
		internal static readonly string DirectorySeparatorStr = new string(PathUtilities.DirectorySeparatorChar, 1);

		// Token: 0x0400002F RID: 47
		internal const char VolumeSeparatorChar = ':';

		// Token: 0x04000030 RID: 48
		private static readonly char[] s_pathChars = new char[]
		{
			':',
			PathUtilities.DirectorySeparatorChar,
			'/'
		};

		// Token: 0x04000031 RID: 49
		public static readonly IEqualityComparer<string> Comparer = new PathUtilities.PathComparer();

		// Token: 0x020004EC RID: 1260
		[NullableContext(2)]
		[Nullable(0)]
		private class PathComparer : IEqualityComparer<string>
		{
			// Token: 0x060032A1 RID: 12961 RVA: 0x0008FDF7 File Offset: 0x0008DFF7
			public bool Equals(string x, string y)
			{
				return (x == null && y == null) || (x != null && y != null && PathUtilities.PathsEqual(x, y));
			}

			// Token: 0x060032A2 RID: 12962 RVA: 0x0008FE10 File Offset: 0x0008E010
			public int GetHashCode(string s)
			{
				return PathUtilities.PathHashCode(s);
			}
		}

		// Token: 0x020004ED RID: 1261
		[NullableContext(0)]
		internal static class TestAccessor
		{
			// Token: 0x060032A4 RID: 12964 RVA: 0x0008FE20 File Offset: 0x0008E020
			[NullableContext(1)]
			[return: Nullable(2)]
			internal static string GetDirectoryName(string path, bool isUnixLike)
			{
				return PathUtilities.GetDirectoryName(path, isUnixLike);
			}
		}
	}
}
