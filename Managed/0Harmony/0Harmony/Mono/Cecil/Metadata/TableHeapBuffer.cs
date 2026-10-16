using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x02000296 RID: 662
	internal sealed class TableHeapBuffer : HeapBuffer
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x00018318 File Offset: 0x00016518
		public override bool IsEmpty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00036A50 File Offset: 0x00034C50
		public TableHeapBuffer(ModuleDefinition module, MetadataBuilder metadata)
			: base(24)
		{
			this.module = module;
			this.metadata = metadata;
			this.counter = new Func<Table, int>(this.GetTableLength);
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00036AAC File Offset: 0x00034CAC
		private int GetTableLength(Table table)
		{
			return (int)this.table_infos[(int)table].Length;
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00036AC0 File Offset: 0x00034CC0
		public TTable GetTable<TTable>(Table table) where TTable : MetadataTable, new()
		{
			TTable ttable = (TTable)((object)this.tables[(int)table]);
			if (ttable != null)
			{
				return ttable;
			}
			ttable = new TTable();
			this.tables[(int)table] = ttable;
			return ttable;
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00036AFA File Offset: 0x00034CFA
		public void WriteBySize(uint value, int size)
		{
			if (size == 4)
			{
				base.WriteUInt32(value);
				return;
			}
			base.WriteUInt16((ushort)value);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00036B10 File Offset: 0x00034D10
		public void WriteBySize(uint value, bool large)
		{
			if (large)
			{
				base.WriteUInt32(value);
				return;
			}
			base.WriteUInt16((ushort)value);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00036B25 File Offset: 0x00034D25
		public void WriteString(uint @string)
		{
			this.WriteBySize(this.string_offsets[(int)@string], this.large_string);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00036B3B File Offset: 0x00034D3B
		public void WriteBlob(uint blob)
		{
			this.WriteBySize(blob, this.large_blob);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00036B4A File Offset: 0x00034D4A
		public void WriteGuid(uint guid)
		{
			this.WriteBySize(guid, this.large_guid);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00036B59 File Offset: 0x00034D59
		public void WriteRID(uint rid, Table table)
		{
			this.WriteBySize(rid, this.table_infos[(int)table].IsLarge);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00036B74 File Offset: 0x00034D74
		private int GetCodedIndexSize(CodedIndex coded_index)
		{
			int num = this.coded_index_sizes[(int)coded_index];
			if (num != 0)
			{
				return num;
			}
			return this.coded_index_sizes[(int)coded_index] = coded_index.GetSize(this.counter);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00036BA8 File Offset: 0x00034DA8
		public void WriteCodedRID(uint rid, CodedIndex coded_index)
		{
			this.WriteBySize(rid, this.GetCodedIndexSize(coded_index));
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00036BB8 File Offset: 0x00034DB8
		public void WriteTableHeap()
		{
			base.WriteUInt32(0U);
			base.WriteByte(this.GetTableHeapVersion());
			base.WriteByte(0);
			base.WriteByte(this.GetHeapSizes());
			base.WriteByte(10);
			base.WriteUInt64(this.GetValid());
			base.WriteUInt64(55193285546867200UL);
			this.WriteRowCount();
			this.WriteTables();
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00036C1C File Offset: 0x00034E1C
		private void WriteRowCount()
		{
			for (int i = 0; i < this.tables.Length; i++)
			{
				MetadataTable metadataTable = this.tables[i];
				if (metadataTable != null && metadataTable.Length != 0)
				{
					base.WriteUInt32((uint)metadataTable.Length);
				}
			}
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00036C5C File Offset: 0x00034E5C
		private void WriteTables()
		{
			for (int i = 0; i < this.tables.Length; i++)
			{
				MetadataTable metadataTable = this.tables[i];
				if (metadataTable != null && metadataTable.Length != 0)
				{
					metadataTable.Write(this);
				}
			}
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00036C98 File Offset: 0x00034E98
		private ulong GetValid()
		{
			ulong num = 0UL;
			for (int i = 0; i < this.tables.Length; i++)
			{
				MetadataTable metadataTable = this.tables[i];
				if (metadataTable != null && metadataTable.Length != 0)
				{
					metadataTable.Sort();
					num |= 1UL << i;
				}
			}
			return num;
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00036CE0 File Offset: 0x00034EE0
		public void ComputeTableInformations()
		{
			if (this.metadata.metadata_builder != null)
			{
				this.ComputeTableInformations(this.metadata.metadata_builder.table_heap);
			}
			this.ComputeTableInformations(this.metadata.table_heap);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00036D18 File Offset: 0x00034F18
		private void ComputeTableInformations(TableHeapBuffer table_heap)
		{
			MetadataTable[] array = table_heap.tables;
			for (int i = 0; i < array.Length; i++)
			{
				MetadataTable metadataTable = array[i];
				if (metadataTable != null && metadataTable.Length > 0)
				{
					this.table_infos[i].Length = (uint)metadataTable.Length;
				}
			}
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00036D64 File Offset: 0x00034F64
		private byte GetHeapSizes()
		{
			byte b = 0;
			if (this.metadata.string_heap.IsLarge)
			{
				this.large_string = true;
				b |= 1;
			}
			if (this.metadata.guid_heap.IsLarge)
			{
				this.large_guid = true;
				b |= 2;
			}
			if (this.metadata.blob_heap.IsLarge)
			{
				this.large_blob = true;
				b |= 4;
			}
			return b;
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00036DD0 File Offset: 0x00034FD0
		private byte GetTableHeapVersion()
		{
			TargetRuntime runtime = this.module.Runtime;
			if (runtime <= TargetRuntime.Net_1_1)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00036DF0 File Offset: 0x00034FF0
		public void FixupData(uint data_rva)
		{
			FieldRVATable table = this.GetTable<FieldRVATable>(Table.FieldRVA);
			if (table.length == 0)
			{
				return;
			}
			int num = (this.GetTable<FieldTable>(Table.Field).IsLarge ? 4 : 2);
			int position = this.position;
			this.position = table.position;
			for (int i = 0; i < table.length; i++)
			{
				uint num2 = base.ReadUInt32();
				this.position -= 4;
				base.WriteUInt32(num2 + data_rva);
				this.position += num;
			}
			this.position = position;
		}

		// Token: 0x04000641 RID: 1601
		private readonly ModuleDefinition module;

		// Token: 0x04000642 RID: 1602
		private readonly MetadataBuilder metadata;

		// Token: 0x04000643 RID: 1603
		internal readonly TableInformation[] table_infos = new TableInformation[58];

		// Token: 0x04000644 RID: 1604
		internal readonly MetadataTable[] tables = new MetadataTable[58];

		// Token: 0x04000645 RID: 1605
		private bool large_string;

		// Token: 0x04000646 RID: 1606
		private bool large_blob;

		// Token: 0x04000647 RID: 1607
		private bool large_guid;

		// Token: 0x04000648 RID: 1608
		private readonly int[] coded_index_sizes = new int[14];

		// Token: 0x04000649 RID: 1609
		private readonly Func<Table, int> counter;

		// Token: 0x0400064A RID: 1610
		internal uint[] string_offsets;
	}
}
