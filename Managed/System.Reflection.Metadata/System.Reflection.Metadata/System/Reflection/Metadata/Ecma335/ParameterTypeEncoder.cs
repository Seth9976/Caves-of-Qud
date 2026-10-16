using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F2 RID: 242
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct ParameterTypeEncoder
	{
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0001A7D4 File Offset: 0x000189D4
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x06000948 RID: 2376 RVA: 0x0001A7DC File Offset: 0x000189DC
		public ParameterTypeEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0001A7E5 File Offset: 0x000189E5
		public CustomModifiersEncoder CustomModifiers()
		{
			return new CustomModifiersEncoder(this.Builder);
		}

		/// <param name="isByRef" />
		// Token: 0x0600094A RID: 2378 RVA: 0x0001A7F2 File Offset: 0x000189F2
		public SignatureTypeEncoder Type(bool isByRef = false)
		{
			if (isByRef)
			{
				this.Builder.WriteByte(16);
			}
			return new SignatureTypeEncoder(this.Builder);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001A80F File Offset: 0x00018A0F
		public void TypedReference()
		{
			this.Builder.WriteByte(22);
		}
	}
}
