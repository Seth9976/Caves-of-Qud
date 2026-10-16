using System;
using System.IO;
using System.IO.Compression;
using Trivial.Mono.Cecil.PE;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000106 RID: 262
	public sealed class EmbeddedPortablePdbWriter : ISymbolWriter, IDisposable, IMetadataSymbolWriter
	{
		// Token: 0x06000A39 RID: 2617 RVA: 0x000227F4 File Offset: 0x000209F4
		internal EmbeddedPortablePdbWriter(Stream stream, PortablePdbWriter writer)
		{
			this.stream = stream;
			this.writer = writer;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0002280A File Offset: 0x00020A0A
		public ISymbolReaderProvider GetReaderProvider()
		{
			return new EmbeddedPortablePdbReaderProvider();
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00022814 File Offset: 0x00020A14
		public ImageDebugHeader GetDebugHeader()
		{
			this.writer.Dispose();
			ImageDebugDirectory imageDebugDirectory = new ImageDebugDirectory
			{
				Type = ImageDebugType.EmbeddedPortablePdb
			};
			MemoryStream memoryStream = new MemoryStream();
			BinaryStreamWriter binaryStreamWriter = new BinaryStreamWriter(memoryStream);
			binaryStreamWriter.WriteByte(77);
			binaryStreamWriter.WriteByte(80);
			binaryStreamWriter.WriteByte(68);
			binaryStreamWriter.WriteByte(66);
			binaryStreamWriter.WriteInt32((int)this.stream.Length);
			this.stream.Position = 0L;
			using (DeflateStream deflateStream = new DeflateStream(memoryStream, 1, true))
			{
				this.stream.CopyTo(deflateStream);
			}
			imageDebugDirectory.SizeOfData = (int)memoryStream.Length;
			return new ImageDebugHeader(new ImageDebugHeaderEntry[]
			{
				this.writer.GetDebugHeader().Entries[0],
				new ImageDebugHeaderEntry(imageDebugDirectory, memoryStream.ToArray())
			});
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00022900 File Offset: 0x00020B00
		public void Write(MethodDebugInformation info)
		{
			this.writer.Write(info);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00002921 File Offset: 0x00000B21
		public void Dispose()
		{
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002290E File Offset: 0x00020B0E
		void IMetadataSymbolWriter.SetMetadata(MetadataBuilder metadata)
		{
			((IMetadataSymbolWriter)this.writer).SetMetadata(metadata);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002291C File Offset: 0x00020B1C
		void IMetadataSymbolWriter.WriteModule()
		{
			((IMetadataSymbolWriter)this.writer).WriteModule();
		}

		// Token: 0x04000537 RID: 1335
		private readonly Stream stream;

		// Token: 0x04000538 RID: 1336
		private readonly PortablePdbWriter writer;
	}
}
