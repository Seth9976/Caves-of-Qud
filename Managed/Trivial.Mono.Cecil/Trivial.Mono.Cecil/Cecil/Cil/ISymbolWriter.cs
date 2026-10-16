using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x0200011B RID: 283
	public interface ISymbolWriter : IDisposable
	{
		// Token: 0x06000ACC RID: 2764
		ISymbolReaderProvider GetReaderProvider();

		// Token: 0x06000ACD RID: 2765
		ImageDebugHeader GetDebugHeader();

		// Token: 0x06000ACE RID: 2766
		void Write(MethodDebugInformation info);
	}
}
