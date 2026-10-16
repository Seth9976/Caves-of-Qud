using System;
using System.Collections.Immutable;

namespace System.Reflection.Metadata
{
	// Token: 0x020000B4 RID: 180
	public sealed class DebugMetadataHeader
	{
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x000115CE File Offset: 0x0000F7CE
		public ImmutableArray<byte> Id { get; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x000115D6 File Offset: 0x0000F7D6
		public MethodDefinitionHandle EntryPoint { get; }

		/// <summary>Gets the offset (in bytes) from the start of the metadata blob to the start of the <see cref="P:System.Reflection.Metadata.DebugMetadataHeader.Id" /> blob.</summary>
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x000115DE File Offset: 0x0000F7DE
		public int IdStartOffset { get; }

		// Token: 0x060006E4 RID: 1764 RVA: 0x000115E6 File Offset: 0x0000F7E6
		internal DebugMetadataHeader(ImmutableArray<byte> id, MethodDefinitionHandle entryPoint, int idStartOffset)
		{
			this.Id = id;
			this.EntryPoint = entryPoint;
			this.IdStartOffset = idStartOffset;
		}
	}
}
