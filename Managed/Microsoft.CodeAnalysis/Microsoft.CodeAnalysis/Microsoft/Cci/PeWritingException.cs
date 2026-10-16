using System;

namespace Microsoft.Cci
{
	// Token: 0x020000BB RID: 187
	internal sealed class PeWritingException : Exception
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x00016638 File Offset: 0x00014838
		public PeWritingException(Exception inner)
			: base(inner.Message, inner)
		{
		}
	}
}
