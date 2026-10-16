using System;

namespace System.Web.Services.Discovery
{
	// Token: 0x020000B6 RID: 182
	internal class InvalidDocumentContentsException : Exception
	{
		// Token: 0x060004BB RID: 1211 RVA: 0x00016219 File Offset: 0x00014419
		internal InvalidDocumentContentsException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
