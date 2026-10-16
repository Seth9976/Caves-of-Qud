using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.IO.Archive
{
	// Token: 0x02000093 RID: 147
	[RequiredByNativeCode]
	[NativeHeader("Runtime/VirtualFileSystem/ArchiveFileSystem/ArchiveFileHandle.h")]
	public struct ArchiveFileInfo
	{
		// Token: 0x04000251 RID: 593
		public string Filename;

		// Token: 0x04000252 RID: 594
		public ulong FileSize;
	}
}
