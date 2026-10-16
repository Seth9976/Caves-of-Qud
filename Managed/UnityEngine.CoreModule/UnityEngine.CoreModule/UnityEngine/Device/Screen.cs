using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Internal;

namespace UnityEngine.Device
{
	// Token: 0x02000463 RID: 1123
	public static class Screen
	{
		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x000533BB File Offset: 0x000515BB
		// (set) Token: 0x06002BB7 RID: 11191 RVA: 0x000533C2 File Offset: 0x000515C2
		public static float brightness
		{
			get
			{
				return Screen.brightness;
			}
			set
			{
				Screen.brightness = value;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x000533CB File Offset: 0x000515CB
		// (set) Token: 0x06002BB9 RID: 11193 RVA: 0x000533D2 File Offset: 0x000515D2
		public static bool autorotateToLandscapeLeft
		{
			get
			{
				return Screen.autorotateToLandscapeLeft;
			}
			set
			{
				Screen.autorotateToLandscapeLeft = value;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06002BBA RID: 11194 RVA: 0x000533DB File Offset: 0x000515DB
		// (set) Token: 0x06002BBB RID: 11195 RVA: 0x000533E2 File Offset: 0x000515E2
		public static bool autorotateToLandscapeRight
		{
			get
			{
				return Screen.autorotateToLandscapeRight;
			}
			set
			{
				Screen.autorotateToLandscapeRight = value;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06002BBC RID: 11196 RVA: 0x000533EB File Offset: 0x000515EB
		// (set) Token: 0x06002BBD RID: 11197 RVA: 0x000533F2 File Offset: 0x000515F2
		public static bool autorotateToPortrait
		{
			get
			{
				return Screen.autorotateToPortrait;
			}
			set
			{
				Screen.autorotateToPortrait = value;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x000533FB File Offset: 0x000515FB
		// (set) Token: 0x06002BBF RID: 11199 RVA: 0x00053402 File Offset: 0x00051602
		public static bool autorotateToPortraitUpsideDown
		{
			get
			{
				return Screen.autorotateToPortraitUpsideDown;
			}
			set
			{
				Screen.autorotateToPortraitUpsideDown = value;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x0005340B File Offset: 0x0005160B
		public static Resolution currentResolution
		{
			get
			{
				return Screen.currentResolution;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06002BC1 RID: 11201 RVA: 0x00053412 File Offset: 0x00051612
		public static Rect[] cutouts
		{
			get
			{
				return Screen.cutouts;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06002BC2 RID: 11202 RVA: 0x00053419 File Offset: 0x00051619
		public static float dpi
		{
			get
			{
				return Screen.dpi;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06002BC3 RID: 11203 RVA: 0x00053420 File Offset: 0x00051620
		// (set) Token: 0x06002BC4 RID: 11204 RVA: 0x00053427 File Offset: 0x00051627
		public static bool fullScreen
		{
			get
			{
				return Screen.fullScreen;
			}
			set
			{
				Screen.fullScreen = value;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x00053430 File Offset: 0x00051630
		// (set) Token: 0x06002BC6 RID: 11206 RVA: 0x00053437 File Offset: 0x00051637
		public static FullScreenMode fullScreenMode
		{
			get
			{
				return Screen.fullScreenMode;
			}
			set
			{
				Screen.fullScreenMode = value;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x00053440 File Offset: 0x00051640
		public static int height
		{
			get
			{
				return Screen.height;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x00053447 File Offset: 0x00051647
		public static int width
		{
			get
			{
				return Screen.width;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x0005344E File Offset: 0x0005164E
		// (set) Token: 0x06002BCA RID: 11210 RVA: 0x00053455 File Offset: 0x00051655
		public static ScreenOrientation orientation
		{
			get
			{
				return Screen.orientation;
			}
			set
			{
				Screen.orientation = value;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06002BCB RID: 11211 RVA: 0x0005345E File Offset: 0x0005165E
		public static Resolution[] resolutions
		{
			get
			{
				return Screen.resolutions;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x00053465 File Offset: 0x00051665
		public static Rect safeArea
		{
			get
			{
				return Screen.safeArea;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06002BCD RID: 11213 RVA: 0x0005346C File Offset: 0x0005166C
		// (set) Token: 0x06002BCE RID: 11214 RVA: 0x00053473 File Offset: 0x00051673
		public static int sleepTimeout
		{
			get
			{
				return Screen.sleepTimeout;
			}
			set
			{
				Screen.sleepTimeout = value;
			}
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x0005347C File Offset: 0x0005167C
		public static void SetResolution(int width, int height, FullScreenMode fullscreenMode, RefreshRate preferredRefreshRate)
		{
			Screen.SetResolution(width, height, fullscreenMode, preferredRefreshRate);
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x0005348C File Offset: 0x0005168C
		[Obsolete("SetResolution(int, int, FullScreenMode, int) is obsolete. Use SetResolution(int, int, FullScreenMode, RefreshRate) instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000534C8 File Offset: 0x000516C8
		public static void SetResolution(int width, int height, FullScreenMode fullscreenMode)
		{
			Screen.SetResolution(width, height, fullscreenMode, new RefreshRate
			{
				numerator = 0U,
				denominator = 1U
			});
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000534F8 File Offset: 0x000516F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("SetResolution(int, int, bool, int) is obsolete. Use SetResolution(int, int, FullScreenMode, RefreshRate) instead.")]
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

		// Token: 0x06002BD3 RID: 11219 RVA: 0x0005353C File Offset: 0x0005173C
		public static void SetResolution(int width, int height, bool fullscreen)
		{
			Screen.SetResolution(width, height, fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed, new RefreshRate
			{
				numerator = 0U,
				denominator = 1U
			});
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06002BD4 RID: 11220 RVA: 0x00053572 File Offset: 0x00051772
		public static Vector2Int mainWindowPosition
		{
			get
			{
				return Screen.mainWindowPosition;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x00053579 File Offset: 0x00051779
		public static DisplayInfo mainWindowDisplayInfo
		{
			get
			{
				return Screen.mainWindowDisplayInfo;
			}
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x00053580 File Offset: 0x00051780
		public static void GetDisplayLayout(List<DisplayInfo> displayLayout)
		{
			Screen.GetDisplayLayout(displayLayout);
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x00053589 File Offset: 0x00051789
		public static AsyncOperation MoveMainWindowTo(in DisplayInfo display, Vector2Int position)
		{
			return Screen.MoveMainWindowTo(in display, position);
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x00053592 File Offset: 0x00051792
		public static void SetMSAASamples(int numSamples)
		{
			Screen.SetMSAASamples(numSamples);
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06002BD9 RID: 11225 RVA: 0x0005359B File Offset: 0x0005179B
		public static int msaaSamples
		{
			get
			{
				return Screen.msaaSamples;
			}
		}
	}
}
