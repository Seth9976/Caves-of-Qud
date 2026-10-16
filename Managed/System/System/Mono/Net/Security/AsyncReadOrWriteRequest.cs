using System;

namespace Mono.Net.Security
{
	// Token: 0x02000093 RID: 147
	internal abstract class AsyncReadOrWriteRequest : AsyncProtocolRequest
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00006AB7 File Offset: 0x00004CB7
		protected BufferOffsetSize UserBuffer { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00006ABF File Offset: 0x00004CBF
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00006AC7 File Offset: 0x00004CC7
		protected int CurrentSize { get; set; }

		// Token: 0x06000245 RID: 581 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public AsyncReadOrWriteRequest(MobileAuthenticatedStream parent, bool sync, byte[] buffer, int offset, int size)
			: base(parent, sync)
		{
			this.UserBuffer = new BufferOffsetSize(buffer, offset, size);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00006AEA File Offset: 0x00004CEA
		public override string ToString()
		{
			return string.Format("[{0}: {1}]", base.Name, this.UserBuffer);
		}
	}
}
