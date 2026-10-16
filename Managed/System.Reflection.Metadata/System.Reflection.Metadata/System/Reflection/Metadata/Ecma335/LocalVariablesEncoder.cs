using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F0 RID: 240
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct LocalVariablesEncoder
	{
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0001A75C File Offset: 0x0001895C
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x06000940 RID: 2368 RVA: 0x0001A764 File Offset: 0x00018964
		public LocalVariablesEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001A76D File Offset: 0x0001896D
		public LocalVariableTypeEncoder AddVariable()
		{
			return new LocalVariableTypeEncoder(this.Builder);
		}
	}
}
