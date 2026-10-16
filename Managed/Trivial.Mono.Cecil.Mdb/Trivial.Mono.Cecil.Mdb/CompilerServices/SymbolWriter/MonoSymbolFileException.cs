using System;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200000F RID: 15
	public class MonoSymbolFileException : Exception
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00004840 File Offset: 0x00002A40
		public MonoSymbolFileException()
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004848 File Offset: 0x00002A48
		public MonoSymbolFileException(string message, params object[] args)
			: base(string.Format(message, args))
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004857 File Offset: 0x00002A57
		public MonoSymbolFileException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
