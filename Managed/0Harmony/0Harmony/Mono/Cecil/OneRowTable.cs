using System;

namespace Mono.Cecil
{
	// Token: 0x020001C2 RID: 450
	internal abstract class OneRowTable<TRow> : MetadataTable where TRow : struct
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00018941 File Offset: 0x00016B41
		public sealed override int Length
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00018659 File Offset: 0x00016859
		public sealed override void Sort()
		{
		}

		// Token: 0x0400029E RID: 670
		internal TRow row;
	}
}
