using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200015F RID: 351
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/DisplayManager.h")]
	public class Display
	{
		// Token: 0x06000B4F RID: 2895 RVA: 0x00014F1B File Offset: 0x0001311B
		internal Display()
		{
			this.nativeDisplay = new IntPtr(0);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00014F31 File Offset: 0x00013131
		internal Display(IntPtr nativeDisplay)
		{
			this.nativeDisplay = nativeDisplay;
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00014F44 File Offset: 0x00013144
		public int renderingWidth
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out num, out num2);
				return num;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00014F6C File Offset: 0x0001316C
		public int renderingHeight
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out num, out num2);
				return num2;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x00014F94 File Offset: 0x00013194
		public int systemWidth
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out num, out num2);
				return num;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00014FBC File Offset: 0x000131BC
		public int systemHeight
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out num, out num2);
				return num2;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x00014FE4 File Offset: 0x000131E4
		public RenderBuffer colorBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				RenderBuffer renderBuffer2;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out renderBuffer, out renderBuffer2);
				return renderBuffer;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00015008 File Offset: 0x00013208
		public RenderBuffer depthBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				RenderBuffer renderBuffer2;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out renderBuffer, out renderBuffer2);
				return renderBuffer2;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0001502C File Offset: 0x0001322C
		public bool active
		{
			get
			{
				return Display.GetActiveImpl(this.nativeDisplay);
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0001504C File Offset: 0x0001324C
		public bool requiresBlitToBackbuffer
		{
			get
			{
				int num = this.nativeDisplay.ToInt32();
				bool flag = num < HDROutputSettings.displays.Length;
				if (flag)
				{
					bool flag2 = HDROutputSettings.displays[num].available && HDROutputSettings.displays[num].active;
					bool flag3 = flag2;
					if (flag3)
					{
						return true;
					}
				}
				return Display.RequiresBlitToBackbufferImpl(this.nativeDisplay);
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x000150B0 File Offset: 0x000132B0
		public bool requiresSrgbBlitToBackbuffer
		{
			get
			{
				return Display.RequiresSrgbBlitToBackbufferImpl(this.nativeDisplay);
			}
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x000150D0 File Offset: 0x000132D0
		public void Activate()
		{
			Display.ActivateDisplayImpl(this.nativeDisplay, 0, 0, new RefreshRate
			{
				numerator = 60U,
				denominator = 1U
			});
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00015106 File Offset: 0x00013306
		public void Activate(int width, int height, RefreshRate refreshRate)
		{
			Display.ActivateDisplayImpl(this.nativeDisplay, width, height, refreshRate);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00015118 File Offset: 0x00013318
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Activate(int, int, int) is deprecated. Use Activate(int, int, RefreshRate) instead.", false)]
		public void Activate(int width, int height, int refreshRate)
		{
			bool flag = refreshRate < 0;
			if (flag)
			{
				refreshRate = 0;
			}
			Display.ActivateDisplayImpl(this.nativeDisplay, width, height, new RefreshRate
			{
				numerator = (uint)refreshRate,
				denominator = 1U
			});
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00015158 File Offset: 0x00013358
		public void SetParams(int width, int height, int x, int y)
		{
			Display.SetParamsImpl(this.nativeDisplay, width, height, x, y);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0001516C File Offset: 0x0001336C
		public void SetRenderingResolution(int w, int h)
		{
			Display.SetRenderingResolutionImpl(this.nativeDisplay, w, h);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00015180 File Offset: 0x00013380
		[Obsolete("MultiDisplayLicense has been deprecated.", false)]
		public static bool MultiDisplayLicense()
		{
			return true;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00015194 File Offset: 0x00013394
		public static Vector3 RelativeMouseAt(Vector3 inputMouseCoordinates)
		{
			int num = 0;
			int num2 = 0;
			int num3 = (int)inputMouseCoordinates.x;
			int num4 = (int)inputMouseCoordinates.y;
			Vector3 vector;
			vector.z = (float)Display.RelativeMouseAtImpl(num3, num4, out num, out num2);
			vector.x = (float)num;
			vector.y = (float)num2;
			return vector;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x000151E4 File Offset: 0x000133E4
		public static Display main
		{
			get
			{
				return Display._mainDisplay;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x000151FC File Offset: 0x000133FC
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x00015213 File Offset: 0x00013413
		public static int activeEditorGameViewTarget
		{
			get
			{
				return Display.m_ActiveEditorGameViewTarget;
			}
			internal set
			{
				Display.m_ActiveEditorGameViewTarget = value;
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0001521C File Offset: 0x0001341C
		[RequiredByNativeCode]
		internal static void RecreateDisplayList(IntPtr[] nativeDisplay)
		{
			bool flag = nativeDisplay.Length == 0;
			if (!flag)
			{
				Display.displays = new Display[nativeDisplay.Length];
				for (int i = 0; i < nativeDisplay.Length; i++)
				{
					Display.displays[i] = new Display(nativeDisplay[i]);
				}
				Display._mainDisplay = Display.displays[0];
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00015270 File Offset: 0x00013470
		[RequiredByNativeCode]
		internal static void FireDisplaysUpdated()
		{
			bool flag = Display.onDisplaysUpdated != null;
			if (flag)
			{
				Display.onDisplaysUpdated();
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000B66 RID: 2918 RVA: 0x00015298 File Offset: 0x00013498
		// (remove) Token: 0x06000B67 RID: 2919 RVA: 0x000152CC File Offset: 0x000134CC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Display.DisplaysUpdatedDelegate onDisplaysUpdated;

		// Token: 0x06000B68 RID: 2920
		[FreeFunction("UnityDisplayManager_DisplaySystemResolution")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSystemExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x06000B69 RID: 2921
		[FreeFunction("UnityDisplayManager_DisplayRenderingResolution")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRenderingExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x06000B6A RID: 2922
		[FreeFunction("UnityDisplayManager_GetRenderingBuffersWrapper")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRenderingBuffersImpl(IntPtr nativeDisplay, out RenderBuffer color, out RenderBuffer depth);

		// Token: 0x06000B6B RID: 2923
		[FreeFunction("UnityDisplayManager_SetRenderingResolution")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderingResolutionImpl(IntPtr nativeDisplay, int w, int h);

		// Token: 0x06000B6C RID: 2924 RVA: 0x00015300 File Offset: 0x00013500
		[FreeFunction("UnityDisplayManager_ActivateDisplay")]
		private static void ActivateDisplayImpl(IntPtr nativeDisplay, int width, int height, RefreshRate refreshRate)
		{
			Display.ActivateDisplayImpl_Injected(nativeDisplay, width, height, ref refreshRate);
		}

		// Token: 0x06000B6D RID: 2925
		[FreeFunction("UnityDisplayManager_SetDisplayParam")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetParamsImpl(IntPtr nativeDisplay, int width, int height, int x, int y);

		// Token: 0x06000B6E RID: 2926
		[FreeFunction("UnityDisplayManager_RelativeMouseAt")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RelativeMouseAtImpl(int x, int y, out int rx, out int ry);

		// Token: 0x06000B6F RID: 2927
		[FreeFunction("UnityDisplayManager_DisplayActive")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetActiveImpl(IntPtr nativeDisplay);

		// Token: 0x06000B70 RID: 2928
		[FreeFunction("UnityDisplayManager_RequiresBlitToBackbuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RequiresBlitToBackbufferImpl(IntPtr nativeDisplay);

		// Token: 0x06000B71 RID: 2929
		[FreeFunction("UnityDisplayManager_RequiresSRGBBlitToBackbuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RequiresSrgbBlitToBackbufferImpl(IntPtr nativeDisplay);

		// Token: 0x06000B72 RID: 2930 RVA: 0x00015317 File Offset: 0x00013517
		// Note: this type is marked as 'beforefieldinit'.
		static Display()
		{
			Display.onDisplaysUpdated = null;
		}

		// Token: 0x06000B73 RID: 2931
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ActivateDisplayImpl_Injected(IntPtr nativeDisplay, int width, int height, [In] ref RefreshRate refreshRate);

		// Token: 0x04000486 RID: 1158
		internal IntPtr nativeDisplay;

		// Token: 0x04000487 RID: 1159
		public static Display[] displays = new Display[]
		{
			new Display()
		};

		// Token: 0x04000488 RID: 1160
		private static Display _mainDisplay = Display.displays[0];

		// Token: 0x04000489 RID: 1161
		private static int m_ActiveEditorGameViewTarget = -1;

		// Token: 0x02000160 RID: 352
		// (Invoke) Token: 0x06000B75 RID: 2933
		public delegate void DisplaysUpdatedDelegate();
	}
}
