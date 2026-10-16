using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000634 RID: 1588
	internal class DrawParams
	{
		// Token: 0x06002EAC RID: 11948 RVA: 0x000A8E88 File Offset: 0x000A7088
		public void Reset()
		{
			this.view.Clear();
			this.view.Push(Matrix4x4.identity);
			this.scissor.Clear();
			this.scissor.Push(DrawParams.k_UnlimitedRect);
			this.renderTexture.Clear();
			this.defaultMaterial.Clear();
		}

		// Token: 0x040015A3 RID: 5539
		internal static readonly Rect k_UnlimitedRect = new Rect(-100000f, -100000f, 200000f, 200000f);

		// Token: 0x040015A4 RID: 5540
		internal static readonly Rect k_FullNormalizedRect = new Rect(-1f, -1f, 2f, 2f);

		// Token: 0x040015A5 RID: 5541
		internal readonly Stack<Matrix4x4> view = new Stack<Matrix4x4>(8);

		// Token: 0x040015A6 RID: 5542
		internal readonly Stack<Rect> scissor = new Stack<Rect>(8);

		// Token: 0x040015A7 RID: 5543
		internal readonly List<RenderTexture> renderTexture = new List<RenderTexture>(8);

		// Token: 0x040015A8 RID: 5544
		internal readonly List<Material> defaultMaterial = new List<Material>(8);
	}
}
