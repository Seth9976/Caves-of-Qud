using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020001F9 RID: 505
	internal interface ICustomAttribute
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000A3C RID: 2620
		TypeReference AttributeType { get; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000A3D RID: 2621
		bool HasFields { get; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000A3E RID: 2622
		bool HasProperties { get; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000A3F RID: 2623
		bool HasConstructorArguments { get; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000A40 RID: 2624
		Collection<CustomAttributeNamedArgument> Fields { get; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000A41 RID: 2625
		Collection<CustomAttributeNamedArgument> Properties { get; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000A42 RID: 2626
		Collection<CustomAttributeArgument> ConstructorArguments { get; }
	}
}
