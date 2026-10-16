using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x0200010F RID: 271
	public sealed class ImageDebugHeader
	{
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00022DA2 File Offset: 0x00020FA2
		public bool HasEntries
		{
			get
			{
				return !this.entries.IsNullOrEmpty<ImageDebugHeaderEntry>();
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00022DB2 File Offset: 0x00020FB2
		public ImageDebugHeaderEntry[] Entries
		{
			get
			{
				return this.entries;
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00022DBA File Offset: 0x00020FBA
		public ImageDebugHeader(ImageDebugHeaderEntry[] entries)
		{
			this.entries = entries ?? Empty<ImageDebugHeaderEntry>.Array;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00022DD2 File Offset: 0x00020FD2
		public ImageDebugHeader()
			: this(Empty<ImageDebugHeaderEntry>.Array)
		{
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00022DDF File Offset: 0x00020FDF
		public ImageDebugHeader(ImageDebugHeaderEntry entry)
			: this(new ImageDebugHeaderEntry[] { entry })
		{
		}

		// Token: 0x0400055D RID: 1373
		private readonly ImageDebugHeaderEntry[] entries;
	}
}
