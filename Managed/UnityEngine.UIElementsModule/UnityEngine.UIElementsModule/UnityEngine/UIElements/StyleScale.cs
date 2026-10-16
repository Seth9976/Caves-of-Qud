using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000471 RID: 1137
	public struct StyleScale : IStyleValue<Scale>, IEquatable<StyleScale>
	{
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x00083A6C File Offset: 0x00081C6C
		// (set) Token: 0x060023BF RID: 9151 RVA: 0x00083AD1 File Offset: 0x00081CD1
		public Scale value
		{
			get
			{
				StyleKeyword keyword = this.m_Keyword;
				if (!true)
				{
				}
				Scale scale;
				switch (keyword)
				{
				case StyleKeyword.Undefined:
					scale = this.m_Value;
					goto IL_004F;
				case StyleKeyword.Null:
					scale = Scale.None();
					goto IL_004F;
				case StyleKeyword.None:
					scale = Scale.None();
					goto IL_004F;
				case StyleKeyword.Initial:
					scale = Scale.Initial();
					goto IL_004F;
				}
				throw new NotImplementedException();
				IL_004F:
				if (!true)
				{
				}
				return scale;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060023C0 RID: 9152 RVA: 0x00083AE4 File Offset: 0x00081CE4
		// (set) Token: 0x060023C1 RID: 9153 RVA: 0x00083AFC File Offset: 0x00081CFC
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

		// Token: 0x060023C2 RID: 9154 RVA: 0x00083B06 File Offset: 0x00081D06
		public StyleScale(Scale v)
		{
			this = new StyleScale(v, StyleKeyword.Undefined);
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x00083B14 File Offset: 0x00081D14
		public StyleScale(StyleKeyword keyword)
		{
			this = new StyleScale(default(Scale), keyword);
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x00083B33 File Offset: 0x00081D33
		public StyleScale(Vector2 scale)
		{
			this = new StyleScale(new Scale(scale));
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x00083B43 File Offset: 0x00081D43
		internal StyleScale(Vector3 scale)
		{
			this = new StyleScale(new Scale(scale));
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x00083B53 File Offset: 0x00081D53
		internal StyleScale(Scale v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x00083B64 File Offset: 0x00081D64
		public static implicit operator StyleScale(Vector2 scale)
		{
			return new Scale(scale);
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x00083B84 File Offset: 0x00081D84
		public static bool operator ==(StyleScale lhs, StyleScale rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x00083BB8 File Offset: 0x00081DB8
		public static bool operator !=(StyleScale lhs, StyleScale rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x00083BD4 File Offset: 0x00081DD4
		public static implicit operator StyleScale(StyleKeyword keyword)
		{
			return new StyleScale(keyword);
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x00083BEC File Offset: 0x00081DEC
		public static implicit operator StyleScale(Scale v)
		{
			return new StyleScale(v);
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x00083C04 File Offset: 0x00081E04
		public bool Equals(StyleScale other)
		{
			return other == this;
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x00083C24 File Offset: 0x00081E24
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleScale)
			{
				StyleScale styleScale = (StyleScale)obj;
				flag = this.Equals(styleScale);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x00083C50 File Offset: 0x00081E50
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x00083C84 File Offset: 0x00081E84
		public override string ToString()
		{
			return this.DebugString<Scale>();
		}

		// Token: 0x04000FCF RID: 4047
		private Scale m_Value;

		// Token: 0x04000FD0 RID: 4048
		private StyleKeyword m_Keyword;
	}
}
