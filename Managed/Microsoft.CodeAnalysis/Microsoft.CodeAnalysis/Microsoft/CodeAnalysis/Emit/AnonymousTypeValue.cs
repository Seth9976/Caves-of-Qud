using System;
using System.Diagnostics;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000477 RID: 1143
	[DebuggerDisplay("{Name, nq}")]
	internal struct AnonymousTypeValue
	{
		// Token: 0x06002C8B RID: 11403 RVA: 0x0007DC68 File Offset: 0x0007BE68
		public AnonymousTypeValue(string name, int uniqueIndex, ITypeDefinition type)
		{
			this.Name = name;
			this.UniqueIndex = uniqueIndex;
			this.Type = type;
		}

		// Token: 0x040011B5 RID: 4533
		public readonly string Name;

		// Token: 0x040011B6 RID: 4534
		public readonly int UniqueIndex;

		// Token: 0x040011B7 RID: 4535
		public readonly ITypeDefinition Type;
	}
}
