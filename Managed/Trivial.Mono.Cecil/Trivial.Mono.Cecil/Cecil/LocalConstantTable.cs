using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000065 RID: 101
	internal sealed class LocalConstantTable : MetadataTable<Row<uint, uint>>
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x00007444 File Offset: 0x00005644
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteString(this.rows[i].Col1);
				buffer.WriteBlob(this.rows[i].Col2);
			}
		}
	}
}
