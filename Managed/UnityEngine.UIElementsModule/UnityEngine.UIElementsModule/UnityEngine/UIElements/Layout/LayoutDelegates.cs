using System;
using System.Runtime.InteropServices;
using AOT;
using Unity.Profiling;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000699 RID: 1689
	internal static class LayoutDelegates
	{
		// Token: 0x060031BD RID: 12733 RVA: 0x000BB9C8 File Offset: 0x000B9BC8
		[MonoPInvokeCallback(typeof(InvokeMeasureFunctionDelegate))]
		private static void InvokeMeasureFunction(ref LayoutNode node, float width, LayoutMeasureMode widthMode, float height, LayoutMeasureMode heightMode, ref IntPtr exception, out LayoutSize result)
		{
			LayoutMeasureFunction measure = node.Measure;
			bool flag = measure == null;
			if (flag)
			{
				Debug.Assert(false, "Measure called on a null method");
				result = default(LayoutSize);
			}
			else
			{
				try
				{
					using (LayoutDelegates.s_InvokeMeasureFunctionMarker.Auto())
					{
						measure(node.GetOwner(), ref node, width, widthMode, height, heightMode, out result);
					}
				}
				catch (Exception ex)
				{
					GCHandle gchandle = GCHandle.Alloc(ex);
					exception = GCHandle.ToIntPtr(gchandle);
					result = default(LayoutSize);
				}
			}
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x000BBA74 File Offset: 0x000B9C74
		[MonoPInvokeCallback(typeof(InvokeBaselineFunctionDelegate))]
		private static float InvokeBaselineFunction(ref LayoutNode node, float width, float height)
		{
			LayoutBaselineFunction baseline = node.Baseline;
			bool flag = baseline == null;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				using (LayoutDelegates.s_InvokeBaselineFunctionMarker.Auto())
				{
					num = baseline(ref node, width, height);
				}
			}
			return num;
		}

		// Token: 0x04001836 RID: 6198
		private static readonly ProfilerMarker s_InvokeMeasureFunctionMarker = new ProfilerMarker("InvokeMeasureFunction");

		// Token: 0x04001837 RID: 6199
		private static readonly ProfilerMarker s_InvokeBaselineFunctionMarker = new ProfilerMarker("InvokeBaselineFunction");

		// Token: 0x04001838 RID: 6200
		private static readonly InvokeMeasureFunctionDelegate s_InvokeMeasureDelegate = new InvokeMeasureFunctionDelegate(LayoutDelegates.InvokeMeasureFunction);

		// Token: 0x04001839 RID: 6201
		private static readonly InvokeBaselineFunctionDelegate s_InvokeBaselineDelegate = new InvokeBaselineFunctionDelegate(LayoutDelegates.InvokeBaselineFunction);

		// Token: 0x0400183A RID: 6202
		internal static readonly IntPtr s_InvokeMeasureFunction = Marshal.GetFunctionPointerForDelegate<InvokeMeasureFunctionDelegate>(LayoutDelegates.s_InvokeMeasureDelegate);

		// Token: 0x0400183B RID: 6203
		internal static readonly IntPtr s_InvokeBaselineFunction = Marshal.GetFunctionPointerForDelegate<InvokeBaselineFunctionDelegate>(LayoutDelegates.s_InvokeBaselineDelegate);
	}
}
