using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001D0 RID: 464
	internal sealed class ResourceException : Exception
	{
		// Token: 0x0600137B RID: 4987 RVA: 0x0003CD3E File Offset: 0x0003AF3E
		[NullableContext(2)]
		internal ResourceException(string name, Exception inner = null)
			: base(name, inner)
		{
		}
	}
}
