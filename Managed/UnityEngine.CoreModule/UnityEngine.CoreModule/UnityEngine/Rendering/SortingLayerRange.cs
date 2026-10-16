using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200055C RID: 1372
	public struct SortingLayerRange : IEquatable<SortingLayerRange>
	{
		// Token: 0x060034F2 RID: 13554 RVA: 0x00068032 File Offset: 0x00066232
		public SortingLayerRange(short lowerBound, short upperBound)
		{
			this.m_LowerBound = lowerBound;
			this.m_UpperBound = upperBound;
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x060034F3 RID: 13555 RVA: 0x00068044 File Offset: 0x00066244
		// (set) Token: 0x060034F4 RID: 13556 RVA: 0x0006805C File Offset: 0x0006625C
		public short lowerBound
		{
			get
			{
				return this.m_LowerBound;
			}
			set
			{
				this.m_LowerBound = value;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x060034F5 RID: 13557 RVA: 0x00068068 File Offset: 0x00066268
		// (set) Token: 0x060034F6 RID: 13558 RVA: 0x00068080 File Offset: 0x00066280
		public short upperBound
		{
			get
			{
				return this.m_UpperBound;
			}
			set
			{
				this.m_UpperBound = value;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x060034F7 RID: 13559 RVA: 0x0006808C File Offset: 0x0006628C
		public static SortingLayerRange all
		{
			get
			{
				return new SortingLayerRange
				{
					m_LowerBound = short.MinValue,
					m_UpperBound = short.MaxValue
				};
			}
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x000680BC File Offset: 0x000662BC
		public bool Equals(SortingLayerRange other)
		{
			return this.m_LowerBound == other.m_LowerBound && this.m_UpperBound == other.m_UpperBound;
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x000680F0 File Offset: 0x000662F0
		public override bool Equals(object obj)
		{
			bool flag = !(obj is SortingLayerRange);
			return !flag && this.Equals((SortingLayerRange)obj);
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x00068124 File Offset: 0x00066324
		public static bool operator !=(SortingLayerRange lhs, SortingLayerRange rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x00068144 File Offset: 0x00066344
		public static bool operator ==(SortingLayerRange lhs, SortingLayerRange rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x00068160 File Offset: 0x00066360
		public override int GetHashCode()
		{
			return ((int)this.m_UpperBound << 16) | ((int)this.m_LowerBound & 65535);
		}

		// Token: 0x040011CC RID: 4556
		private short m_LowerBound;

		// Token: 0x040011CD RID: 4557
		private short m_UpperBound;
	}
}
