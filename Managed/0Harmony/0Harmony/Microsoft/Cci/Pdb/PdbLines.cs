using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x0200040B RID: 1035
	internal class PdbLines
	{
		// Token: 0x060015E6 RID: 5606 RVA: 0x00046404 File Offset: 0x00044604
		internal PdbLines(PdbSource file, uint count)
		{
			this.file = file;
			this.lines = new PdbLine[count];
		}

		// Token: 0x04000F72 RID: 3954
		internal PdbSource file;

		// Token: 0x04000F73 RID: 3955
		internal PdbLine[] lines;
	}
}
