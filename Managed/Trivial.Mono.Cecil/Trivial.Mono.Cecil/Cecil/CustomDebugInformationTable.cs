using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000025 RID: 37
	internal sealed class CustomDebugInformationTable : MetadataTable<Row<uint, uint, uint>>
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00004860 File Offset: 0x00002A60
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteCodedRID(this.rows[i].Col1, CodedIndex.HasCustomDebugInformation);
				buffer.WriteGuid(this.rows[i].Col2);
				buffer.WriteBlob(this.rows[i].Col3);
			}
		}
	}
}
