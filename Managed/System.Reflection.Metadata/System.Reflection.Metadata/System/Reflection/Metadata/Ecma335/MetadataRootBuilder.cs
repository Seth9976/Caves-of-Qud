using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	/// <summary>Builder of a Metadata Root to be embedded in a Portable Executable image.</summary>
	// Token: 0x020000E1 RID: 225
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MetadataRootBuilder
	{
		/// <summary>The metadata version.</summary>
		/// <returns>A string that represents the metadata version.</returns>
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x000179D3 File Offset: 0x00015BD3
		public string MetadataVersion { get; }

		/// <summary>Determines if basic validation of metadata tables should be suppressed. The validation verifies that entries in the tables were added in order required by the ECMA specification. It does not enforce all specification requirements on metadata tables.</summary>
		/// <returns>
		///   <see langword="true" /> to suppress basic validation of metadata tables; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x000179DB File Offset: 0x00015BDB
		public bool SuppressValidation { get; }

		/// <summary>Creates a builder of a metadata root.</summary>
		/// <param name="tablesAndHeaps">Builder populated with metadata entities stored in tables and values stored in heaps. The entities and values will be enumerated when serializing the metadata root.</param>
		/// <param name="metadataVersion">The version string written to the metadata header. The default value is "v4.0.30319".</param>
		/// <param name="suppressValidation">
		///   <see langword="true" /> to suppress basic validation of metadata tables during serialization; otherwise, <paramref name="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tablesAndHeaps" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataVersion" /> is too long (the number of bytes when UTF8-encoded must be less than 255).</exception>
		// Token: 0x06000891 RID: 2193 RVA: 0x000179E4 File Offset: 0x00015BE4
		public MetadataRootBuilder(MetadataBuilder tablesAndHeaps, [Nullable(2)] string metadataVersion = null, bool suppressValidation = false)
		{
			if (tablesAndHeaps == null)
			{
				Throw.ArgumentNull("tablesAndHeaps");
			}
			int num = ((metadataVersion != null) ? BlobUtilities.GetUTF8ByteCount(metadataVersion) : "v4.0.30319".Length);
			if (num > 254)
			{
				Throw.InvalidArgument(SR.MetadataVersionTooLong, "metadataVersion");
			}
			this._tablesAndHeaps = tablesAndHeaps;
			this.MetadataVersion = metadataVersion ?? "v4.0.30319";
			this.SuppressValidation = suppressValidation;
			this._serializedMetadata = tablesAndHeaps.GetSerializedMetadata(MetadataRootBuilder.EmptyRowCounts, num, false);
		}

		/// <summary>Returns sizes of various metadata structures.</summary>
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x00017A62 File Offset: 0x00015C62
		public MetadataSizes Sizes
		{
			get
			{
				return this._serializedMetadata.Sizes;
			}
		}

		/// <summary>Serializes metadata root content into the given <see cref="T:System.Reflection.Metadata.BlobBuilder" />.</summary>
		/// <param name="builder">Builder to write to.</param>
		/// <param name="methodBodyStreamRva">The relative virtual address of the start of the method body stream. Used to calculate the final value of RVA fields of MethodDef table.</param>
		/// <param name="mappedFieldDataStreamRva">The relative virtual address of the start of the field init data stream. Used to calculate the final value of RVA fields of FieldRVA table.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="builder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="methodBodyStreamRva" /> or <paramref name="mappedFieldDataStreamRva" /> is negative.</exception>
		/// <exception cref="T:System.InvalidOperationException">A metadata table is not ordered as required by the specification and <see cref="P:System.Reflection.Metadata.Ecma335.MetadataRootBuilder.SuppressValidation" /> is <see langword="false" />.</exception>
		// Token: 0x06000893 RID: 2195 RVA: 0x00017A70 File Offset: 0x00015C70
		public void Serialize(BlobBuilder builder, int methodBodyStreamRva, int mappedFieldDataStreamRva)
		{
			if (builder == null)
			{
				Throw.ArgumentNull("builder");
			}
			if (methodBodyStreamRva < 0)
			{
				Throw.ArgumentOutOfRange("methodBodyStreamRva");
			}
			if (mappedFieldDataStreamRva < 0)
			{
				Throw.ArgumentOutOfRange("mappedFieldDataStreamRva");
			}
			if (!this.SuppressValidation)
			{
				this._tablesAndHeaps.ValidateOrder();
			}
			MetadataBuilder.SerializeMetadataHeader(builder, this.MetadataVersion, this._serializedMetadata.Sizes);
			this._tablesAndHeaps.SerializeMetadataTables(builder, this._serializedMetadata.Sizes, this._serializedMetadata.StringMap, methodBodyStreamRva, mappedFieldDataStreamRva);
			this._tablesAndHeaps.WriteHeapsTo(builder, this._serializedMetadata.StringHeap);
		}

		// Token: 0x04000535 RID: 1333
		private const string DefaultMetadataVersionString = "v4.0.30319";

		// Token: 0x04000536 RID: 1334
		[Nullable(0)]
		internal static readonly ImmutableArray<int> EmptyRowCounts = ImmutableArray.Create<int>(new int[MetadataTokens.TableCount]);

		// Token: 0x04000537 RID: 1335
		private readonly MetadataBuilder _tablesAndHeaps;

		// Token: 0x04000538 RID: 1336
		private readonly SerializedMetadata _serializedMetadata;
	}
}
