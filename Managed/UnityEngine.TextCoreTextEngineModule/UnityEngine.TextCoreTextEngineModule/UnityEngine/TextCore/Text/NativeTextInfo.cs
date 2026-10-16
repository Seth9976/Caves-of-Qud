using System;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000029 RID: 41
	[NativeHeader("Modules/TextCoreTextEngine/Native/TextInfo.h")]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEngine.IMGUIModule" })]
	internal struct NativeTextInfo
	{
		// Token: 0x040000CE RID: 206
		public ATGMeshInfo[] meshInfos;

		// Token: 0x040000CF RID: 207
		public bool isElided;
	}
}
