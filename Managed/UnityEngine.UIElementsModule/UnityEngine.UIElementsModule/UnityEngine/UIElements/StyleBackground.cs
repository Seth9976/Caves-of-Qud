using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000461 RID: 1121
	public struct StyleBackground : IStyleValue<Background>, IEquatable<StyleBackground>
	{
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060022C5 RID: 8901 RVA: 0x00081C60 File Offset: 0x0007FE60
		// (set) Token: 0x060022C6 RID: 8902 RVA: 0x00081C8B File Offset: 0x0007FE8B
		public Background value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Background);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x00081C9C File Offset: 0x0007FE9C
		// (set) Token: 0x060022C8 RID: 8904 RVA: 0x00081CB4 File Offset: 0x0007FEB4
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

		// Token: 0x060022C9 RID: 8905 RVA: 0x00081CBE File Offset: 0x0007FEBE
		public StyleBackground(Background v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x00081CCA File Offset: 0x0007FECA
		public StyleBackground(Texture2D v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x00081CD6 File Offset: 0x0007FED6
		public StyleBackground(Sprite v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x00081CE2 File Offset: 0x0007FEE2
		public StyleBackground(VectorImage v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x00081CF0 File Offset: 0x0007FEF0
		public StyleBackground(StyleKeyword keyword)
		{
			this = new StyleBackground(default(Background), keyword);
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x00081D0F File Offset: 0x0007FF0F
		internal StyleBackground(Texture2D v, StyleKeyword keyword)
		{
			this = new StyleBackground(Background.FromTexture2D(v), keyword);
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x00081D20 File Offset: 0x0007FF20
		internal StyleBackground(Sprite v, StyleKeyword keyword)
		{
			this = new StyleBackground(Background.FromSprite(v), keyword);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x00081D31 File Offset: 0x0007FF31
		internal StyleBackground(VectorImage v, StyleKeyword keyword)
		{
			this = new StyleBackground(Background.FromVectorImage(v), keyword);
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x00081D42 File Offset: 0x0007FF42
		internal StyleBackground(Background v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x00081D54 File Offset: 0x0007FF54
		public static bool operator ==(StyleBackground lhs, StyleBackground rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x00081D88 File Offset: 0x0007FF88
		public static bool operator !=(StyleBackground lhs, StyleBackground rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x00081DA4 File Offset: 0x0007FFA4
		public static implicit operator StyleBackground(StyleKeyword keyword)
		{
			return new StyleBackground(keyword);
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x00081DBC File Offset: 0x0007FFBC
		public static implicit operator StyleBackground(Background v)
		{
			return new StyleBackground(v);
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x00081DD4 File Offset: 0x0007FFD4
		public static implicit operator StyleBackground(Texture2D v)
		{
			return new StyleBackground(v);
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x00081DEC File Offset: 0x0007FFEC
		public bool Equals(StyleBackground other)
		{
			return other == this;
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x00081E0C File Offset: 0x0008000C
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleBackground)
			{
				StyleBackground styleBackground = (StyleBackground)obj;
				flag = this.Equals(styleBackground);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x00081E38 File Offset: 0x00080038
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x00081E6C File Offset: 0x0008006C
		public override string ToString()
		{
			return this.DebugString<Background>();
		}

		// Token: 0x04000FAE RID: 4014
		private Background m_Value;

		// Token: 0x04000FAF RID: 4015
		private StyleKeyword m_Keyword;
	}
}
