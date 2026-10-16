using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000C3 RID: 195
	internal struct PdbLine
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00006691 File Offset: 0x00004891
		internal PdbLine(uint offset, uint lineBegin, ushort colBegin, uint lineEnd, ushort colEnd)
		{
			this.offset = offset;
			this.lineBegin = lineBegin;
			this.colBegin = colBegin;
			this.lineEnd = lineEnd;
			this.colEnd = colEnd;
		}

		// Token: 0x0400037D RID: 893
		internal uint offset;

		// Token: 0x0400037E RID: 894
		internal uint lineBegin;

		// Token: 0x0400037F RID: 895
		internal uint lineEnd;

		// Token: 0x04000380 RID: 896
		internal ushort colBegin;

		// Token: 0x04000381 RID: 897
		internal ushort colEnd;
	}
}
