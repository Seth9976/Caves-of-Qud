using System;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000028 RID: 40
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEngine.IMGUIModule" })]
	[NativeHeader("Modules/TextCoreTextEngine/Native/TextElementInfo.h")]
	internal struct NativeTextElementInfo
	{
		// Token: 0x040000C9 RID: 201
		public int glyphID;

		// Token: 0x040000CA RID: 202
		public TextCoreVertex bottomLeft;

		// Token: 0x040000CB RID: 203
		public TextCoreVertex topLeft;

		// Token: 0x040000CC RID: 204
		public TextCoreVertex topRight;

		// Token: 0x040000CD RID: 205
		public TextCoreVertex bottomRight;
	}
}
