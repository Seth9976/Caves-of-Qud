using System;
using System.IO;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil.PE
{
	// Token: 0x020000D3 RID: 211
	internal sealed class Image : IDisposable
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x0001D07D File Offset: 0x0001B27D
		public Image()
		{
			this.counter = new Func<Table, int>(this.GetTableLength);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001D0A4 File Offset: 0x0001B2A4
		public bool HasTable(Table table)
		{
			return this.GetTableLength(table) > 0;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001D0B0 File Offset: 0x0001B2B0
		public int GetTableLength(Table table)
		{
			return (int)this.TableHeap[table].Length;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001D0C3 File Offset: 0x0001B2C3
		public int GetTableIndexSize(Table table)
		{
			if (this.GetTableLength(table) >= 65536)
			{
				return 4;
			}
			return 2;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001D0D8 File Offset: 0x0001B2D8
		public int GetCodedIndexSize(CodedIndex coded_index)
		{
			int num = this.coded_index_sizes[(int)coded_index];
			if (num != 0)
			{
				return num;
			}
			return this.coded_index_sizes[(int)coded_index] = coded_index.GetSize(this.counter);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001D10C File Offset: 0x0001B30C
		public uint ResolveVirtualAddress(uint rva)
		{
			Section sectionAtVirtualAddress = this.GetSectionAtVirtualAddress(rva);
			if (sectionAtVirtualAddress == null)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.ResolveVirtualAddressInSection(rva, sectionAtVirtualAddress);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001D132 File Offset: 0x0001B332
		public uint ResolveVirtualAddressInSection(uint rva, Section section)
		{
			return rva + section.PointerToRawData - section.VirtualAddress;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001D144 File Offset: 0x0001B344
		public Section GetSection(string name)
		{
			Section[] sections = this.Sections;
			foreach (Section section in sections)
			{
				if (section.Name == name)
				{
					return section;
				}
			}
			return null;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0001D180 File Offset: 0x0001B380
		public Section GetSectionAtVirtualAddress(uint rva)
		{
			Section[] sections = this.Sections;
			foreach (Section section in sections)
			{
				if (rva >= section.VirtualAddress && rva < section.VirtualAddress + section.SizeOfRawData)
				{
					return section;
				}
			}
			return null;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0001D1C4 File Offset: 0x0001B3C4
		private BinaryStreamReader GetReaderAt(uint rva)
		{
			Section sectionAtVirtualAddress = this.GetSectionAtVirtualAddress(rva);
			if (sectionAtVirtualAddress == null)
			{
				return null;
			}
			BinaryStreamReader binaryStreamReader = new BinaryStreamReader(this.Stream.value);
			binaryStreamReader.MoveTo(this.ResolveVirtualAddressInSection(rva, sectionAtVirtualAddress));
			return binaryStreamReader;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001D200 File Offset: 0x0001B400
		public TRet GetReaderAt<TItem, TRet>(uint rva, TItem item, Func<TItem, BinaryStreamReader, TRet> read) where TRet : class
		{
			long position = this.Stream.value.Position;
			TRet tret;
			try
			{
				BinaryStreamReader readerAt = this.GetReaderAt(rva);
				if (readerAt == null)
				{
					tret = default(TRet);
					tret = tret;
				}
				else
				{
					tret = read.Invoke(item, readerAt);
				}
			}
			finally
			{
				this.Stream.value.Position = position;
			}
			return tret;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001D264 File Offset: 0x0001B464
		public bool HasDebugTables()
		{
			return this.HasTable(Table.Document) || this.HasTable(Table.MethodDebugInformation) || this.HasTable(Table.LocalScope) || this.HasTable(Table.LocalVariable) || this.HasTable(Table.LocalConstant) || this.HasTable(Table.StateMachineMethod) || this.HasTable(Table.CustomDebugInformation);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001D2B7 File Offset: 0x0001B4B7
		public void Dispose()
		{
			this.Stream.Dispose();
		}

		// Token: 0x04000334 RID: 820
		public Disposable<Stream> Stream;

		// Token: 0x04000335 RID: 821
		public string FileName;

		// Token: 0x04000336 RID: 822
		public ModuleKind Kind;

		// Token: 0x04000337 RID: 823
		public string RuntimeVersion;

		// Token: 0x04000338 RID: 824
		public TargetArchitecture Architecture;

		// Token: 0x04000339 RID: 825
		public ModuleCharacteristics Characteristics;

		// Token: 0x0400033A RID: 826
		public ImageDebugHeader DebugHeader;

		// Token: 0x0400033B RID: 827
		public Section[] Sections;

		// Token: 0x0400033C RID: 828
		public Section MetadataSection;

		// Token: 0x0400033D RID: 829
		public uint EntryPointToken;

		// Token: 0x0400033E RID: 830
		public uint Timestamp;

		// Token: 0x0400033F RID: 831
		public ModuleAttributes Attributes;

		// Token: 0x04000340 RID: 832
		public DataDirectory Debug;

		// Token: 0x04000341 RID: 833
		public DataDirectory Resources;

		// Token: 0x04000342 RID: 834
		public DataDirectory StrongName;

		// Token: 0x04000343 RID: 835
		public StringHeap StringHeap;

		// Token: 0x04000344 RID: 836
		public BlobHeap BlobHeap;

		// Token: 0x04000345 RID: 837
		public UserStringHeap UserStringHeap;

		// Token: 0x04000346 RID: 838
		public GuidHeap GuidHeap;

		// Token: 0x04000347 RID: 839
		public TableHeap TableHeap;

		// Token: 0x04000348 RID: 840
		public PdbHeap PdbHeap;

		// Token: 0x04000349 RID: 841
		private readonly int[] coded_index_sizes = new int[14];

		// Token: 0x0400034A RID: 842
		private readonly Func<Table, int> counter;
	}
}
