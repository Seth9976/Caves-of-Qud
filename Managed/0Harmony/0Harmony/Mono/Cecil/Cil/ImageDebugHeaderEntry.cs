using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002D5 RID: 725
	public sealed class ImageDebugHeaderEntry
	{
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x0003C65E File Offset: 0x0003A85E
		// (set) Token: 0x06001278 RID: 4728 RVA: 0x0003C666 File Offset: 0x0003A866
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

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x0003C66F File Offset: 0x0003A86F
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0003C677 File Offset: 0x0003A877
		public ImageDebugHeaderEntry(ImageDebugDirectory directory, byte[] data)
		{
			this.directory = directory;
			this.data = data ?? Empty<byte>.Array;
		}

		// Token: 0x0400095A RID: 2394
		private ImageDebugDirectory directory;

		// Token: 0x0400095B RID: 2395
		private readonly byte[] data;
	}
}
