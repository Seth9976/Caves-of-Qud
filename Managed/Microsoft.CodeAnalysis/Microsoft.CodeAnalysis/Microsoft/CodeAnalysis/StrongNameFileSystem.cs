using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001FA RID: 506
	[NullableContext(1)]
	[Nullable(0)]
	internal class StrongNameFileSystem
	{
		// Token: 0x06001465 RID: 5221 RVA: 0x00040867 File Offset: 0x0003EA67
		[NullableContext(2)]
		internal StrongNameFileSystem(string customTempPath = null)
		{
			this._customTempPath = customTempPath;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00040876 File Offset: 0x0003EA76
		internal virtual FileStream CreateFileStream(string filePath, FileMode fileMode, FileAccess fileAccess, FileShare fileShare)
		{
			return new FileStream(filePath, fileMode, fileAccess, fileShare);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00040882 File Offset: 0x0003EA82
		internal virtual byte[] ReadAllBytes(string fullPath)
		{
			return File.ReadAllBytes(fullPath);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0004088A File Offset: 0x0003EA8A
		[NullableContext(2)]
		internal virtual bool FileExists(string fullPath)
		{
			return File.Exists(fullPath);
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00040892 File Offset: 0x0003EA92
		internal virtual string GetTempPath()
		{
			return this._customTempPath ?? Path.GetTempPath();
		}

		// Token: 0x040006F1 RID: 1777
		internal static readonly StrongNameFileSystem Instance = new StrongNameFileSystem(null);

		// Token: 0x040006F2 RID: 1778
		[Nullable(2)]
		internal readonly string _customTempPath;
	}
}
