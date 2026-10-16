using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000169 RID: 361
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/ScreenManager.h")]
	[NativeHeader("Runtime/Graphics/WindowLayout.h")]
	[StaticAccessor("GetScreenManager()", StaticAccessorType.Dot)]
	public sealed class Screen
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000B8B RID: 2955
		public static extern int width
		{
			[NativeMethod(Name = "GetWidth", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000B8C RID: 2956
		public static extern int height
		{
			[NativeMethod(Name = "GetHeight", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000B8D RID: 2957
		public static extern float dpi
		{
			[NativeName("GetDPI")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000B8E RID: 2958
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RequestOrientation(ScreenOrientation orient);

		// Token: 0x06000B8F RID: 2959
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ScreenOrientation GetScreenOrientation();

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x00015754 File Offset: 0x00013954
		// (set) Token: 0x06000B91 RID: 2961 RVA: 0x0001576C File Offset: 0x0001396C
		public static ScreenOrientation orientation
		{
			get
			{
				return Screen.GetScreenOrientation();
			}
			set
			{
				bool flag = value == ScreenOrientation.Unknown;
				if (flag)
				{
					Debug.Log("ScreenOrientation.Unknown is deprecated. Please use ScreenOrientation.AutoRotation");
					value = ScreenOrientation.AutoRotation;
				}
				Screen.RequestOrientation(value);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000B92 RID: 2962
		// (set) Token: 0x06000B93 RID: 2963
		[NativeProperty("ScreenTimeout")]
		public static extern int sleepTimeout
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000B94 RID: 2964
		[NativeName("GetIsOrientationEnabled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsOrientationEnabled(EnabledOrientation orient);

		// Token: 0x06000B95 RID: 2965
		[NativeName("SetIsOrientationEnabled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetOrientationEnabled(EnabledOrientation orient, bool enabled);

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0001579C File Offset: 0x0001399C
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x000157B4 File Offset: 0x000139B4
		public static bool autorotateToPortrait
		{
			get
			{
				return Screen.IsOrientationEnabled(EnabledOrientation.kAutorotateToPortrait);
			}
			set
			{
				Screen.SetOrientationEnabled(EnabledOrientation.kAutorotateToPortrait, value);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x000157C0 File Offset: 0x000139C0
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x000157D8 File Offset: 0x000139D8
		public static bool autorotateToPortraitUpsideDown
		{
			get
			{
				return Screen.IsOrientationEnabled(EnabledOrientation.kAutorotateToPortraitUpsideDown);
			}
			set
			{
				Screen.SetOrientationEnabled(EnabledOrientation.kAutorotateToPortraitUpsideDown, value);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x000157E4 File Offset: 0x000139E4
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x000157FC File Offset: 0x000139FC
		public static bool autorotateToLandscapeLeft
		{
			get
			{
				return Screen.IsOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeLeft);
			}
			set
			{
				Screen.SetOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeLeft, value);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x00015808 File Offset: 0x00013A08
		// (set) Token: 0x06000B9D RID: 2973 RVA: 0x00015820 File Offset: 0x00013A20
		public static bool autorotateToLandscapeRight
		{
			get
			{
				return Screen.IsOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeRight);
			}
			set
			{
				Screen.SetOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeRight, value);
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0001582C File Offset: 0x00013A2C
		public static Resolution currentResolution
		{
			get
			{
				Resolution resolution;
				Screen.get_currentResolution_Injected(out resolution);
				return resolution;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000B9F RID: 2975
		// (set) Token: 0x06000BA0 RID: 2976
		public static extern bool fullScreen
		{
			[NativeName("IsFullscreen")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("RequestSetFullscreenFromScript")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000BA1 RID: 2977
		// (set) Token: 0x06000BA2 RID: 2978
		public static extern FullScreenMode fullScreenMode
		{
			[NativeName("GetFullscreenMode")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("RequestSetFullscreenModeFromScript")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00015844 File Offset: 0x00013A44
		public static Rect safeArea
		{
			get
			{
				Rect rect;
				Screen.get_safeArea_Injected(out rect);
				return rect;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0001585C File Offset: 0x00013A5C
		public static Rect[] cutouts
		{
			[FreeFunction("ScreenScripting::GetCutouts")]
			get
			{
				Rect[] array2;
				try
				{
					BlittableArrayWrapper blittableArrayWrapper;
					Screen.get_cutouts_Injected(out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					Rect[] array;
					blittableArrayWrapper.Unmarshal<Rect>(ref array);
					array2 = array;
				}
				return array2;
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00015890 File Offset: 0x00013A90
		[NativeName("RequestResolution")]
		public static void SetResolution(int width, int height, FullScreenMode fullscreenMode, RefreshRate preferredRefreshRate)
		{
			Screen.SetResolution_Injected(width, height, fullscreenMode, ref preferredRefreshRate);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x000158A8 File Offset: 0x00013AA8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("SetResolution(int, int, FullScreenMode, int) is obsolete. Use SetResolution(int, int, FullScreenMode, RefreshRate) instead.")]
		public static void SetResolution(int width, int height, FullScreenMode fullscreenMode, [DefaultValue("0")] int preferredRefreshRate)
		{
			bool flag = preferredRefreshRate < 0;
			if (flag)
			{
				preferredRefreshRate = 0;
			}
			Screen.SetResolution(width, height, fullscreenMode, new RefreshRate
			{
				numerator = (uint)preferredRefreshRate,
				denominator = 1U
			});
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x000158E4 File Offset: 0x00013AE4
		public static void SetResolution(int width, int height, FullScreenMode fullscreenMode)
		{
			Screen.SetResolution(width, height, fullscreenMode, new RefreshRate
			{
				numerator = 0U,
				denominator = 1U
			});
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00015914 File Offset: 0x00013B14
		[Obsolete("SetResolution(int, int, bool, int) is obsolete. Use SetResolution(int, int, FullScreenMode, RefreshRate) instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void SetResolution(int width, int height, bool fullscreen, [DefaultValue("0")] int preferredRefreshRate)
		{
			bool flag = preferredRefreshRate < 0;
			if (flag)
			{
				preferredRefreshRate = 0;
			}
			Screen.SetResolution(width, height, fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed, new RefreshRate
			{
				numerator = (uint)preferredRefreshRate,
				denominator = 1U
			});
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00015955 File Offset: 0x00013B55
		public static void SetResolution(int width, int height, bool fullscreen)
		{
			Screen.SetResolution(width, height, fullscreen, 0);
		}

		// Token: 0x06000BAA RID: 2986
		[NativeName("SetRequestedMSAASamples")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetMSAASamples(int numSamples);

		// Token: 0x06000BAB RID: 2987
		[NativeName("GetRequestedMSAASamples")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMSAASamples();

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00015964 File Offset: 0x00013B64
		public static int msaaSamples
		{
			get
			{
				return Screen.GetMSAASamples();
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0001597C File Offset: 0x00013B7C
		public static Vector2Int mainWindowPosition
		{
			get
			{
				return Screen.GetMainWindowPosition();
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x00015994 File Offset: 0x00013B94
		public static DisplayInfo mainWindowDisplayInfo
		{
			get
			{
				return Screen.GetMainWindowDisplayInfo();
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x000159AC File Offset: 0x00013BAC
		public static void GetDisplayLayout(List<DisplayInfo> displayLayout)
		{
			bool flag = displayLayout == null;
			if (flag)
			{
				throw new ArgumentNullException();
			}
			Screen.GetDisplayLayoutImpl(displayLayout);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000159D0 File Offset: 0x00013BD0
		public static AsyncOperation MoveMainWindowTo(in DisplayInfo display, Vector2Int position)
		{
			return Screen.MoveMainWindowImpl(in display, position);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000159EC File Offset: 0x00013BEC
		[FreeFunction("GetMainWindowPosition")]
		private static Vector2Int GetMainWindowPosition()
		{
			Vector2Int vector2Int;
			Screen.GetMainWindowPosition_Injected(out vector2Int);
			return vector2Int;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00015A04 File Offset: 0x00013C04
		[FreeFunction("GetMainWindowDisplayInfo")]
		private static DisplayInfo GetMainWindowDisplayInfo()
		{
			DisplayInfo displayInfo;
			Screen.GetMainWindowDisplayInfo_Injected(out displayInfo);
			return displayInfo;
		}

		// Token: 0x06000BB3 RID: 2995
		[FreeFunction("GetDisplayLayout")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDisplayLayoutImpl(List<DisplayInfo> displayLayout);

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00015A1C File Offset: 0x00013C1C
		[FreeFunction("MoveMainWindow")]
		private static AsyncOperation MoveMainWindowImpl(in DisplayInfo display, Vector2Int position)
		{
			IntPtr intPtr = Screen.MoveMainWindowImpl_Injected(in display, ref position);
			return (intPtr == 0) ? null : AsyncOperation.BindingsMarshaller.ConvertToManaged(intPtr);
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x00015A40 File Offset: 0x00013C40
		public static Resolution[] resolutions
		{
			[FreeFunction("ScreenScripting::GetResolutions")]
			get
			{
				Resolution[] array2;
				try
				{
					BlittableArrayWrapper blittableArrayWrapper;
					Screen.get_resolutions_Injected(out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					Resolution[] array;
					blittableArrayWrapper.Unmarshal<Resolution>(ref array);
					array2 = array;
				}
				return array2;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000BB6 RID: 2998
		// (set) Token: 0x06000BB7 RID: 2999
		public static extern float brightness
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x00015A74 File Offset: 0x00013C74
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x00015A90 File Offset: 0x00013C90
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use Cursor.lockState and Cursor.visible instead.", false)]
		public static bool lockCursor
		{
			get
			{
				return CursorLockMode.Locked == Cursor.lockState;
			}
			set
			{
				if (value)
				{
					Cursor.visible = false;
					Cursor.lockState = CursorLockMode.Locked;
				}
				else
				{
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
				}
			}
		}

		// Token: 0x06000BBB RID: 3003
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_currentResolution_Injected(out Resolution ret);

		// Token: 0x06000BBC RID: 3004
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_safeArea_Injected(out Rect ret);

		// Token: 0x06000BBD RID: 3005
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_cutouts_Injected(out BlittableArrayWrapper ret);

		// Token: 0x06000BBE RID: 3006
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetResolution_Injected(int width, int height, FullScreenMode fullscreenMode, [In] ref RefreshRate preferredRefreshRate);

		// Token: 0x06000BBF RID: 3007
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMainWindowPosition_Injected(out Vector2Int ret);

		// Token: 0x06000BC0 RID: 3008
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMainWindowDisplayInfo_Injected(out DisplayInfo ret);

		// Token: 0x06000BC1 RID: 3009
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr MoveMainWindowImpl_Injected(in DisplayInfo display, [In] ref Vector2Int position);

		// Token: 0x06000BC2 RID: 3010
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_resolutions_Injected(out BlittableArrayWrapper ret);
	}
}
