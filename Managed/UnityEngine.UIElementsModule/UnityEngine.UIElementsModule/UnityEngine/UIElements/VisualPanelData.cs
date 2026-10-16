using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000608 RID: 1544
	[NativeType(Header = "Modules/UIElements/VisualPanelData.h")]
	internal struct VisualPanelData
	{
		// Token: 0x040014B9 RID: 5305
		public VisualNodeHandle RootContainer;

		// Token: 0x040014BA RID: 5306
		[MarshalAs(UnmanagedType.U1)]
		public bool DuringLayoutPhase;
	}
}
