using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x02000526 RID: 1318
	public struct AttachmentDescriptor : IEquatable<AttachmentDescriptor>
	{
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06003282 RID: 12930 RVA: 0x00061D6C File Offset: 0x0005FF6C
		// (set) Token: 0x06003283 RID: 12931 RVA: 0x00061D84 File Offset: 0x0005FF84
		public RenderBufferLoadAction loadAction
		{
			get
			{
				return this.m_LoadAction;
			}
			set
			{
				this.m_LoadAction = value;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06003284 RID: 12932 RVA: 0x00061D90 File Offset: 0x0005FF90
		// (set) Token: 0x06003285 RID: 12933 RVA: 0x00061DA8 File Offset: 0x0005FFA8
		public RenderBufferStoreAction storeAction
		{
			get
			{
				return this.m_StoreAction;
			}
			set
			{
				this.m_StoreAction = value;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06003286 RID: 12934 RVA: 0x00061DB4 File Offset: 0x0005FFB4
		// (set) Token: 0x06003287 RID: 12935 RVA: 0x00061DCC File Offset: 0x0005FFCC
		public GraphicsFormat graphicsFormat
		{
			get
			{
				return this.m_Format;
			}
			set
			{
				this.m_Format = value;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06003288 RID: 12936 RVA: 0x00061DD8 File Offset: 0x0005FFD8
		// (set) Token: 0x06003289 RID: 12937 RVA: 0x00061E09 File Offset: 0x00060009
		public RenderTextureFormat format
		{
			get
			{
				bool flag = GraphicsFormatUtility.IsDepthStencilFormat(this.m_Format);
				RenderTextureFormat renderTextureFormat;
				if (flag)
				{
					renderTextureFormat = RenderTextureFormat.Depth;
				}
				else
				{
					renderTextureFormat = GraphicsFormatUtility.GetRenderTextureFormat(this.m_Format);
				}
				return renderTextureFormat;
			}
			set
			{
				this.m_Format = AttachmentDescriptor.GetAdjustedFormat(value, RenderTextureReadWrite.Default);
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600328A RID: 12938 RVA: 0x00061E1C File Offset: 0x0006001C
		// (set) Token: 0x0600328B RID: 12939 RVA: 0x00061E34 File Offset: 0x00060034
		public RenderTargetIdentifier loadStoreTarget
		{
			get
			{
				return this.m_LoadStoreTarget;
			}
			set
			{
				this.m_LoadStoreTarget = value;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x0600328C RID: 12940 RVA: 0x00061E40 File Offset: 0x00060040
		// (set) Token: 0x0600328D RID: 12941 RVA: 0x00061E58 File Offset: 0x00060058
		public RenderTargetIdentifier resolveTarget
		{
			get
			{
				return this.m_ResolveTarget;
			}
			set
			{
				this.m_ResolveTarget = value;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x0600328E RID: 12942 RVA: 0x00061E64 File Offset: 0x00060064
		// (set) Token: 0x0600328F RID: 12943 RVA: 0x00061E7C File Offset: 0x0006007C
		public Color clearColor
		{
			get
			{
				return this.m_ClearColor;
			}
			set
			{
				this.m_ClearColor = value;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06003290 RID: 12944 RVA: 0x00061E88 File Offset: 0x00060088
		// (set) Token: 0x06003291 RID: 12945 RVA: 0x00061EA0 File Offset: 0x000600A0
		public float clearDepth
		{
			get
			{
				return this.m_ClearDepth;
			}
			set
			{
				this.m_ClearDepth = value;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x00061EAC File Offset: 0x000600AC
		// (set) Token: 0x06003293 RID: 12947 RVA: 0x00061EC4 File Offset: 0x000600C4
		public uint clearStencil
		{
			get
			{
				return this.m_ClearStencil;
			}
			set
			{
				this.m_ClearStencil = value;
			}
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x00061ED0 File Offset: 0x000600D0
		public void ConfigureTarget(RenderTargetIdentifier target, bool loadExistingContents, bool storeResults)
		{
			this.m_LoadStoreTarget = target;
			bool flag = loadExistingContents && this.m_LoadAction != RenderBufferLoadAction.Clear;
			if (flag)
			{
				this.m_LoadAction = RenderBufferLoadAction.Load;
			}
			if (storeResults)
			{
				bool flag2 = this.m_StoreAction == RenderBufferStoreAction.StoreAndResolve || this.m_StoreAction == RenderBufferStoreAction.Resolve;
				if (flag2)
				{
					this.m_StoreAction = RenderBufferStoreAction.StoreAndResolve;
				}
				else
				{
					this.m_StoreAction = RenderBufferStoreAction.Store;
				}
			}
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x00061F34 File Offset: 0x00060134
		public void ConfigureResolveTarget(RenderTargetIdentifier target)
		{
			this.m_ResolveTarget = target;
			bool flag = this.m_StoreAction == RenderBufferStoreAction.StoreAndResolve || this.m_StoreAction == RenderBufferStoreAction.Store;
			if (flag)
			{
				this.m_StoreAction = RenderBufferStoreAction.StoreAndResolve;
			}
			else
			{
				this.m_StoreAction = RenderBufferStoreAction.Resolve;
			}
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x00061F72 File Offset: 0x00060172
		public void ConfigureClear(Color clearColor, float clearDepth = 1f, uint clearStencil = 0U)
		{
			this.m_ClearColor = clearColor;
			this.m_ClearDepth = clearDepth;
			this.m_ClearStencil = clearStencil;
			this.m_LoadAction = RenderBufferLoadAction.Clear;
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x00061F94 File Offset: 0x00060194
		public AttachmentDescriptor(GraphicsFormat format)
		{
			this = default(AttachmentDescriptor);
			this.m_LoadAction = RenderBufferLoadAction.DontCare;
			this.m_StoreAction = RenderBufferStoreAction.DontCare;
			this.m_Format = format;
			this.m_LoadStoreTarget = new RenderTargetIdentifier(BuiltinRenderTextureType.None);
			this.m_ResolveTarget = new RenderTargetIdentifier(BuiltinRenderTextureType.None);
			this.m_ClearColor = new Color(0f, 0f, 0f, 0f);
			this.m_ClearDepth = 1f;
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x00062000 File Offset: 0x00060200
		public AttachmentDescriptor(RenderTextureFormat format)
		{
			this = new AttachmentDescriptor(AttachmentDescriptor.GetAdjustedFormat(format, RenderTextureReadWrite.Default));
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x00062000 File Offset: 0x00060200
		public AttachmentDescriptor(RenderTextureFormat format, RenderTargetIdentifier target, bool loadExistingContents = false, bool storeResults = false, bool resolve = false)
		{
			this = new AttachmentDescriptor(AttachmentDescriptor.GetAdjustedFormat(format, RenderTextureReadWrite.Default));
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x00062014 File Offset: 0x00060214
		private static GraphicsFormat GetAdjustedFormat(RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			GraphicsFormat graphicsFormat;
			if (format != RenderTextureFormat.Depth && format != RenderTextureFormat.Shadowmap)
			{
				graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(format, readWrite);
			}
			else
			{
				graphicsFormat = SystemInfo.GetGraphicsFormat((format == RenderTextureFormat.Depth) ? DefaultFormat.DepthStencil : DefaultFormat.Shadow);
			}
			return graphicsFormat;
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x00062050 File Offset: 0x00060250
		public bool Equals(AttachmentDescriptor other)
		{
			return this.m_LoadAction == other.m_LoadAction && this.m_StoreAction == other.m_StoreAction && this.m_Format == other.m_Format && this.m_LoadStoreTarget.Equals(other.m_LoadStoreTarget) && this.m_ResolveTarget.Equals(other.m_ResolveTarget) && this.m_ClearColor.Equals(other.m_ClearColor) && this.m_ClearDepth.Equals(other.m_ClearDepth) && this.m_ClearStencil == other.m_ClearStencil;
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x000620EC File Offset: 0x000602EC
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is AttachmentDescriptor && this.Equals((AttachmentDescriptor)obj);
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x00062124 File Offset: 0x00060324
		public override int GetHashCode()
		{
			int num = (int)this.m_LoadAction;
			num = (num * 397) ^ (int)this.m_StoreAction;
			num = (num * 397) ^ (int)this.m_Format;
			num = (num * 397) ^ this.m_LoadStoreTarget.GetHashCode();
			num = (num * 397) ^ this.m_ResolveTarget.GetHashCode();
			num = (num * 397) ^ this.m_ClearColor.GetHashCode();
			num = (num * 397) ^ this.m_ClearDepth.GetHashCode();
			return (num * 397) ^ (int)this.m_ClearStencil;
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x000621D0 File Offset: 0x000603D0
		public static bool operator ==(AttachmentDescriptor left, AttachmentDescriptor right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x000621EC File Offset: 0x000603EC
		public static bool operator !=(AttachmentDescriptor left, AttachmentDescriptor right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04001095 RID: 4245
		private RenderBufferLoadAction m_LoadAction;

		// Token: 0x04001096 RID: 4246
		private RenderBufferStoreAction m_StoreAction;

		// Token: 0x04001097 RID: 4247
		private GraphicsFormat m_Format;

		// Token: 0x04001098 RID: 4248
		private RenderTargetIdentifier m_LoadStoreTarget;

		// Token: 0x04001099 RID: 4249
		private RenderTargetIdentifier m_ResolveTarget;

		// Token: 0x0400109A RID: 4250
		private Color m_ClearColor;

		// Token: 0x0400109B RID: 4251
		private float m_ClearDepth;

		// Token: 0x0400109C RID: 4252
		private uint m_ClearStencil;
	}
}
