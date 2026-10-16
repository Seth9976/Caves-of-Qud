using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000543 RID: 1347
	public struct RasterState : IEquatable<RasterState>
	{
		// Token: 0x06003385 RID: 13189 RVA: 0x00064BCD File Offset: 0x00062DCD
		public RasterState(CullMode cullingMode = CullMode.Back, int offsetUnits = 0, float offsetFactor = 0f, bool depthClip = true)
		{
			this.m_CullingMode = cullingMode;
			this.m_OffsetUnits = offsetUnits;
			this.m_OffsetFactor = offsetFactor;
			this.m_DepthClip = Convert.ToByte(depthClip);
			this.m_Conservative = Convert.ToByte(false);
			this.m_Padding1 = 0;
			this.m_Padding2 = 0;
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06003386 RID: 13190 RVA: 0x00064C0C File Offset: 0x00062E0C
		// (set) Token: 0x06003387 RID: 13191 RVA: 0x00064C24 File Offset: 0x00062E24
		public CullMode cullingMode
		{
			get
			{
				return this.m_CullingMode;
			}
			set
			{
				this.m_CullingMode = value;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06003388 RID: 13192 RVA: 0x00064C30 File Offset: 0x00062E30
		// (set) Token: 0x06003389 RID: 13193 RVA: 0x00064C4D File Offset: 0x00062E4D
		public bool depthClip
		{
			get
			{
				return Convert.ToBoolean(this.m_DepthClip);
			}
			set
			{
				this.m_DepthClip = Convert.ToByte(value);
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x0600338A RID: 13194 RVA: 0x00064C5C File Offset: 0x00062E5C
		// (set) Token: 0x0600338B RID: 13195 RVA: 0x00064C79 File Offset: 0x00062E79
		public bool conservative
		{
			get
			{
				return Convert.ToBoolean(this.m_Conservative);
			}
			set
			{
				this.m_Conservative = Convert.ToByte(value);
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x0600338C RID: 13196 RVA: 0x00064C88 File Offset: 0x00062E88
		// (set) Token: 0x0600338D RID: 13197 RVA: 0x00064CA0 File Offset: 0x00062EA0
		public int offsetUnits
		{
			get
			{
				return this.m_OffsetUnits;
			}
			set
			{
				this.m_OffsetUnits = value;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x0600338E RID: 13198 RVA: 0x00064CAC File Offset: 0x00062EAC
		// (set) Token: 0x0600338F RID: 13199 RVA: 0x00064CC4 File Offset: 0x00062EC4
		public float offsetFactor
		{
			get
			{
				return this.m_OffsetFactor;
			}
			set
			{
				this.m_OffsetFactor = value;
			}
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x00064CD0 File Offset: 0x00062ED0
		public bool Equals(RasterState other)
		{
			return this.m_CullingMode == other.m_CullingMode && this.m_OffsetUnits == other.m_OffsetUnits && this.m_OffsetFactor.Equals(other.m_OffsetFactor) && this.m_DepthClip == other.m_DepthClip && this.m_Conservative == other.m_Conservative;
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x00064D30 File Offset: 0x00062F30
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RasterState && this.Equals((RasterState)obj);
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x00064D68 File Offset: 0x00062F68
		public override int GetHashCode()
		{
			int num = (int)this.m_CullingMode;
			num = (num * 397) ^ this.m_OffsetUnits;
			num = (num * 397) ^ this.m_OffsetFactor.GetHashCode();
			num = (num * 397) ^ this.m_DepthClip.GetHashCode();
			return (num * 397) ^ this.m_Conservative.GetHashCode();
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x00064DD0 File Offset: 0x00062FD0
		public static bool operator ==(RasterState left, RasterState right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x00064DEC File Offset: 0x00062FEC
		public static bool operator !=(RasterState left, RasterState right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400114F RID: 4431
		public static readonly RasterState defaultValue = new RasterState(CullMode.Back, 0, 0f, true);

		// Token: 0x04001150 RID: 4432
		private CullMode m_CullingMode;

		// Token: 0x04001151 RID: 4433
		private int m_OffsetUnits;

		// Token: 0x04001152 RID: 4434
		private float m_OffsetFactor;

		// Token: 0x04001153 RID: 4435
		private byte m_DepthClip;

		// Token: 0x04001154 RID: 4436
		private byte m_Conservative;

		// Token: 0x04001155 RID: 4437
		private byte m_Padding1;

		// Token: 0x04001156 RID: 4438
		private byte m_Padding2;
	}
}
