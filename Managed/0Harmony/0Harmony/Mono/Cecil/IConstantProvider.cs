using System;

namespace Mono.Cecil
{
	// Token: 0x02000212 RID: 530
	internal interface IConstantProvider : IMetadataTokenProvider
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000B84 RID: 2948
		// (set) Token: 0x06000B85 RID: 2949
		bool HasConstant { get; set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000B86 RID: 2950
		// (set) Token: 0x06000B87 RID: 2951
		object Constant { get; set; }
	}
}
