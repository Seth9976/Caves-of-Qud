using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002EB RID: 747
	public interface ISymbolReader : IDisposable
	{
		// Token: 0x060012FC RID: 4860
		ISymbolWriterProvider GetWriterProvider();

		// Token: 0x060012FD RID: 4861
		bool ProcessDebugHeader(ImageDebugHeader header);

		// Token: 0x060012FE RID: 4862
		MethodDebugInformation Read(MethodDefinition method);
	}
}
