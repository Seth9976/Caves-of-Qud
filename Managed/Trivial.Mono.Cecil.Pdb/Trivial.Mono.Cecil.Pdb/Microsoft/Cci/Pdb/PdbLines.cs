using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000C4 RID: 196
	internal class PdbLines
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x000066B8 File Offset: 0x000048B8
		internal PdbLines(PdbSource file, uint count)
		{
			this.file = file;
			this.lines = new PdbLine[count];
		}

		// Token: 0x04000382 RID: 898
		internal PdbSource file;

		// Token: 0x04000383 RID: 899
		internal PdbLine[] lines;
	}
}
