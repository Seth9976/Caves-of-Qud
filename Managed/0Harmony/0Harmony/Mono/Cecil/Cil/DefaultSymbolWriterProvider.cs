using System;
using System.IO;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002F4 RID: 756
	internal class DefaultSymbolWriterProvider : ISymbolWriterProvider
	{
		// Token: 0x06001313 RID: 4883 RVA: 0x0003D740 File Offset: 0x0003B940
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

		// Token: 0x06001314 RID: 4884 RVA: 0x00003B32 File Offset: 0x00001D32
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream)
		{
			throw new NotSupportedException();
		}
	}
}
