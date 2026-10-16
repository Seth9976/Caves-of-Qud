using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Pool;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200054D RID: 1357
	public static class RenderPipelineManager
	{
		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060033E8 RID: 13288 RVA: 0x000658DB File Offset: 0x00063ADB
		internal static RenderPipelineAsset currentPipelineAsset
		{
			get
			{
				return RenderPipelineManager.s_CurrentPipelineAsset;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060033E9 RID: 13289 RVA: 0x000658E2 File Offset: 0x00063AE2
		// (set) Token: 0x060033EA RID: 13290 RVA: 0x000658E9 File Offset: 0x00063AE9
		public static RenderPipeline currentPipeline
		{
			get
			{
				return RenderPipelineManager.s_CurrentPipeline;
			}
			private set
			{
				RenderPipelineManager.s_CurrentPipelineType = ((value != null) ? value.GetType().ToString() : "Built-in Pipeline");
				RenderPipelineManager.s_CurrentPipeline = value;
			}
		}

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x060033EB RID: 13291 RVA: 0x0006590C File Offset: 0x00063B0C
		// (remove) Token: 0x060033EC RID: 13292 RVA: 0x00065940 File Offset: 0x00063B40
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<ScriptableRenderContext, List<Camera>> beginContextRendering;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x060033ED RID: 13293 RVA: 0x00065974 File Offset: 0x00063B74
		// (remove) Token: 0x060033EE RID: 13294 RVA: 0x000659A8 File Offset: 0x00063BA8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<ScriptableRenderContext, List<Camera>> endContextRendering;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x060033EF RID: 13295 RVA: 0x000659DC File Offset: 0x00063BDC
		// (remove) Token: 0x060033F0 RID: 13296 RVA: 0x00065A10 File Offset: 0x00063C10
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<ScriptableRenderContext, Camera> beginCameraRendering;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060033F1 RID: 13297 RVA: 0x00065A44 File Offset: 0x00063C44
		// (remove) Token: 0x060033F2 RID: 13298 RVA: 0x00065A78 File Offset: 0x00063C78
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<ScriptableRenderContext, Camera> endCameraRendering;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x060033F3 RID: 13299 RVA: 0x00065AAC File Offset: 0x00063CAC
		// (remove) Token: 0x060033F4 RID: 13300 RVA: 0x00065AE0 File Offset: 0x00063CE0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action activeRenderPipelineTypeChanged;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x060033F5 RID: 13301 RVA: 0x00065B14 File Offset: 0x00063D14
		// (remove) Token: 0x060033F6 RID: 13302 RVA: 0x00065B48 File Offset: 0x00063D48
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<RenderPipelineAsset, RenderPipelineAsset> activeRenderPipelineAssetChanged;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x060033F7 RID: 13303 RVA: 0x00065B7C File Offset: 0x00063D7C
		// (remove) Token: 0x060033F8 RID: 13304 RVA: 0x00065BB0 File Offset: 0x00063DB0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action activeRenderPipelineCreated;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x060033F9 RID: 13305 RVA: 0x00065BE4 File Offset: 0x00063DE4
		// (remove) Token: 0x060033FA RID: 13306 RVA: 0x00065C18 File Offset: 0x00063E18
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action activeRenderPipelineDisposed;

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060033FB RID: 13307 RVA: 0x00065C4B File Offset: 0x00063E4B
		public static bool pipelineSwitchCompleted
		{
			get
			{
				return RenderPipelineManager.s_CurrentPipelineAsset == GraphicsSettings.currentRenderPipeline && !RenderPipelineManager.IsPipelineRequireCreation();
			}
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x00065C64 File Offset: 0x00063E64
		internal static void BeginContextRendering(ScriptableRenderContext context, List<Camera> cameras)
		{
			Action<ScriptableRenderContext, List<Camera>> action = RenderPipelineManager.beginContextRendering;
			if (action != null)
			{
				action(context, cameras);
			}
			Action<ScriptableRenderContext, Camera[]> action2 = RenderPipelineManager.beginFrameRendering;
			if (action2 != null)
			{
				action2(context, cameras.ToArray());
			}
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x00065C92 File Offset: 0x00063E92
		internal static void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			Action<ScriptableRenderContext, Camera> action = RenderPipelineManager.beginCameraRendering;
			if (action != null)
			{
				action(context, camera);
			}
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x00065CA8 File Offset: 0x00063EA8
		internal static void EndContextRendering(ScriptableRenderContext context, List<Camera> cameras)
		{
			Action<ScriptableRenderContext, Camera[]> action = RenderPipelineManager.endFrameRendering;
			if (action != null)
			{
				action(context, cameras.ToArray());
			}
			Action<ScriptableRenderContext, List<Camera>> action2 = RenderPipelineManager.endContextRendering;
			if (action2 != null)
			{
				action2(context, cameras);
			}
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x00065CD6 File Offset: 0x00063ED6
		internal static void EndCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			Action<ScriptableRenderContext, Camera> action = RenderPipelineManager.endCameraRendering;
			if (action != null)
			{
				action(context, camera);
			}
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x00065CEC File Offset: 0x00063EEC
		[RequiredByNativeCode]
		internal static void OnActiveRenderPipelineTypeChanged()
		{
			Action action = RenderPipelineManager.activeRenderPipelineTypeChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x00065D00 File Offset: 0x00063F00
		[RequiredByNativeCode]
		internal static void OnActiveRenderPipelineAssetChanged(ScriptableObject from, ScriptableObject to)
		{
			Action<RenderPipelineAsset, RenderPipelineAsset> action = RenderPipelineManager.activeRenderPipelineAssetChanged;
			if (action != null)
			{
				action(from as RenderPipelineAsset, to as RenderPipelineAsset);
			}
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x00065D20 File Offset: 0x00063F20
		[RequiredByNativeCode]
		internal static void HandleRenderPipelineChange(RenderPipelineAsset pipelineAsset)
		{
			bool flag = RenderPipelineManager.s_CurrentPipelineAsset != pipelineAsset;
			bool flag2 = RenderPipelineManager.s_CleanUpPipeline || flag;
			if (flag2)
			{
				RenderPipelineManager.CleanupRenderPipeline();
				RenderPipelineManager.s_CurrentPipelineAsset = pipelineAsset;
			}
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x00065D54 File Offset: 0x00063F54
		[RequiredByNativeCode]
		internal static void RecreateCurrentPipeline(RenderPipelineAsset pipelineAsset)
		{
			bool flag = RenderPipelineManager.s_CurrentPipelineAsset == pipelineAsset;
			if (flag)
			{
				RenderPipelineManager.s_CleanUpPipeline = true;
			}
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x00065D7C File Offset: 0x00063F7C
		[RequiredByNativeCode]
		internal static void CleanupRenderPipeline()
		{
			bool flag = !RenderPipelineManager.isCurrentPipelineValid;
			if (!flag)
			{
				bool flag2 = GraphicsSettings.currentRenderPipeline == null;
				if (flag2)
				{
					Shader.globalRenderPipeline = string.Empty;
				}
				Action action = RenderPipelineManager.activeRenderPipelineDisposed;
				if (action != null)
				{
					action();
				}
				RenderPipelineManager.currentPipeline.Dispose();
				RenderPipelineManager.currentPipeline = null;
				RenderPipelineManager.s_CleanUpPipeline = false;
				RenderPipelineManager.s_CurrentPipelineAsset = null;
				SupportedRenderingFeatures.active = null;
			}
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x00065DE8 File Offset: 0x00063FE8
		[RequiredByNativeCode]
		private static string GetCurrentPipelineAssetType()
		{
			return RenderPipelineManager.s_CurrentPipelineType;
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x00065E00 File Offset: 0x00064000
		[RequiredByNativeCode]
		private static void DoRenderLoop_Internal(RenderPipelineAsset pipelineAsset, IntPtr loopPtr, Object renderRequest)
		{
			bool flag = !RenderPipelineManager.TryPrepareRenderPipeline(pipelineAsset);
			if (!flag)
			{
				ScriptableRenderContext scriptableRenderContext = new ScriptableRenderContext(loopPtr);
				List<Camera> list;
				using (CollectionPool<List<Camera>, Camera>.Get(out list))
				{
					scriptableRenderContext.GetCameras(list);
					bool flag2 = renderRequest == null;
					if (flag2)
					{
						RenderPipelineManager.currentPipeline.InternalRender(scriptableRenderContext, list);
					}
					else
					{
						RenderPipelineManager.currentPipeline.InternalProcessRenderRequests<Object>(scriptableRenderContext, list[0], renderRequest);
					}
				}
			}
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x00065E8C File Offset: 0x0006408C
		internal static bool TryPrepareRenderPipeline(RenderPipelineAsset pipelineAsset)
		{
			RenderPipelineManager.HandleRenderPipelineChange(pipelineAsset);
			bool flag = !RenderPipelineManager.IsPipelineRequireCreation();
			bool flag2;
			if (flag)
			{
				flag2 = RenderPipelineManager.currentPipeline != null;
			}
			else
			{
				RenderPipelineManager.currentPipeline = RenderPipelineManager.s_CurrentPipelineAsset.InternalCreatePipeline();
				Shader.globalRenderPipeline = RenderPipelineManager.s_CurrentPipelineAsset.renderPipelineShaderTag;
				Action action = RenderPipelineManager.activeRenderPipelineCreated;
				if (action != null)
				{
					action();
				}
				flag2 = RenderPipelineManager.currentPipeline != null;
			}
			return flag2;
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06003408 RID: 13320 RVA: 0x00065EF8 File Offset: 0x000640F8
		private static bool isCurrentPipelineValid
		{
			get
			{
				RenderPipeline currentPipeline = RenderPipelineManager.currentPipeline;
				return currentPipeline != null && !currentPipeline.disposed;
			}
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x00065F1A File Offset: 0x0006411A
		private static bool IsPipelineRequireCreation()
		{
			return RenderPipelineManager.s_CurrentPipelineAsset != null && (RenderPipelineManager.currentPipeline == null || RenderPipelineManager.currentPipeline.disposed);
		}

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x0600340A RID: 13322 RVA: 0x00065F40 File Offset: 0x00064140
		// (remove) Token: 0x0600340B RID: 13323 RVA: 0x00065F74 File Offset: 0x00064174
		[Obsolete("beginFrameRendering is deprecated. Use beginContextRendering instead. #from 2023.3", false)]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<ScriptableRenderContext, Camera[]> beginFrameRendering;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x0600340C RID: 13324 RVA: 0x00065FA8 File Offset: 0x000641A8
		// (remove) Token: 0x0600340D RID: 13325 RVA: 0x00065FDC File Offset: 0x000641DC
		[Obsolete("endFrameRendering is deprecated. Use endContextRendering instead. #from 2023.3", false)]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<ScriptableRenderContext, Camera[]> endFrameRendering;

		// Token: 0x04001176 RID: 4470
		private static bool s_CleanUpPipeline = false;

		// Token: 0x04001177 RID: 4471
		private const string k_BuiltinPipelineName = "Built-in Pipeline";

		// Token: 0x04001178 RID: 4472
		private static string s_CurrentPipelineType = "Built-in Pipeline";

		// Token: 0x04001179 RID: 4473
		private static RenderPipelineAsset s_CurrentPipelineAsset;

		// Token: 0x0400117A RID: 4474
		private static RenderPipeline s_CurrentPipeline = null;
	}
}
