using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x0200040A RID: 1034
	internal struct PdbLine
	{
		// Token: 0x060015E5 RID: 5605 RVA: 0x000463DD File Offset: 0x000445DD
		internal PdbLine(uint offset, uint lineBegin, ushort colBegin, uint lineEnd, ushort colEnd)
		{
			this.offset = offset;
			this.lineBegin = lineBegin;
			this.colBegin = colBegin;
			this.lineEnd = lineEnd;
			this.colEnd = colEnd;
		}

		// Token: 0x04000F6D RID: 3949
		internal uint offset;

		// Token: 0x04000F6E RID: 3950
		internal uint lineBegin;

		// Token: 0x04000F6F RID: 3951
		internal uint lineEnd;

		// Token: 0x04000F70 RID: 3952
		internal ushort colBegin;

		// Token: 0x04000F71 RID: 3953
		internal ushort colEnd;
	}
}
