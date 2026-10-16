using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000472 RID: 1138
	public struct StyleTranslate : IStyleValue<Translate>, IEquatable<StyleTranslate>
	{
		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x00083CA8 File Offset: 0x00081EA8
		// (set) Token: 0x060023D1 RID: 9169 RVA: 0x00083D0D File Offset: 0x00081F0D
		public Translate value
		{
			get
			{
				StyleKeyword keyword = this.m_Keyword;
				if (!true)
				{
				}
				Translate translate;
				switch (keyword)
				{
				case StyleKeyword.Undefined:
					translate = this.m_Value;
					goto IL_004F;
				case StyleKeyword.Null:
					translate = Translate.None();
					goto IL_004F;
				case StyleKeyword.None:
					translate = Translate.None();
					goto IL_004F;
				case StyleKeyword.Initial:
					translate = Translate.None();
					goto IL_004F;
				}
				throw new NotImplementedException();
				IL_004F:
				if (!true)
				{
				}
				return translate;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060023D2 RID: 9170 RVA: 0x00083D20 File Offset: 0x00081F20
		// (set) Token: 0x060023D3 RID: 9171 RVA: 0x00083D38 File Offset: 0x00081F38
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

		// Token: 0x060023D4 RID: 9172 RVA: 0x00083D42 File Offset: 0x00081F42
		public StyleTranslate(Translate v)
		{
			this = new StyleTranslate(v, StyleKeyword.Undefined);
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x00083D50 File Offset: 0x00081F50
		public StyleTranslate(StyleKeyword keyword)
		{
			this = new StyleTranslate(default(Translate), keyword);
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x00083D6F File Offset: 0x00081F6F
		internal StyleTranslate(Translate v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x00083D80 File Offset: 0x00081F80
		public static bool operator ==(StyleTranslate lhs, StyleTranslate rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x00083DB4 File Offset: 0x00081FB4
		public static bool operator !=(StyleTranslate lhs, StyleTranslate rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x00083DD0 File Offset: 0x00081FD0
		public static implicit operator StyleTranslate(StyleKeyword keyword)
		{
			return new StyleTranslate(keyword);
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x00083DE8 File Offset: 0x00081FE8
		public static implicit operator StyleTranslate(Translate v)
		{
			return new StyleTranslate(v);
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x00083E00 File Offset: 0x00082000
		public bool Equals(StyleTranslate other)
		{
			return other == this;
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x00083E20 File Offset: 0x00082020
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleTranslate)
			{
				StyleTranslate styleTranslate = (StyleTranslate)obj;
				flag = this.Equals(styleTranslate);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x00083E4C File Offset: 0x0008204C
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x00083E80 File Offset: 0x00082080
		public override string ToString()
		{
			return this.DebugString<Translate>();
		}

		// Token: 0x04000FD1 RID: 4049
		private Translate m_Value;

		// Token: 0x04000FD2 RID: 4050
		private StyleKeyword m_Keyword;
	}
}
