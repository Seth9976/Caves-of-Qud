using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000464 RID: 1124
	public struct StyleBackgroundSize : IStyleValue<BackgroundSize>, IEquatable<StyleBackgroundSize>
	{
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x00082210 File Offset: 0x00080410
		// (set) Token: 0x060022FA RID: 8954 RVA: 0x0008223B File Offset: 0x0008043B
		public BackgroundSize value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(BackgroundSize);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x0008224C File Offset: 0x0008044C
		// (set) Token: 0x060022FC RID: 8956 RVA: 0x00082264 File Offset: 0x00080464
		public StyleKeyword keyword
		{
			get
			{
				return this.m_Keyword;
			}
			set
			{
				this.m_Keyword = value;
			}
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x0008226E File Offset: 0x0008046E
		public StyleBackgroundSize(BackgroundSize v)
		{
			this = new StyleBackgroundSize(v, StyleKeyword.Undefined);
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x0008227C File Offset: 0x0008047C
		public StyleBackgroundSize(StyleKeyword keyword)
		{
			this = new StyleBackgroundSize(default(BackgroundSize), keyword);
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x0008229B File Offset: 0x0008049B
		internal StyleBackgroundSize(BackgroundSize v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x000822AC File Offset: 0x000804AC
		public static bool operator ==(StyleBackgroundSize lhs, StyleBackgroundSize rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x000822E0 File Offset: 0x000804E0
		public static bool operator !=(StyleBackgroundSize lhs, StyleBackgroundSize rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x000822FC File Offset: 0x000804FC
		public static implicit operator StyleBackgroundSize(StyleKeyword keyword)
		{
			return new StyleBackgroundSize(keyword);
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x00082314 File Offset: 0x00080514
		public static implicit operator StyleBackgroundSize(BackgroundSize v)
		{
			return new StyleBackgroundSize(v);
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x0008232C File Offset: 0x0008052C
		public bool Equals(StyleBackgroundSize other)
		{
			return other == this;
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x0008234C File Offset: 0x0008054C
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleBackgroundSize)
			{
				StyleBackgroundSize styleBackgroundSize = (StyleBackgroundSize)obj;
				flag = this.Equals(styleBackgroundSize);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x00082378 File Offset: 0x00080578
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x000823AC File Offset: 0x000805AC
		public override string ToString()
		{
			return this.DebugString<BackgroundSize>();
		}

		// Token: 0x04000FB4 RID: 4020
		private BackgroundSize m_Value;

		// Token: 0x04000FB5 RID: 4021
		private StyleKeyword m_Keyword;
	}
}
