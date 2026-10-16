using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000470 RID: 1136
	public struct StyleRotate : IStyleValue<Rotate>, IEquatable<StyleRotate>
	{
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x00083870 File Offset: 0x00081A70
		// (set) Token: 0x060023B0 RID: 9136 RVA: 0x000838D5 File Offset: 0x00081AD5
		public Rotate value
		{
			get
			{
				StyleKeyword keyword = this.m_Keyword;
				if (!true)
				{
				}
				Rotate rotate;
				switch (keyword)
				{
				case StyleKeyword.Undefined:
					rotate = this.m_Value;
					goto IL_004F;
				case StyleKeyword.Null:
					rotate = Rotate.None();
					goto IL_004F;
				case StyleKeyword.None:
					rotate = Rotate.None();
					goto IL_004F;
				case StyleKeyword.Initial:
					rotate = Rotate.Initial();
					goto IL_004F;
				}
				throw new NotImplementedException();
				IL_004F:
				if (!true)
				{
				}
				return rotate;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x000838E8 File Offset: 0x00081AE8
		// (set) Token: 0x060023B2 RID: 9138 RVA: 0x00083900 File Offset: 0x00081B00
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

		// Token: 0x060023B3 RID: 9139 RVA: 0x0008390A File Offset: 0x00081B0A
		public StyleRotate(Rotate v)
		{
			this = new StyleRotate(v, StyleKeyword.Undefined);
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x00083918 File Offset: 0x00081B18
		public StyleRotate(StyleKeyword keyword)
		{
			this = new StyleRotate(default(Rotate), keyword);
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x00083937 File Offset: 0x00081B37
		internal StyleRotate(Rotate v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x00083948 File Offset: 0x00081B48
		public static bool operator ==(StyleRotate lhs, StyleRotate rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x0008397C File Offset: 0x00081B7C
		public static bool operator !=(StyleRotate lhs, StyleRotate rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x00083998 File Offset: 0x00081B98
		public static implicit operator StyleRotate(StyleKeyword keyword)
		{
			return new StyleRotate(keyword);
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x000839B0 File Offset: 0x00081BB0
		public static implicit operator StyleRotate(Rotate v)
		{
			return new StyleRotate(v);
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x000839C8 File Offset: 0x00081BC8
		public bool Equals(StyleRotate other)
		{
			return other == this;
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x000839E8 File Offset: 0x00081BE8
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleRotate)
			{
				StyleRotate styleRotate = (StyleRotate)obj;
				flag = this.Equals(styleRotate);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x00083A14 File Offset: 0x00081C14
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x00083A48 File Offset: 0x00081C48
		public override string ToString()
		{
			return this.DebugString<Rotate>();
		}

		// Token: 0x04000FCD RID: 4045
		private Rotate m_Value;

		// Token: 0x04000FCE RID: 4046
		private StyleKeyword m_Keyword;
	}
}
