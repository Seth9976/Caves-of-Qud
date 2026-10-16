using System;
using System.IO;
using Mono.Cecil.PE;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002EF RID: 751
	internal class DefaultSymbolReaderProvider : ISymbolReaderProvider
	{
		// Token: 0x06001305 RID: 4869 RVA: 0x0003D354 File Offset: 0x0003B554
		public DefaultSymbolReaderProvider()
			: this(true)
		{
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0003D35D File Offset: 0x0003B55D
		public DefaultSymbolReaderProvider(bool throwIfNoSymbol)
		{
			this.throw_if_no_symbol = throwIfNoSymbol;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0003D36C File Offset: 0x0003B56C
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
				catch (Exception)
				{
				}
			}
			if (File.Exists(Mixin.GetMdbFileName(fileName)))
			{
				try
				{
					return SymbolProvider.GetReaderProvider(SymbolKind.Mdb).GetSymbolReader(module, fileName);
				}
				catch (Exception)
				{
				}
			}
			if (this.throw_if_no_symbol)
			{
				throw new SymbolsNotFoundException(string.Format("No symbol found for file: {0}", fileName));
			}
			return null;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0003D440 File Offset: 0x0003B640
		public ISymbolReader GetSymbolReader(ModuleDefinition module, Stream symbolStream)
		{
			if (module.Image.HasDebugTables())
			{
				return null;
			}
			if (module.HasDebugHeader && module.GetDebugHeader().GetEmbeddedPortablePdbEntry() != null)
			{
				return new EmbeddedPortablePdbReaderProvider().GetSymbolReader(module, "");
			}
			Mixin.CheckStream(symbolStream);
			Mixin.CheckReadSeek(symbolStream);
			long position = symbolStream.Position;
			BinaryStreamReader binaryStreamReader = new BinaryStreamReader(symbolStream);
			int num = binaryStreamReader.ReadInt32();
			symbolStream.Position = position;
			if (num == 1112167234)
			{
				return new PortablePdbReaderProvider().GetSymbolReader(module, symbolStream);
			}
			byte[] array = binaryStreamReader.ReadBytes("Microsoft C/C++ MSF 7.00".Length);
			symbolStream.Position = position;
			bool flag = true;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != (byte)"Microsoft C/C++ MSF 7.00"[i])
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				try
				{
					return SymbolProvider.GetReaderProvider(SymbolKind.NativePdb).GetSymbolReader(module, symbolStream);
				}
				catch (Exception)
				{
				}
			}
			long num2 = binaryStreamReader.ReadInt64();
			symbolStream.Position = position;
			if (num2 == 5037318119232611860L)
			{
				try
				{
					return SymbolProvider.GetReaderProvider(SymbolKind.Mdb).GetSymbolReader(module, symbolStream);
				}
				catch (Exception)
				{
				}
			}
			if (this.throw_if_no_symbol)
			{
				throw new SymbolsNotFoundException(string.Format("No symbols found in stream", new object[0]));
			}
			return null;
		}

		// Token: 0x040009AB RID: 2475
		private readonly bool throw_if_no_symbol;
	}
}
