using System;

namespace Mono.Cecil
{
	// Token: 0x02000228 RID: 552
	public class MarshalInfo
	{
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0002978F File Offset: 0x0002798F
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x00029797 File Offset: 0x00027997
		public NativeType NativeType
		{
			get
			{
				return this.native;
			}
			set
			{
				this.native = value;
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x000297A0 File Offset: 0x000279A0
		public MarshalInfo(NativeType native)
		{
			this.native = native;
		}

		// Token: 0x0400035B RID: 859
		internal NativeType native;
	}
}
