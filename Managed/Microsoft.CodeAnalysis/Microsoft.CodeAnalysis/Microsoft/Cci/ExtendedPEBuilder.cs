using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.Cci
{
	// Token: 0x0200008B RID: 139
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ExtendedPEBuilder : ManagedPEBuilder
	{
		// Token: 0x06000375 RID: 885 RVA: 0x0000D018 File Offset: 0x0000B218
		public ExtendedPEBuilder(PEHeaderBuilder header, MetadataRootBuilder metadataRootBuilder, BlobBuilder ilStream, BlobBuilder mappedFieldData, BlobBuilder managedResources, ResourceSectionBuilder nativeResources, DebugDirectoryBuilder debugDirectoryBuilder, int strongNameSignatureSize, MethodDefinitionHandle entryPoint, CorFlags flags, Func<IEnumerable<Blob>, BlobContentId> deterministicIdProvider, bool withMvidSection)
			: base(header, metadataRootBuilder, ilStream, mappedFieldData, managedResources, nativeResources, debugDirectoryBuilder, strongNameSignatureSize, entryPoint, flags, deterministicIdProvider)
		{
			this._withMvidSection = withMvidSection;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000D048 File Offset: 0x0000B248
		[NullableContext(0)]
		protected override ImmutableArray<PEBuilder.Section> CreateSections()
		{
			ImmutableArray<PEBuilder.Section> immutableArray = base.CreateSections();
			if (this._withMvidSection)
			{
				ArrayBuilder<PEBuilder.Section> instance = ArrayBuilder<PEBuilder.Section>.GetInstance(immutableArray.Length + 1);
				instance.Add(new PEBuilder.Section(".mvid", SectionCharacteristics.ContainsInitializedData | SectionCharacteristics.MemDiscardable | SectionCharacteristics.MemRead));
				instance.AddRange(immutableArray);
				return instance.ToImmutableAndFree();
			}
			return immutableArray;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000D095 File Offset: 0x0000B295
		protected override BlobBuilder SerializeSection(string name, SectionLocation location)
		{
			if (name.Equals(".mvid", 4))
			{
				return this.SerializeMvidSection(location);
			}
			return base.SerializeSection(name, location);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000D0B5 File Offset: 0x0000B2B5
		internal BlobContentId Serialize(BlobBuilder peBlob, out Blob mvidSectionFixup)
		{
			BlobContentId blobContentId = base.Serialize(peBlob);
			mvidSectionFixup = this._mvidSectionFixup;
			return blobContentId;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		private BlobBuilder SerializeMvidSection(SectionLocation location)
		{
			BlobBuilder blobBuilder = new BlobBuilder(256);
			this._mvidSectionFixup = blobBuilder.ReserveBytes(16);
			BlobWriter blobWriter = new BlobWriter(this._mvidSectionFixup);
			blobWriter.WriteBytes(0, this._mvidSectionFixup.Length);
			return blobBuilder;
		}

		// Token: 0x0400017B RID: 379
		private const string MvidSectionName = ".mvid";

		// Token: 0x0400017C RID: 380
		public const int SizeOfGuid = 16;

		// Token: 0x0400017D RID: 381
		private Blob _mvidSectionFixup;

		// Token: 0x0400017E RID: 382
		private readonly bool _withMvidSection;
	}
}
