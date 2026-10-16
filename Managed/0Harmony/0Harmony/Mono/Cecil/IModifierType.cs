using System;

namespace Mono.Cecil
{
	// Token: 0x02000240 RID: 576
	internal interface IModifierType
	{
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000D44 RID: 3396
		TypeReference ModifierType { get; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000D45 RID: 3397
		TypeReference ElementType { get; }
	}
}
