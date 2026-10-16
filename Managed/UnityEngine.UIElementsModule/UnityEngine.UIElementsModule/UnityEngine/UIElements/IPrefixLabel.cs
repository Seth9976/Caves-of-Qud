using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000115 RID: 277
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal interface IPrefixLabel
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060008B3 RID: 2227
		string label { get; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060008B4 RID: 2228
		Label labelElement { get; }
	}
}
