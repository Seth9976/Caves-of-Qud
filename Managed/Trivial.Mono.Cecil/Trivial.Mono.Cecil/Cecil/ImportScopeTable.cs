using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200005D RID: 93
	internal sealed class ImportScopeTable : MetadataTable<Row<uint, uint>>
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x0000726C File Offset: 0x0000546C
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteRID(this.rows[i].Col1, Table.ImportScope);
				buffer.WriteBlob(this.rows[i].Col2);
			}
		}
	}
}
