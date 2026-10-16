using System;
using System.IO;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.CompilerServices.SymbolWriter;

namespace Trivial.Mono.Cecil.Mdb
{
	// Token: 0x0200001D RID: 29
	public sealed class MdbReaderProvider : ISymbolReaderProvider
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00005DEF File Offset: 0x00003FEF
		public ISymbolReader GetSymbolReader(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			return new MdbReader(module, MonoSymbolFile.ReadSymbolFile(Mixin.GetMdbFileName(fileName), module.Mvid));
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005E14 File Offset: 0x00004014
		public ISymbolReader GetSymbolReader(ModuleDefinition module, Stream symbolStream)
		{
			Mixin.CheckModule(module);
			Mixin.CheckStream(symbolStream);
			MonoSymbolFile monoSymbolFile = MonoSymbolFile.ReadSymbolFile(symbolStream);
			if (!(module.Mvid != monoSymbolFile.Guid))
			{
				return new MdbReader(module, monoSymbolFile);
			}
			FileStream fileStream = symbolStream as FileStream;
			if (fileStream != null)
			{
				throw new MonoSymbolFileException("Symbol file `{0}' does not match assembly", new object[] { fileStream.Name });
			}
			throw new MonoSymbolFileException("Symbol file from stream does not match assembly", new object[0]);
		}
	}
}
