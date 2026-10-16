using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x020001E3 RID: 483
	internal sealed class NestedClassTable : SortedTable<Row<uint, uint>>
	{
		// Token: 0x0600094E RID: 2382 RVA: 0x00021AE4 File Offset: 0x0001FCE4
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteRID(this.rows[i].Col1, Table.TypeDef);
				buffer.WriteRID(this.rows[i].Col2, Table.TypeDef);
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0002124D File Offset: 0x0001F44D
		public override int Compare(Row<uint, uint> x, Row<uint, uint> y)
		{
			return SortedTable<Row<uint, uint>>.Compare(x.Col1, y.Col1);
		}
	}
}
