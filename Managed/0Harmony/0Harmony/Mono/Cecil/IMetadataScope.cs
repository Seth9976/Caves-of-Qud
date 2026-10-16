using System;

namespace Mono.Cecil
{
	// Token: 0x0200021B RID: 539
	public interface IMetadataScope : IMetadataTokenProvider
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000BA0 RID: 2976
		MetadataScopeType MetadataScopeType { get; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000BA1 RID: 2977
		// (set) Token: 0x06000BA2 RID: 2978
		string Name { get; set; }
	}
}
