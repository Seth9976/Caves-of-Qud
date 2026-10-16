using System;

namespace Mono.Cecil
{
	// Token: 0x0200022D RID: 557
	internal sealed class FixedSysStringMarshalInfo : MarshalInfo
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x000298BE File Offset: 0x00027ABE
		// (set) Token: 0x06000C12 RID: 3090 RVA: 0x000298C6 File Offset: 0x00027AC6
		public int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x000298CF File Offset: 0x00027ACF
		public FixedSysStringMarshalInfo()
			: base(NativeType.FixedSysString)
		{
			this.size = -1;
		}

		// Token: 0x04000367 RID: 871
		internal int size;
	}
}
