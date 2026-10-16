using System;
using System.IO;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000BE RID: 190
	internal class PdbDebugException : IOException
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00004F67 File Offset: 0x00003167
		internal PdbDebugException(string format, params object[] args)
			: base(string.Format(format, args))
		{
		}
	}
}
