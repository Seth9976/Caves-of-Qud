using System;
using System.IO;
using Trivial.Mono.Cecil.PE;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000128 RID: 296
	public sealed class PortablePdbWriterProvider : ISymbolWriterProvider
	{
		// Token: 0x06000B2D RID: 2861 RVA: 0x0002562C File Offset: 0x0002382C
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			FileStream fileStream = File.OpenWrite(Mixin.GetPdbFileName(fileName));
			return this.GetSymbolWriter(module, Disposable.Owned<Stream>(fileStream));
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0002565E File Offset: 0x0002385E
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream)
		{
			Mixin.CheckModule(module);
			Mixin.CheckStream(symbolStream);
			return this.GetSymbolWriter(module, Disposable.NotOwned<Stream>(symbolStream));
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0002567C File Offset: 0x0002387C
		private ISymbolWriter GetSymbolWriter(ModuleDefinition module, Disposable<Stream> stream)
		{
			MetadataBuilder metadataBuilder = new MetadataBuilder(module, this);
			ImageWriter imageWriter = ImageWriter.CreateDebugWriter(module, metadataBuilder, stream);
			return new PortablePdbWriter(metadataBuilder, module, imageWriter);
		}
	}
}
