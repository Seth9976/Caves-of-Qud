using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F5 RID: 245
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct FixedArgumentsEncoder
	{
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0001A933 File Offset: 0x00018B33
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x06000954 RID: 2388 RVA: 0x0001A93B File Offset: 0x00018B3B
		public FixedArgumentsEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0001A944 File Offset: 0x00018B44
		public LiteralEncoder AddArgument()
		{
			return new LiteralEncoder(this.Builder);
		}
	}
}
