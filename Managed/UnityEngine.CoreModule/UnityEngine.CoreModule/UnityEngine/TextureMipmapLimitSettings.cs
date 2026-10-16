using System;

namespace UnityEngine
{
	// Token: 0x020001DF RID: 479
	public struct TextureMipmapLimitSettings
	{
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x000236A7 File Offset: 0x000218A7
		// (set) Token: 0x060014E2 RID: 5346 RVA: 0x000236AF File Offset: 0x000218AF
		public TextureMipmapLimitBiasMode limitBiasMode { readonly get; set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x000236B8 File Offset: 0x000218B8
		// (set) Token: 0x060014E4 RID: 5348 RVA: 0x000236C0 File Offset: 0x000218C0
		public int limitBias { readonly get; set; }
	}
}
