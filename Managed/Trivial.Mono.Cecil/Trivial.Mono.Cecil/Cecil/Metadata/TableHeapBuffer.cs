using System;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000F0 RID: 240
	internal sealed class TableHeapBuffer : HeapBuffer
	{
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x000025EF File Offset: 0x000007EF
		public override bool IsEmpty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001FF54 File Offset: 0x0001E154
		public TableHeapBuffer(ModuleDefinition module, MetadataBuilder metadata)
			: base(24)
		{
			this.module = module;
			this.metadata = metadata;
			this.counter = new Func<Table, int>(this.GetTableLength);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001FFB0 File Offset: 0x0001E1B0
		private int GetTableLength(Table table)
		{
			return (int)this.table_infos[(int)table].Length;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001FFC4 File Offset: 0x0001E1C4
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

		// Token: 0x06000992 RID: 2450 RVA: 0x0001FFFE File Offset: 0x0001E1FE
		public void WriteBySize(uint value, int size)
		{
			if (size == 4)
			{
				base.WriteUInt32(value);
				return;
			}
			base.WriteUInt16((ushort)value);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00020014 File Offset: 0x0001E214
		public void WriteBySize(uint value, bool large)
		{
			if (large)
			{
				base.WriteUInt32(value);
				return;
			}
			base.WriteUInt16((ushort)value);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00020029 File Offset: 0x0001E229
		public void WriteString(uint @string)
		{
			this.WriteBySize(this.string_offsets[(int)@string], this.large_string);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0002003F File Offset: 0x0001E23F
		public void WriteBlob(uint blob)
		{
			this.WriteBySize(blob, this.large_blob);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0002004E File Offset: 0x0001E24E
		public void WriteGuid(uint guid)
		{
			this.WriteBySize(guid, this.large_guid);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0002005D File Offset: 0x0001E25D
		public void WriteRID(uint rid, Table table)
		{
			this.WriteBySize(rid, this.table_infos[(int)table].IsLarge);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00020078 File Offset: 0x0001E278
		private int GetCodedIndexSize(CodedIndex coded_index)
		{
			int num = this.coded_index_sizes[(int)coded_index];
			if (num != 0)
			{
				return num;
			}
			return this.coded_index_sizes[(int)coded_index] = coded_index.GetSize(this.counter);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000200AA File Offset: 0x0001E2AA
		public void WriteCodedRID(uint rid, CodedIndex coded_index)
		{
			this.WriteBySize(rid, this.GetCodedIndexSize(coded_index));
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x000200BC File Offset: 0x0001E2BC
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

		// Token: 0x0600099B RID: 2459 RVA: 0x00020120 File Offset: 0x0001E320
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

		// Token: 0x0600099C RID: 2460 RVA: 0x00020160 File Offset: 0x0001E360
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

		// Token: 0x0600099D RID: 2461 RVA: 0x0002019C File Offset: 0x0001E39C
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

		// Token: 0x0600099E RID: 2462 RVA: 0x000201E4 File Offset: 0x0001E3E4
		public void ComputeTableInformations()
		{
			if (this.metadata.metadata_builder != null)
			{
				this.ComputeTableInformations(this.metadata.metadata_builder.table_heap);
			}
			this.ComputeTableInformations(this.metadata.table_heap);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0002021C File Offset: 0x0001E41C
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

		// Token: 0x060009A0 RID: 2464 RVA: 0x00020268 File Offset: 0x0001E468
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

		// Token: 0x060009A1 RID: 2465 RVA: 0x000202D4 File Offset: 0x0001E4D4
		private byte GetTableHeapVersion()
		{
			TargetRuntime runtime = this.module.Runtime;
			if (runtime == TargetRuntime.Net_1_0 || runtime == TargetRuntime.Net_1_1)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000202F8 File Offset: 0x0001E4F8
		public void FixupData(uint data_rva)
		{
			FieldRVATable table = this.GetTable<FieldRVATable>(Table.FieldRVA);
			if (table.length != 0)
			{
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
		}

		// Token: 0x04000410 RID: 1040
		private readonly ModuleDefinition module;

		// Token: 0x04000411 RID: 1041
		private readonly MetadataBuilder metadata;

		// Token: 0x04000412 RID: 1042
		internal readonly TableInformation[] table_infos = new TableInformation[58];

		// Token: 0x04000413 RID: 1043
		internal readonly MetadataTable[] tables = new MetadataTable[58];

		// Token: 0x04000414 RID: 1044
		private bool large_string;

		// Token: 0x04000415 RID: 1045
		private bool large_blob;

		// Token: 0x04000416 RID: 1046
		private bool large_guid;

		// Token: 0x04000417 RID: 1047
		private readonly int[] coded_index_sizes = new int[14];

		// Token: 0x04000418 RID: 1048
		private readonly Func<Table, int> counter;

		// Token: 0x04000419 RID: 1049
		internal uint[] string_offsets;
	}
}
