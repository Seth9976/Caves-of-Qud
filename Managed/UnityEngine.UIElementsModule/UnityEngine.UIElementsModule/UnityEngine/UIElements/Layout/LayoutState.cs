using System;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006CB RID: 1739
	internal struct LayoutState
	{
		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x000BD4EC File Offset: 0x000BB6EC
		public static LayoutState Default
		{
			get
			{
				return new LayoutState
				{
					measureFunctionCallback = LayoutDelegates.s_InvokeMeasureFunction,
					baselineFunctionCallback = LayoutDelegates.s_InvokeBaselineFunction
				};
			}
		}

		// Token: 0x0400192A RID: 6442
		public IntPtr measureFunctionCallback;

		// Token: 0x0400192B RID: 6443
		public IntPtr baselineFunctionCallback;

		// Token: 0x0400192C RID: 6444
		public IntPtr unusedExceptionPointer;

		// Token: 0x0400192D RID: 6445
		public uint depth;

		// Token: 0x0400192E RID: 6446
		public uint currentGenerationCount;

		// Token: 0x0400192F RID: 6447
		[MarshalAs(UnmanagedType.U1)]
		public bool error;
	}
}
