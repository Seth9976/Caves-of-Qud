using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000022 RID: 34
	internal sealed class CustomAttributeTable : SortedTable<Row<uint, uint, uint>>
	{
		// Token: 0x06000116 RID: 278 RVA: 0x000047C8 File Offset: 0x000029C8
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteCodedRID(this.rows[i].Col1, CodedIndex.HasCustomAttribute);
				buffer.WriteCodedRID(this.rows[i].Col2, CodedIndex.CustomAttributeType);
				buffer.WriteBlob(this.rows[i].Col3);
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000482E File Offset: 0x00002A2E
		public override int Compare(Row<uint, uint, uint> x, Row<uint, uint, uint> y)
		{
			return base.Compare(x.Col1, y.Col1);
		}
	}
}
