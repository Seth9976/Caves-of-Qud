using System;
using System.IO;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002F3 RID: 755
	public interface ISymbolWriterProvider
	{
		// Token: 0x06001311 RID: 4881
		ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fileName);

		// Token: 0x06001312 RID: 4882
		ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream);
	}
}
