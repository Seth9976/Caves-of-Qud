using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020005FF RID: 1535
	[NativeType(Header = "Modules/UIElements/VisualNodeImguiData.h")]
	internal struct VisualNodeImguiData
	{
		// Token: 0x040014A8 RID: 5288
		[MarshalAs(UnmanagedType.U1)]
		public bool IsContainer;

		// Token: 0x040014A9 RID: 5289
		public int DescendantCount;
	}
}
