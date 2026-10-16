using System;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000698 RID: 1688
	// (Invoke) Token: 0x060031BA RID: 12730
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate float InvokeBaselineFunctionDelegate(ref LayoutNode node, float width, float height);
}
