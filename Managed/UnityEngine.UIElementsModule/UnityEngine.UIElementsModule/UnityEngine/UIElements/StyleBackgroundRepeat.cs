using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000463 RID: 1123
	public struct StyleBackgroundRepeat : IStyleValue<BackgroundRepeat>, IEquatable<StyleBackgroundRepeat>
	{
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x00082050 File Offset: 0x00080250
		// (set) Token: 0x060022EB RID: 8939 RVA: 0x0008207B File Offset: 0x0008027B
		public BackgroundRepeat value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(BackgroundRepeat);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x0008208C File Offset: 0x0008028C
		// (set) Token: 0x060022ED RID: 8941 RVA: 0x000820A4 File Offset: 0x000802A4
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

		// Token: 0x060022EE RID: 8942 RVA: 0x000820AE File Offset: 0x000802AE
		public StyleBackgroundRepeat(BackgroundRepeat v)
		{
			this = new StyleBackgroundRepeat(v, StyleKeyword.Undefined);
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x000820BC File Offset: 0x000802BC
		public StyleBackgroundRepeat(StyleKeyword keyword)
		{
			this = new StyleBackgroundRepeat(default(BackgroundRepeat), keyword);
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x000820DB File Offset: 0x000802DB
		internal StyleBackgroundRepeat(BackgroundRepeat v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000820EC File Offset: 0x000802EC
		public static bool operator ==(StyleBackgroundRepeat lhs, StyleBackgroundRepeat rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x00082120 File Offset: 0x00080320
		public static bool operator !=(StyleBackgroundRepeat lhs, StyleBackgroundRepeat rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x0008213C File Offset: 0x0008033C
		public static implicit operator StyleBackgroundRepeat(StyleKeyword keyword)
		{
			return new StyleBackgroundRepeat(keyword);
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x00082154 File Offset: 0x00080354
		public static implicit operator StyleBackgroundRepeat(BackgroundRepeat v)
		{
			return new StyleBackgroundRepeat(v);
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x0008216C File Offset: 0x0008036C
		public bool Equals(StyleBackgroundRepeat other)
		{
			return other == this;
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x0008218C File Offset: 0x0008038C
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleBackgroundRepeat)
			{
				StyleBackgroundRepeat styleBackgroundRepeat = (StyleBackgroundRepeat)obj;
				flag = this.Equals(styleBackgroundRepeat);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x000821B8 File Offset: 0x000803B8
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x000821EC File Offset: 0x000803EC
		public override string ToString()
		{
			return this.DebugString<BackgroundRepeat>();
		}

		// Token: 0x04000FB2 RID: 4018
		private BackgroundRepeat m_Value;

		// Token: 0x04000FB3 RID: 4019
		private StyleKeyword m_Keyword;
	}
}
