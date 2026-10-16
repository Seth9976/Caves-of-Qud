using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x020001DD RID: 477
	internal sealed class FieldRVATable : SortedTable<Row<uint, uint>>
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x0002174C File Offset: 0x0001F94C
		public override void Write(TableHeapBuffer buffer)
		{
			this.position = buffer.position;
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteUInt32(this.rows[i].Col1);
				buffer.WriteRID(this.rows[i].Col2, Table.Field);
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x000213B5 File Offset: 0x0001F5B5
		public override int Compare(Row<uint, uint> x, Row<uint, uint> y)
		{
			return SortedTable<Row<uint, uint>>.Compare(x.Col2, y.Col2);
		}

		// Token: 0x040002A1 RID: 673
		internal int position;
	}
}
