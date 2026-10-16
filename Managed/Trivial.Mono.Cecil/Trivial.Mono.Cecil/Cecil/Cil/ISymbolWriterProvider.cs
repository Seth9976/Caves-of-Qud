using System;
using System.IO;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x0200011C RID: 284
	public interface ISymbolWriterProvider
	{
		// Token: 0x06000ACF RID: 2767
		ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fileName);

		// Token: 0x06000AD0 RID: 2768
		ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream);
	}
}
