using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000052 RID: 82
	public interface IMemberDefinition : ICustomAttributeProvider, IMetadataTokenProvider
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000277 RID: 631
		// (set) Token: 0x06000278 RID: 632
		string Name { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000279 RID: 633
		string FullName { get; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600027A RID: 634
		// (set) Token: 0x0600027B RID: 635
		bool IsSpecialName { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600027C RID: 636
		// (set) Token: 0x0600027D RID: 637
		bool IsRuntimeSpecialName { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600027E RID: 638
		// (set) Token: 0x0600027F RID: 639
		TypeDefinition DeclaringType { get; set; }
	}
}
