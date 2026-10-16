using System;
using System.Text;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Internal;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x0200024C RID: 588
	[NLogConfigurationItem]
	[Preserve]
	public abstract class LayoutRenderer : ISupportsInitialize, IRenderable, IDisposable
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x00076CBD File Offset: 0x00074EBD
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x00076CC4 File Offset: 0x00074EC4
		// (set) Token: 0x0600159B RID: 5531 RVA: 0x00076CCC File Offset: 0x00074ECC
		private protected LoggingConfiguration LoggingConfiguration { protected get; private set; }

		// Token: 0x0600159C RID: 5532 RVA: 0x00076CD8 File Offset: 0x00074ED8
		public override string ToString()
		{
			LayoutRendererAttribute layoutRendererAttribute = (LayoutRendererAttribute)Attribute.GetCustomAttribute(base.GetType(), typeof(LayoutRendererAttribute));
			if (layoutRendererAttribute != null)
			{
				return "Layout Renderer: ${" + layoutRendererAttribute.DisplayName + "}";
			}
			return base.GetType().Name;
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00076D24 File Offset: 0x00074F24
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00076D34 File Offset: 0x00074F34
		public string Render(LogEventInfo logEvent)
		{
			int num = this.maxRenderedLength;
			if (num > 16384)
			{
				num = 16384;
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			this.Render(stringBuilder, logEvent);
			if (stringBuilder.Length > this.maxRenderedLength)
			{
				this.maxRenderedLength = stringBuilder.Length;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00076D85 File Offset: 0x00074F85
		void ISupportsInitialize.Initialize(LoggingConfiguration configuration)
		{
			this.Initialize(configuration);
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00076D8E File Offset: 0x00074F8E
		void ISupportsInitialize.Close()
		{
			this.Close();
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00076D96 File Offset: 0x00074F96
		internal void Initialize(LoggingConfiguration configuration)
		{
			if (!this.isInitialized)
			{
				this.LoggingConfiguration = configuration;
				this.isInitialized = true;
				this.InitializeLayoutRenderer();
			}
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00076DB4 File Offset: 0x00074FB4
		internal void Close()
		{
			if (this.isInitialized)
			{
				this.LoggingConfiguration = null;
				this.isInitialized = false;
				this.CloseLayoutRenderer();
			}
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00076DD4 File Offset: 0x00074FD4
		internal void Render(StringBuilder builder, LogEventInfo logEvent)
		{
			if (!this.isInitialized)
			{
				this.isInitialized = true;
				this.InitializeLayoutRenderer();
			}
			try
			{
				this.Append(builder, logEvent);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				LayoutRenderer.Logger.Warn("Exception in layout renderer: {0}", new object[] { ex });
			}
		}

		// Token: 0x060015A4 RID: 5540
		protected abstract void Append(StringBuilder builder, LogEventInfo logEvent);

		// Token: 0x060015A5 RID: 5541 RVA: 0x00076E38 File Offset: 0x00075038
		protected virtual void InitializeLayoutRenderer()
		{
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00076E3A File Offset: 0x0007503A
		protected virtual void CloseLayoutRenderer()
		{
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00076E3C File Offset: 0x0007503C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x04000DA1 RID: 3489
		private const int MaxInitialRenderBufferLength = 16384;

		// Token: 0x04000DA2 RID: 3490
		private int maxRenderedLength;

		// Token: 0x04000DA3 RID: 3491
		private bool isInitialized;
	}
}
