using System;
using System.IO;
using Mono.Cecil.Cil;

namespace Mono.Cecil.Mdb
{
	// Token: 0x0200031E RID: 798
	internal sealed class MdbWriterProvider : ISymbolWriterProvider
	{
		// Token: 0x06001411 RID: 5137 RVA: 0x00041559 File Offset: 0x0003F759
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			return new MdbWriter(module, fileName);
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0004156E File Offset: 0x0003F76E
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream)
		{
			throw new NotImplementedException();
		}
	}
}
