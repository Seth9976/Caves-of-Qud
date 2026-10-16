using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200054F RID: 1359
	public struct RenderStateBlock : IEquatable<RenderStateBlock>
	{
		// Token: 0x0600341D RID: 13341 RVA: 0x000662E7 File Offset: 0x000644E7
		public RenderStateBlock(RenderStateMask mask)
		{
			this.m_BlendState = BlendState.defaultValue;
			this.m_RasterState = RasterState.defaultValue;
			this.m_DepthState = DepthState.defaultValue;
			this.m_StencilState = StencilState.defaultValue;
			this.m_StencilReference = 0;
			this.m_Mask = mask;
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x0600341E RID: 13342 RVA: 0x00066324 File Offset: 0x00064524
		// (set) Token: 0x0600341F RID: 13343 RVA: 0x0006633C File Offset: 0x0006453C
		public BlendState blendState
		{
			get
			{
				return this.m_BlendState;
			}
			set
			{
				this.m_BlendState = value;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06003420 RID: 13344 RVA: 0x00066348 File Offset: 0x00064548
		// (set) Token: 0x06003421 RID: 13345 RVA: 0x00066360 File Offset: 0x00064560
		public RasterState rasterState
		{
			get
			{
				return this.m_RasterState;
			}
			set
			{
				this.m_RasterState = value;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06003422 RID: 13346 RVA: 0x0006636C File Offset: 0x0006456C
		// (set) Token: 0x06003423 RID: 13347 RVA: 0x00066384 File Offset: 0x00064584
		public DepthState depthState
		{
			get
			{
				return this.m_DepthState;
			}
			set
			{
				this.m_DepthState = value;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06003424 RID: 13348 RVA: 0x00066390 File Offset: 0x00064590
		// (set) Token: 0x06003425 RID: 13349 RVA: 0x000663A8 File Offset: 0x000645A8
		public StencilState stencilState
		{
			get
			{
				return this.m_StencilState;
			}
			set
			{
				this.m_StencilState = value;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06003426 RID: 13350 RVA: 0x000663B4 File Offset: 0x000645B4
		// (set) Token: 0x06003427 RID: 13351 RVA: 0x000663CC File Offset: 0x000645CC
		public int stencilReference
		{
			get
			{
				return this.m_StencilReference;
			}
			set
			{
				this.m_StencilReference = value;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06003428 RID: 13352 RVA: 0x000663D8 File Offset: 0x000645D8
		// (set) Token: 0x06003429 RID: 13353 RVA: 0x000663F0 File Offset: 0x000645F0
		public RenderStateMask mask
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x000663FC File Offset: 0x000645FC
		public bool Equals(RenderStateBlock other)
		{
			return this.m_BlendState.Equals(other.m_BlendState) && this.m_RasterState.Equals(other.m_RasterState) && this.m_DepthState.Equals(other.m_DepthState) && this.m_StencilState.Equals(other.m_StencilState) && this.m_StencilReference == other.m_StencilReference && this.m_Mask == other.m_Mask;
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x0006647C File Offset: 0x0006467C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RenderStateBlock && this.Equals((RenderStateBlock)obj);
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x000664B4 File Offset: 0x000646B4
		public override int GetHashCode()
		{
			int num = this.m_BlendState.GetHashCode();
			num = (num * 397) ^ this.m_RasterState.GetHashCode();
			num = (num * 397) ^ this.m_DepthState.GetHashCode();
			num = (num * 397) ^ this.m_StencilState.GetHashCode();
			num = (num * 397) ^ this.m_StencilReference;
			return (num * 397) ^ (int)this.m_Mask;
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x00066548 File Offset: 0x00064748
		public static bool operator ==(RenderStateBlock left, RenderStateBlock right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x00066564 File Offset: 0x00064764
		public static bool operator !=(RenderStateBlock left, RenderStateBlock right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400118B RID: 4491
		private BlendState m_BlendState;

		// Token: 0x0400118C RID: 4492
		private RasterState m_RasterState;

		// Token: 0x0400118D RID: 4493
		private DepthState m_DepthState;

		// Token: 0x0400118E RID: 4494
		private StencilState m_StencilState;

		// Token: 0x0400118F RID: 4495
		private int m_StencilReference;

		// Token: 0x04001190 RID: 4496
		private RenderStateMask m_Mask;
	}
}
