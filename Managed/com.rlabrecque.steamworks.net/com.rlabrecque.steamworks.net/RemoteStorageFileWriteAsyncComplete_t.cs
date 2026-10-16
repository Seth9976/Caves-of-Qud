using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B1 RID: 177
	[CallbackIdentity(1331)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageFileWriteAsyncComplete_t
	{
		// Token: 0x04000298 RID: 664
		public const int k_iCallback = 1331;

		// Token: 0x04000299 RID: 665
		public EResult m_eResult;
	}
}
