using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200006A RID: 106
	public class MarshalInfo
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00007640 File Offset: 0x00005840
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00007648 File Offset: 0x00005848
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

		// Token: 0x060002DF RID: 735 RVA: 0x00007651 File Offset: 0x00005851
		public MarshalInfo(NativeType native)
		{
			this.native = native;
		}

		// Token: 0x040000AD RID: 173
		internal NativeType native;
	}
}
