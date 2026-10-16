using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000096 RID: 150
	internal abstract class OneRowTable<TRow> : MetadataTable where TRow : struct
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public sealed override int Length
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00002921 File Offset: 0x00000B21
		public sealed override void Sort()
		{
		}

		// Token: 0x04000205 RID: 517
		internal TRow row;
	}
}
