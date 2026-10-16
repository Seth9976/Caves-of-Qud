using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000C8 RID: 200
	internal class PdbSource
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x00006A3D File Offset: 0x00004C3D
		internal PdbSource(string name, Guid doctype, Guid language, Guid vendor)
		{
			this.name = name;
			this.doctype = doctype;
			this.language = language;
			this.vendor = vendor;
		}

		// Token: 0x04000391 RID: 913
		internal string name;

		// Token: 0x04000392 RID: 914
		internal Guid doctype;

		// Token: 0x04000393 RID: 915
		internal Guid language;

		// Token: 0x04000394 RID: 916
		internal Guid vendor;
	}
}
