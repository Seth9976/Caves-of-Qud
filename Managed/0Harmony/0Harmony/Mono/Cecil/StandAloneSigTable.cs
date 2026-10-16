using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x020001D3 RID: 467
	internal sealed class StandAloneSigTable : MetadataTable<uint>
	{
		// Token: 0x0600092B RID: 2347 RVA: 0x000213C8 File Offset: 0x0001F5C8
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteBlob(this.rows[i]);
			}
		}
	}
}
