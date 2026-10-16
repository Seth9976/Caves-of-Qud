using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x0200046F RID: 1135
	public struct StyleList<T> : IStyleValue<List<T>>, IEquatable<StyleList<T>>
	{
		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060023A0 RID: 9120 RVA: 0x00083628 File Offset: 0x00081828
		// (set) Token: 0x060023A1 RID: 9121 RVA: 0x0008364B File Offset: 0x0008184B
		public List<T> value
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

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x0008365C File Offset: 0x0008185C
		// (set) Token: 0x060023A3 RID: 9123 RVA: 0x00083674 File Offset: 0x00081874
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

		// Token: 0x060023A4 RID: 9124 RVA: 0x0008367E File Offset: 0x0008187E
		public StyleList(List<T> v)
		{
			this = new StyleList<T>(v, StyleKeyword.Undefined);
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x0008368A File Offset: 0x0008188A
		public StyleList(StyleKeyword keyword)
		{
			this = new StyleList<T>(null, keyword);
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x00083696 File Offset: 0x00081896
		internal StyleList(List<T> v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x000836A8 File Offset: 0x000818A8
		public static bool operator ==(StyleList<T> lhs, StyleList<T> rhs)
		{
			bool flag = lhs.m_Keyword != rhs.m_Keyword;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				List<T> value = lhs.m_Value;
				List<T> value2 = rhs.m_Value;
				bool flag3 = value == value2;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = value == null || value2 == null;
					flag2 = !flag4 && value.Count == value2.Count && value.SequenceEqual<T>(value2);
				}
			}
			return flag2;
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x0008371C File Offset: 0x0008191C
		public static bool operator !=(StyleList<T> lhs, StyleList<T> rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x00083738 File Offset: 0x00081938
		public static implicit operator StyleList<T>(StyleKeyword keyword)
		{
			return new StyleList<T>(keyword);
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x00083750 File Offset: 0x00081950
		public static implicit operator StyleList<T>(List<T> v)
		{
			return new StyleList<T>(v);
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x00083768 File Offset: 0x00081968
		public bool Equals(StyleList<T> other)
		{
			return other == this;
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x00083788 File Offset: 0x00081988
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleList<T>)
			{
				StyleList<T> styleList = (StyleList<T>)obj;
				flag = this.Equals(styleList);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000837B4 File Offset: 0x000819B4
		public override int GetHashCode()
		{
			int num = 0;
			bool flag = this.m_Value != null && this.m_Value.Count > 0;
			if (flag)
			{
				num = EqualityComparer<T>.Default.GetHashCode(this.m_Value[0]);
				for (int i = 1; i < this.m_Value.Count; i++)
				{
					num = (num * 397) ^ EqualityComparer<T>.Default.GetHashCode(this.m_Value[i]);
				}
			}
			return (num * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x0008384C File Offset: 0x00081A4C
		public override string ToString()
		{
			return this.DebugString<List<T>>();
		}

		// Token: 0x04000FCB RID: 4043
		private StyleKeyword m_Keyword;

		// Token: 0x04000FCC RID: 4044
		private List<T> m_Value;
	}
}
