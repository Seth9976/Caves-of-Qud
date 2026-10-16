using System;
using System.IO;
using System.IO.Compression;
using Trivial.Mono.Cecil.PE;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000105 RID: 261
	public sealed class EmbeddedPortablePdbReaderProvider : ISymbolReaderProvider
	{
		// Token: 0x06000A35 RID: 2613 RVA: 0x00022744 File Offset: 0x00020944
		public ISymbolReader GetSymbolReader(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			ImageDebugHeaderEntry embeddedPortablePdbEntry = module.GetDebugHeader().GetEmbeddedPortablePdbEntry();
			if (embeddedPortablePdbEntry == null)
			{
				throw new InvalidOperationException();
			}
			return new EmbeddedPortablePdbReader((PortablePdbReader)new PortablePdbReaderProvider().GetSymbolReader(module, EmbeddedPortablePdbReaderProvider.GetPortablePdbStream(embeddedPortablePdbEntry)));
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00022790 File Offset: 0x00020990
		private static Stream GetPortablePdbStream(ImageDebugHeaderEntry entry)
		{
			MemoryStream memoryStream = new MemoryStream(entry.Data);
			BinaryStreamReader binaryStreamReader = new BinaryStreamReader(memoryStream);
			binaryStreamReader.ReadInt32();
			MemoryStream memoryStream2 = new MemoryStream(binaryStreamReader.ReadInt32());
			Stream stream;
			using (DeflateStream deflateStream = new DeflateStream(memoryStream, 0, true))
			{
				deflateStream.CopyTo(memoryStream2);
				stream = memoryStream2;
			}
			return stream;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000060E6 File Offset: 0x000042E6
		public ISymbolReader GetSymbolReader(ModuleDefinition module, Stream symbolStream)
		{
			throw new NotSupportedException();
		}
	}
}
