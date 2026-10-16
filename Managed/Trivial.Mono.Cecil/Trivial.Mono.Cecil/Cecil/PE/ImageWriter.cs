using System;
using System.IO;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil.PE
{
	// Token: 0x020000D5 RID: 213
	internal sealed class ImageWriter : BinaryStreamWriter
	{
		// Token: 0x0600091A RID: 2330 RVA: 0x0001E1E8 File Offset: 0x0001C3E8
		private ImageWriter(ModuleDefinition module, string runtime_version, MetadataBuilder metadata, Disposable<Stream> stream, bool metadataOnly = false)
			: base(stream.value)
		{
			this.module = module;
			this.runtime_version = runtime_version;
			this.text_map = metadata.text_map;
			this.stream = stream;
			this.metadata = metadata;
			if (!metadataOnly)
			{
				this.pe64 = module.Architecture == TargetArchitecture.AMD64 || module.Architecture == TargetArchitecture.IA64 || module.Architecture == TargetArchitecture.ARM64;
				this.has_reloc = module.Architecture == TargetArchitecture.I386;
				this.GetDebugHeader();
				this.GetWin32Resources();
				this.BuildTextMap();
				this.sections = ((!this.has_reloc) ? 1 : 2);
			}
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001E298 File Offset: 0x0001C498
		private void GetDebugHeader()
		{
			ISymbolWriter symbol_writer = this.metadata.symbol_writer;
			if (symbol_writer != null)
			{
				this.debug_header = symbol_writer.GetDebugHeader();
			}
			if (this.module.HasDebugHeader && this.module.GetDebugHeader().GetDeterministicEntry() != null)
			{
				this.debug_header = this.debug_header.AddDeterministicEntry();
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001E2F0 File Offset: 0x0001C4F0
		private void GetWin32Resources()
		{
			Section imageResourceSection = this.GetImageResourceSection();
			if (imageResourceSection != null)
			{
				this.win32_resources = this.module.Image.GetReaderAt<uint, ByteBuffer>(imageResourceSection.VirtualAddress, imageResourceSection.SizeOfRawData, (uint s, BinaryStreamReader reader) => new ByteBuffer(reader.ReadBytes((int)s)));
			}
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0001E348 File Offset: 0x0001C548
		private Section GetImageResourceSection()
		{
			if (!this.module.HasImage)
			{
				return null;
			}
			return this.module.Image.GetSection(".rsrc");
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001E370 File Offset: 0x0001C570
		public static ImageWriter CreateWriter(ModuleDefinition module, MetadataBuilder metadata, Disposable<Stream> stream)
		{
			ImageWriter imageWriter = new ImageWriter(module, module.runtime_version, metadata, stream, false);
			imageWriter.BuildSections();
			return imageWriter;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001E394 File Offset: 0x0001C594
		public static ImageWriter CreateDebugWriter(ModuleDefinition module, MetadataBuilder metadata, Disposable<Stream> stream)
		{
			ImageWriter imageWriter = new ImageWriter(module, "PDB v1.0", metadata, stream, true);
			uint length = metadata.text_map.GetLength();
			imageWriter.text = new Section
			{
				SizeOfRawData = length,
				VirtualSize = length
			};
			return imageWriter;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001E3D8 File Offset: 0x0001C5D8
		private void BuildSections()
		{
			bool flag = this.win32_resources != null;
			if (flag)
			{
				this.sections += 1;
			}
			this.text = this.CreateSection(".text", this.text_map.GetLength(), null);
			Section section = this.text;
			if (flag)
			{
				this.rsrc = this.CreateSection(".rsrc", (uint)this.win32_resources.length, section);
				this.PatchWin32Resources(this.win32_resources);
				section = this.rsrc;
			}
			if (this.has_reloc)
			{
				this.reloc = this.CreateSection(".reloc", 12U, section);
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001E474 File Offset: 0x0001C674
		private Section CreateSection(string name, uint size, Section previous)
		{
			return new Section
			{
				Name = name,
				VirtualAddress = ((previous != null) ? (previous.VirtualAddress + ImageWriter.Align(previous.VirtualSize, 8192U)) : 8192U),
				VirtualSize = size,
				PointerToRawData = ((previous != null) ? (previous.PointerToRawData + previous.SizeOfRawData) : ImageWriter.Align(this.GetHeaderSize(), 512U)),
				SizeOfRawData = ImageWriter.Align(size, 512U)
			};
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001E4F4 File Offset: 0x0001C6F4
		private static uint Align(uint value, uint align)
		{
			align -= 1U;
			return (value + align) & ~align;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001E501 File Offset: 0x0001C701
		private void WriteDOSHeader()
		{
			this.Write(new byte[]
			{
				77, 90, 144, 0, 3, 0, 0, 0, 4, 0,
				0, 0, byte.MaxValue, byte.MaxValue, 0, 0, 184, 0, 0, 0,
				0, 0, 0, 0, 64, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				128, 0, 0, 0, 14, 31, 186, 14, 0, 180,
				9, 205, 33, 184, 1, 76, 205, 33, 84, 104,
				105, 115, 32, 112, 114, 111, 103, 114, 97, 109,
				32, 99, 97, 110, 110, 111, 116, 32, 98, 101,
				32, 114, 117, 110, 32, 105, 110, 32, 68, 79,
				83, 32, 109, 111, 100, 101, 46, 13, 13, 10,
				36, 0, 0, 0, 0, 0, 0, 0
			});
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0001E51E File Offset: 0x0001C71E
		private ushort SizeOfOptionalHeader()
		{
			return (!this.pe64) ? 224 : 240;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001E538 File Offset: 0x0001C738
		private void WritePEFileHeader()
		{
			base.WriteUInt32(17744U);
			base.WriteUInt16((ushort)this.module.Architecture);
			base.WriteUInt16(this.sections);
			base.WriteUInt32(this.metadata.timestamp);
			base.WriteUInt32(0U);
			base.WriteUInt32(0U);
			base.WriteUInt16(this.SizeOfOptionalHeader());
			ushort num = (ushort)(2 | ((!this.pe64) ? 256 : 32));
			if (this.module.Kind == ModuleKind.Dll || this.module.Kind == ModuleKind.NetModule)
			{
				num |= 8192;
			}
			base.WriteUInt16(num);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001E5D9 File Offset: 0x0001C7D9
		private Section LastSection()
		{
			if (this.reloc != null)
			{
				return this.reloc;
			}
			if (this.rsrc != null)
			{
				return this.rsrc;
			}
			return this.text;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0001E600 File Offset: 0x0001C800
		private void WriteOptionalHeaders()
		{
			base.WriteUInt16((!this.pe64) ? 267 : 523);
			base.WriteByte(8);
			base.WriteByte(0);
			base.WriteUInt32(this.text.SizeOfRawData);
			base.WriteUInt32(((this.reloc != null) ? this.reloc.SizeOfRawData : 0U) + ((this.rsrc != null) ? this.rsrc.SizeOfRawData : 0U));
			base.WriteUInt32(0U);
			Range range = this.text_map.GetRange(TextSegment.StartupStub);
			base.WriteUInt32((range.Length != 0U) ? range.Start : 0U);
			base.WriteUInt32(8192U);
			if (!this.pe64)
			{
				base.WriteUInt32(0U);
				base.WriteUInt32(4194304U);
			}
			else
			{
				base.WriteUInt64(4194304UL);
			}
			base.WriteUInt32(8192U);
			base.WriteUInt32(512U);
			base.WriteUInt16(4);
			base.WriteUInt16(0);
			base.WriteUInt16(0);
			base.WriteUInt16(0);
			base.WriteUInt16(4);
			base.WriteUInt16(0);
			base.WriteUInt32(0U);
			Section section = this.LastSection();
			base.WriteUInt32(section.VirtualAddress + ImageWriter.Align(section.VirtualSize, 8192U));
			base.WriteUInt32(this.text.PointerToRawData);
			base.WriteUInt32(0U);
			base.WriteUInt16(this.GetSubSystem());
			base.WriteUInt16((ushort)this.module.Characteristics);
			if (!this.pe64)
			{
				base.WriteUInt32(1048576U);
				base.WriteUInt32(4096U);
				base.WriteUInt32(1048576U);
				base.WriteUInt32(4096U);
			}
			else
			{
				base.WriteUInt64(1048576UL);
				base.WriteUInt64(4096UL);
				base.WriteUInt64(1048576UL);
				base.WriteUInt64(4096UL);
			}
			base.WriteUInt32(0U);
			base.WriteUInt32(16U);
			this.WriteZeroDataDirectory();
			base.WriteDataDirectory(this.text_map.GetDataDirectory(TextSegment.ImportDirectory));
			if (this.rsrc != null)
			{
				base.WriteUInt32(this.rsrc.VirtualAddress);
				base.WriteUInt32(this.rsrc.VirtualSize);
			}
			else
			{
				this.WriteZeroDataDirectory();
			}
			this.WriteZeroDataDirectory();
			this.WriteZeroDataDirectory();
			base.WriteUInt32((this.reloc != null) ? this.reloc.VirtualAddress : 0U);
			base.WriteUInt32((this.reloc != null) ? this.reloc.VirtualSize : 0U);
			if (this.text_map.GetLength(TextSegment.DebugDirectory) > 0)
			{
				base.WriteUInt32(this.text_map.GetRVA(TextSegment.DebugDirectory));
				base.WriteUInt32((uint)(this.debug_header.Entries.Length * 28));
			}
			else
			{
				this.WriteZeroDataDirectory();
			}
			this.WriteZeroDataDirectory();
			this.WriteZeroDataDirectory();
			this.WriteZeroDataDirectory();
			this.WriteZeroDataDirectory();
			this.WriteZeroDataDirectory();
			base.WriteDataDirectory(this.text_map.GetDataDirectory(TextSegment.ImportAddressTable));
			this.WriteZeroDataDirectory();
			base.WriteDataDirectory(this.text_map.GetDataDirectory(TextSegment.CLIHeader));
			this.WriteZeroDataDirectory();
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0001E90D File Offset: 0x0001CB0D
		private void WriteZeroDataDirectory()
		{
			base.WriteUInt32(0U);
			base.WriteUInt32(0U);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0001E920 File Offset: 0x0001CB20
		private ushort GetSubSystem()
		{
			switch (this.module.Kind)
			{
			case ModuleKind.Dll:
			case ModuleKind.Console:
			case ModuleKind.NetModule:
				return 3;
			case ModuleKind.Windows:
				return 2;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001E95C File Offset: 0x0001CB5C
		private void WriteSectionHeaders()
		{
			this.WriteSection(this.text, 1610612768U);
			if (this.rsrc != null)
			{
				this.WriteSection(this.rsrc, 1073741888U);
			}
			if (this.reloc != null)
			{
				this.WriteSection(this.reloc, 1107296320U);
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0001E9AC File Offset: 0x0001CBAC
		private void WriteSection(Section section, uint characteristics)
		{
			byte[] array = new byte[8];
			string name = section.Name;
			for (int i = 0; i < name.Length; i++)
			{
				array[i] = (byte)name.get_Chars(i);
			}
			base.WriteBytes(array);
			base.WriteUInt32(section.VirtualSize);
			base.WriteUInt32(section.VirtualAddress);
			base.WriteUInt32(section.SizeOfRawData);
			base.WriteUInt32(section.PointerToRawData);
			base.WriteUInt32(0U);
			base.WriteUInt32(0U);
			base.WriteUInt16(0);
			base.WriteUInt16(0);
			base.WriteUInt32(characteristics);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001EA3D File Offset: 0x0001CC3D
		private void MoveTo(uint pointer)
		{
			this.BaseStream.Seek((long)((ulong)pointer), 0);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0001EA4E File Offset: 0x0001CC4E
		private void MoveToRVA(Section section, uint rva)
		{
			this.BaseStream.Seek((long)((ulong)(section.PointerToRawData + rva - section.VirtualAddress)), 0);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001EA6D File Offset: 0x0001CC6D
		private void MoveToRVA(TextSegment segment)
		{
			this.MoveToRVA(this.text, this.text_map.GetRVA(segment));
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001EA87 File Offset: 0x0001CC87
		private void WriteRVA(uint rva)
		{
			if (!this.pe64)
			{
				base.WriteUInt32(rva);
				return;
			}
			base.WriteUInt64((ulong)rva);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001EAA4 File Offset: 0x0001CCA4
		private void PrepareSection(Section section)
		{
			this.MoveTo(section.PointerToRawData);
			if (section.SizeOfRawData <= 4096U)
			{
				this.Write(new byte[section.SizeOfRawData]);
				this.MoveTo(section.PointerToRawData);
				return;
			}
			int num = 0;
			byte[] array = new byte[4096];
			while ((long)num != (long)((ulong)section.SizeOfRawData))
			{
				int num2 = Math.Min((int)(section.SizeOfRawData - (uint)num), 4096);
				this.Write(array, 0, num2);
				num += num2;
			}
			this.MoveTo(section.PointerToRawData);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001EB30 File Offset: 0x0001CD30
		private void WriteText()
		{
			this.PrepareSection(this.text);
			if (this.has_reloc)
			{
				this.WriteRVA(this.text_map.GetRVA(TextSegment.ImportHintNameTable));
				this.WriteRVA(0U);
			}
			base.WriteUInt32(72U);
			base.WriteUInt16(2);
			base.WriteUInt16((this.module.Runtime > TargetRuntime.Net_1_1) ? 5 : 0);
			base.WriteUInt32(this.text_map.GetRVA(TextSegment.MetadataHeader));
			base.WriteUInt32(this.GetMetadataLength());
			base.WriteUInt32((uint)this.module.Attributes);
			base.WriteUInt32(this.metadata.entry_point.ToUInt32());
			base.WriteDataDirectory(this.text_map.GetDataDirectory(TextSegment.Resources));
			base.WriteDataDirectory(this.text_map.GetDataDirectory(TextSegment.StrongNameSignature));
			this.WriteZeroDataDirectory();
			this.WriteZeroDataDirectory();
			this.WriteZeroDataDirectory();
			this.WriteZeroDataDirectory();
			this.MoveToRVA(TextSegment.Code);
			base.WriteBuffer(this.metadata.code);
			this.MoveToRVA(TextSegment.Resources);
			base.WriteBuffer(this.metadata.resources);
			if (this.metadata.data.length > 0)
			{
				this.MoveToRVA(TextSegment.Data);
				base.WriteBuffer(this.metadata.data);
			}
			this.MoveToRVA(TextSegment.MetadataHeader);
			this.WriteMetadataHeader();
			this.WriteMetadata();
			if (this.text_map.GetLength(TextSegment.DebugDirectory) > 0)
			{
				this.MoveToRVA(TextSegment.DebugDirectory);
				this.WriteDebugDirectory();
			}
			if (this.has_reloc)
			{
				this.MoveToRVA(TextSegment.ImportDirectory);
				this.WriteImportDirectory();
				this.MoveToRVA(TextSegment.StartupStub);
				this.WriteStartupStub();
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001ECC4 File Offset: 0x0001CEC4
		private uint GetMetadataLength()
		{
			return this.text_map.GetRVA(TextSegment.DebugDirectory) - this.text_map.GetRVA(TextSegment.MetadataHeader);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001ECE0 File Offset: 0x0001CEE0
		public void WriteMetadataHeader()
		{
			base.WriteUInt32(1112167234U);
			base.WriteUInt16(1);
			base.WriteUInt16(1);
			base.WriteUInt32(0U);
			byte[] zeroTerminatedString = ImageWriter.GetZeroTerminatedString(this.runtime_version);
			base.WriteUInt32((uint)zeroTerminatedString.Length);
			base.WriteBytes(zeroTerminatedString);
			base.WriteUInt16(0);
			base.WriteUInt16(this.GetStreamCount());
			uint num = this.text_map.GetRVA(TextSegment.TableHeap) - this.text_map.GetRVA(TextSegment.MetadataHeader);
			this.WriteStreamHeader(ref num, TextSegment.TableHeap, "#~");
			this.WriteStreamHeader(ref num, TextSegment.StringHeap, "#Strings");
			this.WriteStreamHeader(ref num, TextSegment.UserStringHeap, "#US");
			this.WriteStreamHeader(ref num, TextSegment.GuidHeap, "#GUID");
			this.WriteStreamHeader(ref num, TextSegment.BlobHeap, "#Blob");
			this.WriteStreamHeader(ref num, TextSegment.PdbHeap, "#Pdb");
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001EDB0 File Offset: 0x0001CFB0
		private ushort GetStreamCount()
		{
			return (ushort)(2 + ((!this.metadata.user_string_heap.IsEmpty) ? 1 : 0) + ((!this.metadata.guid_heap.IsEmpty) ? 1 : 0) + ((!this.metadata.blob_heap.IsEmpty) ? 1 : 0) + ((this.metadata.pdb_heap != null) ? 1 : 0));
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0001EE18 File Offset: 0x0001D018
		private void WriteStreamHeader(ref uint offset, TextSegment heap, string name)
		{
			uint length = (uint)this.text_map.GetLength(heap);
			if (length != 0U)
			{
				base.WriteUInt32(offset);
				base.WriteUInt32(length);
				base.WriteBytes(ImageWriter.GetZeroTerminatedString(name));
				offset += length;
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0001EE56 File Offset: 0x0001D056
		private static int GetZeroTerminatedStringLength(string @string)
		{
			return (@string.Length + 1 + 3) & -4;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0001EE65 File Offset: 0x0001D065
		private static byte[] GetZeroTerminatedString(string @string)
		{
			return ImageWriter.GetString(@string, ImageWriter.GetZeroTerminatedStringLength(@string));
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001EE73 File Offset: 0x0001D073
		private static byte[] GetSimpleString(string @string)
		{
			return ImageWriter.GetString(@string, @string.Length);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0001EE84 File Offset: 0x0001D084
		private static byte[] GetString(string @string, int length)
		{
			byte[] array = new byte[length];
			for (int i = 0; i < @string.Length; i++)
			{
				array[i] = (byte)@string.get_Chars(i);
			}
			return array;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001EEB8 File Offset: 0x0001D0B8
		public void WriteMetadata()
		{
			this.WriteHeap(TextSegment.TableHeap, this.metadata.table_heap);
			this.WriteHeap(TextSegment.StringHeap, this.metadata.string_heap);
			this.WriteHeap(TextSegment.UserStringHeap, this.metadata.user_string_heap);
			this.WriteHeap(TextSegment.GuidHeap, this.metadata.guid_heap);
			this.WriteHeap(TextSegment.BlobHeap, this.metadata.blob_heap);
			this.WriteHeap(TextSegment.PdbHeap, this.metadata.pdb_heap);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0001EF35 File Offset: 0x0001D135
		private void WriteHeap(TextSegment heap, HeapBuffer buffer)
		{
			if (buffer != null && !buffer.IsEmpty)
			{
				this.MoveToRVA(heap);
				base.WriteBuffer(buffer);
			}
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001EF50 File Offset: 0x0001D150
		private void WriteDebugDirectory()
		{
			int num = (int)this.BaseStream.Position + this.debug_header.Entries.Length * 28;
			for (int i = 0; i < this.debug_header.Entries.Length; i++)
			{
				ImageDebugHeaderEntry imageDebugHeaderEntry = this.debug_header.Entries[i];
				ImageDebugDirectory directory = imageDebugHeaderEntry.Directory;
				base.WriteInt32(directory.Characteristics);
				base.WriteInt32(directory.TimeDateStamp);
				base.WriteInt16(directory.MajorVersion);
				base.WriteInt16(directory.MinorVersion);
				base.WriteInt32((int)directory.Type);
				base.WriteInt32(directory.SizeOfData);
				base.WriteInt32(directory.AddressOfRawData);
				base.WriteInt32(num);
				num += imageDebugHeaderEntry.Data.Length;
			}
			for (int j = 0; j < this.debug_header.Entries.Length; j++)
			{
				ImageDebugHeaderEntry imageDebugHeaderEntry2 = this.debug_header.Entries[j];
				base.WriteBytes(imageDebugHeaderEntry2.Data);
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001F04C File Offset: 0x0001D24C
		private void WriteImportDirectory()
		{
			base.WriteUInt32(this.text_map.GetRVA(TextSegment.ImportDirectory) + 40U);
			base.WriteUInt32(0U);
			base.WriteUInt32(0U);
			base.WriteUInt32(this.text_map.GetRVA(TextSegment.ImportHintNameTable) + 14U);
			base.WriteUInt32(this.text_map.GetRVA(TextSegment.ImportAddressTable));
			base.Advance(20);
			base.WriteUInt32(this.text_map.GetRVA(TextSegment.ImportHintNameTable));
			this.MoveToRVA(TextSegment.ImportHintNameTable);
			base.WriteUInt16(0);
			base.WriteBytes(this.GetRuntimeMain());
			base.WriteByte(0);
			base.WriteBytes(ImageWriter.GetSimpleString("mscoree.dll"));
			base.WriteUInt16(0);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001F0F9 File Offset: 0x0001D2F9
		private byte[] GetRuntimeMain()
		{
			if (this.module.Kind != ModuleKind.Dll && this.module.Kind != ModuleKind.NetModule)
			{
				return ImageWriter.GetSimpleString("_CorExeMain");
			}
			return ImageWriter.GetSimpleString("_CorDllMain");
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0001F12C File Offset: 0x0001D32C
		private void WriteStartupStub()
		{
			TargetArchitecture architecture = this.module.Architecture;
			if (architecture == TargetArchitecture.I386)
			{
				base.WriteUInt16(9727);
				base.WriteUInt32(4194304U + this.text_map.GetRVA(TextSegment.ImportAddressTable));
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001F176 File Offset: 0x0001D376
		private void WriteRsrc()
		{
			this.PrepareSection(this.rsrc);
			base.WriteBuffer(this.win32_resources);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001F190 File Offset: 0x0001D390
		private void WriteReloc()
		{
			this.PrepareSection(this.reloc);
			uint num = this.text_map.GetRVA(TextSegment.StartupStub);
			num += ((this.module.Architecture == TargetArchitecture.IA64) ? 32U : 2U);
			uint num2 = num & 4294963200U;
			base.WriteUInt32(num2);
			base.WriteUInt32(12U);
			TargetArchitecture architecture = this.module.Architecture;
			if (architecture == TargetArchitecture.I386)
			{
				base.WriteUInt32(12288U + num - num2);
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001F214 File Offset: 0x0001D414
		public void WriteImage()
		{
			this.WriteDOSHeader();
			this.WritePEFileHeader();
			this.WriteOptionalHeaders();
			this.WriteSectionHeaders();
			this.WriteText();
			if (this.rsrc != null)
			{
				this.WriteRsrc();
			}
			if (this.reloc != null)
			{
				this.WriteReloc();
			}
			this.Flush();
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001F264 File Offset: 0x0001D464
		private void BuildTextMap()
		{
			TextMap textMap = this.text_map;
			textMap.AddMap(TextSegment.Code, this.metadata.code.length, (!this.pe64) ? 4 : 16);
			textMap.AddMap(TextSegment.Resources, this.metadata.resources.length, 8);
			textMap.AddMap(TextSegment.Data, this.metadata.data.length, 4);
			if (this.metadata.data.length > 0)
			{
				this.metadata.table_heap.FixupData(textMap.GetRVA(TextSegment.Data));
			}
			textMap.AddMap(TextSegment.StrongNameSignature, this.GetStrongNameLength(), 4);
			this.BuildMetadataTextMap();
			int num = 0;
			if (this.debug_header != null && this.debug_header.HasEntries)
			{
				int num2 = this.debug_header.Entries.Length * 28;
				int num3 = (int)(textMap.GetNextRVA(TextSegment.BlobHeap) + (uint)num2);
				int num4 = 0;
				for (int i = 0; i < this.debug_header.Entries.Length; i++)
				{
					ImageDebugHeaderEntry imageDebugHeaderEntry = this.debug_header.Entries[i];
					ImageDebugDirectory directory = imageDebugHeaderEntry.Directory;
					directory.AddressOfRawData = ((imageDebugHeaderEntry.Data.Length != 0) ? num3 : 0);
					imageDebugHeaderEntry.Directory = directory;
					num4 += imageDebugHeaderEntry.Data.Length;
					num3 += num4;
				}
				num = num2 + num4;
			}
			textMap.AddMap(TextSegment.DebugDirectory, num, 4);
			if (!this.has_reloc)
			{
				uint nextRVA = textMap.GetNextRVA(TextSegment.DebugDirectory);
				textMap.AddMap(TextSegment.ImportDirectory, new Range(nextRVA, 0U));
				textMap.AddMap(TextSegment.ImportHintNameTable, new Range(nextRVA, 0U));
				textMap.AddMap(TextSegment.StartupStub, new Range(nextRVA, 0U));
				return;
			}
			uint nextRVA2 = textMap.GetNextRVA(TextSegment.DebugDirectory);
			uint num5 = nextRVA2 + 48U;
			num5 = (num5 + 15U) & 4294967280U;
			uint num6 = num5 - nextRVA2 + 27U;
			uint num7 = nextRVA2 + num6;
			num7 = ((this.module.Architecture == TargetArchitecture.IA64) ? ((num7 + 15U) & 4294967280U) : (2U + ((num7 + 3U) & 4294967292U)));
			textMap.AddMap(TextSegment.ImportDirectory, new Range(nextRVA2, num6));
			textMap.AddMap(TextSegment.ImportHintNameTable, new Range(num5, 0U));
			textMap.AddMap(TextSegment.StartupStub, new Range(num7, this.GetStartupStubLength()));
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001F484 File Offset: 0x0001D684
		public void BuildMetadataTextMap()
		{
			TextMap textMap = this.text_map;
			textMap.AddMap(TextSegment.MetadataHeader, this.GetMetadataHeaderLength(this.module.RuntimeVersion));
			textMap.AddMap(TextSegment.TableHeap, this.metadata.table_heap.length, 4);
			textMap.AddMap(TextSegment.StringHeap, this.metadata.string_heap.length, 4);
			textMap.AddMap(TextSegment.UserStringHeap, (!this.metadata.user_string_heap.IsEmpty) ? this.metadata.user_string_heap.length : 0, 4);
			textMap.AddMap(TextSegment.GuidHeap, this.metadata.guid_heap.length, 4);
			textMap.AddMap(TextSegment.BlobHeap, (!this.metadata.blob_heap.IsEmpty) ? this.metadata.blob_heap.length : 0, 4);
			textMap.AddMap(TextSegment.PdbHeap, (this.metadata.pdb_heap != null) ? this.metadata.pdb_heap.length : 0, 4);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001F580 File Offset: 0x0001D780
		private uint GetStartupStubLength()
		{
			TargetArchitecture architecture = this.module.Architecture;
			if (architecture == TargetArchitecture.I386)
			{
				return 6U;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001F5A8 File Offset: 0x0001D7A8
		private int GetMetadataHeaderLength(string runtimeVersion)
		{
			return 20 + ImageWriter.GetZeroTerminatedStringLength(runtimeVersion) + 12 + 20 + ((!this.metadata.user_string_heap.IsEmpty) ? 12 : 0) + 16 + ((!this.metadata.blob_heap.IsEmpty) ? 16 : 0) + ((this.metadata.pdb_heap != null) ? 16 : 0);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001F60C File Offset: 0x0001D80C
		private int GetStrongNameLength()
		{
			if (this.module.Assembly == null)
			{
				return 0;
			}
			byte[] publicKey = this.module.Assembly.Name.PublicKey;
			if (publicKey.IsNullOrEmpty<byte>())
			{
				return 0;
			}
			int num = publicKey.Length;
			if (num > 32)
			{
				return num - 32;
			}
			return 128;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001F65B File Offset: 0x0001D85B
		public DataDirectory GetStrongNameSignatureDirectory()
		{
			return this.text_map.GetDataDirectory(TextSegment.StrongNameSignature);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0001F669 File Offset: 0x0001D869
		public uint GetHeaderSize()
		{
			return (uint)(152 + this.SizeOfOptionalHeader() + this.sections * 40);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0001F681 File Offset: 0x0001D881
		private void PatchWin32Resources(ByteBuffer resources)
		{
			this.PatchResourceDirectoryTable(resources);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001F68C File Offset: 0x0001D88C
		private void PatchResourceDirectoryTable(ByteBuffer resources)
		{
			resources.Advance(12);
			int num = (int)(resources.ReadUInt16() + resources.ReadUInt16());
			for (int i = 0; i < num; i++)
			{
				this.PatchResourceDirectoryEntry(resources);
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001F6C4 File Offset: 0x0001D8C4
		private void PatchResourceDirectoryEntry(ByteBuffer resources)
		{
			resources.Advance(4);
			uint num = resources.ReadUInt32();
			int position = resources.position;
			resources.position = (int)(num & 2147483647U);
			if ((num & 2147483648U) != 0U)
			{
				this.PatchResourceDirectoryTable(resources);
			}
			else
			{
				this.PatchResourceDataEntry(resources);
			}
			resources.position = position;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001F714 File Offset: 0x0001D914
		private void PatchResourceDataEntry(ByteBuffer resources)
		{
			Section imageResourceSection = this.GetImageResourceSection();
			uint num = resources.ReadUInt32();
			resources.position -= 4;
			resources.WriteUInt32(num - imageResourceSection.VirtualAddress + this.rsrc.VirtualAddress);
		}

		// Token: 0x0400034F RID: 847
		private readonly ModuleDefinition module;

		// Token: 0x04000350 RID: 848
		private readonly MetadataBuilder metadata;

		// Token: 0x04000351 RID: 849
		private readonly TextMap text_map;

		// Token: 0x04000352 RID: 850
		internal readonly Disposable<Stream> stream;

		// Token: 0x04000353 RID: 851
		private readonly string runtime_version;

		// Token: 0x04000354 RID: 852
		private ImageDebugHeader debug_header;

		// Token: 0x04000355 RID: 853
		private ByteBuffer win32_resources;

		// Token: 0x04000356 RID: 854
		private const uint pe_header_size = 152U;

		// Token: 0x04000357 RID: 855
		private const uint section_header_size = 40U;

		// Token: 0x04000358 RID: 856
		private const uint file_alignment = 512U;

		// Token: 0x04000359 RID: 857
		private const uint section_alignment = 8192U;

		// Token: 0x0400035A RID: 858
		private const ulong image_base = 4194304UL;

		// Token: 0x0400035B RID: 859
		internal const uint text_rva = 8192U;

		// Token: 0x0400035C RID: 860
		private readonly bool pe64;

		// Token: 0x0400035D RID: 861
		private readonly bool has_reloc;

		// Token: 0x0400035E RID: 862
		internal Section text;

		// Token: 0x0400035F RID: 863
		internal Section rsrc;

		// Token: 0x04000360 RID: 864
		internal Section reloc;

		// Token: 0x04000361 RID: 865
		private ushort sections;
	}
}
