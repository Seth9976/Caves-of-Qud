using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000175 RID: 373
	[UsedByNativeCode]
	[NativeHeader("Runtime/GfxDevice/HDROutputSettings.h")]
	public class HDROutputSettings
	{
		// Token: 0x06000D7D RID: 3453 RVA: 0x0001936C File Offset: 0x0001756C
		[VisibleToOtherModules(new string[] { "UnityEngine.XRModule" })]
		internal HDROutputSettings()
		{
			this.m_DisplayIndex = 0;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0001937D File Offset: 0x0001757D
		[VisibleToOtherModules(new string[] { "UnityEngine.XRModule" })]
		internal HDROutputSettings(int displayIndex)
		{
			this.m_DisplayIndex = displayIndex;
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x00019390 File Offset: 0x00017590
		public static HDROutputSettings main
		{
			get
			{
				return HDROutputSettings._mainDisplay;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x000193A8 File Offset: 0x000175A8
		public bool active
		{
			get
			{
				return HDROutputSettings.GetActive(this.m_DisplayIndex);
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x000193C8 File Offset: 0x000175C8
		public bool available
		{
			get
			{
				return HDROutputSettings.GetAvailable(this.m_DisplayIndex);
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x000193E8 File Offset: 0x000175E8
		// (set) Token: 0x06000D83 RID: 3459 RVA: 0x00019405 File Offset: 0x00017605
		public bool automaticHDRTonemapping
		{
			get
			{
				return HDROutputSettings.GetAutomaticHDRTonemapping(this.m_DisplayIndex);
			}
			set
			{
				HDROutputSettings.SetAutomaticHDRTonemapping(this.m_DisplayIndex, value);
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00019418 File Offset: 0x00017618
		public ColorGamut displayColorGamut
		{
			get
			{
				return HDROutputSettings.GetDisplayColorGamut(this.m_DisplayIndex);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00019438 File Offset: 0x00017638
		public RenderTextureFormat format
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(HDROutputSettings.GetGraphicsFormat(this.m_DisplayIndex));
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x0001945C File Offset: 0x0001765C
		public GraphicsFormat graphicsFormat
		{
			get
			{
				return HDROutputSettings.GetGraphicsFormat(this.m_DisplayIndex);
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0001947C File Offset: 0x0001767C
		// (set) Token: 0x06000D88 RID: 3464 RVA: 0x00019499 File Offset: 0x00017699
		public float paperWhiteNits
		{
			get
			{
				return HDROutputSettings.GetPaperWhiteNits(this.m_DisplayIndex);
			}
			set
			{
				HDROutputSettings.SetPaperWhiteNits(this.m_DisplayIndex, value);
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x000194AC File Offset: 0x000176AC
		public int maxFullFrameToneMapLuminance
		{
			get
			{
				return HDROutputSettings.GetMaxFullFrameToneMapLuminance(this.m_DisplayIndex);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x000194CC File Offset: 0x000176CC
		public int maxToneMapLuminance
		{
			get
			{
				return HDROutputSettings.GetMaxToneMapLuminance(this.m_DisplayIndex);
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x000194EC File Offset: 0x000176EC
		public int minToneMapLuminance
		{
			get
			{
				return HDROutputSettings.GetMinToneMapLuminance(this.m_DisplayIndex);
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0001950C File Offset: 0x0001770C
		public bool HDRModeChangeRequested
		{
			get
			{
				return HDROutputSettings.GetHDRModeChangeRequested(this.m_DisplayIndex);
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00019529 File Offset: 0x00017729
		public void RequestHDRModeChange(bool enabled)
		{
			HDROutputSettings.RequestHDRModeChangeInternal(this.m_DisplayIndex, enabled);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0001953C File Offset: 0x0001773C
		[Obsolete("SetPaperWhiteInNits is deprecated, please use paperWhiteNits instead.")]
		public static void SetPaperWhiteInNits(float paperWhite)
		{
			int num = 0;
			bool available = HDROutputSettings.GetAvailable(num);
			if (available)
			{
				HDROutputSettings.SetPaperWhiteNits(num, paperWhite);
			}
		}

		// Token: 0x06000D8F RID: 3471
		[FreeFunction("HDROutputSettingsBindings::GetActive", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetActive(int displayIndex);

		// Token: 0x06000D90 RID: 3472
		[FreeFunction("HDROutputSettingsBindings::GetAvailable", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetAvailable(int displayIndex);

		// Token: 0x06000D91 RID: 3473
		[FreeFunction("HDROutputSettingsBindings::GetAutomaticHDRTonemapping", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetAutomaticHDRTonemapping(int displayIndex);

		// Token: 0x06000D92 RID: 3474
		[FreeFunction("HDROutputSettingsBindings::SetAutomaticHDRTonemapping", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAutomaticHDRTonemapping(int displayIndex, bool scripted);

		// Token: 0x06000D93 RID: 3475
		[FreeFunction("HDROutputSettingsBindings::GetDisplayColorGamut", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ColorGamut GetDisplayColorGamut(int displayIndex);

		// Token: 0x06000D94 RID: 3476
		[FreeFunction("HDROutputSettingsBindings::GetGraphicsFormat", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat GetGraphicsFormat(int displayIndex);

		// Token: 0x06000D95 RID: 3477
		[FreeFunction("HDROutputSettingsBindings::GetPaperWhiteNits", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetPaperWhiteNits(int displayIndex);

		// Token: 0x06000D96 RID: 3478
		[FreeFunction("HDROutputSettingsBindings::SetPaperWhiteNits", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPaperWhiteNits(int displayIndex, float paperWhite);

		// Token: 0x06000D97 RID: 3479
		[FreeFunction("HDROutputSettingsBindings::GetMaxFullFrameToneMapLuminance", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxFullFrameToneMapLuminance(int displayIndex);

		// Token: 0x06000D98 RID: 3480
		[FreeFunction("HDROutputSettingsBindings::GetMaxToneMapLuminance", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxToneMapLuminance(int displayIndex);

		// Token: 0x06000D99 RID: 3481
		[FreeFunction("HDROutputSettingsBindings::GetMinToneMapLuminance", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMinToneMapLuminance(int displayIndex);

		// Token: 0x06000D9A RID: 3482
		[FreeFunction("HDROutputSettingsBindings::GetHDRModeChangeRequested", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetHDRModeChangeRequested(int displayIndex);

		// Token: 0x06000D9B RID: 3483
		[FreeFunction("HDROutputSettingsBindings::RequestHDRModeChange", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RequestHDRModeChangeInternal(int displayIndex, bool enabled);

		// Token: 0x040004C7 RID: 1223
		private int m_DisplayIndex;

		// Token: 0x040004C8 RID: 1224
		public static HDROutputSettings[] displays = new HDROutputSettings[]
		{
			new HDROutputSettings()
		};

		// Token: 0x040004C9 RID: 1225
		private static HDROutputSettings _mainDisplay = HDROutputSettings.displays[0];
	}
}
