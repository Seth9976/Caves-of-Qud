using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200046B RID: 1131
	public struct StyleFont : IStyleValue<Font>, IEquatable<StyleFont>
	{
		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x00082DCC File Offset: 0x00080FCC
		// (set) Token: 0x0600235C RID: 9052 RVA: 0x00082DEF File Offset: 0x00080FEF
		public Font value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : null;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x0600235D RID: 9053 RVA: 0x00082E00 File Offset: 0x00081000
		// (set) Token: 0x0600235E RID: 9054 RVA: 0x00082E18 File Offset: 0x00081018
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

		// Token: 0x0600235F RID: 9055 RVA: 0x00082E22 File Offset: 0x00081022
		public StyleFont(Font v)
		{
			this = new StyleFont(v, StyleKeyword.Undefined);
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x00082E2E File Offset: 0x0008102E
		public StyleFont(StyleKeyword keyword)
		{
			this = new StyleFont(null, keyword);
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x00082E3A File Offset: 0x0008103A
		internal StyleFont(Font v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x00082E4C File Offset: 0x0008104C
		public static bool operator ==(StyleFont lhs, StyleFont rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x00082E80 File Offset: 0x00081080
		public static bool operator !=(StyleFont lhs, StyleFont rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x00082E9C File Offset: 0x0008109C
		public static implicit operator StyleFont(StyleKeyword keyword)
		{
			return new StyleFont(keyword);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x00082EB4 File Offset: 0x000810B4
		public static implicit operator StyleFont(Font v)
		{
			return new StyleFont(v);
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x00082ECC File Offset: 0x000810CC
		public bool Equals(StyleFont other)
		{
			return other == this;
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x00082EEC File Offset: 0x000810EC
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleFont)
			{
				StyleFont styleFont = (StyleFont)obj;
				flag = this.Equals(styleFont);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x00082F18 File Offset: 0x00081118
		public override int GetHashCode()
		{
			return (((this.m_Value != null) ? this.m_Value.GetHashCode() : 0) * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x00082F54 File Offset: 0x00081154
		public override string ToString()
		{
			return this.DebugString<Font>();
		}

		// Token: 0x04000FC3 RID: 4035
		private Font m_Value;

		// Token: 0x04000FC4 RID: 4036
		private StyleKeyword m_Keyword;
	}
}
