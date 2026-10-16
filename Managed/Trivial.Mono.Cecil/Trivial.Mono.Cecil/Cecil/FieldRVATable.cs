using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200003A RID: 58
	internal sealed class FieldRVATable : SortedTable<Row<uint, uint>>
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public override void Write(TableHeapBuffer buffer)
		{
			this.position = buffer.position;
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteUInt32(this.rows[i].Col1);
				buffer.WriteRID(this.rows[i].Col2, Table.Field);
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005B95 File Offset: 0x00003D95
		public override int Compare(Row<uint, uint> x, Row<uint, uint> y)
		{
			return base.Compare(x.Col2, y.Col2);
		}

		// Token: 0x04000083 RID: 131
		internal int position;
	}
}
