using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Debugging;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.Cci
{
	// Token: 0x02000082 RID: 130
	internal struct DebugSourceInfo
	{
		// Token: 0x06000359 RID: 857 RVA: 0x0000CF10 File Offset: 0x0000B110
		public DebugSourceInfo(ImmutableArray<byte> checksum, SourceHashAlgorithm checksumAlgorithm, ImmutableArray<byte> embeddedTextBlob = default(ImmutableArray<byte>))
		{
			this = new DebugSourceInfo(checksum, SourceHashAlgorithms.GetAlgorithmGuid(checksumAlgorithm), embeddedTextBlob);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000CF20 File Offset: 0x0000B120
		public DebugSourceInfo(ImmutableArray<byte> checksum, Guid checksumAlgorithmId, ImmutableArray<byte> embeddedTextBlob = default(ImmutableArray<byte>))
		{
			this.ChecksumAlgorithmId = checksumAlgorithmId;
			this.Checksum = checksum;
			this.EmbeddedTextBlob = embeddedTextBlob;
		}

		// Token: 0x0400016F RID: 367
		public readonly Guid ChecksumAlgorithmId;

		// Token: 0x04000170 RID: 368
		public readonly ImmutableArray<byte> Checksum;

		// Token: 0x04000171 RID: 369
		public readonly ImmutableArray<byte> EmbeddedTextBlob;
	}
}
