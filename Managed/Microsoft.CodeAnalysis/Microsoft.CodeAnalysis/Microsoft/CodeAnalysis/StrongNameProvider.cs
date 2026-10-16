using System;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001FC RID: 508
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class StrongNameProvider
	{
		// Token: 0x0600147A RID: 5242
		public abstract override int GetHashCode();

		// Token: 0x0600147B RID: 5243
		[NullableContext(2)]
		public abstract override bool Equals(object other);

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600147C RID: 5244
		internal abstract StrongNameFileSystem FileSystem { get; }

		// Token: 0x0600147D RID: 5245
		internal abstract void SignFile(StrongNameKeys keys, string filePath);

		// Token: 0x0600147E RID: 5246
		internal abstract void SignBuilder(ExtendedPEBuilder peBuilder, BlobBuilder peBlob, RSAParameters privateKey);

		// Token: 0x0600147F RID: 5247
		internal abstract StrongNameKeys CreateKeys([Nullable(2)] string keyFilePath, [Nullable(2)] string keyContainerName, bool hasCounterSignature, CommonMessageProvider messageProvider);
	}
}
