using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x020001D4 RID: 468
	internal sealed class EventMapTable : MetadataTable<Row<uint, uint>>
	{
		// Token: 0x0600092D RID: 2349 RVA: 0x000213FC File Offset: 0x0001F5FC
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
