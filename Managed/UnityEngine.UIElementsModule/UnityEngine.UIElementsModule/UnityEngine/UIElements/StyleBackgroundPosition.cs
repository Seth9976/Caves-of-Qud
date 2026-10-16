using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000462 RID: 1122
	public struct StyleBackgroundPosition : IStyleValue<BackgroundPosition>, IEquatable<StyleBackgroundPosition>
	{
		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060022DB RID: 8923 RVA: 0x00081E90 File Offset: 0x00080090
		// (set) Token: 0x060022DC RID: 8924 RVA: 0x00081EBB File Offset: 0x000800BB
		public BackgroundPosition value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(BackgroundPosition);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x00081ECC File Offset: 0x000800CC
		// (set) Token: 0x060022DE RID: 8926 RVA: 0x00081EE4 File Offset: 0x000800E4
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

		// Token: 0x060022DF RID: 8927 RVA: 0x00081EEE File Offset: 0x000800EE
		public StyleBackgroundPosition(BackgroundPosition v)
		{
			this = new StyleBackgroundPosition(v, StyleKeyword.Undefined);
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x00081EFC File Offset: 0x000800FC
		public StyleBackgroundPosition(StyleKeyword keyword)
		{
			this = new StyleBackgroundPosition(default(BackgroundPosition), keyword);
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x00081F1B File Offset: 0x0008011B
		internal StyleBackgroundPosition(BackgroundPosition v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x00081F2C File Offset: 0x0008012C
		public static bool operator ==(StyleBackgroundPosition lhs, StyleBackgroundPosition rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x00081F60 File Offset: 0x00080160
		public static bool operator !=(StyleBackgroundPosition lhs, StyleBackgroundPosition rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x00081F7C File Offset: 0x0008017C
		public static implicit operator StyleBackgroundPosition(StyleKeyword keyword)
		{
			return new StyleBackgroundPosition(keyword);
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x00081F94 File Offset: 0x00080194
		public static implicit operator StyleBackgroundPosition(BackgroundPosition v)
		{
			return new StyleBackgroundPosition(v);
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x00081FAC File Offset: 0x000801AC
		public bool Equals(StyleBackgroundPosition other)
		{
			return other == this;
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x00081FCC File Offset: 0x000801CC
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleBackgroundPosition)
			{
				StyleBackgroundPosition styleBackgroundPosition = (StyleBackgroundPosition)obj;
				flag = this.Equals(styleBackgroundPosition);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x00081FF8 File Offset: 0x000801F8
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x0008202C File Offset: 0x0008022C
		public override string ToString()
		{
			return this.DebugString<BackgroundPosition>();
		}

		// Token: 0x04000FB0 RID: 4016
		private BackgroundPosition m_Value;

		// Token: 0x04000FB1 RID: 4017
		private StyleKeyword m_Keyword;
	}
}
