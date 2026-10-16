using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200007B RID: 123
	internal sealed class MethodDebugInformationTable : MetadataTable<Row<uint, uint>>
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x0000FB74 File Offset: 0x0000DD74
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteRID(this.rows[i].Col1, Table.Document);
				buffer.WriteBlob(this.rows[i].Col2);
			}
		}
	}
}
