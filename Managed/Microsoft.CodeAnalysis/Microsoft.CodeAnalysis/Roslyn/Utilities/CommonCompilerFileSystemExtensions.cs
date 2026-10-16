using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200002A RID: 42
	internal static class CommonCompilerFileSystemExtensions
	{
		// Token: 0x0600008F RID: 143 RVA: 0x0000378C File Offset: 0x0000198C
		[NullableContext(1)]
		internal static Stream OpenFileWithNormalizedException(this ICommonCompilerFileSystem fileSystem, string filePath, FileMode fileMode, FileAccess fileAccess, FileShare fileShare)
		{
			Stream stream;
			try
			{
				stream = fileSystem.OpenFile(filePath, fileMode, fileAccess, fileShare);
			}
			catch (ArgumentException)
			{
				throw;
			}
			catch (DirectoryNotFoundException ex)
			{
				throw new FileNotFoundException(ex.Message, filePath, ex);
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
	}
}
