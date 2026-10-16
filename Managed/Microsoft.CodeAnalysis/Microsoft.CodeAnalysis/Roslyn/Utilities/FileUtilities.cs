using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;

namespace Roslyn.Utilities
{
	// Token: 0x02000028 RID: 40
	[NullableContext(1)]
	[Nullable(0)]
	internal static class FileUtilities
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003304 File Offset: 0x00001504
		[return: Nullable(2)]
		internal static string ResolveRelativePath(string path, [Nullable(2)] string basePath, [Nullable(2)] string baseDirectory, IEnumerable<string> searchPaths, Func<string, bool> fileExists)
		{
			PathKind pathKind = PathUtilities.GetPathKind(path);
			string text;
			if (pathKind == PathKind.Relative)
			{
				baseDirectory = FileUtilities.GetBaseDirectory(basePath, baseDirectory);
				if (baseDirectory != null)
				{
					text = PathUtilities.CombinePathsUnchecked(baseDirectory, path);
					if (fileExists.Invoke(text))
					{
						return text;
					}
				}
				foreach (string text2 in searchPaths)
				{
					text = PathUtilities.CombinePathsUnchecked(text2, path);
					if (fileExists.Invoke(text))
					{
						return text;
					}
				}
				return null;
			}
			text = FileUtilities.ResolveRelativePath(pathKind, path, basePath, baseDirectory);
			if (text != null && fileExists.Invoke(text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000033A4 File Offset: 0x000015A4
		[NullableContext(2)]
		internal static string ResolveRelativePath(string path, string baseDirectory)
		{
			return FileUtilities.ResolveRelativePath(path, null, baseDirectory);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000033AE File Offset: 0x000015AE
		[NullableContext(2)]
		internal static string ResolveRelativePath(string path, string basePath, string baseDirectory)
		{
			return FileUtilities.ResolveRelativePath(PathUtilities.GetPathKind(path), path, basePath, baseDirectory);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000033C0 File Offset: 0x000015C0
		[NullableContext(2)]
		private static string ResolveRelativePath(PathKind kind, string path, string basePath, string baseDirectory)
		{
			switch (kind)
			{
			case PathKind.Empty:
				return null;
			case PathKind.Relative:
				baseDirectory = FileUtilities.GetBaseDirectory(basePath, baseDirectory);
				if (baseDirectory == null)
				{
					return null;
				}
				return PathUtilities.CombinePathsUnchecked(baseDirectory, path);
			case PathKind.RelativeToCurrentDirectory:
				baseDirectory = FileUtilities.GetBaseDirectory(basePath, baseDirectory);
				if (baseDirectory == null)
				{
					return null;
				}
				if (path.Length == 1)
				{
					return baseDirectory;
				}
				return PathUtilities.CombinePathsUnchecked(baseDirectory, path);
			case PathKind.RelativeToCurrentParent:
				baseDirectory = FileUtilities.GetBaseDirectory(basePath, baseDirectory);
				if (baseDirectory == null)
				{
					return null;
				}
				return PathUtilities.CombinePathsUnchecked(baseDirectory, path);
			case PathKind.RelativeToCurrentRoot:
			{
				string text;
				if (basePath != null)
				{
					text = PathUtilities.GetPathRoot(basePath);
				}
				else
				{
					if (baseDirectory == null)
					{
						return null;
					}
					text = PathUtilities.GetPathRoot(baseDirectory);
				}
				if (RoslynString.IsNullOrEmpty(text))
				{
					return null;
				}
				return PathUtilities.CombinePathsUnchecked(text, path.Substring(1));
			}
			case PathKind.RelativeToDriveDirectory:
				return null;
			case PathKind.Absolute:
				return path;
			default:
				throw ExceptionUtilities.UnexpectedValue(kind);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003484 File Offset: 0x00001684
		[NullableContext(2)]
		private static string GetBaseDirectory(string basePath, string baseDirectory)
		{
			string text = FileUtilities.ResolveRelativePath(basePath, baseDirectory);
			if (text == null)
			{
				return baseDirectory;
			}
			string text2;
			try
			{
				text2 = Path.GetDirectoryName(text);
			}
			catch (Exception)
			{
				text2 = null;
			}
			return text2;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000034C0 File Offset: 0x000016C0
		[NullableContext(2)]
		internal static string NormalizeRelativePath([Nullable(1)] string path, string basePath, string baseDirectory)
		{
			if (path.IndexOf("://", 4) >= 0 || path.IndexOfAny(FileUtilities.s_invalidPathChars) >= 0)
			{
				return null;
			}
			string text = FileUtilities.ResolveRelativePath(path, basePath, baseDirectory);
			if (text == null)
			{
				return null;
			}
			string text2 = FileUtilities.TryNormalizeAbsolutePath(text);
			if (text2 == null)
			{
				return null;
			}
			return text2;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003508 File Offset: 0x00001708
		internal static string NormalizeAbsolutePath(string path)
		{
			string fullPath;
			try
			{
				fullPath = Path.GetFullPath(path);
			}
			catch (ArgumentException ex)
			{
				throw new IOException(ex.Message, ex);
			}
			catch (SecurityException ex2)
			{
				throw new IOException(ex2.Message, ex2);
			}
			catch (NotSupportedException ex3)
			{
				throw new IOException(ex3.Message, ex3);
			}
			return fullPath;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003574 File Offset: 0x00001774
		internal static string NormalizeDirectoryPath(string path)
		{
			return FileUtilities.NormalizeAbsolutePath(path).TrimEnd(new char[]
			{
				Path.DirectorySeparatorChar,
				Path.AltDirectorySeparatorChar
			});
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003598 File Offset: 0x00001798
		[return: Nullable(2)]
		internal static string TryNormalizeAbsolutePath(string path)
		{
			string text;
			try
			{
				text = Path.GetFullPath(path);
			}
			catch
			{
				text = null;
			}
			return text;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000035C4 File Offset: 0x000017C4
		internal static Stream OpenRead(string fullPath)
		{
			Stream stream;
			try
			{
				stream = new FileStream(fullPath, 3, 1, 1);
			}
			catch (IOException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IOException(ex.Message, ex);
			}
			return stream;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000360C File Offset: 0x0000180C
		internal static Stream OpenAsyncRead(string fullPath)
		{
			return FileUtilities.RethrowExceptionsAsIOException<FileStream>(() => new FileStream(fullPath, 3, 1, 1, 4096, 1073741824));
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000362C File Offset: 0x0000182C
		internal static T RethrowExceptionsAsIOException<[Nullable(2)] T>(Func<T> operation)
		{
			T t;
			try
			{
				t = operation.Invoke();
			}
			catch (IOException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IOException(ex.Message, ex);
			}
			return t;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003670 File Offset: 0x00001870
		internal static Stream CreateFileStreamChecked(Func<string, Stream> factory, string path, [Nullable(2)] string paramName = null)
		{
			Stream stream;
			try
			{
				stream = factory.Invoke(path);
			}
			catch (ArgumentNullException)
			{
				if (paramName == null)
				{
					throw;
				}
				throw new ArgumentNullException(paramName);
			}
			catch (ArgumentException ex)
			{
				if (paramName == null)
				{
					throw;
				}
				throw new ArgumentException(ex.Message, paramName);
			}
			catch (IOException)
			{
				throw;
			}
			catch (Exception ex2)
			{
				throw new IOException(ex2.Message, ex2);
			}
			return stream;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000036F0 File Offset: 0x000018F0
		internal static DateTime GetFileTimeStamp(string fullPath)
		{
			DateTime lastWriteTimeUtc;
			try
			{
				lastWriteTimeUtc = File.GetLastWriteTimeUtc(fullPath);
			}
			catch (IOException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IOException(ex.Message, ex);
			}
			return lastWriteTimeUtc;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003734 File Offset: 0x00001934
		internal static long GetFileLength(string fullPath)
		{
			long length;
			try
			{
				length = new FileInfo(fullPath).Length;
			}
			catch (IOException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IOException(ex.Message, ex);
			}
			return length;
		}

		// Token: 0x04000020 RID: 32
		private static readonly char[] s_invalidPathChars = Path.GetInvalidPathChars();
	}
}
