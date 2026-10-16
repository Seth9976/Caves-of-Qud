using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000092 RID: 146
	internal sealed class ModuleTable : OneRowTable<Row<uint, uint>>
	{
		// Token: 0x06000640 RID: 1600 RVA: 0x00014140 File Offset: 0x00012340
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
