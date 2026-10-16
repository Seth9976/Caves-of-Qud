using System;
using System.Reflection.Internal;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ManagedTextSection
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003708 File Offset: 0x00001908
		public Characteristics ImageCharacteristics { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00003710 File Offset: 0x00001910
		public Machine Machine { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003718 File Offset: 0x00001918
		public int ILStreamSize { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003720 File Offset: 0x00001920
		public int MetadataSize { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00003728 File Offset: 0x00001928
		public int ResourceDataSize { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00003730 File Offset: 0x00001930
		public int StrongNameSignatureSize { get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003738 File Offset: 0x00001938
		public int DebugDataSize { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00003740 File Offset: 0x00001940
		public int MappedFieldDataSize { get; }

		// Token: 0x060000F1 RID: 241 RVA: 0x00003748 File Offset: 0x00001948
		public ManagedTextSection(Characteristics imageCharacteristics, Machine machine, int ilStreamSize, int metadataSize, int resourceDataSize, int strongNameSignatureSize, int debugDataSize, int mappedFieldDataSize)
		{
			this.MetadataSize = metadataSize;
			this.ResourceDataSize = resourceDataSize;
			this.ILStreamSize = ilStreamSize;
			this.MappedFieldDataSize = mappedFieldDataSize;
			this.StrongNameSignatureSize = strongNameSignatureSize;
			this.ImageCharacteristics = imageCharacteristics;
			this.Machine = machine;
			this.DebugDataSize = debugDataSize;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00003798 File Offset: 0x00001998
		internal bool RequiresStartupStub
		{
			get
			{
				return this.Machine == Machine.I386 || this.Machine == Machine.Unknown;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000037B2 File Offset: 0x000019B2
		internal bool Requires64bits
		{
			get
			{
				return this.Machine == Machine.Amd64 || this.Machine == Machine.IA64 || this.Machine == Machine.Arm64;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000037DD File Offset: 0x000019DD
		public bool Is32Bit
		{
			get
			{
				return !this.Requires64bits;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000037E8 File Offset: 0x000019E8
		private string CorEntryPointName
		{
			get
			{
				if ((this.ImageCharacteristics & Characteristics.Dll) == (Characteristics)0)
				{
					return "_CorExeMain";
				}
				return "_CorDllMain";
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00003803 File Offset: 0x00001A03
		private int SizeOfImportAddressTable
		{
			get
			{
				if (!this.RequiresStartupStub)
				{
					return 0;
				}
				if (!this.Is32Bit)
				{
					return 16;
				}
				return 8;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000381B File Offset: 0x00001A1B
		private int SizeOfImportTable
		{
			get
			{
				return 40 + (this.Is32Bit ? 12 : 16) + 2 + this.CorEntryPointName.Length + 1;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000383E File Offset: 0x00001A3E
		private static int SizeOfNameTable
		{
			get
			{
				return "mscoree.dll".Length + 1 + 2;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000384E File Offset: 0x00001A4E
		private int SizeOfRuntimeStartupStub
		{
			get
			{
				if (!this.Is32Bit)
				{
					return 16;
				}
				return 8;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000385C File Offset: 0x00001A5C
		public int CalculateOffsetToMappedFieldDataStream()
		{
			int num = this.ComputeOffsetToImportTable();
			if (this.RequiresStartupStub)
			{
				num += this.SizeOfImportTable + ManagedTextSection.SizeOfNameTable;
				num = BitArithmetic.Align(num, this.Is32Bit ? 4 : 8);
				num += this.SizeOfRuntimeStartupStub;
			}
			return num;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000038A4 File Offset: 0x00001AA4
		internal int ComputeOffsetToDebugDirectory()
		{
			return this.ComputeOffsetToMetadata() + this.MetadataSize + this.ResourceDataSize + this.StrongNameSignatureSize;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000038C1 File Offset: 0x00001AC1
		private int ComputeOffsetToImportTable()
		{
			return this.ComputeOffsetToDebugDirectory() + this.DebugDataSize;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000038D0 File Offset: 0x00001AD0
		public int OffsetToILStream
		{
			get
			{
				return this.SizeOfImportAddressTable + 72;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000038DB File Offset: 0x00001ADB
		private int ComputeOffsetToMetadata()
		{
			return this.OffsetToILStream + BitArithmetic.Align(this.ILStreamSize, 4);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000038F0 File Offset: 0x00001AF0
		public int ComputeSizeOfTextSection()
		{
			return this.CalculateOffsetToMappedFieldDataStream() + this.MappedFieldDataSize;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000038FF File Offset: 0x00001AFF
		public int GetEntryPointAddress(int rva)
		{
			if (!this.RequiresStartupStub)
			{
				return 0;
			}
			return rva + this.CalculateOffsetToMappedFieldDataStream() - (this.Is32Bit ? 6 : 10);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003924 File Offset: 0x00001B24
		public DirectoryEntry GetImportAddressTableDirectoryEntry(int rva)
		{
			if (!this.RequiresStartupStub)
			{
				return default(DirectoryEntry);
			}
			return new DirectoryEntry(rva, this.SizeOfImportAddressTable);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00003950 File Offset: 0x00001B50
		public DirectoryEntry GetImportTableDirectoryEntry(int rva)
		{
			if (!this.RequiresStartupStub)
			{
				return default(DirectoryEntry);
			}
			return new DirectoryEntry(rva + this.ComputeOffsetToImportTable(), (this.Is32Bit ? 66 : 70) + 13);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000398D File Offset: 0x00001B8D
		public DirectoryEntry GetCorHeaderDirectoryEntry(int rva)
		{
			return new DirectoryEntry(rva + this.SizeOfImportAddressTable, 72);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000039A0 File Offset: 0x00001BA0
		public void Serialize(BlobBuilder builder, int relativeVirtualAddess, int entryPointTokenOrRelativeVirtualAddress, CorFlags corFlags, ulong baseAddress, BlobBuilder metadataBuilder, BlobBuilder ilBuilder, [Nullable(2)] BlobBuilder mappedFieldDataBuilderOpt, [Nullable(2)] BlobBuilder resourceBuilderOpt, [Nullable(2)] BlobBuilder debugDataBuilderOpt, out Blob strongNameSignature)
		{
			int relativeVirtualAddress = this.GetImportTableDirectoryEntry(relativeVirtualAddess).RelativeVirtualAddress;
			int relativeVirtualAddress2 = this.GetImportAddressTableDirectoryEntry(relativeVirtualAddess).RelativeVirtualAddress;
			if (this.RequiresStartupStub)
			{
				this.WriteImportAddressTable(builder, relativeVirtualAddress);
			}
			this.WriteCorHeader(builder, relativeVirtualAddess, entryPointTokenOrRelativeVirtualAddress, corFlags);
			ilBuilder.Align(4);
			builder.LinkSuffix(ilBuilder);
			builder.LinkSuffix(metadataBuilder);
			if (resourceBuilderOpt != null)
			{
				builder.LinkSuffix(resourceBuilderOpt);
			}
			strongNameSignature = builder.ReserveBytes(this.StrongNameSignatureSize);
			new BlobWriter(strongNameSignature).WriteBytes(0, this.StrongNameSignatureSize);
			if (debugDataBuilderOpt != null)
			{
				builder.LinkSuffix(debugDataBuilderOpt);
			}
			if (this.RequiresStartupStub)
			{
				this.WriteImportTable(builder, relativeVirtualAddress, relativeVirtualAddress2);
				ManagedTextSection.WriteNameTable(builder);
				this.WriteRuntimeStartupStub(builder, relativeVirtualAddress2, baseAddress);
			}
			if (mappedFieldDataBuilderOpt != null)
			{
				builder.LinkSuffix(mappedFieldDataBuilderOpt);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003A70 File Offset: 0x00001C70
		private void WriteImportAddressTable(BlobBuilder builder, int importTableRva)
		{
			int count = builder.Count;
			int num = importTableRva + 40;
			int num2 = num + (this.Is32Bit ? 12 : 16);
			if (this.Is32Bit)
			{
				builder.WriteUInt32((uint)num2);
				builder.WriteUInt32(0U);
				return;
			}
			builder.WriteUInt64((ulong)num2);
			builder.WriteUInt64(0UL);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003AC4 File Offset: 0x00001CC4
		private void WriteImportTable(BlobBuilder builder, int importTableRva, int importAddressTableRva)
		{
			int count = builder.Count;
			int num = importTableRva + 40;
			int num2 = num + (this.Is32Bit ? 12 : 16);
			int num3 = num2 + 12 + 2;
			builder.WriteUInt32((uint)num);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt32((uint)num3);
			builder.WriteUInt32((uint)importAddressTableRva);
			builder.WriteBytes(0, 20);
			if (this.Is32Bit)
			{
				builder.WriteUInt32((uint)num2);
				builder.WriteUInt32(0U);
				builder.WriteUInt32(0U);
			}
			else
			{
				builder.WriteUInt64((ulong)num2);
				builder.WriteUInt64(0UL);
			}
			builder.WriteUInt16(0);
			string corEntryPointName = this.CorEntryPointName;
			for (int i = 0; i < corEntryPointName.Length; i++)
			{
				char c = corEntryPointName.get_Chars(i);
				builder.WriteByte((byte)c);
			}
			builder.WriteByte(0);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003B90 File Offset: 0x00001D90
		private static void WriteNameTable(BlobBuilder builder)
		{
			int count = builder.Count;
			string text = "mscoree.dll";
			for (int i = 0; i < text.Length; i++)
			{
				char c = text.get_Chars(i);
				builder.WriteByte((byte)c);
			}
			builder.WriteByte(0);
			builder.WriteUInt16(0);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00003BDC File Offset: 0x00001DDC
		private void WriteCorHeader(BlobBuilder builder, int textSectionRva, int entryPointTokenOrRva, CorFlags corFlags)
		{
			int num = textSectionRva + this.ComputeOffsetToMetadata();
			int num2 = num + this.MetadataSize;
			int num3 = num2 + this.ResourceDataSize;
			int count = builder.Count;
			builder.WriteUInt32(72U);
			builder.WriteUInt16(2);
			builder.WriteUInt16(5);
			builder.WriteUInt32((uint)num);
			builder.WriteUInt32((uint)this.MetadataSize);
			builder.WriteUInt32((uint)corFlags);
			builder.WriteUInt32((uint)entryPointTokenOrRva);
			builder.WriteUInt32((uint)((this.ResourceDataSize == 0) ? 0 : num2));
			builder.WriteUInt32((uint)this.ResourceDataSize);
			builder.WriteUInt32((uint)((this.StrongNameSignatureSize == 0) ? 0 : num3));
			builder.WriteUInt32((uint)this.StrongNameSignatureSize);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003CB8 File Offset: 0x00001EB8
		private void WriteRuntimeStartupStub(BlobBuilder sectionBuilder, int importAddressTableRva, ulong baseAddress)
		{
			if (this.Is32Bit)
			{
				sectionBuilder.Align(4);
				sectionBuilder.WriteUInt16(0);
				sectionBuilder.WriteByte(byte.MaxValue);
				sectionBuilder.WriteByte(37);
				sectionBuilder.WriteUInt32((uint)(importAddressTableRva + (int)((uint)baseAddress)));
				return;
			}
			sectionBuilder.Align(8);
			sectionBuilder.WriteUInt32(0U);
			sectionBuilder.WriteUInt16(0);
			sectionBuilder.WriteByte(byte.MaxValue);
			sectionBuilder.WriteByte(37);
			sectionBuilder.WriteUInt64((ulong)((long)importAddressTableRva + (long)baseAddress));
		}

		// Token: 0x04000063 RID: 99
		public const int ManagedResourcesDataAlignment = 8;

		// Token: 0x04000064 RID: 100
		private const string CorEntryPointDll = "mscoree.dll";

		// Token: 0x04000065 RID: 101
		public const int MappedFieldDataAlignment = 8;

		// Token: 0x04000066 RID: 102
		private const int CorHeaderSize = 72;
	}
}
