using System;
using System.IO;
using Trivial.Mono.Cecil.Cil;

namespace Trivial.Mono.Cecil.Pdb
{
	// Token: 0x0200000C RID: 12
	public sealed class NativePdbWriterProvider : ISymbolWriterProvider
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00003784 File Offset: 0x00001984
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			return new NativePdbWriter(module, NativePdbWriterProvider.CreateWriter(module, Mixin.GetPdbFileName(fileName)));
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000037A4 File Offset: 0x000019A4
		private static SymWriter CreateWriter(ModuleDefinition module, string pdb)
		{
			SymWriter symWriter = new SymWriter();
			if (File.Exists(pdb))
			{
				File.Delete(pdb);
			}
			symWriter.Initialize(new ModuleMetadata(module), pdb, true);
			return symWriter;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000023D0 File Offset: 0x000005D0
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream)
		{
			throw new NotImplementedException();
		}
	}
}
