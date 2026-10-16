using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Internal;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000020 RID: 32
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class PEBuilder
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00003D2B File Offset: 0x00001F2B
		public PEHeaderBuilder Header { get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00003D33 File Offset: 0x00001F33
		public Func<IEnumerable<Blob>, BlobContentId> IdProvider { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00003D3B File Offset: 0x00001F3B
		public bool IsDeterministic { get; }

		/// <param name="header" />
		/// <param name="deterministicIdProvider" />
		// Token: 0x0600010D RID: 269 RVA: 0x00003D44 File Offset: 0x00001F44
		protected PEBuilder(PEHeaderBuilder header, [Nullable(new byte[] { 2, 1 })] Func<IEnumerable<Blob>, BlobContentId> deterministicIdProvider)
		{
			if (header == null)
			{
				Throw.ArgumentNull("header");
			}
			this.IdProvider = deterministicIdProvider ?? BlobContentId.GetTimeBasedProvider();
			this.IsDeterministic = deterministicIdProvider != null;
			this.Header = header;
			this._lazySections = new Lazy<ImmutableArray<PEBuilder.Section>>(new Func<ImmutableArray<PEBuilder.Section>>(this.CreateSections));
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00003DA0 File Offset: 0x00001FA0
		[NullableContext(0)]
		protected ImmutableArray<PEBuilder.Section> GetSections()
		{
			ImmutableArray<PEBuilder.Section> value = this._lazySections.Value;
			if (value.IsDefault)
			{
				throw new InvalidOperationException(SR.Format(SR.MustNotReturnNull, "CreateSections"));
			}
			return value;
		}

		// Token: 0x0600010F RID: 271
		[NullableContext(0)]
		protected abstract ImmutableArray<PEBuilder.Section> CreateSections();

		/// <param name="name" />
		/// <param name="location" />
		// Token: 0x06000110 RID: 272
		protected abstract BlobBuilder SerializeSection(string name, SectionLocation location);

		// Token: 0x06000111 RID: 273
		protected internal abstract PEDirectoriesBuilder GetDirectories();

		/// <param name="builder" />
		// Token: 0x06000112 RID: 274 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public BlobContentId Serialize(BlobBuilder builder)
		{
			ImmutableArray<PEBuilder.SerializedSection> immutableArray = this.SerializeSections();
			PEDirectoriesBuilder directories = this.GetDirectories();
			this.WritePESignature(builder);
			Blob blob;
			this.WriteCoffHeader(builder, immutableArray, out blob);
			this.WritePEHeader(builder, directories, immutableArray);
			this.WriteSectionHeaders(builder, immutableArray);
			builder.Align(this.Header.FileAlignment);
			foreach (PEBuilder.SerializedSection serializedSection in immutableArray)
			{
				builder.LinkSuffix(serializedSection.Builder);
				builder.Align(this.Header.FileAlignment);
			}
			BlobContentId blobContentId = this.IdProvider.Invoke(builder.GetBlobs());
			BlobWriter blobWriter = new BlobWriter(blob);
			blobWriter.WriteUInt32(blobContentId.Stamp);
			return blobContentId;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00003E90 File Offset: 0x00002090
		private ImmutableArray<PEBuilder.SerializedSection> SerializeSections()
		{
			ImmutableArray<PEBuilder.Section> sections = this.GetSections();
			ImmutableArray<PEBuilder.SerializedSection>.Builder builder = ImmutableArray.CreateBuilder<PEBuilder.SerializedSection>(sections.Length);
			int num = this.Header.ComputeSizeOfPEHeaders(sections.Length);
			int num2 = BitArithmetic.Align(num, this.Header.SectionAlignment);
			int num3 = BitArithmetic.Align(num, this.Header.FileAlignment);
			foreach (PEBuilder.Section section in sections)
			{
				BlobBuilder blobBuilder = this.SerializeSection(section.Name, new SectionLocation(num2, num3));
				PEBuilder.SerializedSection serializedSection = new PEBuilder.SerializedSection(blobBuilder, section.Name, section.Characteristics, num2, BitArithmetic.Align(blobBuilder.Count, this.Header.FileAlignment), num3);
				builder.Add(serializedSection);
				num2 = BitArithmetic.Align(serializedSection.RelativeVirtualAddress + serializedSection.VirtualSize, this.Header.SectionAlignment);
				num3 = serializedSection.PointerToRawData + serializedSection.SizeOfRawData;
			}
			return builder.MoveToImmutable();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003F93 File Offset: 0x00002193
		private void WritePESignature(BlobBuilder builder)
		{
			builder.WriteBytes(PEBuilder.s_dosHeader);
			builder.WriteUInt32(17744U);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003FAC File Offset: 0x000021AC
		private void WriteCoffHeader(BlobBuilder builder, ImmutableArray<PEBuilder.SerializedSection> sections, out Blob stampFixup)
		{
			builder.WriteUInt16((ushort)((this.Header.Machine == Machine.Unknown) ? Machine.I386 : this.Header.Machine));
			builder.WriteUInt16((ushort)sections.Length);
			stampFixup = builder.ReserveBytes(4);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt16((ushort)PEHeader.Size(this.Header.Is32Bit));
			builder.WriteUInt16((ushort)this.Header.ImageCharacteristics);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004030 File Offset: 0x00002230
		private void WritePEHeader(BlobBuilder builder, PEDirectoriesBuilder directories, ImmutableArray<PEBuilder.SerializedSection> sections)
		{
			builder.WriteUInt16(this.Header.Is32Bit ? 267 : 523);
			builder.WriteByte(this.Header.MajorLinkerVersion);
			builder.WriteByte(this.Header.MinorLinkerVersion);
			builder.WriteUInt32((uint)PEBuilder.SumRawDataSizes(sections, SectionCharacteristics.ContainsCode));
			builder.WriteUInt32((uint)PEBuilder.SumRawDataSizes(sections, SectionCharacteristics.ContainsInitializedData));
			builder.WriteUInt32((uint)PEBuilder.SumRawDataSizes(sections, SectionCharacteristics.ContainsUninitializedData));
			builder.WriteUInt32((uint)directories.AddressOfEntryPoint);
			int num = PEBuilder.IndexOfSection(sections, SectionCharacteristics.ContainsCode);
			builder.WriteUInt32((uint)((num != -1) ? sections[num].RelativeVirtualAddress : 0));
			if (this.Header.Is32Bit)
			{
				int num2 = PEBuilder.IndexOfSection(sections, SectionCharacteristics.ContainsInitializedData);
				builder.WriteUInt32((uint)((num2 != -1) ? sections[num2].RelativeVirtualAddress : 0));
				builder.WriteUInt32((uint)this.Header.ImageBase);
			}
			else
			{
				builder.WriteUInt64(this.Header.ImageBase);
			}
			builder.WriteUInt32((uint)this.Header.SectionAlignment);
			builder.WriteUInt32((uint)this.Header.FileAlignment);
			builder.WriteUInt16(this.Header.MajorOperatingSystemVersion);
			builder.WriteUInt16(this.Header.MinorOperatingSystemVersion);
			builder.WriteUInt16(this.Header.MajorImageVersion);
			builder.WriteUInt16(this.Header.MinorImageVersion);
			builder.WriteUInt16(this.Header.MajorSubsystemVersion);
			builder.WriteUInt16(this.Header.MinorSubsystemVersion);
			builder.WriteUInt32(0U);
			PEBuilder.SerializedSection serializedSection = sections[sections.Length - 1];
			builder.WriteUInt32((uint)BitArithmetic.Align(serializedSection.RelativeVirtualAddress + serializedSection.VirtualSize, this.Header.SectionAlignment));
			builder.WriteUInt32((uint)BitArithmetic.Align(this.Header.ComputeSizeOfPEHeaders(sections.Length), this.Header.FileAlignment));
			this._lazyChecksum = builder.ReserveBytes(4);
			new BlobWriter(this._lazyChecksum).WriteUInt32(0U);
			builder.WriteUInt16((ushort)this.Header.Subsystem);
			builder.WriteUInt16((ushort)this.Header.DllCharacteristics);
			if (this.Header.Is32Bit)
			{
				builder.WriteUInt32((uint)this.Header.SizeOfStackReserve);
				builder.WriteUInt32((uint)this.Header.SizeOfStackCommit);
				builder.WriteUInt32((uint)this.Header.SizeOfHeapReserve);
				builder.WriteUInt32((uint)this.Header.SizeOfHeapCommit);
			}
			else
			{
				builder.WriteUInt64(this.Header.SizeOfStackReserve);
				builder.WriteUInt64(this.Header.SizeOfStackCommit);
				builder.WriteUInt64(this.Header.SizeOfHeapReserve);
				builder.WriteUInt64(this.Header.SizeOfHeapCommit);
			}
			builder.WriteUInt32(0U);
			builder.WriteUInt32(16U);
			builder.WriteUInt32((uint)directories.ExportTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)directories.ExportTable.Size);
			builder.WriteUInt32((uint)directories.ImportTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)directories.ImportTable.Size);
			builder.WriteUInt32((uint)directories.ResourceTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)directories.ResourceTable.Size);
			builder.WriteUInt32((uint)directories.ExceptionTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)directories.ExceptionTable.Size);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt32((uint)directories.BaseRelocationTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)directories.BaseRelocationTable.Size);
			builder.WriteUInt32((uint)directories.DebugTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)directories.DebugTable.Size);
			builder.WriteUInt32((uint)directories.CopyrightTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)directories.CopyrightTable.Size);
			builder.WriteUInt32((uint)directories.GlobalPointerTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)directories.GlobalPointerTable.Size);
			builder.WriteUInt32((uint)directories.ThreadLocalStorageTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)directories.ThreadLocalStorageTable.Size);
			builder.WriteUInt32((uint)directories.LoadConfigTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)directories.LoadConfigTable.Size);
			builder.WriteUInt32((uint)directories.BoundImportTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)directories.BoundImportTable.Size);
			builder.WriteUInt32((uint)directories.ImportAddressTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)directories.ImportAddressTable.Size);
			builder.WriteUInt32((uint)directories.DelayImportTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)directories.DelayImportTable.Size);
			builder.WriteUInt32((uint)directories.CorHeaderTable.RelativeVirtualAddress);
			builder.WriteUInt32((uint)directories.CorHeaderTable.Size);
			builder.WriteUInt64(0UL);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000044FC File Offset: 0x000026FC
		private void WriteSectionHeaders(BlobBuilder builder, ImmutableArray<PEBuilder.SerializedSection> serializedSections)
		{
			foreach (PEBuilder.SerializedSection serializedSection in serializedSections)
			{
				PEBuilder.WriteSectionHeader(builder, serializedSection);
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000452C File Offset: 0x0000272C
		private static void WriteSectionHeader(BlobBuilder builder, PEBuilder.SerializedSection serializedSection)
		{
			if (serializedSection.VirtualSize == 0)
			{
				return;
			}
			int i = 0;
			int length = serializedSection.Name.Length;
			while (i < 8)
			{
				if (i < length)
				{
					builder.WriteByte((byte)serializedSection.Name.get_Chars(i));
				}
				else
				{
					builder.WriteByte(0);
				}
				i++;
			}
			builder.WriteUInt32((uint)serializedSection.VirtualSize);
			builder.WriteUInt32((uint)serializedSection.RelativeVirtualAddress);
			builder.WriteUInt32((uint)serializedSection.SizeOfRawData);
			builder.WriteUInt32((uint)serializedSection.PointerToRawData);
			builder.WriteUInt32(0U);
			builder.WriteUInt32(0U);
			builder.WriteUInt16(0);
			builder.WriteUInt16(0);
			builder.WriteUInt32((uint)serializedSection.Characteristics);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000045D4 File Offset: 0x000027D4
		private static int IndexOfSection(ImmutableArray<PEBuilder.SerializedSection> sections, SectionCharacteristics characteristics)
		{
			for (int i = 0; i < sections.Length; i++)
			{
				if ((sections[i].Characteristics & characteristics) == characteristics)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004608 File Offset: 0x00002808
		private static int SumRawDataSizes(ImmutableArray<PEBuilder.SerializedSection> sections, SectionCharacteristics characteristics)
		{
			int num = 0;
			for (int i = 0; i < sections.Length; i++)
			{
				if ((sections[i].Characteristics & characteristics) == characteristics)
				{
					num += sections[i].SizeOfRawData;
				}
			}
			return num;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000464C File Offset: 0x0000284C
		internal static IEnumerable<Blob> GetContentToSign(BlobBuilder peImage, int peHeadersSize, int peHeaderAlignment, Blob strongNameSignatureFixup)
		{
			int remainingHeaderToSign = peHeadersSize;
			int remainingHeader = BitArithmetic.Align(peHeadersSize, peHeaderAlignment);
			foreach (Blob blob in peImage.GetBlobs())
			{
				int blobStart = blob.Start;
				int blobLength = blob.Length;
				while (blobLength > 0)
				{
					if (remainingHeader > 0)
					{
						int length;
						if (remainingHeaderToSign > 0)
						{
							length = Math.Min(remainingHeaderToSign, blobLength);
							yield return new Blob(blob.Buffer, blobStart, length);
							remainingHeaderToSign -= length;
						}
						else
						{
							length = Math.Min(remainingHeader, blobLength);
						}
						remainingHeader -= length;
						blobStart += length;
						blobLength -= length;
					}
					else
					{
						if (blob.Buffer == strongNameSignatureFixup.Buffer)
						{
							yield return PEBuilder.GetPrefixBlob(new Blob(blob.Buffer, blobStart, blobLength), strongNameSignatureFixup);
							yield return PEBuilder.GetSuffixBlob(new Blob(blob.Buffer, blobStart, blobLength), strongNameSignatureFixup);
							break;
						}
						yield return new Blob(blob.Buffer, blobStart, blobLength);
						break;
					}
				}
				blob = default(Blob);
			}
			BlobBuilder.Blobs blobs = default(BlobBuilder.Blobs);
			yield break;
			yield break;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004671 File Offset: 0x00002871
		internal static Blob GetPrefixBlob(Blob container, Blob blob)
		{
			return new Blob(container.Buffer, container.Start, blob.Start - container.Start);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004691 File Offset: 0x00002891
		internal static Blob GetSuffixBlob(Blob container, Blob blob)
		{
			return new Blob(container.Buffer, blob.Start + blob.Length, container.Start + container.Length - blob.Start - blob.Length);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000046C9 File Offset: 0x000028C9
		internal static IEnumerable<Blob> GetContentToChecksum(BlobBuilder peImage, Blob checksumFixup)
		{
			foreach (Blob blob in peImage.GetBlobs())
			{
				if (blob.Buffer == checksumFixup.Buffer)
				{
					yield return PEBuilder.GetPrefixBlob(blob, checksumFixup);
					yield return PEBuilder.GetSuffixBlob(blob, checksumFixup);
				}
				else
				{
					yield return blob;
				}
				blob = default(Blob);
			}
			BlobBuilder.Blobs blobs = default(BlobBuilder.Blobs);
			yield break;
			yield break;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000046E0 File Offset: 0x000028E0
		internal void Sign(BlobBuilder peImage, Blob strongNameSignatureFixup, Func<IEnumerable<Blob>, byte[]> signatureProvider)
		{
			int num = this.Header.ComputeSizeOfPEHeaders(this.GetSections().Length);
			byte[] array = signatureProvider.Invoke(PEBuilder.GetContentToSign(peImage, num, this.Header.FileAlignment, strongNameSignatureFixup));
			if (array == null || array.Length > strongNameSignatureFixup.Length)
			{
				throw new InvalidOperationException(SR.SignatureProviderReturnedInvalidSignature);
			}
			BlobWriter blobWriter = new BlobWriter(strongNameSignatureFixup);
			blobWriter.WriteBytes(array);
			uint num2 = PEBuilder.CalculateChecksum(peImage, this._lazyChecksum);
			new BlobWriter(this._lazyChecksum).WriteUInt32(num2);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000476E File Offset: 0x0000296E
		internal static uint CalculateChecksum(BlobBuilder peImage, Blob checksumFixup)
		{
			return PEBuilder.CalculateChecksum(PEBuilder.GetContentToChecksum(peImage, checksumFixup)) + (uint)peImage.Count;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004784 File Offset: 0x00002984
		private unsafe static uint CalculateChecksum(IEnumerable<Blob> blobs)
		{
			uint num = 0U;
			int num2 = -1;
			foreach (Blob blob in blobs)
			{
				ArraySegment<byte> bytes = blob.GetBytes();
				try
				{
					byte[] array;
					byte* ptr;
					if ((array = bytes.Array) == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					byte* ptr2 = ptr + bytes.Offset;
					byte* ptr3 = ptr2 + bytes.Count;
					if (num2 >= 0)
					{
						num = PEBuilder.AggregateChecksum(num, (ushort)(((int)(*ptr2) << 8) | num2));
						ptr2++;
					}
					if ((long)(ptr3 - ptr2) % 2L != 0L)
					{
						ptr3--;
						num2 = (int)(*ptr3);
					}
					else
					{
						num2 = -1;
					}
					while (ptr2 < ptr3)
					{
						num = PEBuilder.AggregateChecksum(num, *(ushort*)ptr2);
						ptr2 += 2;
					}
				}
				finally
				{
					byte[] array = null;
				}
			}
			if (num2 >= 0)
			{
				num = PEBuilder.AggregateChecksum(num, (ushort)num2);
			}
			return num;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004880 File Offset: 0x00002A80
		private static uint AggregateChecksum(uint checksum, ushort value)
		{
			uint num = checksum + (uint)value;
			return (num >> 16) + (uint)((ushort)num);
		}

		// Token: 0x0400006A RID: 106
		private readonly Lazy<ImmutableArray<PEBuilder.Section>> _lazySections;

		// Token: 0x0400006B RID: 107
		private Blob _lazyChecksum;

		// Token: 0x0400006C RID: 108
		private static readonly byte[] s_dosHeader = new byte[]
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
		};

		// Token: 0x0400006D RID: 109
		internal static int DosHeaderSize = PEBuilder.s_dosHeader.Length;

		// Token: 0x0200018D RID: 397
		[Nullable(0)]
		protected readonly struct Section
		{
			/// <param name="name" />
			/// <param name="characteristics" />
			// Token: 0x06000C19 RID: 3097 RVA: 0x0002486C File Offset: 0x00022A6C
			public Section(string name, SectionCharacteristics characteristics)
			{
				if (name == null)
				{
					Throw.ArgumentNull("name");
				}
				this.Name = name;
				this.Characteristics = characteristics;
			}

			// Token: 0x0400099F RID: 2463
			public readonly string Name;

			// Token: 0x040009A0 RID: 2464
			public readonly SectionCharacteristics Characteristics;
		}

		// Token: 0x0200018E RID: 398
		private readonly struct SerializedSection
		{
			// Token: 0x06000C1A RID: 3098 RVA: 0x00024889 File Offset: 0x00022A89
			public SerializedSection(BlobBuilder builder, string name, SectionCharacteristics characteristics, int relativeVirtualAddress, int sizeOfRawData, int pointerToRawData)
			{
				this.Name = name;
				this.Characteristics = characteristics;
				this.Builder = builder;
				this.RelativeVirtualAddress = relativeVirtualAddress;
				this.SizeOfRawData = sizeOfRawData;
				this.PointerToRawData = pointerToRawData;
			}

			// Token: 0x1700030E RID: 782
			// (get) Token: 0x06000C1B RID: 3099 RVA: 0x000248B8 File Offset: 0x00022AB8
			public int VirtualSize
			{
				get
				{
					return this.Builder.Count;
				}
			}

			// Token: 0x040009A1 RID: 2465
			public readonly BlobBuilder Builder;

			// Token: 0x040009A2 RID: 2466
			public readonly string Name;

			// Token: 0x040009A3 RID: 2467
			public readonly SectionCharacteristics Characteristics;

			// Token: 0x040009A4 RID: 2468
			public readonly int RelativeVirtualAddress;

			// Token: 0x040009A5 RID: 2469
			public readonly int SizeOfRawData;

			// Token: 0x040009A6 RID: 2470
			public readonly int PointerToRawData;
		}
	}
}
