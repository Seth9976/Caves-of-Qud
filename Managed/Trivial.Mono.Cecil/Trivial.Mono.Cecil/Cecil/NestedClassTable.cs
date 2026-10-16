using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000095 RID: 149
	internal sealed class NestedClassTable : SortedTable<Row<uint, uint>>
	{
		// Token: 0x06000646 RID: 1606 RVA: 0x0001438C File Offset: 0x0001258C
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteRID(this.rows[i].Col1, Table.TypeDef);
				buffer.WriteRID(this.rows[i].Col2, Table.TypeDef);
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00005C01 File Offset: 0x00003E01
		public override int Compare(Row<uint, uint> x, Row<uint, uint> y)
		{
			return base.Compare(x.Col1, y.Col1);
		}
	}
}
