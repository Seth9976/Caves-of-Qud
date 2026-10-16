using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x020001DA RID: 474
	internal sealed class ModuleRefTable : MetadataTable<uint>
	{
		// Token: 0x0600093A RID: 2362 RVA: 0x0002165C File Offset: 0x0001F85C
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteString(this.rows[i]);
			}
		}
	}
}
