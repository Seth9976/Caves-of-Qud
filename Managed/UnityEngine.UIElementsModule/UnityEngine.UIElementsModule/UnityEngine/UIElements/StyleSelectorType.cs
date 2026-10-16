using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004D0 RID: 1232
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal enum StyleSelectorType
	{
		// Token: 0x040010D0 RID: 4304
		Unknown,
		// Token: 0x040010D1 RID: 4305
		Wildcard,
		// Token: 0x040010D2 RID: 4306
		Type,
		// Token: 0x040010D3 RID: 4307
		Class,
		// Token: 0x040010D4 RID: 4308
		PseudoClass,
		// Token: 0x040010D5 RID: 4309
		RecursivePseudoClass,
		// Token: 0x040010D6 RID: 4310
		ID,
		// Token: 0x040010D7 RID: 4311
		Predicate
	}
}
