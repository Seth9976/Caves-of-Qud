using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.UIElements.Layout;

namespace UnityEngine.UIElements
{
	// Token: 0x020005FD RID: 1533
	[NativeType(Header = "Modules/UIElements/VisualNodeData.h")]
	internal struct VisualNodeData
	{
		// Token: 0x0400149D RID: 5277
		public VisualPanelHandle Panel;

		// Token: 0x0400149E RID: 5278
		public VisualNodeHandle LogicalParent;

		// Token: 0x0400149F RID: 5279
		public VisualElementFlags Flags;

		// Token: 0x040014A0 RID: 5280
		public VisualNodeCallbackInterest CallbackInterest;

		// Token: 0x040014A1 RID: 5281
		public LayoutNode LayoutNode;

		// Token: 0x040014A2 RID: 5282
		public uint ControlId;

		// Token: 0x040014A3 RID: 5283
		[MarshalAs(UnmanagedType.U1)]
		public bool Enabled;

		// Token: 0x040014A4 RID: 5284
		[MarshalAs(UnmanagedType.U1)]
		public bool IsRootVisualContainer;
	}
}
