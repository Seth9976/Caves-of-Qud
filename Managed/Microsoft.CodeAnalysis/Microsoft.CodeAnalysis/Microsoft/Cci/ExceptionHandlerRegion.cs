using System;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x02000083 RID: 131
	[NullableContext(2)]
	[Nullable(0)]
	internal abstract class ExceptionHandlerRegion
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000CF37 File Offset: 0x0000B137
		public int TryStartOffset { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000CF3F File Offset: 0x0000B13F
		public int TryEndOffset { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000CF47 File Offset: 0x0000B147
		public int HandlerStartOffset { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000CF4F File Offset: 0x0000B14F
		public int HandlerEndOffset { get; }

		// Token: 0x0600035F RID: 863 RVA: 0x0000CF57 File Offset: 0x0000B157
		public ExceptionHandlerRegion(int tryStartOffset, int tryEndOffset, int handlerStartOffset, int handlerEndOffset)
		{
			this.TryStartOffset = tryStartOffset;
			this.TryEndOffset = tryEndOffset;
			this.HandlerStartOffset = handlerStartOffset;
			this.HandlerEndOffset = handlerEndOffset;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000CF7C File Offset: 0x0000B17C
		public int HandlerLength
		{
			get
			{
				return this.HandlerEndOffset - this.HandlerStartOffset;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000CF8B File Offset: 0x0000B18B
		public int TryLength
		{
			get
			{
				return this.TryEndOffset - this.TryStartOffset;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000362 RID: 866
		public abstract ExceptionRegionKind HandlerKind { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000CF9A File Offset: 0x0000B19A
		public virtual ITypeReference ExceptionType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000CF9D File Offset: 0x0000B19D
		public virtual int FilterDecisionStartOffset
		{
			get
			{
				return 0;
			}
		}
	}
}
