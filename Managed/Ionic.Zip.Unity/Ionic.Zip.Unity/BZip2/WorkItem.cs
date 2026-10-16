using System;
using System.IO;

namespace Ionic.BZip2
{
	// Token: 0x0200004A RID: 74
	internal class WorkItem
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000399 RID: 921 RVA: 0x000150D1 File Offset: 0x000132D1
		// (set) Token: 0x0600039A RID: 922 RVA: 0x000150D9 File Offset: 0x000132D9
		public BZip2Compressor Compressor { get; private set; }

		// Token: 0x0600039B RID: 923 RVA: 0x000150E2 File Offset: 0x000132E2
		public WorkItem(int ix, int blockSize)
		{
			this.ms = new MemoryStream();
			this.bw = new BitWriter(this.ms);
			this.Compressor = new BZip2Compressor(this.bw, blockSize);
			this.index = ix;
		}

		// Token: 0x0400022E RID: 558
		public int index;

		// Token: 0x0400022F RID: 559
		public MemoryStream ms;

		// Token: 0x04000230 RID: 560
		public int ordinal;

		// Token: 0x04000231 RID: 561
		public BitWriter bw;
	}
}
