using System;
using System.IO;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002EC RID: 748
	public interface ISymbolReaderProvider
	{
		// Token: 0x060012FF RID: 4863
		ISymbolReader GetSymbolReader(ModuleDefinition module, string fileName);

		// Token: 0x06001300 RID: 4864
		ISymbolReader GetSymbolReader(ModuleDefinition module, Stream symbolStream);
	}
}
