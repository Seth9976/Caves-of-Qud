using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000103 RID: 259
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct ReturnTypeEncoder
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0001B2DA File Offset: 0x000194DA
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x060009B7 RID: 2487 RVA: 0x0001B2E2 File Offset: 0x000194E2
		public ReturnTypeEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0001B2EB File Offset: 0x000194EB
		public CustomModifiersEncoder CustomModifiers()
		{
			return new CustomModifiersEncoder(this.Builder);
		}

		/// <param name="isByRef" />
		// Token: 0x060009B9 RID: 2489 RVA: 0x0001B2F8 File Offset: 0x000194F8
		public SignatureTypeEncoder Type(bool isByRef = false)
		{
			if (isByRef)
			{
				this.Builder.WriteByte(16);
			}
			return new SignatureTypeEncoder(this.Builder);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0001B315 File Offset: 0x00019515
		public void TypedReference()
		{
			this.Builder.WriteByte(22);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0001B324 File Offset: 0x00019524
		public void Void()
		{
			this.Builder.WriteByte(1);
		}
	}
}
