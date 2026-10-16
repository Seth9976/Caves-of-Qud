using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x0200007A RID: 122
	[NullableContext(1)]
	[Nullable(0)]
	internal struct AssemblyReferenceAlias
	{
		// Token: 0x06000340 RID: 832 RVA: 0x0000C95B File Offset: 0x0000AB5B
		internal AssemblyReferenceAlias(string name, IAssemblyReference assembly)
		{
			this.Name = name;
			this.Assembly = assembly;
		}

		// Token: 0x0400010D RID: 269
		public readonly string Name;

		// Token: 0x0400010E RID: 270
		public readonly IAssemblyReference Assembly;
	}
}
