using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200002A RID: 42
	internal sealed class DocumentTable : MetadataTable<Row<uint, uint, uint, uint>>
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00004AD8 File Offset: 0x00002CD8
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteBlob(this.rows[i].Col1);
				buffer.WriteGuid(this.rows[i].Col2);
				buffer.WriteBlob(this.rows[i].Col3);
				buffer.WriteGuid(this.rows[i].Col4);
			}
		}
	}
}
