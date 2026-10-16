using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000051 RID: 81
	public interface IMarshalInfoProvider : IMetadataTokenProvider
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000274 RID: 628
		bool HasMarshalInfo { get; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000275 RID: 629
		// (set) Token: 0x06000276 RID: 630
		MarshalInfo MarshalInfo { get; set; }
	}
}
