using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000FD RID: 253
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct NamedArgumentTypeEncoder
	{
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x0001AC50 File Offset: 0x00018E50
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x06000975 RID: 2421 RVA: 0x0001AC58 File Offset: 0x00018E58
		public NamedArgumentTypeEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001AC61 File Offset: 0x00018E61
		public CustomAttributeElementTypeEncoder ScalarType()
		{
			return new CustomAttributeElementTypeEncoder(this.Builder);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001AC6E File Offset: 0x00018E6E
		public void Object()
		{
			this.Builder.WriteByte(81);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001AC7D File Offset: 0x00018E7D
		public CustomAttributeArrayTypeEncoder SZArray()
		{
			return new CustomAttributeArrayTypeEncoder(this.Builder);
		}
	}
}
