using System;
using System.IO;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x0200011A RID: 282
	public interface ISymbolReaderProvider
	{
		// Token: 0x06000ACA RID: 2762
		ISymbolReader GetSymbolReader(ModuleDefinition module, string fileName);

		// Token: 0x06000ACB RID: 2763
		ISymbolReader GetSymbolReader(ModuleDefinition module, Stream symbolStream);
	}
}
