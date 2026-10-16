using System;

namespace NAudio.Dmo
{
	// Token: 0x0200008C RID: 140
	internal struct DmoPartialMediaType
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000A221 File Offset: 0x00008421
		// (set) Token: 0x060002FE RID: 766 RVA: 0x0000A229 File Offset: 0x00008429
		public Guid Type
		{
			get
			{
				return this.type;
			}
			internal set
			{
				this.type = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000A232 File Offset: 0x00008432
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000A23A File Offset: 0x0000843A
		public Guid Subtype
		{
			get
			{
				return this.subtype;
			}
			internal set
			{
				this.subtype = value;
			}
		}

		// Token: 0x040003F9 RID: 1017
		private Guid type;

		// Token: 0x040003FA RID: 1018
		private Guid subtype;
	}
}
