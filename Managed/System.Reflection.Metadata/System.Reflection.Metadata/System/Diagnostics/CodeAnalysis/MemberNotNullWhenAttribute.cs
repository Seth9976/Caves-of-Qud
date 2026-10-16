using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000014 RID: 20
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(192, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullWhenAttribute : Attribute
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00002957 File Offset: 0x00000B57
		public MemberNotNullWhenAttribute(bool returnValue, string member)
		{
			this.ReturnValue = returnValue;
			this.Members = new string[] { member };
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002976 File Offset: 0x00000B76
		public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
		{
			this.ReturnValue = returnValue;
			this.Members = members;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000298C File Offset: 0x00000B8C
		public bool ReturnValue { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002994 File Offset: 0x00000B94
		public string[] Members { get; }
	}
}
