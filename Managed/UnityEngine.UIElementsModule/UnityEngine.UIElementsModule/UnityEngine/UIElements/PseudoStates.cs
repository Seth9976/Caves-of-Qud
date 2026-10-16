using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020005A9 RID: 1449
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Flags]
	internal enum PseudoStates
	{
		// Token: 0x04001386 RID: 4998
		Active = 1,
		// Token: 0x04001387 RID: 4999
		Hover = 2,
		// Token: 0x04001388 RID: 5000
		Checked = 8,
		// Token: 0x04001389 RID: 5001
		Disabled = 32,
		// Token: 0x0400138A RID: 5002
		Focus = 64,
		// Token: 0x0400138B RID: 5003
		Root = 128
	}
}
