using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000FA RID: 250
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct NameEncoder
	{
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0001AB14 File Offset: 0x00018D14
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x0600096B RID: 2411 RVA: 0x0001AB1C File Offset: 0x00018D1C
		public NameEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		/// <param name="name" />
		// Token: 0x0600096C RID: 2412 RVA: 0x0001AB25 File Offset: 0x00018D25
		public void Name(string name)
		{
			if (name == null)
			{
				Throw.ArgumentNull("name");
			}
			if (name.Length == 0)
			{
				Throw.ArgumentEmptyString("name");
			}
			this.Builder.WriteSerializedString(name);
		}
	}
}
