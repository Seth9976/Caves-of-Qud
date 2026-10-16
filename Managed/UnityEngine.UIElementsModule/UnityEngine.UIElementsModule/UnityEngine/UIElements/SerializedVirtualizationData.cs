using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200009D RID: 157
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class SerializedVirtualizationData
	{
		// Token: 0x04000346 RID: 838
		public Vector2 scrollOffset;

		// Token: 0x04000347 RID: 839
		public int firstVisibleIndex;

		// Token: 0x04000348 RID: 840
		public float contentPadding;

		// Token: 0x04000349 RID: 841
		public float contentHeight;

		// Token: 0x0400034A RID: 842
		public int anchoredItemIndex;

		// Token: 0x0400034B RID: 843
		public float anchorOffset;
	}
}
