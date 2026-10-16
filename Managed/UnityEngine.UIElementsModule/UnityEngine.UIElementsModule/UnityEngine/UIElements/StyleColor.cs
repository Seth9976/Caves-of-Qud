using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000465 RID: 1125
	public struct StyleColor : IStyleValue<Color>, IEquatable<StyleColor>
	{
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x000823D0 File Offset: 0x000805D0
		// (set) Token: 0x06002309 RID: 8969 RVA: 0x000823F7 File Offset: 0x000805F7
		public Color value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : Color.clear;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x00082408 File Offset: 0x00080608
		// (set) Token: 0x0600230B RID: 8971 RVA: 0x00082420 File Offset: 0x00080620
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

		// Token: 0x0600230C RID: 8972 RVA: 0x0008242A File Offset: 0x0008062A
		public StyleColor(Color v)
		{
			this = new StyleColor(v, StyleKeyword.Undefined);
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x00082436 File Offset: 0x00080636
		public StyleColor(StyleKeyword keyword)
		{
			this = new StyleColor(Color.clear, keyword);
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x00082446 File Offset: 0x00080646
		internal StyleColor(Color v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x00082458 File Offset: 0x00080658
		public static bool operator ==(StyleColor lhs, StyleColor rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x0008248C File Offset: 0x0008068C
		public static bool operator !=(StyleColor lhs, StyleColor rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000824A8 File Offset: 0x000806A8
		public static bool operator ==(StyleColor lhs, Color rhs)
		{
			StyleColor styleColor = new StyleColor(rhs);
			return lhs == styleColor;
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000824CC File Offset: 0x000806CC
		public static bool operator !=(StyleColor lhs, Color rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000824E8 File Offset: 0x000806E8
		public static implicit operator StyleColor(StyleKeyword keyword)
		{
			return new StyleColor(keyword);
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x00082500 File Offset: 0x00080700
		public static implicit operator StyleColor(Color v)
		{
			return new StyleColor(v);
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x00082518 File Offset: 0x00080718
		public bool Equals(StyleColor other)
		{
			return other == this;
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x00082538 File Offset: 0x00080738
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleColor)
			{
				StyleColor styleColor = (StyleColor)obj;
				flag = this.Equals(styleColor);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x00082564 File Offset: 0x00080764
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x00082598 File Offset: 0x00080798
		public override string ToString()
		{
			return this.DebugString<Color>();
		}

		// Token: 0x04000FB6 RID: 4022
		private Color m_Value;

		// Token: 0x04000FB7 RID: 4023
		private StyleKeyword m_Keyword;
	}
}
