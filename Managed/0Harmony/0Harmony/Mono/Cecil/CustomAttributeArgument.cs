using System;

namespace Mono.Cecil
{
	// Token: 0x020001F7 RID: 503
	public struct CustomAttributeArgument
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x000264D5 File Offset: 0x000246D5
		public TypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x000264DD File Offset: 0x000246DD
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x000264E5 File Offset: 0x000246E5
		public CustomAttributeArgument(TypeReference type, object value)
		{
			Mixin.CheckType(type);
			this.type = type;
			this.value = value;
		}

		// Token: 0x040002E3 RID: 739
		private readonly TypeReference type;

		// Token: 0x040002E4 RID: 740
		private readonly object value;
	}
}
