using System;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000697 RID: 1687
	// (Invoke) Token: 0x060031B6 RID: 12726
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void InvokeMeasureFunctionDelegate(ref LayoutNode node, float width, LayoutMeasureMode widthMode, float height, LayoutMeasureMode heightMode, ref IntPtr exception, out LayoutSize result);
}
