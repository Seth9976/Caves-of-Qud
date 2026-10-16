using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000FE RID: 254
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct CustomAttributeArrayTypeEncoder
	{
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0001AC8A File Offset: 0x00018E8A
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x0600097A RID: 2426 RVA: 0x0001AC92 File Offset: 0x00018E92
		public CustomAttributeArrayTypeEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0001AC9B File Offset: 0x00018E9B
		public void ObjectArray()
		{
			this.Builder.WriteByte(29);
			this.Builder.WriteByte(81);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0001ACB7 File Offset: 0x00018EB7
		public CustomAttributeElementTypeEncoder ElementType()
		{
			this.Builder.WriteByte(29);
			return new CustomAttributeElementTypeEncoder(this.Builder);
		}
	}
}
