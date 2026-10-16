using System;

namespace System.Net
{
	// Token: 0x020005CA RID: 1482
	internal class SystemNetworkCredential : NetworkCredential
	{
		// Token: 0x06002FF9 RID: 12281 RVA: 0x000A575D File Offset: 0x000A395D
		private SystemNetworkCredential()
			: base(string.Empty, string.Empty, string.Empty)
		{
		}

		// Token: 0x04001A8B RID: 6795
		internal static readonly SystemNetworkCredential defaultCredential = new SystemNetworkCredential();
	}
}
