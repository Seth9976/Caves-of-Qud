using System;
using System.IO;
using Trivial.Mono.Cecil.Cil;

namespace Trivial.Mono.Cecil.Pdb
{
	// Token: 0x0200000E RID: 14
	public sealed class PdbWriterProvider : ISymbolWriterProvider
	{
		// Token: 0x06000131 RID: 305 RVA: 0x0000386A File Offset: 0x00001A6A
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			if (PdbWriterProvider.HasPortablePdbSymbols(module))
			{
				return new PortablePdbWriterProvider().GetSymbolWriter(module, fileName);
			}
			return new NativePdbWriterProvider().GetSymbolWriter(module, fileName);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00003899 File Offset: 0x00001A99
		private static bool HasPortablePdbSymbols(ModuleDefinition module)
		{
			return module.symbol_reader != null && module.symbol_reader is PortablePdbReader;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000038B3 File Offset: 0x00001AB3
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream)
		{
			Mixin.CheckModule(module);
			Mixin.CheckStream(symbolStream);
			Mixin.CheckReadSeek(symbolStream);
			if (PdbWriterProvider.HasPortablePdbSymbols(module))
			{
				return new PortablePdbWriterProvider().GetSymbolWriter(module, symbolStream);
			}
			return new NativePdbWriterProvider().GetSymbolWriter(module, symbolStream);
		}
	}
}
