using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002D4 RID: 724
	public sealed class ImageDebugHeader
	{
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x0003C60F File Offset: 0x0003A80F
		public bool HasEntries
		{
			get
			{
				return !this.entries.IsNullOrEmpty<ImageDebugHeaderEntry>();
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x0003C61F File Offset: 0x0003A81F
		public ImageDebugHeaderEntry[] Entries
		{
			get
			{
				return this.entries;
			}
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0003C627 File Offset: 0x0003A827
		public ImageDebugHeader(ImageDebugHeaderEntry[] entries)
		{
			this.entries = entries ?? Empty<ImageDebugHeaderEntry>.Array;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0003C63F File Offset: 0x0003A83F
		public ImageDebugHeader()
			: this(Empty<ImageDebugHeaderEntry>.Array)
		{
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0003C64C File Offset: 0x0003A84C
		public ImageDebugHeader(ImageDebugHeaderEntry entry)
			: this(new ImageDebugHeaderEntry[] { entry })
		{
		}

		// Token: 0x04000959 RID: 2393
		private readonly ImageDebugHeaderEntry[] entries;
	}
}
