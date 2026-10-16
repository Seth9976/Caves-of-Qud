using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000110 RID: 272
	public sealed class ImageDebugHeaderEntry
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00022DF1 File Offset: 0x00020FF1
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x00022DF9 File Offset: 0x00020FF9
		public ImageDebugDirectory Directory
		{
			get
			{
				return this.directory;
			}
			internal set
			{
				this.directory = value;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x00022E02 File Offset: 0x00021002
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00022E0A File Offset: 0x0002100A
		public ImageDebugHeaderEntry(ImageDebugDirectory directory, byte[] data)
		{
			this.directory = directory;
			this.data = data ?? Empty<byte>.Array;
		}

		// Token: 0x0400055E RID: 1374
		private ImageDebugDirectory directory;

		// Token: 0x0400055F RID: 1375
		private readonly byte[] data;
	}
}
