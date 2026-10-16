using System;
using System.Diagnostics.CodeAnalysis;

namespace UnityEngine.Rendering
{
	// Token: 0x02000540 RID: 1344
	public class ObjectIdRequest
	{
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06003377 RID: 13175 RVA: 0x00064AE1 File Offset: 0x00062CE1
		// (set) Token: 0x06003378 RID: 13176 RVA: 0x00064AE9 File Offset: 0x00062CE9
		public RenderTexture destination
		{
			[return: NotNull]
			get;
			set; }

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06003379 RID: 13177 RVA: 0x00064AF2 File Offset: 0x00062CF2
		// (set) Token: 0x0600337A RID: 13178 RVA: 0x00064AFA File Offset: 0x00062CFA
		public int mipLevel { get; set; }

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x0600337B RID: 13179 RVA: 0x00064B03 File Offset: 0x00062D03
		// (set) Token: 0x0600337C RID: 13180 RVA: 0x00064B0B File Offset: 0x00062D0B
		public CubemapFace face { get; set; }

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x0600337D RID: 13181 RVA: 0x00064B14 File Offset: 0x00062D14
		// (set) Token: 0x0600337E RID: 13182 RVA: 0x00064B1C File Offset: 0x00062D1C
		public int slice { get; set; }

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x0600337F RID: 13183 RVA: 0x00064B25 File Offset: 0x00062D25
		// (set) Token: 0x06003380 RID: 13184 RVA: 0x00064B2D File Offset: 0x00062D2D
		public ObjectIdResult result
		{
			[return: MaybeNull]
			get;
			internal set; }

		// Token: 0x06003381 RID: 13185 RVA: 0x00064B36 File Offset: 0x00062D36
		public ObjectIdRequest([NotNull] RenderTexture destination, int mipLevel = 0, CubemapFace face = CubemapFace.Unknown, int slice = 0)
		{
			this.destination = destination;
			this.mipLevel = mipLevel;
			this.face = face;
			this.slice = slice;
		}
	}
}
