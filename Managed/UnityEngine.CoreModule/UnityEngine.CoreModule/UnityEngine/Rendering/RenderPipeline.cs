using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000548 RID: 1352
	public abstract class RenderPipeline
	{
		// Token: 0x060033A5 RID: 13221
		protected abstract void Render(ScriptableRenderContext context, Camera[] cameras);

		// Token: 0x060033A6 RID: 13222 RVA: 0x00002925 File Offset: 0x00000B25
		protected virtual void ProcessRenderRequests<RequestData>(ScriptableRenderContext context, Camera camera, RequestData renderRequest)
		{
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x0006538C File Offset: 0x0006358C
		protected internal virtual bool IsRenderRequestSupported<RequestData>(Camera camera, RequestData data)
		{
			return false;
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x0006539F File Offset: 0x0006359F
		protected static void BeginFrameRendering(ScriptableRenderContext context, Camera[] cameras)
		{
			RenderPipelineManager.BeginContextRendering(context, new List<Camera>(cameras));
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x000653AF File Offset: 0x000635AF
		protected static void BeginContextRendering(ScriptableRenderContext context, List<Camera> cameras)
		{
			RenderPipelineManager.BeginContextRendering(context, cameras);
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x000653BA File Offset: 0x000635BA
		protected static void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			RenderPipelineManager.BeginCameraRendering(context, camera);
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x000653C5 File Offset: 0x000635C5
		protected static void EndContextRendering(ScriptableRenderContext context, List<Camera> cameras)
		{
			RenderPipelineManager.EndContextRendering(context, cameras);
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x000653D0 File Offset: 0x000635D0
		protected static void EndFrameRendering(ScriptableRenderContext context, Camera[] cameras)
		{
			RenderPipelineManager.EndContextRendering(context, new List<Camera>(cameras));
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x000653E0 File Offset: 0x000635E0
		protected static void EndCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			RenderPipelineManager.EndCameraRendering(context, camera);
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x000653EB File Offset: 0x000635EB
		protected virtual void Render(ScriptableRenderContext context, List<Camera> cameras)
		{
			this.Render(context, cameras.ToArray());
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x000653FC File Offset: 0x000635FC
		internal void InternalRender(ScriptableRenderContext context, List<Camera> cameras)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				throw new ObjectDisposedException(string.Format("{0} has been disposed. Do not call Render on disposed a RenderPipeline.", this));
			}
			this.Render(context, cameras);
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x00065430 File Offset: 0x00063630
		internal void InternalProcessRenderRequests<RequestData>(ScriptableRenderContext context, Camera camera, RequestData renderRequest)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				throw new ObjectDisposedException(string.Format("{0} has been disposed. Do not call Render on disposed a RenderPipeline.", this));
			}
			this.ProcessRenderRequests<RequestData>(context, camera, renderRequest);
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x00065464 File Offset: 0x00063664
		public static bool SupportsRenderRequest<RequestData>(Camera camera, RequestData data)
		{
			bool flag = false;
			bool flag2 = GraphicsSettings.currentRenderPipeline != null;
			if (flag2)
			{
				bool flag3 = RenderPipelineManager.currentPipeline == null;
				if (flag3)
				{
					bool flag4 = RenderPipelineManager.TryPrepareRenderPipeline(GraphicsSettings.currentRenderPipeline);
					Debug.Assert(flag4);
				}
				bool flag5 = RenderPipelineManager.currentPipeline != null;
				if (flag5)
				{
					flag = RenderPipelineManager.currentPipeline.IsRenderRequestSupported<RequestData>(camera, data);
				}
			}
			return flag;
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x000654C7 File Offset: 0x000636C7
		public static void SubmitRenderRequest<RequestData>(Camera camera, RequestData data)
		{
			camera.SubmitRenderRequest<RequestData>(data);
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x060033B3 RID: 13235 RVA: 0x000654D2 File Offset: 0x000636D2
		// (set) Token: 0x060033B4 RID: 13236 RVA: 0x000654DA File Offset: 0x000636DA
		public bool disposed { get; private set; }

		// Token: 0x060033B5 RID: 13237 RVA: 0x000654E3 File Offset: 0x000636E3
		internal void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
			this.disposed = true;
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x00002925 File Offset: 0x00000B25
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x060033B7 RID: 13239 RVA: 0x00065500 File Offset: 0x00063700
		public virtual RenderPipelineGlobalSettings defaultSettings
		{
			get
			{
				return null;
			}
		}

		// Token: 0x02000549 RID: 1353
		public class StandardRequest
		{
			// Token: 0x04001170 RID: 4464
			public RenderTexture destination = null;

			// Token: 0x04001171 RID: 4465
			public int mipLevel = 0;

			// Token: 0x04001172 RID: 4466
			public CubemapFace face = CubemapFace.Unknown;

			// Token: 0x04001173 RID: 4467
			public int slice = 0;
		}
	}
}
