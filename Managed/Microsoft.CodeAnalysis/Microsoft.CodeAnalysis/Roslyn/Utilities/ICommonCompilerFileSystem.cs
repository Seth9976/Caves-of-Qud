using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000029 RID: 41
	[NullableContext(1)]
	internal interface ICommonCompilerFileSystem
	{
		// Token: 0x0600008C RID: 140
		bool FileExists(string filePath);

		// Token: 0x0600008D RID: 141
		Stream OpenFile(string filePath, FileMode mode, FileAccess access, FileShare share);

		// Token: 0x0600008E RID: 142
		Stream OpenFileEx(string filePath, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options, out string normalizedFilePath);
	}
}
