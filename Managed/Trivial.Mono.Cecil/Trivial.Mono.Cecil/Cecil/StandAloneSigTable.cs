using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000B6 RID: 182
	internal sealed class StandAloneSigTable : MetadataTable<uint>
	{
		// Token: 0x06000791 RID: 1937 RVA: 0x00017A64 File Offset: 0x00015C64
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteBlob(this.rows[i]);
			}
		}
	}
}
