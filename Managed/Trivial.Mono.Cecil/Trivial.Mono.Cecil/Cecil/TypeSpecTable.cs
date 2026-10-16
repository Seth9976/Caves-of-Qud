using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000C9 RID: 201
	internal sealed class TypeSpecTable : MetadataTable<uint>
	{
		// Token: 0x0600086F RID: 2159 RVA: 0x0001A364 File Offset: 0x00018564
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteBlob(this.rows[i]);
			}
		}
	}
}
