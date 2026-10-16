using System;
using System.IO;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000401 RID: 1025
	internal class PdbException : IOException
	{
		// Token: 0x060015BF RID: 5567 RVA: 0x000449E1 File Offset: 0x00042BE1
		internal PdbException(string format, params object[] args)
			: base(string.Format(format, args))
		{
		}
	}
}
