using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000117 RID: 279
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal interface IEditableElement
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060008B6 RID: 2230
		// (set) Token: 0x060008B7 RID: 2231
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		Action editingStarted { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060008B8 RID: 2232
		// (set) Token: 0x060008B9 RID: 2233
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		Action editingEnded { get; set; }
	}
}
