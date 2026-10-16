using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200002E RID: 46
	internal sealed class EventMapTable : MetadataTable<Row<uint, uint>>
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00004F14 File Offset: 0x00003114
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteRID(this.rows[i].Col1, Table.TypeDef);
				buffer.WriteRID(this.rows[i].Col2, Table.Event);
			}
		}
	}
}
