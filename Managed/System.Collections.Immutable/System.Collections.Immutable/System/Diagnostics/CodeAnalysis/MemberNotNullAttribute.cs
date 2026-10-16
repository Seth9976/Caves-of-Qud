using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(192, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullAttribute : Attribute
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000023B8 File Offset: 0x000005B8
		public MemberNotNullAttribute(string member)
		{
			this.Members = new string[] { member };
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000023D0 File Offset: 0x000005D0
		public MemberNotNullAttribute(params string[] members)
		{
			this.Members = members;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000023DF File Offset: 0x000005DF
		public string[] Members { get; }
	}
}
