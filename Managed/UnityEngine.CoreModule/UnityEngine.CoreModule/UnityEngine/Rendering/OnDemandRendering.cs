using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020004D6 RID: 1238
	[RequiredByNativeCode]
	public class OnDemandRendering
	{
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002E4E RID: 11854 RVA: 0x00057C10 File Offset: 0x00055E10
		public static bool willCurrentFrameRender
		{
			get
			{
				return Time.frameCount % OnDemandRendering.renderFrameInterval == 0;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002E4F RID: 11855 RVA: 0x00057C30 File Offset: 0x00055E30
		// (set) Token: 0x06002E50 RID: 11856 RVA: 0x00057C47 File Offset: 0x00055E47
		public static int renderFrameInterval
		{
			get
			{
				return OnDemandRendering.m_RenderFrameInterval;
			}
			set
			{
				OnDemandRendering.m_RenderFrameInterval = Math.Max(1, value);
			}
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x00057C56 File Offset: 0x00055E56
		[RequiredByNativeCode]
		internal static void GetRenderFrameInterval(out int frameInterval)
		{
			frameInterval = OnDemandRendering.renderFrameInterval;
		}

		// Token: 0x06002E52 RID: 11858
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float GetEffectiveRenderFrameRate();

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002E53 RID: 11859 RVA: 0x00057C60 File Offset: 0x00055E60
		public static int effectiveRenderFrameRate
		{
			get
			{
				float effectiveRenderFrameRate = OnDemandRendering.GetEffectiveRenderFrameRate();
				bool flag = (double)effectiveRenderFrameRate <= 0.0;
				int num;
				if (flag)
				{
					num = (int)effectiveRenderFrameRate;
				}
				else
				{
					num = (int)(effectiveRenderFrameRate + 0.5f);
				}
				return num;
			}
		}

		// Token: 0x04000ED0 RID: 3792
		private static int m_RenderFrameInterval = 1;
	}
}
