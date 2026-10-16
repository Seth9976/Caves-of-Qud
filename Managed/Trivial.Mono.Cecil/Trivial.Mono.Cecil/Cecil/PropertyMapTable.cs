using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000A3 RID: 163
	internal sealed class PropertyMapTable : MetadataTable<Row<uint, uint>>
	{
		// Token: 0x060006D6 RID: 1750 RVA: 0x00015038 File Offset: 0x00013238
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteRID(this.rows[i].Col1, Table.TypeDef);
				buffer.WriteRID(this.rows[i].Col2, Table.Property);
			}
		}
	}
}
