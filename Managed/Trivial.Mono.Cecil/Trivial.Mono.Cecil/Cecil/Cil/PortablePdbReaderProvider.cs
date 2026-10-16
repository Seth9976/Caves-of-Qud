using System;
using System.IO;
using Trivial.Mono.Cecil.PE;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000126 RID: 294
	public sealed class PortablePdbReaderProvider : ISymbolReaderProvider
	{
		// Token: 0x06000B1C RID: 2844 RVA: 0x00025268 File Offset: 0x00023468
		public ISymbolReader GetSymbolReader(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			FileStream fileStream = File.OpenRead(Mixin.GetPdbFileName(fileName));
			return this.GetSymbolReader(module, Disposable.Owned<Stream>(fileStream), fileStream.Name);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000252A0 File Offset: 0x000234A0
		public ISymbolReader GetSymbolReader(ModuleDefinition module, Stream symbolStream)
		{
			Mixin.CheckModule(module);
			Mixin.CheckStream(symbolStream);
			return this.GetSymbolReader(module, Disposable.NotOwned<Stream>(symbolStream), symbolStream.GetFileName());
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x000252C1 File Offset: 0x000234C1
		private ISymbolReader GetSymbolReader(ModuleDefinition module, Disposable<Stream> symbolStream, string fileName)
		{
			return new PortablePdbReader(ImageReader.ReadPortablePdb(symbolStream, fileName), module);
		}
	}
}
