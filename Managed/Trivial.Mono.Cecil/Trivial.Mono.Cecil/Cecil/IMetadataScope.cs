using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000056 RID: 86
	public interface IMetadataScope : IMetadataTokenProvider
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000287 RID: 647
		MetadataScopeType MetadataScopeType { get; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000288 RID: 648
		// (set) Token: 0x06000289 RID: 649
		string Name { get; set; }
	}
}
