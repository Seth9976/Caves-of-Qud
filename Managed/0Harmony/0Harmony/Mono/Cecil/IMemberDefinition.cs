using System;

namespace Mono.Cecil
{
	// Token: 0x02000219 RID: 537
	public interface IMemberDefinition : ICustomAttributeProvider, IMetadataTokenProvider
	{
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000B97 RID: 2967
		// (set) Token: 0x06000B98 RID: 2968
		string Name { get; set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000B99 RID: 2969
		string FullName { get; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000B9A RID: 2970
		// (set) Token: 0x06000B9B RID: 2971
		bool IsSpecialName { get; set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000B9C RID: 2972
		// (set) Token: 0x06000B9D RID: 2973
		bool IsRuntimeSpecialName { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000B9E RID: 2974
		// (set) Token: 0x06000B9F RID: 2975
		TypeDefinition DeclaringType { get; set; }
	}
}
