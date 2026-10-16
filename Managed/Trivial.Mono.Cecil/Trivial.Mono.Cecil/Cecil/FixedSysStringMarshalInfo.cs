using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200003F RID: 63
	public sealed class FixedSysStringMarshalInfo : MarshalInfo
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00005E47 File Offset: 0x00004047
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00005E4F File Offset: 0x0000404F
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

		// Token: 0x060001F6 RID: 502 RVA: 0x00005E58 File Offset: 0x00004058
		public FixedSysStringMarshalInfo()
			: base(NativeType.FixedSysString)
		{
			this.size = -1;
		}

		// Token: 0x04000089 RID: 137
		internal int size;
	}
}
