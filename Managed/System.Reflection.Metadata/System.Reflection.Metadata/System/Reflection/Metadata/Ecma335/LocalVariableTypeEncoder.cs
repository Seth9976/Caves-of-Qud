using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F1 RID: 241
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct LocalVariableTypeEncoder
	{
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0001A77A File Offset: 0x0001897A
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x06000943 RID: 2371 RVA: 0x0001A782 File Offset: 0x00018982
		public LocalVariableTypeEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001A78B File Offset: 0x0001898B
		public CustomModifiersEncoder CustomModifiers()
		{
			return new CustomModifiersEncoder(this.Builder);
		}

		/// <param name="isByRef" />
		/// <param name="isPinned" />
		// Token: 0x06000945 RID: 2373 RVA: 0x0001A798 File Offset: 0x00018998
		public SignatureTypeEncoder Type(bool isByRef = false, bool isPinned = false)
		{
			if (isPinned)
			{
				this.Builder.WriteByte(69);
			}
			if (isByRef)
			{
				this.Builder.WriteByte(16);
			}
			return new SignatureTypeEncoder(this.Builder);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001A7C5 File Offset: 0x000189C5
		public void TypedReference()
		{
			this.Builder.WriteByte(22);
		}
	}
}
