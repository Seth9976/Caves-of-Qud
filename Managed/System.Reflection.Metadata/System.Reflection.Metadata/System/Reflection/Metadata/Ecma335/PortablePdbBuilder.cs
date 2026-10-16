using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	/// <summary>Represents the builder of a Portable PDB image.</summary>
	// Token: 0x020000ED RID: 237
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PortablePdbBuilder
	{
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0001A212 File Offset: 0x00018412
		public string MetadataVersion
		{
			get
			{
				return "PDB v1.0";
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0001A219 File Offset: 0x00018419
		public ushort FormatVersion
		{
			get
			{
				return 256;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0001A220 File Offset: 0x00018420
		public Func<IEnumerable<Blob>, BlobContentId> IdProvider { get; }

		/// <summary>Creates a builder of a Portable PDB image.</summary>
		/// <param name="tablesAndHeaps">A builder populated with debug metadata entities stored in tables and values stored in heaps. The entities and values are enumerated when serializing the Portable PDB image.</param>
		/// <param name="typeSystemRowCounts">The row counts of all tables that the associated type system metadata contain. Each slot in the array corresponds to a table (<see cref="T:System.Reflection.Metadata.Ecma335.TableIndex" />). The length of the array must be equal <see cref="F:System.Reflection.Metadata.Ecma335.MetadataTokens.TableCount" />.</param>
		/// <param name="entryPoint">An entry point method definition handle.</param>
		/// <param name="idProvider">A function that calculates the ID of content represented as a sequence of blobs. If not specified, a default function that ignores the content and returns a content ID based on the current time is used (<see cref="M:System.Reflection.Metadata.BlobContentId.GetTimeBasedProvider" />). You must specify a deterministic function to produce a deterministic Portable PDB image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tablesAndHeaps" /> or <paramref name="typeSystemRowCounts" /> is <see langword="null" />.</exception>
		// Token: 0x0600092A RID: 2346 RVA: 0x0001A228 File Offset: 0x00018428
		[NullableContext(0)]
		public PortablePdbBuilder([Nullable(1)] MetadataBuilder tablesAndHeaps, ImmutableArray<int> typeSystemRowCounts, MethodDefinitionHandle entryPoint, [Nullable(new byte[] { 2, 1 })] Func<IEnumerable<Blob>, BlobContentId> idProvider = null)
		{
			if (tablesAndHeaps == null)
			{
				Throw.ArgumentNull("tablesAndHeaps");
			}
			PortablePdbBuilder.ValidateTypeSystemRowCounts(typeSystemRowCounts);
			this._builder = tablesAndHeaps;
			this._entryPoint = entryPoint;
			this._serializedMetadata = tablesAndHeaps.GetSerializedMetadata(typeSystemRowCounts, this.MetadataVersion.Length, true);
			this.IdProvider = idProvider ?? BlobContentId.GetTimeBasedProvider();
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0001A288 File Offset: 0x00018488
		private static void ValidateTypeSystemRowCounts(ImmutableArray<int> typeSystemRowCounts)
		{
			if (typeSystemRowCounts.IsDefault)
			{
				Throw.ArgumentNull("typeSystemRowCounts");
			}
			if (typeSystemRowCounts.Length != MetadataTokens.TableCount)
			{
				throw new ArgumentException(SR.Format(SR.ExpectedArrayOfSize, MetadataTokens.TableCount), "typeSystemRowCounts");
			}
			for (int i = 0; i < typeSystemRowCounts.Length; i++)
			{
				if (typeSystemRowCounts[i] != 0)
				{
					if ((typeSystemRowCounts[i] & -16777216) != 0)
					{
						throw new ArgumentOutOfRangeException("typeSystemRowCounts", SR.Format(SR.RowCountOutOfRange, i));
					}
					if (((1L << i) & 34949217910615L) == 0L)
					{
						throw new ArgumentException(SR.Format(SR.RowCountMustBeZero, i), "typeSystemRowCounts");
					}
				}
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001A34C File Offset: 0x0001854C
		private void SerializeStandalonePdbStream(BlobBuilder builder)
		{
			int count = builder.Count;
			this._pdbIdBlob = builder.ReserveBytes(20);
			builder.WriteInt32(this._entryPoint.IsNil ? 0 : MetadataTokens.GetToken(this._entryPoint));
			builder.WriteUInt64(this._serializedMetadata.Sizes.ExternalTablesMask);
			MetadataWriterUtilities.SerializeRowCounts(builder, this._serializedMetadata.Sizes.ExternalRowCounts);
			int count2 = builder.Count;
		}

		/// <summary>Serializes portable PDB content into the given <see cref="T:System.Reflection.Metadata.BlobBuilder" />.</summary>
		/// <param name="builder">The builder to write to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="builder" /> is <see langword="null" />.</exception>
		/// <returns>The ID of the serialized content.</returns>
		// Token: 0x0600092D RID: 2349 RVA: 0x0001A3C8 File Offset: 0x000185C8
		public BlobContentId Serialize(BlobBuilder builder)
		{
			if (builder == null)
			{
				Throw.ArgumentNull("builder");
			}
			MetadataBuilder.SerializeMetadataHeader(builder, this.MetadataVersion, this._serializedMetadata.Sizes);
			this.SerializeStandalonePdbStream(builder);
			this._builder.SerializeMetadataTables(builder, this._serializedMetadata.Sizes, this._serializedMetadata.StringMap, 0, 0);
			this._builder.WriteHeapsTo(builder, this._serializedMetadata.StringHeap);
			BlobContentId blobContentId = this.IdProvider.Invoke(builder.GetBlobs());
			BlobWriter blobWriter = new BlobWriter(this._pdbIdBlob);
			blobWriter.WriteGuid(blobContentId.Guid);
			blobWriter.WriteUInt32(blobContentId.Stamp);
			return blobContentId;
		}

		// Token: 0x04000556 RID: 1366
		private Blob _pdbIdBlob;

		// Token: 0x04000557 RID: 1367
		private readonly MethodDefinitionHandle _entryPoint;

		// Token: 0x04000558 RID: 1368
		private readonly MetadataBuilder _builder;

		// Token: 0x04000559 RID: 1369
		private readonly SerializedMetadata _serializedMetadata;
	}
}
