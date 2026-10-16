using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200003F RID: 63
	[NativeHeader("Modules/TextCoreTextEngine/Native/TextCoreVertex.h")]
	[UsedByNativeCode("TextCoreVertex")]
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal struct TextCoreVertex
	{
		// Token: 0x04000188 RID: 392
		public Vector3 position;

		// Token: 0x04000189 RID: 393
		public Color32 color;

		// Token: 0x0400018A RID: 394
		public Vector2 uv0;

		// Token: 0x0400018B RID: 395
		public Vector2 uv2;
	}
}
