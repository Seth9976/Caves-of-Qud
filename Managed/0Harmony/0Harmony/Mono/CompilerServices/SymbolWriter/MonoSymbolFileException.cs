using System;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x020002FB RID: 763
	internal class MonoSymbolFileException : Exception
	{
		// Token: 0x06001320 RID: 4896 RVA: 0x0000BC3C File Offset: 0x00009E3C
		public MonoSymbolFileException()
		{
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0003D824 File Offset: 0x0003BA24
		public MonoSymbolFileException(string message, params object[] args)
			: base(string.Format(message, args))
		{
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0000BC4D File Offset: 0x00009E4D
		public MonoSymbolFileException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
