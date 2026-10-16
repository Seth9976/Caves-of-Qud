using System;
using System.Runtime.Serialization;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002EE RID: 750
	[Serializable]
	public sealed class SymbolsNotMatchingException : InvalidOperationException
	{
		// Token: 0x06001303 RID: 4867 RVA: 0x0003D341 File Offset: 0x0003B541
		public SymbolsNotMatchingException(string message)
			: base(message)
		{
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0003D34A File Offset: 0x0003B54A
		private SymbolsNotMatchingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
