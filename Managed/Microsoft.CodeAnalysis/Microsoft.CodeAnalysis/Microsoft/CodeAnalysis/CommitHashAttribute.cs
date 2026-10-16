using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000137 RID: 311
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(1, AllowMultiple = false)]
	internal sealed class CommitHashAttribute : Attribute
	{
		// Token: 0x06000B7E RID: 2942 RVA: 0x00023619 File Offset: 0x00021819
		public CommitHashAttribute(string hash)
		{
			this.Hash = hash;
		}

		// Token: 0x04000344 RID: 836
		internal readonly string Hash;
	}
}
