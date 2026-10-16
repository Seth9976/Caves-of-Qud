using System;
using System.IO;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000107 RID: 263
	public sealed class EmbeddedPortablePdbWriterProvider : ISymbolWriterProvider
	{
		// Token: 0x06000A40 RID: 2624 RVA: 0x0002292C File Offset: 0x00020B2C
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			MemoryStream memoryStream = new MemoryStream();
			PortablePdbWriter portablePdbWriter = (PortablePdbWriter)new PortablePdbWriterProvider().GetSymbolWriter(module, memoryStream);
			return new EmbeddedPortablePdbWriter(memoryStream, portablePdbWriter);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x000060E6 File Offset: 0x000042E6
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream)
		{
			throw new NotSupportedException();
		}
	}
}
