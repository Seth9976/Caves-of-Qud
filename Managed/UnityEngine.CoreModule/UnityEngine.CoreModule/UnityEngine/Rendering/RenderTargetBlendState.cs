using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000551 RID: 1361
	public struct RenderTargetBlendState : IEquatable<RenderTargetBlendState>
	{
		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x0600342F RID: 13359 RVA: 0x00066584 File Offset: 0x00064784
		public static RenderTargetBlendState defaultValue
		{
			get
			{
				return new RenderTargetBlendState(ColorWriteMask.All, BlendMode.One, BlendMode.Zero, BlendMode.One, BlendMode.Zero, BlendOp.Add, BlendOp.Add);
			}
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000665A4 File Offset: 0x000647A4
		public RenderTargetBlendState(ColorWriteMask writeMask = ColorWriteMask.All, BlendMode sourceColorBlendMode = BlendMode.One, BlendMode destinationColorBlendMode = BlendMode.Zero, BlendMode sourceAlphaBlendMode = BlendMode.One, BlendMode destinationAlphaBlendMode = BlendMode.Zero, BlendOp colorBlendOperation = BlendOp.Add, BlendOp alphaBlendOperation = BlendOp.Add)
		{
			this.m_WriteMask = (byte)writeMask;
			this.m_SourceColorBlendMode = (byte)sourceColorBlendMode;
			this.m_DestinationColorBlendMode = (byte)destinationColorBlendMode;
			this.m_SourceAlphaBlendMode = (byte)sourceAlphaBlendMode;
			this.m_DestinationAlphaBlendMode = (byte)destinationAlphaBlendMode;
			this.m_ColorBlendOperation = (byte)colorBlendOperation;
			this.m_AlphaBlendOperation = (byte)alphaBlendOperation;
			this.m_Padding = 0;
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06003431 RID: 13361 RVA: 0x000665F8 File Offset: 0x000647F8
		// (set) Token: 0x06003432 RID: 13362 RVA: 0x00066610 File Offset: 0x00064810
		public ColorWriteMask writeMask
		{
			get
			{
				return (ColorWriteMask)this.m_WriteMask;
			}
			set
			{
				this.m_WriteMask = (byte)value;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06003433 RID: 13363 RVA: 0x0006661C File Offset: 0x0006481C
		// (set) Token: 0x06003434 RID: 13364 RVA: 0x00066634 File Offset: 0x00064834
		public BlendMode sourceColorBlendMode
		{
			get
			{
				return (BlendMode)this.m_SourceColorBlendMode;
			}
			set
			{
				this.m_SourceColorBlendMode = (byte)value;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06003435 RID: 13365 RVA: 0x00066640 File Offset: 0x00064840
		// (set) Token: 0x06003436 RID: 13366 RVA: 0x00066658 File Offset: 0x00064858
		public BlendMode destinationColorBlendMode
		{
			get
			{
				return (BlendMode)this.m_DestinationColorBlendMode;
			}
			set
			{
				this.m_DestinationColorBlendMode = (byte)value;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x00066664 File Offset: 0x00064864
		// (set) Token: 0x06003438 RID: 13368 RVA: 0x0006667C File Offset: 0x0006487C
		public BlendMode sourceAlphaBlendMode
		{
			get
			{
				return (BlendMode)this.m_SourceAlphaBlendMode;
			}
			set
			{
				this.m_SourceAlphaBlendMode = (byte)value;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x00066688 File Offset: 0x00064888
		// (set) Token: 0x0600343A RID: 13370 RVA: 0x000666A0 File Offset: 0x000648A0
		public BlendMode destinationAlphaBlendMode
		{
			get
			{
				return (BlendMode)this.m_DestinationAlphaBlendMode;
			}
			set
			{
				this.m_DestinationAlphaBlendMode = (byte)value;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x0600343B RID: 13371 RVA: 0x000666AC File Offset: 0x000648AC
		// (set) Token: 0x0600343C RID: 13372 RVA: 0x000666C4 File Offset: 0x000648C4
		public BlendOp colorBlendOperation
		{
			get
			{
				return (BlendOp)this.m_ColorBlendOperation;
			}
			set
			{
				this.m_ColorBlendOperation = (byte)value;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600343D RID: 13373 RVA: 0x000666D0 File Offset: 0x000648D0
		// (set) Token: 0x0600343E RID: 13374 RVA: 0x000666E8 File Offset: 0x000648E8
		public BlendOp alphaBlendOperation
		{
			get
			{
				return (BlendOp)this.m_AlphaBlendOperation;
			}
			set
			{
				this.m_AlphaBlendOperation = (byte)value;
			}
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x000666F4 File Offset: 0x000648F4
		public bool Equals(RenderTargetBlendState other)
		{
			return this.m_WriteMask == other.m_WriteMask && this.m_SourceColorBlendMode == other.m_SourceColorBlendMode && this.m_DestinationColorBlendMode == other.m_DestinationColorBlendMode && this.m_SourceAlphaBlendMode == other.m_SourceAlphaBlendMode && this.m_DestinationAlphaBlendMode == other.m_DestinationAlphaBlendMode && this.m_ColorBlendOperation == other.m_ColorBlendOperation && this.m_AlphaBlendOperation == other.m_AlphaBlendOperation;
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x0006676C File Offset: 0x0006496C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RenderTargetBlendState && this.Equals((RenderTargetBlendState)obj);
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x000667A4 File Offset: 0x000649A4
		public override int GetHashCode()
		{
			int num = this.m_WriteMask.GetHashCode();
			num = (num * 397) ^ this.m_SourceColorBlendMode.GetHashCode();
			num = (num * 397) ^ this.m_DestinationColorBlendMode.GetHashCode();
			num = (num * 397) ^ this.m_SourceAlphaBlendMode.GetHashCode();
			num = (num * 397) ^ this.m_DestinationAlphaBlendMode.GetHashCode();
			num = (num * 397) ^ this.m_ColorBlendOperation.GetHashCode();
			return (num * 397) ^ this.m_AlphaBlendOperation.GetHashCode();
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x0006683C File Offset: 0x00064A3C
		public static bool operator ==(RenderTargetBlendState left, RenderTargetBlendState right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x00066858 File Offset: 0x00064A58
		public static bool operator !=(RenderTargetBlendState left, RenderTargetBlendState right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04001198 RID: 4504
		private byte m_WriteMask;

		// Token: 0x04001199 RID: 4505
		private byte m_SourceColorBlendMode;

		// Token: 0x0400119A RID: 4506
		private byte m_DestinationColorBlendMode;

		// Token: 0x0400119B RID: 4507
		private byte m_SourceAlphaBlendMode;

		// Token: 0x0400119C RID: 4508
		private byte m_DestinationAlphaBlendMode;

		// Token: 0x0400119D RID: 4509
		private byte m_ColorBlendOperation;

		// Token: 0x0400119E RID: 4510
		private byte m_AlphaBlendOperation;

		// Token: 0x0400119F RID: 4511
		private byte m_Padding;
	}
}
