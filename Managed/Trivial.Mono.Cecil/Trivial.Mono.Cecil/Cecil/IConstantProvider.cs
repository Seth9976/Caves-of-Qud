using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200004B RID: 75
	public interface IConstantProvider : IMetadataTokenProvider
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600025F RID: 607
		// (set) Token: 0x06000260 RID: 608
		bool HasConstant { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000261 RID: 609
		// (set) Token: 0x06000262 RID: 610
		object Constant { get; set; }
	}
}
