using System;
using System.IO;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000400 RID: 1024
	internal class PdbDebugException : IOException
	{
		// Token: 0x060015BE RID: 5566 RVA: 0x000449E1 File Offset: 0x00042BE1
		internal PdbDebugException(string format, params object[] args)
			: base(string.Format(format, args))
		{
		}
	}
}
