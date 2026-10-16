using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x020001C5 RID: 453
	internal sealed class ModuleTable : OneRowTable<Row<uint, uint>>
	{
		// Token: 0x06000909 RID: 2313 RVA: 0x00020D40 File Offset: 0x0001EF40
		public override void Write(TableHeapBuffer buffer)
		{
			buffer.WriteUInt16(0);
			buffer.WriteString(this.row.Col1);
			buffer.WriteGuid(this.row.Col2);
			buffer.WriteUInt16(0);
			buffer.WriteUInt16(0);
		}
	}
}
