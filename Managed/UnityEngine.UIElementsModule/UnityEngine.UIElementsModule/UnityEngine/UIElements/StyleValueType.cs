using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004D9 RID: 1241
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal enum StyleValueType
	{
		// Token: 0x04001108 RID: 4360
		Invalid,
		// Token: 0x04001109 RID: 4361
		Keyword,
		// Token: 0x0400110A RID: 4362
		Float,
		// Token: 0x0400110B RID: 4363
		Dimension,
		// Token: 0x0400110C RID: 4364
		Color,
		// Token: 0x0400110D RID: 4365
		ResourcePath,
		// Token: 0x0400110E RID: 4366
		AssetReference,
		// Token: 0x0400110F RID: 4367
		Enum,
		// Token: 0x04001110 RID: 4368
		Variable,
		// Token: 0x04001111 RID: 4369
		String,
		// Token: 0x04001112 RID: 4370
		Function,
		// Token: 0x04001113 RID: 4371
		CommaSeparator,
		// Token: 0x04001114 RID: 4372
		ScalableImage,
		// Token: 0x04001115 RID: 4373
		MissingAssetReference
	}
}
