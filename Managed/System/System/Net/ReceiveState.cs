using System;

namespace System.Net
{
	// Token: 0x02000588 RID: 1416
	internal class ReceiveState
	{
		// Token: 0x06002DCD RID: 11725 RVA: 0x0009CDA8 File Offset: 0x0009AFA8
		internal ReceiveState(CommandStream connection)
		{
			this.Connection = connection;
			this.Resp = new ResponseDescription();
			this.Buffer = new byte[1024];
			this.ValidThrough = 0;
		}

		// Token: 0x0400193F RID: 6463
		private const int bufferSize = 1024;

		// Token: 0x04001940 RID: 6464
		internal ResponseDescription Resp;

		// Token: 0x04001941 RID: 6465
		internal int ValidThrough;

		// Token: 0x04001942 RID: 6466
		internal byte[] Buffer;

		// Token: 0x04001943 RID: 6467
		internal CommandStream Connection;
	}
}
