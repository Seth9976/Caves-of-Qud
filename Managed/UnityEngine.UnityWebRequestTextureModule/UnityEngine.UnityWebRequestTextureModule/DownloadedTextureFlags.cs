using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000002 RID: 2
	[Flags]
	public enum DownloadedTextureFlags : uint
	{
		// Token: 0x04000002 RID: 2
		None = 0U,
		// Token: 0x04000003 RID: 3
		Readable = 1U,
		// Token: 0x04000004 RID: 4
		MipmapChain = 2U,
		// Token: 0x04000005 RID: 5
		LinearColorSpace = 4U
	}
}
