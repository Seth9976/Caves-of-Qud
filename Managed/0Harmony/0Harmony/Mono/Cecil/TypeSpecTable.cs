using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x020001DB RID: 475
	internal sealed class TypeSpecTable : MetadataTable<uint>
	{
		// Token: 0x0600093C RID: 2364 RVA: 0x00021688 File Offset: 0x0001F888
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteBlob(this.rows[i]);
			}
		}
	}
}
