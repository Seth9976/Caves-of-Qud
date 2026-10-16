using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x0200040F RID: 1039
	internal class PdbSource
	{
		// Token: 0x060015F0 RID: 5616 RVA: 0x000467C9 File Offset: 0x000449C9
		internal PdbSource(string name, Guid doctype, Guid language, Guid vendor, Guid checksumAlgorithm, byte[] checksum)
		{
			this.name = name;
			this.doctype = doctype;
			this.language = language;
			this.vendor = vendor;
			this.checksumAlgorithm = checksumAlgorithm;
			this.checksum = checksum;
		}

		// Token: 0x04000F81 RID: 3969
		internal string name;

		// Token: 0x04000F82 RID: 3970
		internal Guid doctype;

		// Token: 0x04000F83 RID: 3971
		internal Guid language;

		// Token: 0x04000F84 RID: 3972
		internal Guid vendor;

		// Token: 0x04000F85 RID: 3973
		internal Guid checksumAlgorithm;

		// Token: 0x04000F86 RID: 3974
		internal byte[] checksum;
	}
}
