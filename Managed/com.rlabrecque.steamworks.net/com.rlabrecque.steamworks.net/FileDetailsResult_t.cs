using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200001C RID: 28
	[CallbackIdentity(1023)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FileDetailsResult_t
	{
		// Token: 0x0400005F RID: 95
		public const int k_iCallback = 1023;

		// Token: 0x04000060 RID: 96
		public EResult m_eResult;

		// Token: 0x04000061 RID: 97
		public ulong m_ulFileSize;

		// Token: 0x04000062 RID: 98
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
		public byte[] m_FileSHA;

		// Token: 0x04000063 RID: 99
		public uint m_unFlags;
	}
}
