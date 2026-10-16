using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000B7 RID: 183
	internal sealed class StateMachineMethodTable : MetadataTable<Row<uint, uint>>
	{
		// Token: 0x06000793 RID: 1939 RVA: 0x00017A90 File Offset: 0x00015C90
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteRID(this.rows[i].Col1, Table.Method);
				buffer.WriteRID(this.rows[i].Col2, Table.Method);
			}
		}
	}
}
