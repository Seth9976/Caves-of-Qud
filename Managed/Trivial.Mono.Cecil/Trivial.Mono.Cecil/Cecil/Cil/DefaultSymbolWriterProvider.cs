using System;
using System.IO;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x020000FE RID: 254
	public class DefaultSymbolWriterProvider : ISymbolWriterProvider
	{
		// Token: 0x06000A20 RID: 2592 RVA: 0x00022624 File Offset: 0x00020824
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fileName)
		{
			ISymbolReader symbolReader = module.SymbolReader;
			if (symbolReader == null)
			{
				throw new InvalidOperationException();
			}
			if (module.Image != null && module.Image.HasDebugTables())
			{
				return null;
			}
			return symbolReader.GetWriterProvider().GetSymbolWriter(module, fileName);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x000060E6 File Offset: 0x000042E6
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream)
		{
			throw new NotSupportedException();
		}
	}
}
