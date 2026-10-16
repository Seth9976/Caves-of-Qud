using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200046E RID: 1134
	public struct StyleLength : IStyleValue<Length>, IEquatable<StyleLength>
	{
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x0008335C File Offset: 0x0008155C
		// (set) Token: 0x06002390 RID: 9104 RVA: 0x000833A4 File Offset: 0x000815A4
		public Length value
		{
			get
			{
				bool flag = this.m_Keyword == StyleKeyword.Auto || this.m_Keyword == StyleKeyword.None || this.m_Keyword == StyleKeyword.Undefined;
				Length length;
				if (flag)
				{
					length = this.m_Value;
				}
				else
				{
					length = default(Length);
				}
				return length;
			}
			set
			{
				bool flag = value.IsAuto();
				if (flag)
				{
					this.m_Keyword = StyleKeyword.Auto;
				}
				else
				{
					bool flag2 = value.IsNone();
					if (flag2)
					{
						this.m_Keyword = StyleKeyword.None;
					}
					else
					{
						this.m_Keyword = StyleKeyword.Undefined;
					}
				}
				this.m_Value = value;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x000833E8 File Offset: 0x000815E8
		// (set) Token: 0x06002392 RID: 9106 RVA: 0x00083400 File Offset: 0x00081600
		public StyleKeyword keyword
		{
			get
			{
				return this.m_Keyword;
			}
			set
			{
				this.m_Keyword = value;
				bool flag = this.m_Keyword == StyleKeyword.Auto;
				if (flag)
				{
					this.m_Value = Length.Auto();
				}
				else
				{
					bool flag2 = this.m_Keyword == StyleKeyword.None;
					if (flag2)
					{
						this.m_Value = Length.None();
					}
					else
					{
						bool flag3 = this.m_Keyword > StyleKeyword.Undefined;
						if (flag3)
						{
							this.m_Value = default(Length);
						}
					}
				}
			}
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x00083468 File Offset: 0x00081668
		public StyleLength(float v)
		{
			this = new StyleLength(new Length(v, LengthUnit.Pixel), StyleKeyword.Undefined);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x0008347A File Offset: 0x0008167A
		public StyleLength(Length v)
		{
			this = new StyleLength(v, StyleKeyword.Undefined);
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x00083488 File Offset: 0x00081688
		public StyleLength(StyleKeyword keyword)
		{
			this = new StyleLength(default(Length), keyword);
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000834A8 File Offset: 0x000816A8
		internal StyleLength(Length v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
			bool flag = v.IsAuto();
			if (flag)
			{
				this.m_Keyword = StyleKeyword.Auto;
			}
			else
			{
				bool flag2 = v.IsNone();
				if (flag2)
				{
					this.m_Keyword = StyleKeyword.None;
				}
			}
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000834EC File Offset: 0x000816EC
		public static bool operator ==(StyleLength lhs, StyleLength rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x00083520 File Offset: 0x00081720
		public static bool operator !=(StyleLength lhs, StyleLength rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x0008353C File Offset: 0x0008173C
		public static implicit operator StyleLength(StyleKeyword keyword)
		{
			return new StyleLength(keyword);
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x00083554 File Offset: 0x00081754
		public static implicit operator StyleLength(float v)
		{
			return new StyleLength(v);
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x0008356C File Offset: 0x0008176C
		public static implicit operator StyleLength(Length v)
		{
			return new StyleLength(v);
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x00083584 File Offset: 0x00081784
		public bool Equals(StyleLength other)
		{
			return other == this;
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000835A4 File Offset: 0x000817A4
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleLength)
			{
				StyleLength styleLength = (StyleLength)obj;
				flag = this.Equals(styleLength);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000835D0 File Offset: 0x000817D0
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x00083604 File Offset: 0x00081804
		public override string ToString()
		{
			return this.DebugString<Length>();
		}

		// Token: 0x04000FC9 RID: 4041
		private Length m_Value;

		// Token: 0x04000FCA RID: 4042
		private StyleKeyword m_Keyword;
	}
}
