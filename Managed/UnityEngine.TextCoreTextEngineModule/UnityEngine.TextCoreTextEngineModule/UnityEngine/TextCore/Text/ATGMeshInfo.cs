using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000016 RID: 22
	[NativeHeader("Modules/TextCoreTextEngine/Native/ATGMeshInfo.h")]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct ATGMeshInfo
	{
		// Token: 0x04000070 RID: 112
		public NativeTextElementInfo[] textElementInfos;

		// Token: 0x04000071 RID: 113
		public int fontAssetId;

		// Token: 0x04000072 RID: 114
		public int textElementCount;

		// Token: 0x04000073 RID: 115
		[Ignore]
		public FontAsset fontAsset;

		// Token: 0x04000074 RID: 116
		[Ignore]
		public List<List<int>> textElementInfoIndicesByAtlas;

		// Token: 0x04000075 RID: 117
		[Ignore]
		public bool hasMultipleColors;
	}
}
