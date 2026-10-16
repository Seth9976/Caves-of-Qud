using System;

namespace Mono.Cecil
{
	// Token: 0x0200022C RID: 556
	internal sealed class FixedArrayMarshalInfo : MarshalInfo
	{
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0002988A File Offset: 0x00027A8A
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x00029892 File Offset: 0x00027A92
		public NativeType ElementType
		{
			get
			{
				return this.element_type;
			}
			set
			{
				this.element_type = value;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0002989B File Offset: 0x00027A9B
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x000298A3 File Offset: 0x00027AA3
		public int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x000298AC File Offset: 0x00027AAC
		public FixedArrayMarshalInfo()
			: base(NativeType.FixedArray)
		{
			this.element_type = NativeType.None;
		}

		// Token: 0x04000365 RID: 869
		internal NativeType element_type;

		// Token: 0x04000366 RID: 870
		internal int size;
	}
}
