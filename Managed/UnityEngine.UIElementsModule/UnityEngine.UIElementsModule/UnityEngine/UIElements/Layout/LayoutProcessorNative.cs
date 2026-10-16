using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006CC RID: 1740
	internal class LayoutProcessorNative : ILayoutProcessor
	{
		// Token: 0x06003224 RID: 12836 RVA: 0x000BD51C File Offset: 0x000BB71C
		unsafe void ILayoutProcessor.CalculateLayout(LayoutNode node, float parentWidth, float parentHeight, LayoutDirection parentDirection)
		{
			IntPtr intPtr = (IntPtr)((void*)(&node));
			IntPtr zero = IntPtr.Zero;
			fixed (LayoutState* ptr = &this.m_State)
			{
				void* ptr2 = (void*)ptr;
				IntPtr intPtr2 = (IntPtr)ptr2;
				LayoutNative.CalculateLayout(intPtr, parentWidth, parentHeight, (int)parentDirection, intPtr2, (IntPtr)((void*)(&zero)));
				bool flag = zero != IntPtr.Zero;
				if (flag)
				{
					GCHandle gchandle = GCHandle.FromIntPtr(zero);
					Exception ex = gchandle.Target as Exception;
					gchandle.Free();
					this.m_State.error = false;
					ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(ex);
					exceptionDispatchInfo.Throw();
				}
			}
		}

		// Token: 0x04001930 RID: 6448
		private LayoutState m_State = LayoutState.Default;
	}
}
