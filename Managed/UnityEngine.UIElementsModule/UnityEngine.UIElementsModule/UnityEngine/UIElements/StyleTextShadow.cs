using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000473 RID: 1139
	public struct StyleTextShadow : IStyleValue<TextShadow>, IEquatable<StyleTextShadow>
	{
		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060023DF RID: 9183 RVA: 0x00083EA4 File Offset: 0x000820A4
		// (set) Token: 0x060023E0 RID: 9184 RVA: 0x00083ECF File Offset: 0x000820CF
		public TextShadow value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(TextShadow);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060023E1 RID: 9185 RVA: 0x00083EE0 File Offset: 0x000820E0
		// (set) Token: 0x060023E2 RID: 9186 RVA: 0x00083EF8 File Offset: 0x000820F8
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

		// Token: 0x060023E3 RID: 9187 RVA: 0x00083F02 File Offset: 0x00082102
		public StyleTextShadow(TextShadow v)
		{
			this = new StyleTextShadow(v, StyleKeyword.Undefined);
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x00083F10 File Offset: 0x00082110
		public StyleTextShadow(StyleKeyword keyword)
		{
			this = new StyleTextShadow(default(TextShadow), keyword);
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x00083F2F File Offset: 0x0008212F
		internal StyleTextShadow(TextShadow v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x00083F40 File Offset: 0x00082140
		public static bool operator ==(StyleTextShadow lhs, StyleTextShadow rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x00083F74 File Offset: 0x00082174
		public static bool operator !=(StyleTextShadow lhs, StyleTextShadow rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x00083F90 File Offset: 0x00082190
		public static implicit operator StyleTextShadow(StyleKeyword keyword)
		{
			return new StyleTextShadow(keyword);
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x00083FA8 File Offset: 0x000821A8
		public static implicit operator StyleTextShadow(TextShadow v)
		{
			return new StyleTextShadow(v);
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x00083FC0 File Offset: 0x000821C0
		public bool Equals(StyleTextShadow other)
		{
			return other == this;
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x00083FE0 File Offset: 0x000821E0
		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleTextShadow);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				StyleTextShadow styleTextShadow = (StyleTextShadow)obj;
				flag2 = styleTextShadow == this;
			}
			return flag2;
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x0008401C File Offset: 0x0008221C
		public override int GetHashCode()
		{
			int num = 917506989;
			num = num * -1521134295 + this.m_Keyword.GetHashCode();
			return num * -1521134295 + this.m_Value.GetHashCode();
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x0008406C File Offset: 0x0008226C
		public override string ToString()
		{
			return this.DebugString<TextShadow>();
		}

		// Token: 0x04000FD3 RID: 4051
		private StyleKeyword m_Keyword;

		// Token: 0x04000FD4 RID: 4052
		private TextShadow m_Value;
	}
}
