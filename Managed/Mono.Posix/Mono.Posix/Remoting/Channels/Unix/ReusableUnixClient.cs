using System;
using System.Net.Sockets;
using Mono.Unix;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200008C RID: 140
	internal class ReusableUnixClient : UnixClient
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x0000F1AC File Offset: 0x0000D3AC
		public ReusableUnixClient(string path)
			: base(path)
		{
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0000F1B5 File Offset: 0x0000D3B5
		public bool IsAlive
		{
			get
			{
				return !base.Client.Poll(0, SelectMode.SelectRead);
			}
		}
	}
}
