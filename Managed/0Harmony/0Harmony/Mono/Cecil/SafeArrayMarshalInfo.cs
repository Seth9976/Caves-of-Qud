using System;

namespace Mono.Cecil
{
	// Token: 0x0200022B RID: 555
	internal sealed class SafeArrayMarshalInfo : MarshalInfo
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00029868 File Offset: 0x00027A68
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x00029870 File Offset: 0x00027A70
		public VariantType ElementType
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

		// Token: 0x06000C0B RID: 3083 RVA: 0x00029879 File Offset: 0x00027A79
		public SafeArrayMarshalInfo()
			: base(NativeType.SafeArray)
		{
			this.element_type = VariantType.None;
		}

		// Token: 0x04000364 RID: 868
		internal VariantType element_type;
	}
}
