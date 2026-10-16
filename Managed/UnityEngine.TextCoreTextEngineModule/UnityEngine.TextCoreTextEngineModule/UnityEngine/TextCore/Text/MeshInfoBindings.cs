using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000026 RID: 38
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
	[UsedByNativeCode("MeshInfo")]
	[NativeHeader("Modules/TextCoreTextEngine/Native/MeshInfo.h")]
	internal struct MeshInfoBindings
	{
		// Token: 0x040000C0 RID: 192
		public TextCoreVertex[] vertexData;

		// Token: 0x040000C1 RID: 193
		public Material material;

		// Token: 0x040000C2 RID: 194
		public int vertexCount;
	}
}
