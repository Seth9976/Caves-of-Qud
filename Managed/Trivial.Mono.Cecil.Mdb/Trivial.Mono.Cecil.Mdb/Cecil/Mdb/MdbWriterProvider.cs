using System;
using System.IO;
using Trivial.Mono.Cecil.Cil;

namespace Trivial.Mono.Cecil.Mdb
{
	// Token: 0x0200001F RID: 31
	public sealed class MdbWriterProvider : ISymbolWriterProvider
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x000061C2 File Offset: 0x000043C2
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			return new MdbWriter(module.Mvid, fileName);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000061DC File Offset: 0x000043DC
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream)
		{
			throw new NotImplementedException();
		}
	}
}
