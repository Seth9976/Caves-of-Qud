using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000020 RID: 32
	public struct CustomAttributeArgument
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000477C File Offset: 0x0000297C
		public TypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004784 File Offset: 0x00002984
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000478C File Offset: 0x0000298C
		public CustomAttributeArgument(TypeReference type, object value)
		{
			Mixin.CheckType(type);
			this.type = type;
			this.value = value;
		}

		// Token: 0x0400003B RID: 59
		private readonly TypeReference type;

		// Token: 0x0400003C RID: 60
		private readonly object value;
	}
}
