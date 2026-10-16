using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004A6 RID: 1190
	public struct RenderTargetBinding
	{
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x00056C58 File Offset: 0x00054E58
		// (set) Token: 0x06002DB8 RID: 11704 RVA: 0x00056C70 File Offset: 0x00054E70
		public RenderTargetIdentifier[] colorRenderTargets
		{
			get
			{
				return this.m_ColorRenderTargets;
			}
			set
			{
				this.m_ColorRenderTargets = value;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002DB9 RID: 11705 RVA: 0x00056C7C File Offset: 0x00054E7C
		// (set) Token: 0x06002DBA RID: 11706 RVA: 0x00056C94 File Offset: 0x00054E94
		public RenderTargetIdentifier depthRenderTarget
		{
			get
			{
				return this.m_DepthRenderTarget;
			}
			set
			{
				this.m_DepthRenderTarget = value;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002DBB RID: 11707 RVA: 0x00056CA0 File Offset: 0x00054EA0
		// (set) Token: 0x06002DBC RID: 11708 RVA: 0x00056CB8 File Offset: 0x00054EB8
		public RenderBufferLoadAction[] colorLoadActions
		{
			get
			{
				return this.m_ColorLoadActions;
			}
			set
			{
				this.m_ColorLoadActions = value;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002DBD RID: 11709 RVA: 0x00056CC4 File Offset: 0x00054EC4
		// (set) Token: 0x06002DBE RID: 11710 RVA: 0x00056CDC File Offset: 0x00054EDC
		public RenderBufferStoreAction[] colorStoreActions
		{
			get
			{
				return this.m_ColorStoreActions;
			}
			set
			{
				this.m_ColorStoreActions = value;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002DBF RID: 11711 RVA: 0x00056CE8 File Offset: 0x00054EE8
		// (set) Token: 0x06002DC0 RID: 11712 RVA: 0x00056D00 File Offset: 0x00054F00
		public RenderBufferLoadAction depthLoadAction
		{
			get
			{
				return this.m_DepthLoadAction;
			}
			set
			{
				this.m_DepthLoadAction = value;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x00056D0C File Offset: 0x00054F0C
		// (set) Token: 0x06002DC2 RID: 11714 RVA: 0x00056D24 File Offset: 0x00054F24
		public RenderBufferStoreAction depthStoreAction
		{
			get
			{
				return this.m_DepthStoreAction;
			}
			set
			{
				this.m_DepthStoreAction = value;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002DC3 RID: 11715 RVA: 0x00056D30 File Offset: 0x00054F30
		// (set) Token: 0x06002DC4 RID: 11716 RVA: 0x00056D48 File Offset: 0x00054F48
		public RenderTargetFlags flags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x00056D52 File Offset: 0x00054F52
		public RenderTargetBinding(RenderTargetIdentifier[] colorRenderTargets, RenderBufferLoadAction[] colorLoadActions, RenderBufferStoreAction[] colorStoreActions, RenderTargetIdentifier depthRenderTarget, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			this.m_ColorRenderTargets = colorRenderTargets;
			this.m_DepthRenderTarget = depthRenderTarget;
			this.m_ColorLoadActions = colorLoadActions;
			this.m_ColorStoreActions = colorStoreActions;
			this.m_DepthLoadAction = depthLoadAction;
			this.m_DepthStoreAction = depthStoreAction;
			this.m_Flags = RenderTargetFlags.None;
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x00056D89 File Offset: 0x00054F89
		public RenderTargetBinding(RenderTargetIdentifier colorRenderTarget, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depthRenderTarget, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			this = new RenderTargetBinding(new RenderTargetIdentifier[] { colorRenderTarget }, new RenderBufferLoadAction[] { colorLoadAction }, new RenderBufferStoreAction[] { colorStoreAction }, depthRenderTarget, depthLoadAction, depthStoreAction);
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x00056DBC File Offset: 0x00054FBC
		public RenderTargetBinding(RenderTargetSetup setup)
		{
			this.m_ColorRenderTargets = new RenderTargetIdentifier[setup.color.Length];
			for (int i = 0; i < this.m_ColorRenderTargets.Length; i++)
			{
				this.m_ColorRenderTargets[i] = new RenderTargetIdentifier(setup.color[i], setup.mipLevel, setup.cubemapFace, setup.depthSlice);
			}
			this.m_DepthRenderTarget = setup.depth;
			this.m_ColorLoadActions = (RenderBufferLoadAction[])setup.colorLoad.Clone();
			this.m_ColorStoreActions = (RenderBufferStoreAction[])setup.colorStore.Clone();
			this.m_DepthLoadAction = setup.depthLoad;
			this.m_DepthStoreAction = setup.depthStore;
			this.m_Flags = RenderTargetFlags.None;
		}

		// Token: 0x04000DE0 RID: 3552
		private RenderTargetIdentifier[] m_ColorRenderTargets;

		// Token: 0x04000DE1 RID: 3553
		private RenderTargetIdentifier m_DepthRenderTarget;

		// Token: 0x04000DE2 RID: 3554
		private RenderBufferLoadAction[] m_ColorLoadActions;

		// Token: 0x04000DE3 RID: 3555
		private RenderBufferStoreAction[] m_ColorStoreActions;

		// Token: 0x04000DE4 RID: 3556
		private RenderBufferLoadAction m_DepthLoadAction;

		// Token: 0x04000DE5 RID: 3557
		private RenderBufferStoreAction m_DepthStoreAction;

		// Token: 0x04000DE6 RID: 3558
		private RenderTargetFlags m_Flags;
	}
}
