using System;

namespace UnityEngine.AMD
{
	// Token: 0x02000006 RID: 6
	public struct FSR2TextureTable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020AE File Offset: 0x000002AE
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020A5 File Offset: 0x000002A5
		public Texture colorInput { readonly get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020BF File Offset: 0x000002BF
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000020B6 File Offset: 0x000002B6
		public Texture colorOutput { readonly get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020D0 File Offset: 0x000002D0
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020C7 File Offset: 0x000002C7
		public Texture depth { readonly get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020E1 File Offset: 0x000002E1
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020D8 File Offset: 0x000002D8
		public Texture motionVectors { readonly get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020F2 File Offset: 0x000002F2
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020E9 File Offset: 0x000002E9
		public Texture transparencyMask { readonly get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002103 File Offset: 0x00000303
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020FA File Offset: 0x000002FA
		public Texture exposureTexture { readonly get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002114 File Offset: 0x00000314
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000210B File Offset: 0x0000030B
		public Texture reactiveMask { readonly get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002125 File Offset: 0x00000325
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000211C File Offset: 0x0000031C
		public Texture biasColorMask { readonly get; set; }
	}
}
