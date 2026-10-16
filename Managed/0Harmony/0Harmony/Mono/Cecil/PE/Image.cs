using System;
using System.IO;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace Mono.Cecil.PE
{
	// Token: 0x0200028E RID: 654
	internal sealed class Image : IDisposable
	{
		// Token: 0x06001079 RID: 4217 RVA: 0x000340A5 File Offset: 0x000322A5
		public Image()
		{
			this.counter = new Func<Table, int>(this.GetTableLength);
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x000340CC File Offset: 0x000322CC
		public bool HasTable(Table table)
		{
			return this.GetTableLength(table) > 0;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x000340D8 File Offset: 0x000322D8
		public int GetTableLength(Table table)
		{
			return (int)this.TableHeap[table].Length;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x000340EB File Offset: 0x000322EB
		public int GetTableIndexSize(Table table)
		{
			if (this.GetTableLength(table) >= 65536)
			{
				return 4;
			}
			return 2;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x00034100 File Offset: 0x00032300
		public int GetCodedIndexSize(CodedIndex coded_index)
		{
			int num = this.coded_index_sizes[(int)coded_index];
			if (num != 0)
			{
				return num;
			}
			return this.coded_index_sizes[(int)coded_index] = coded_index.GetSize(this.counter);
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00034134 File Offset: 0x00032334
		public uint ResolveVirtualAddress(uint rva)
		{
			Section sectionAtVirtualAddress = this.GetSectionAtVirtualAddress(rva);
			if (sectionAtVirtualAddress == null)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.ResolveVirtualAddressInSection(rva, sectionAtVirtualAddress);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0003415A File Offset: 0x0003235A
		public uint ResolveVirtualAddressInSection(uint rva, Section section)
		{
			return rva + section.PointerToRawData - section.VirtualAddress;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0003416C File Offset: 0x0003236C
		public Section GetSection(string name)
		{
			foreach (Section section in this.Sections)
			{
				if (section.Name == name)
				{
					return section;
				}
			}
			return null;
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x000341A4 File Offset: 0x000323A4
		public Section GetSectionAtVirtualAddress(uint rva)
		{
			foreach (Section section in this.Sections)
			{
				if (rva >= section.VirtualAddress && rva < section.VirtualAddress + section.SizeOfRawData)
				{
					return section;
				}
			}
			return null;
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x000341E8 File Offset: 0x000323E8
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

		// Token: 0x06001083 RID: 4227 RVA: 0x00034220 File Offset: 0x00032420
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
					tret = read(item, readerAt);
				}
			}
			finally
			{
				this.Stream.value.Position = position;
			}
			return tret;
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00034284 File Offset: 0x00032484
		public bool HasDebugTables()
		{
			return this.HasTable(Table.Document) || this.HasTable(Table.MethodDebugInformation) || this.HasTable(Table.LocalScope) || this.HasTable(Table.LocalVariable) || this.HasTable(Table.LocalConstant) || this.HasTable(Table.StateMachineMethod) || this.HasTable(Table.CustomDebugInformation);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x000342D7 File Offset: 0x000324D7
		public void Dispose()
		{
			this.Stream.Dispose();
		}

		// Token: 0x040005F4 RID: 1524
		public Disposable<Stream> Stream;

		// Token: 0x040005F5 RID: 1525
		public string FileName;

		// Token: 0x040005F6 RID: 1526
		public ModuleKind Kind;

		// Token: 0x040005F7 RID: 1527
		public uint Characteristics;

		// Token: 0x040005F8 RID: 1528
		public string RuntimeVersion;

		// Token: 0x040005F9 RID: 1529
		public TargetArchitecture Architecture;

		// Token: 0x040005FA RID: 1530
		public ModuleCharacteristics DllCharacteristics;

		// Token: 0x040005FB RID: 1531
		public ushort LinkerVersion;

		// Token: 0x040005FC RID: 1532
		public ushort SubSystemMajor;

		// Token: 0x040005FD RID: 1533
		public ushort SubSystemMinor;

		// Token: 0x040005FE RID: 1534
		public ImageDebugHeader DebugHeader;

		// Token: 0x040005FF RID: 1535
		public Section[] Sections;

		// Token: 0x04000600 RID: 1536
		public Section MetadataSection;

		// Token: 0x04000601 RID: 1537
		public uint EntryPointToken;

		// Token: 0x04000602 RID: 1538
		public uint Timestamp;

		// Token: 0x04000603 RID: 1539
		public ModuleAttributes Attributes;

		// Token: 0x04000604 RID: 1540
		public DataDirectory Win32Resources;

		// Token: 0x04000605 RID: 1541
		public DataDirectory Debug;

		// Token: 0x04000606 RID: 1542
		public DataDirectory Resources;

		// Token: 0x04000607 RID: 1543
		public DataDirectory StrongName;

		// Token: 0x04000608 RID: 1544
		public StringHeap StringHeap;

		// Token: 0x04000609 RID: 1545
		public BlobHeap BlobHeap;

		// Token: 0x0400060A RID: 1546
		public UserStringHeap UserStringHeap;

		// Token: 0x0400060B RID: 1547
		public GuidHeap GuidHeap;

		// Token: 0x0400060C RID: 1548
		public TableHeap TableHeap;

		// Token: 0x0400060D RID: 1549
		public PdbHeap PdbHeap;

		// Token: 0x0400060E RID: 1550
		private readonly int[] coded_index_sizes = new int[14];

		// Token: 0x0400060F RID: 1551
		private readonly Func<Table, int> counter;
	}
}
