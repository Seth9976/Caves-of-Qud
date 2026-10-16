using System;
using System.IO;
using Trivial.Mono.Cecil.Cil;

namespace Trivial.Mono.Cecil.Pdb
{
	// Token: 0x0200000A RID: 10
	public sealed class NativePdbReaderProvider : ISymbolReaderProvider
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00002F07 File Offset: 0x00001107
		public ISymbolReader GetSymbolReader(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			return new NativePdbReader(Disposable.Owned<Stream>(File.OpenRead(Mixin.GetPdbFileName(fileName))));
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00002F2A File Offset: 0x0000112A
		public ISymbolReader GetSymbolReader(ModuleDefinition module, Stream symbolStream)
		{
			Mixin.CheckModule(module);
			Mixin.CheckStream(symbolStream);
			return new NativePdbReader(Disposable.NotOwned<Stream>(symbolStream));
		}
	}
}
