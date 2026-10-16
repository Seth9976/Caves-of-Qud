using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000FB RID: 251
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct CustomAttributeNamedArgumentsEncoder
	{
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0001AB52 File Offset: 0x00018D52
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x0600096E RID: 2414 RVA: 0x0001AB5A File Offset: 0x00018D5A
		public CustomAttributeNamedArgumentsEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		/// <param name="count" />
		// Token: 0x0600096F RID: 2415 RVA: 0x0001AB63 File Offset: 0x00018D63
		public NamedArgumentsEncoder Count(int count)
		{
			if (count > 65535)
			{
				Throw.ArgumentOutOfRange("count");
			}
			this.Builder.WriteUInt16((ushort)count);
			return new NamedArgumentsEncoder(this.Builder);
		}
	}
}
