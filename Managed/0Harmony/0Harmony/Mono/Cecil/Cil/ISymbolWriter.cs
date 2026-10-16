using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002F2 RID: 754
	public interface ISymbolWriter : IDisposable
	{
		// Token: 0x0600130E RID: 4878
		ISymbolReaderProvider GetReaderProvider();

		// Token: 0x0600130F RID: 4879
		ImageDebugHeader GetDebugHeader();

		// Token: 0x06001310 RID: 4880
		void Write(MethodDebugInformation info);
	}
}
