using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000091 RID: 145
	internal sealed class ModuleRefTable : MetadataTable<uint>
	{
		// Token: 0x0600063E RID: 1598 RVA: 0x0001410C File Offset: 0x0001230C
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteString(this.rows[i]);
			}
		}
	}
}
