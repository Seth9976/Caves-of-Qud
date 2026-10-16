using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006D8 RID: 1752
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal struct ScalableImage
	{
		// Token: 0x060032B2 RID: 12978 RVA: 0x000C315C File Offset: 0x000C135C
		public override string ToString()
		{
			return string.Format("{0}: {1}, {2}: {3}", new object[] { "normalImage", this.normalImage, "highResolutionImage", this.highResolutionImage });
		}

		// Token: 0x040019D4 RID: 6612
		public Texture2D normalImage;

		// Token: 0x040019D5 RID: 6613
		public Texture2D highResolutionImage;
	}
}
