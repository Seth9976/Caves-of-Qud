using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000088 RID: 136
	internal sealed class MethodTable : MetadataTable<Row<uint, MethodImplAttributes, MethodAttributes, uint, uint, uint>>
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x00010EA0 File Offset: 0x0000F0A0
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteUInt32(this.rows[i].Col1);
				buffer.WriteUInt16((ushort)this.rows[i].Col2);
				buffer.WriteUInt16((ushort)this.rows[i].Col3);
				buffer.WriteString(this.rows[i].Col4);
				buffer.WriteBlob(this.rows[i].Col5);
				buffer.WriteRID(this.rows[i].Col6, Table.Param);
			}
		}
	}
}
