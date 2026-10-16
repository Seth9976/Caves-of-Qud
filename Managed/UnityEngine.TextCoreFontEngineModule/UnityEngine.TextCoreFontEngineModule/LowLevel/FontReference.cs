using System;
using System.Diagnostics;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200000D RID: 13
	[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
	[DebuggerDisplay("{familyName} - {styleName}")]
	[UsedByNativeCode]
	internal struct FontReference
	{
		// Token: 0x04000078 RID: 120
		public string familyName;

		// Token: 0x04000079 RID: 121
		public string styleName;

		// Token: 0x0400007A RID: 122
		public int faceIndex;

		// Token: 0x0400007B RID: 123
		public string filePath;
	}
}
