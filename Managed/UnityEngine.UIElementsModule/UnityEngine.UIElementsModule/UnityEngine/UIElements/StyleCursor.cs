using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000466 RID: 1126
	public struct StyleCursor : IStyleValue<Cursor>, IEquatable<StyleCursor>
	{
		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x000825BC File Offset: 0x000807BC
		// (set) Token: 0x0600231A RID: 8986 RVA: 0x000825E7 File Offset: 0x000807E7
		public Cursor value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Cursor);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x000825F8 File Offset: 0x000807F8
		// (set) Token: 0x0600231C RID: 8988 RVA: 0x00082610 File Offset: 0x00080810
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

		// Token: 0x0600231D RID: 8989 RVA: 0x0008261A File Offset: 0x0008081A
		public StyleCursor(Cursor v)
		{
			this = new StyleCursor(v, StyleKeyword.Undefined);
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x00082628 File Offset: 0x00080828
		public StyleCursor(StyleKeyword keyword)
		{
			this = new StyleCursor(default(Cursor), keyword);
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x00082647 File Offset: 0x00080847
		internal StyleCursor(Cursor v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x00082658 File Offset: 0x00080858
		public static bool operator ==(StyleCursor lhs, StyleCursor rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x0008268C File Offset: 0x0008088C
		public static bool operator !=(StyleCursor lhs, StyleCursor rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x000826A8 File Offset: 0x000808A8
		public static implicit operator StyleCursor(StyleKeyword keyword)
		{
			return new StyleCursor(keyword);
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000826C0 File Offset: 0x000808C0
		public static implicit operator StyleCursor(Cursor v)
		{
			return new StyleCursor(v);
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x000826D8 File Offset: 0x000808D8
		public bool Equals(StyleCursor other)
		{
			return other == this;
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x000826F8 File Offset: 0x000808F8
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleCursor)
			{
				StyleCursor styleCursor = (StyleCursor)obj;
				flag = this.Equals(styleCursor);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x00082724 File Offset: 0x00080924
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x00082758 File Offset: 0x00080958
		public override string ToString()
		{
			return this.DebugString<Cursor>();
		}

		// Token: 0x04000FB8 RID: 4024
		private Cursor m_Value;

		// Token: 0x04000FB9 RID: 4025
		private StyleKeyword m_Keyword;
	}
}
