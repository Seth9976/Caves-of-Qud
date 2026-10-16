using System;
using System.IO;
using System.Text;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Cecil.PE;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000127 RID: 295
	public sealed class PortablePdbWriter : ISymbolWriter, IDisposable, IMetadataSymbolWriter
	{
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x000252D0 File Offset: 0x000234D0
		private bool IsEmbedded
		{
			get
			{
				return this.writer == null;
			}
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000252DB File Offset: 0x000234DB
		internal PortablePdbWriter(MetadataBuilder pdb_metadata, ModuleDefinition module)
		{
			this.pdb_metadata = pdb_metadata;
			this.module = module;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x000252F1 File Offset: 0x000234F1
		internal PortablePdbWriter(MetadataBuilder pdb_metadata, ModuleDefinition module, ImageWriter writer)
			: this(pdb_metadata, module)
		{
			this.writer = writer;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00025302 File Offset: 0x00023502
		void IMetadataSymbolWriter.SetMetadata(MetadataBuilder metadata)
		{
			this.module_metadata = metadata;
			if (this.module_metadata != this.pdb_metadata)
			{
				this.pdb_metadata.metadata_builder = metadata;
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00025325 File Offset: 0x00023525
		void IMetadataSymbolWriter.WriteModule()
		{
			this.pdb_metadata.AddCustomDebugInformations(this.module);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00025338 File Offset: 0x00023538
		public ISymbolReaderProvider GetReaderProvider()
		{
			return new PortablePdbReaderProvider();
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00025340 File Offset: 0x00023540
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
			if (!string.IsNullOrEmpty(text))
			{
				text = Path.GetFileName(text);
			}
			byteBuffer.WriteBytes(Encoding.UTF8.GetBytes(text));
			byteBuffer.WriteByte(0);
			byte[] array = new byte[byteBuffer.length];
			Buffer.BlockCopy(byteBuffer.buffer, 0, array, 0, byteBuffer.length);
			imageDebugDirectory.SizeOfData = array.Length;
			return new ImageDebugHeader(new ImageDebugHeaderEntry(imageDebugDirectory, array));
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002543B File Offset: 0x0002363B
		public void Write(MethodDebugInformation info)
		{
			this.CheckMethodDebugInformationTable();
			this.pdb_metadata.AddMethodDebugInformation(info);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00025450 File Offset: 0x00023650
		private void CheckMethodDebugInformationTable()
		{
			MethodDebugInformationTable table = this.pdb_metadata.table_heap.GetTable<MethodDebugInformationTable>(Table.MethodDebugInformation);
			if (table.length <= 0)
			{
				table.rows = new Row<uint, uint>[this.module_metadata.method_rid - 1U];
				table.length = table.rows.Length;
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002549F File Offset: 0x0002369F
		public void Dispose()
		{
			if (!this.IsEmbedded)
			{
				this.WritePdbFile();
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000254B0 File Offset: 0x000236B0
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

		// Token: 0x06000B2B RID: 2859 RVA: 0x00025508 File Offset: 0x00023708
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

		// Token: 0x06000B2C RID: 2860 RVA: 0x000255DC File Offset: 0x000237DC
		private void WriteTableHeap()
		{
			this.pdb_metadata.table_heap.string_offsets = this.pdb_metadata.string_heap.WriteStrings();
			this.pdb_metadata.table_heap.ComputeTableInformations();
			this.pdb_metadata.table_heap.WriteTableHeap();
		}

		// Token: 0x0400069B RID: 1691
		private readonly MetadataBuilder pdb_metadata;

		// Token: 0x0400069C RID: 1692
		private readonly ModuleDefinition module;

		// Token: 0x0400069D RID: 1693
		private readonly ImageWriter writer;

		// Token: 0x0400069E RID: 1694
		private MetadataBuilder module_metadata;
	}
}
