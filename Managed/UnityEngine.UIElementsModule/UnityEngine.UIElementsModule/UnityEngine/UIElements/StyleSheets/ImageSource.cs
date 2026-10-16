using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006DA RID: 1754
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal struct ImageSource
	{
		// Token: 0x060032B3 RID: 12979 RVA: 0x000C31A0 File Offset: 0x000C13A0
		public bool IsNull()
		{
			return this.texture == null && this.sprite == null && this.vectorImage == null && this.renderTexture == null;
		}

		// Token: 0x040019D8 RID: 6616
		public Texture2D texture;

		// Token: 0x040019D9 RID: 6617
		public Sprite sprite;

		// Token: 0x040019DA RID: 6618
		public VectorImage vectorImage;

		// Token: 0x040019DB RID: 6619
		public RenderTexture renderTexture;
	}
}
