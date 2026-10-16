using System;
using System.Text;
using Mono.Cecil.Metadata;
using Mono.Cecil.PE;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002CD RID: 717
	internal sealed class PortablePdbWriter : ISymbolWriter, IDisposable
	{
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x0003BDFE File Offset: 0x00039FFE
		private bool IsEmbedded
		{
			get
			{
				return this.writer == null;
			}
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0003BE0C File Offset: 0x0003A00C
		internal PortablePdbWriter(MetadataBuilder pdb_metadata, ModuleDefinition module)
		{
			this.pdb_metadata = pdb_metadata;
			this.module = module;
			this.module_metadata = module.metadata_builder;
			if (this.module_metadata != pdb_metadata)
			{
				this.pdb_metadata.metadata_builder = this.module_metadata;
			}
			pdb_metadata.AddCustomDebugInformations(module);
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0003BE5A File Offset: 0x0003A05A
		internal PortablePdbWriter(MetadataBuilder pdb_metadata, ModuleDefinition module, ImageWriter writer)
			: this(pdb_metadata, module)
		{
			this.writer = writer;
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0003BE6B File Offset: 0x0003A06B
		public ISymbolReaderProvider GetReaderProvider()
		{
			return new PortablePdbReaderProvider();
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0003BE74 File Offset: 0x0003A074
		public ImageDebugHeader GetDebugHeader()
		{
			if (this.IsEmbedded)
			{
				return new ImageDebugHeader();
			}
			ImageDebugDirectory imageDebugDirectory = new ImageDebugDirectory
			{
				MajorVersion = 256,
				MinorVersion = 20557,
				Type = ImageDebugType.CodeView,
				TimeDateStamp = (int)this.module.timestamp
			};
			ByteBuffer byteBuffer = new ByteBuffer();
			byteBuffer.WriteUInt32(1396986706U);
			byteBuffer.WriteBytes(this.module.Mvid.ToByteArray());
			byteBuffer.WriteUInt32(1U);
			string text = this.writer.BaseStream.GetFileName();
			if (string.IsNullOrEmpty(text))
			{
				text = this.module.Assembly.Name.Name + ".pdb";
			}
			byteBuffer.WriteBytes(Encoding.UTF8.GetBytes(text));
			byteBuffer.WriteByte(0);
			byte[] array = new byte[byteBuffer.length];
			Buffer.BlockCopy(byteBuffer.buffer, 0, array, 0, byteBuffer.length);
			imageDebugDirectory.SizeOfData = array.Length;
			return new ImageDebugHeader(new ImageDebugHeaderEntry(imageDebugDirectory, array));
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0003BF85 File Offset: 0x0003A185
		public void Write(MethodDebugInformation info)
		{
			this.CheckMethodDebugInformationTable();
			this.pdb_metadata.AddMethodDebugInformation(info);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0003BF9C File Offset: 0x0003A19C
		private void CheckMethodDebugInformationTable()
		{
			MethodDebugInformationTable table = this.pdb_metadata.table_heap.GetTable<MethodDebugInformationTable>(Table.MethodDebugInformation);
			if (table.length > 0)
			{
				return;
			}
			table.rows = new Row<uint, uint>[this.module_metadata.method_rid - 1U];
			table.length = table.rows.Length;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0003BFEC File Offset: 0x0003A1EC
		public void Dispose()
		{
			if (this.IsEmbedded)
			{
				return;
			}
			this.WritePdbFile();
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0003C000 File Offset: 0x0003A200
		private void WritePdbFile()
		{
			this.WritePdbHeap();
			this.WriteTableHeap();
			this.writer.BuildMetadataTextMap();
			this.writer.WriteMetadataHeader();
			this.writer.WriteMetadata();
			this.writer.Flush();
			this.writer.stream.Dispose();
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0003C058 File Offset: 0x0003A258
		private void WritePdbHeap()
		{
			PdbHeapBuffer pdb_heap = this.pdb_metadata.pdb_heap;
			pdb_heap.WriteBytes(this.module.Mvid.ToByteArray());
			pdb_heap.WriteUInt32(this.module_metadata.timestamp);
			pdb_heap.WriteUInt32(this.module_metadata.entry_point.ToUInt32());
			MetadataTable[] tables = this.module_metadata.table_heap.tables;
			ulong num = 0UL;
			for (int i = 0; i < tables.Length; i++)
			{
				if (tables[i] != null && tables[i].Length != 0)
				{
					num |= 1UL << i;
				}
			}
			pdb_heap.WriteUInt64(num);
			for (int j = 0; j < tables.Length; j++)
			{
				if (tables[j] != null && tables[j].Length != 0)
				{
					pdb_heap.WriteUInt32((uint)tables[j].Length);
				}
			}
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0003C12C File Offset: 0x0003A32C
		private void WriteTableHeap()
		{
			this.pdb_metadata.table_heap.string_offsets = this.pdb_metadata.string_heap.WriteStrings();
			this.pdb_metadata.table_heap.ComputeTableInformations();
			this.pdb_metadata.table_heap.WriteTableHeap();
		}

		// Token: 0x04000939 RID: 2361
		private readonly MetadataBuilder pdb_metadata;

		// Token: 0x0400093A RID: 2362
		private readonly ModuleDefinition module;

		// Token: 0x0400093B RID: 2363
		private readonly ImageWriter writer;

		// Token: 0x0400093C RID: 2364
		private MetadataBuilder module_metadata;
	}
}
