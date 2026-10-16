using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F9 RID: 249
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct VectorEncoder
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0001AADC File Offset: 0x00018CDC
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x06000968 RID: 2408 RVA: 0x0001AAE4 File Offset: 0x00018CE4
		public VectorEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		/// <param name="count" />
		// Token: 0x06000969 RID: 2409 RVA: 0x0001AAED File Offset: 0x00018CED
		public LiteralsEncoder Count(int count)
		{
			if (count < 0)
			{
				Throw.ArgumentOutOfRange("count");
			}
			this.Builder.WriteUInt32((uint)count);
			return new LiteralsEncoder(this.Builder);
		}
	}
}
