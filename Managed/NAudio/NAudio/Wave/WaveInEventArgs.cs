using System;

namespace NAudio.Wave
{
	// Token: 0x0200017D RID: 381
	public class WaveInEventArgs : EventArgs
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x000166DB File Offset: 0x000148DB
		public WaveInEventArgs(byte[] buffer, int bytes)
		{
			this.buffer = buffer;
			this.bytes = bytes;
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x000166F1 File Offset: 0x000148F1
		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x000166F9 File Offset: 0x000148F9
		public int BytesRecorded
		{
			get
			{
				return this.bytes;
			}
		}

		// Token: 0x04000923 RID: 2339
		private byte[] buffer;

		// Token: 0x04000924 RID: 2340
		private int bytes;
	}
}
