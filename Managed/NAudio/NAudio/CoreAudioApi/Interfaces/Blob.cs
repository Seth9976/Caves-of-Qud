using System;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000119 RID: 281
	internal struct Blob
	{
		// Token: 0x0600060D RID: 1549 RVA: 0x00013688 File Offset: 0x00011888
		private void FixCS0649()
		{
			this.Length = 0;
			this.Data = IntPtr.Zero;
		}

		// Token: 0x040006D9 RID: 1753
		public int Length;

		// Token: 0x040006DA RID: 1754
		public IntPtr Data;
	}
}
