using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(192, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullAttribute : Attribute
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00002928 File Offset: 0x00000B28
		public MemberNotNullAttribute(string member)
		{
			this.Members = new string[] { member };
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002940 File Offset: 0x00000B40
		public MemberNotNullAttribute(params string[] members)
		{
			this.Members = members;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000294F File Offset: 0x00000B4F
		public string[] Members { get; }
	}
}
