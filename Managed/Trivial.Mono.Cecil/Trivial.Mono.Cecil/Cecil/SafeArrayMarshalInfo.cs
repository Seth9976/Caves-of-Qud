using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000AE RID: 174
	public sealed class SafeArrayMarshalInfo : MarshalInfo
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00015C0F File Offset: 0x00013E0F
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x00015C17 File Offset: 0x00013E17
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

		// Token: 0x0600072F RID: 1839 RVA: 0x00015C20 File Offset: 0x00013E20
		public SafeArrayMarshalInfo()
			: base(NativeType.SafeArray)
		{
			this.element_type = VariantType.None;
		}

		// Token: 0x0400025A RID: 602
		internal VariantType element_type;
	}
}
