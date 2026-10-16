using System;
using System.IO;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000BF RID: 191
	internal class PdbException : IOException
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00004F67 File Offset: 0x00003167
		internal PdbException(string format, params object[] args)
			: base(string.Format(format, args))
		{
		}
	}
}
