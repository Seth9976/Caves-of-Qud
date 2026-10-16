using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000B1 RID: 177
	[NullableContext(1)]
	[Nullable(0)]
	internal struct SecurityAttribute
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0001600E File Offset: 0x0001420E
		public readonly DeclarativeSecurityAction Action { get; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00016016 File Offset: 0x00014216
		public readonly ICustomAttribute Attribute { get; }

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001601E File Offset: 0x0001421E
		public SecurityAttribute(DeclarativeSecurityAction action, ICustomAttribute attribute)
		{
			this.Action = action;
			this.Attribute = attribute;
		}
	}
}
