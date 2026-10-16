using System;

namespace Mono.Cecil
{
	// Token: 0x02000218 RID: 536
	internal interface IMarshalInfoProvider : IMetadataTokenProvider
	{
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000B94 RID: 2964
		bool HasMarshalInfo { get; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000B95 RID: 2965
		// (set) Token: 0x06000B96 RID: 2966
		MarshalInfo MarshalInfo { get; set; }
	}
}
