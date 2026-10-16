using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200002B RID: 43
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class StandardFileSystem : ICommonCompilerFileSystem
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003800 File Offset: 0x00001A00
		public static StandardFileSystem Instance { get; } = new StandardFileSystem();

		// Token: 0x06000091 RID: 145 RVA: 0x00003807 File Offset: 0x00001A07
		private StandardFileSystem()
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000380F File Offset: 0x00001A0F
		public bool FileExists(string filePath)
		{
			return File.Exists(filePath);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003817 File Offset: 0x00001A17
		public Stream OpenFile(string filePath, FileMode mode, FileAccess access, FileShare share)
		{
			return new FileStream(filePath, mode, access, share);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003824 File Offset: 0x00001A24
		public Stream OpenFileEx(string filePath, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options, out string normalizedFilePath)
		{
			FileStream fileStream = new FileStream(filePath, mode, access, share, bufferSize, options);
			normalizedFilePath = fileStream.Name;
			return fileStream;
		}
	}
}
