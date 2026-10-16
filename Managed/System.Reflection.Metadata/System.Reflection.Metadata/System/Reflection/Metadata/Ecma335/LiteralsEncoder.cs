using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F8 RID: 248
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct LiteralsEncoder
	{
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0001AABE File Offset: 0x00018CBE
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x06000965 RID: 2405 RVA: 0x0001AAC6 File Offset: 0x00018CC6
		public LiteralsEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0001AACF File Offset: 0x00018CCF
		public LiteralEncoder AddLiteral()
		{
			return new LiteralEncoder(this.Builder);
		}
	}
}
