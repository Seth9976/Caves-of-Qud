using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F4 RID: 244
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct GenericTypeArgumentsEncoder
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0001A915 File Offset: 0x00018B15
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x06000951 RID: 2385 RVA: 0x0001A91D File Offset: 0x00018B1D
		public GenericTypeArgumentsEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0001A926 File Offset: 0x00018B26
		public SignatureTypeEncoder AddArgument()
		{
			return new SignatureTypeEncoder(this.Builder);
		}
	}
}
