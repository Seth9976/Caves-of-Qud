using System;
using System.IO;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x020000FD RID: 253
	public class DefaultSymbolReaderProvider : ISymbolReaderProvider
	{
		// Token: 0x06000A1A RID: 2586 RVA: 0x00022499 File Offset: 0x00020699
		public DefaultSymbolReaderProvider()
			: this(true)
		{
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000224A2 File Offset: 0x000206A2
		public DefaultSymbolReaderProvider(bool throwIfNoSymbol)
		{
			this.throw_if_no_symbol = throwIfNoSymbol;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x000224B4 File Offset: 0x000206B4
		public ISymbolReader GetSymbolReader(ModuleDefinition module, string fileName)
		{
			if (module.Image.HasDebugTables())
			{
				return null;
			}
			if (module.HasDebugHeader && module.GetDebugHeader().GetEmbeddedPortablePdbEntry() != null)
			{
				return new EmbeddedPortablePdbReaderProvider().GetSymbolReader(module, fileName);
			}
			if (File.Exists(Mixin.GetPdbFileName(fileName)))
			{
				if (Mixin.IsPortablePdb(Mixin.GetPdbFileName(fileName)))
				{
					return new PortablePdbReaderProvider().GetSymbolReader(module, fileName);
				}
				try
				{
					return SymbolProvider.GetReaderProvider(SymbolKind.NativePdb).GetSymbolReader(module, fileName);
				}
				catch (TypeLoadException)
				{
				}
			}
			if (File.Exists(Mixin.GetMdbFileName(fileName)))
			{
				try
				{
					return SymbolProvider.GetReaderProvider(SymbolKind.Mdb).GetSymbolReader(module, fileName);
				}
				catch (TypeLoadException)
				{
				}
			}
			bool flag = this.throw_if_no_symbol;
			return null;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00022574 File Offset: 0x00020774
		public static SymbolReaderKind GetSymbolReaderKind(string fileName)
		{
			SymbolReaderKind symbolReaderKind;
			try
			{
				using (AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(fileName))
				{
					if (assemblyDefinition.Modules.Count <= 0)
					{
						symbolReaderKind = SymbolReaderKind.None;
					}
					else
					{
						ModuleDefinition moduleDefinition = assemblyDefinition.Modules[0];
						symbolReaderKind = new DefaultSymbolReaderProvider().GetSymbolReaderKind(moduleDefinition, fileName);
					}
				}
			}
			catch (Exception)
			{
				symbolReaderKind = SymbolReaderKind.None;
			}
			return symbolReaderKind;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x000225E4 File Offset: 0x000207E4
		public SymbolReaderKind GetSymbolReaderKind(ModuleDefinition module, string fileName)
		{
			if (module.Image.HasDebugTables())
			{
				return SymbolReaderKind.None;
			}
			if (File.Exists(Mixin.GetPdbFileName(fileName)))
			{
				if (!Mixin.IsPortablePdb(Mixin.GetPdbFileName(fileName)))
				{
					return SymbolReaderKind.NativePdb;
				}
				return SymbolReaderKind.PortablePdb;
			}
			else
			{
				if (File.Exists(Mixin.GetMdbFileName(fileName)))
				{
					return SymbolReaderKind.Mdb;
				}
				return SymbolReaderKind.None;
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x000060E6 File Offset: 0x000042E6
		public ISymbolReader GetSymbolReader(ModuleDefinition module, Stream symbolStream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000516 RID: 1302
		private readonly bool throw_if_no_symbol;
	}
}
