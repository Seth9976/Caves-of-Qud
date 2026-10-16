using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200004C RID: 76
	public interface ICustomAttribute
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000263 RID: 611
		TypeReference AttributeType { get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000264 RID: 612
		bool HasFields { get; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000265 RID: 613
		bool HasProperties { get; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000266 RID: 614
		Collection<CustomAttributeNamedArgument> Fields { get; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000267 RID: 615
		Collection<CustomAttributeNamedArgument> Properties { get; }
	}
}
