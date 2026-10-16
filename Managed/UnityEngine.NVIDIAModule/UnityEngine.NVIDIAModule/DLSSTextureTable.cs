using System;

namespace UnityEngine.NVIDIA
{
	// Token: 0x02000007 RID: 7
	public struct DLSSTextureTable
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002216 File Offset: 0x00000416
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000220D File Offset: 0x0000040D
		public Texture colorInput { readonly get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002227 File Offset: 0x00000427
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000221E File Offset: 0x0000041E
		public Texture colorOutput { readonly get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002238 File Offset: 0x00000438
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000222F File Offset: 0x0000042F
		public Texture depth { readonly get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002249 File Offset: 0x00000449
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002240 File Offset: 0x00000440
		public Texture motionVectors { readonly get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000225A File Offset: 0x0000045A
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002251 File Offset: 0x00000451
		public Texture transparencyMask { readonly get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000226B File Offset: 0x0000046B
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002262 File Offset: 0x00000462
		public Texture exposureTexture { readonly get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000227C File Offset: 0x0000047C
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002273 File Offset: 0x00000473
		public Texture biasColorMask { readonly get; set; }
	}
}
