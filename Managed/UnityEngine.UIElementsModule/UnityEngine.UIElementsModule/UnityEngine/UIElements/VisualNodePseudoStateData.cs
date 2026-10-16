using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000605 RID: 1541
	[NativeType(Header = "Modules/UIElements/VisualNodePseudoStateData.h")]
	internal struct VisualNodePseudoStateData
	{
		// Token: 0x040014B3 RID: 5299
		public PseudoStates States;

		// Token: 0x040014B4 RID: 5300
		public PseudoStates TriggerMask;

		// Token: 0x040014B5 RID: 5301
		public PseudoStates DependencyMask;
	}
}
