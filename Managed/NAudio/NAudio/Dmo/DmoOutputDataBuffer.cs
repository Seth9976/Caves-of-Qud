using System;
using System.Runtime.InteropServices;

namespace NAudio.Dmo
{
	// Token: 0x020001FB RID: 507
	[StructLayout(0, Pack = 8)]
	public struct DmoOutputDataBuffer : IDisposable
	{
		// Token: 0x06000B7E RID: 2942 RVA: 0x00022994 File Offset: 0x00020B94
		public DmoOutputDataBuffer(int maxBufferSize)
		{
			this.pBuffer = new MediaBuffer(maxBufferSize);
			this.dwStatus = DmoOutputDataBufferFlags.None;
			this.rtTimestamp = 0L;
			this.referenceTimeDuration = 0L;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x000229B9 File Offset: 0x00020BB9
		public void Dispose()
		{
			if (this.pBuffer != null)
			{
				((MediaBuffer)this.pBuffer).Dispose();
				this.pBuffer = null;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x000229EA File Offset: 0x00020BEA
		// (set) Token: 0x06000B81 RID: 2945 RVA: 0x000229F2 File Offset: 0x00020BF2
		public IMediaBuffer MediaBuffer
		{
			get
			{
				return this.pBuffer;
			}
			internal set
			{
				this.pBuffer = value;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x000229FB File Offset: 0x00020BFB
		public int Length
		{
			get
			{
				return ((MediaBuffer)this.pBuffer).Length;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x00022A0D File Offset: 0x00020C0D
		// (set) Token: 0x06000B84 RID: 2948 RVA: 0x00022A15 File Offset: 0x00020C15
		public DmoOutputDataBufferFlags StatusFlags
		{
			get
			{
				return this.dwStatus;
			}
			internal set
			{
				this.dwStatus = value;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x00022A1E File Offset: 0x00020C1E
		// (set) Token: 0x06000B86 RID: 2950 RVA: 0x00022A26 File Offset: 0x00020C26
		public long Timestamp
		{
			get
			{
				return this.rtTimestamp;
			}
			internal set
			{
				this.rtTimestamp = value;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x00022A2F File Offset: 0x00020C2F
		// (set) Token: 0x06000B88 RID: 2952 RVA: 0x00022A37 File Offset: 0x00020C37
		public long Duration
		{
			get
			{
				return this.referenceTimeDuration;
			}
			internal set
			{
				this.referenceTimeDuration = value;
			}
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00022A40 File Offset: 0x00020C40
		public void RetrieveData(byte[] data, int offset)
		{
			((MediaBuffer)this.pBuffer).RetrieveData(data, offset);
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x00022A54 File Offset: 0x00020C54
		public bool MoreDataAvailable
		{
			get
			{
				return (this.StatusFlags & DmoOutputDataBufferFlags.Incomplete) == DmoOutputDataBufferFlags.Incomplete;
			}
		}

		// Token: 0x04000C03 RID: 3075
		[MarshalAs(28)]
		private IMediaBuffer pBuffer;

		// Token: 0x04000C04 RID: 3076
		private DmoOutputDataBufferFlags dwStatus;

		// Token: 0x04000C05 RID: 3077
		private long rtTimestamp;

		// Token: 0x04000C06 RID: 3078
		private long referenceTimeDuration;
	}
}
