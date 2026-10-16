using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200046D RID: 1133
	public struct StyleInt : IStyleValue<int>, IEquatable<StyleInt>
	{
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x000831C4 File Offset: 0x000813C4
		// (set) Token: 0x06002381 RID: 9089 RVA: 0x000831E7 File Offset: 0x000813E7
		public int value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : 0;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002382 RID: 9090 RVA: 0x000831F8 File Offset: 0x000813F8
		// (set) Token: 0x06002383 RID: 9091 RVA: 0x00083210 File Offset: 0x00081410
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

		// Token: 0x06002384 RID: 9092 RVA: 0x0008321A File Offset: 0x0008141A
		public StyleInt(int v)
		{
			this = new StyleInt(v, StyleKeyword.Undefined);
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x00083226 File Offset: 0x00081426
		public StyleInt(StyleKeyword keyword)
		{
			this = new StyleInt(0, keyword);
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x00083232 File Offset: 0x00081432
		internal StyleInt(int v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x00083244 File Offset: 0x00081444
		public static bool operator ==(StyleInt lhs, StyleInt rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x00083278 File Offset: 0x00081478
		public static bool operator !=(StyleInt lhs, StyleInt rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x00083294 File Offset: 0x00081494
		public static implicit operator StyleInt(StyleKeyword keyword)
		{
			return new StyleInt(keyword);
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000832AC File Offset: 0x000814AC
		public static implicit operator StyleInt(int v)
		{
			return new StyleInt(v);
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x000832C4 File Offset: 0x000814C4
		public bool Equals(StyleInt other)
		{
			return other == this;
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x000832E4 File Offset: 0x000814E4
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleInt)
			{
				StyleInt styleInt = (StyleInt)obj;
				flag = this.Equals(styleInt);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x00083310 File Offset: 0x00081510
		public override int GetHashCode()
		{
			return (this.m_Value * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x00083338 File Offset: 0x00081538
		public override string ToString()
		{
			return this.DebugString<int>();
		}

		// Token: 0x04000FC7 RID: 4039
		private int m_Value;

		// Token: 0x04000FC8 RID: 4040
		private StyleKeyword m_Keyword;
	}
}
