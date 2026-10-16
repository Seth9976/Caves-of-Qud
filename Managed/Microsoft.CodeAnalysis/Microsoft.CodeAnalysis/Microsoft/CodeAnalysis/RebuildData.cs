using System;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200014E RID: 334
	internal sealed class RebuildData
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x000273F4 File Offset: 0x000255F4
		[Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<string> NonSourceFileDocumentNames
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x000273FC File Offset: 0x000255FC
		internal BlobReader OptionsBlobReader { get; }

		// Token: 0x06000D53 RID: 3411 RVA: 0x00027404 File Offset: 0x00025604
		internal RebuildData(BlobReader optionsBlobReader, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> nonSourceFileDocumentNames)
		{
			this.OptionsBlobReader = optionsBlobReader;
			this.NonSourceFileDocumentNames = nonSourceFileDocumentNames;
		}
	}
}
