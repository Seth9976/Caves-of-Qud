using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000474 RID: 1140
	public struct StyleTransformOrigin : IStyleValue<TransformOrigin>, IEquatable<StyleTransformOrigin>
	{
		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x00084090 File Offset: 0x00082290
		// (set) Token: 0x060023EF RID: 9199 RVA: 0x000840F5 File Offset: 0x000822F5
		public TransformOrigin value
		{
			get
			{
				StyleKeyword keyword = this.m_Keyword;
				if (!true)
				{
				}
				TransformOrigin transformOrigin;
				switch (keyword)
				{
				case StyleKeyword.Undefined:
					transformOrigin = this.m_Value;
					goto IL_004F;
				case StyleKeyword.Null:
					transformOrigin = TransformOrigin.Initial();
					goto IL_004F;
				case StyleKeyword.None:
					transformOrigin = TransformOrigin.Initial();
					goto IL_004F;
				case StyleKeyword.Initial:
					transformOrigin = TransformOrigin.Initial();
					goto IL_004F;
				}
				throw new NotImplementedException();
				IL_004F:
				if (!true)
				{
				}
				return transformOrigin;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x00084108 File Offset: 0x00082308
		// (set) Token: 0x060023F1 RID: 9201 RVA: 0x00084120 File Offset: 0x00082320
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

		// Token: 0x060023F2 RID: 9202 RVA: 0x0008412A File Offset: 0x0008232A
		public StyleTransformOrigin(TransformOrigin v)
		{
			this = new StyleTransformOrigin(v, StyleKeyword.Undefined);
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x00084138 File Offset: 0x00082338
		public StyleTransformOrigin(StyleKeyword keyword)
		{
			this = new StyleTransformOrigin(default(TransformOrigin), keyword);
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x00084157 File Offset: 0x00082357
		internal StyleTransformOrigin(TransformOrigin v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x00084168 File Offset: 0x00082368
		public static bool operator ==(StyleTransformOrigin lhs, StyleTransformOrigin rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x0008419C File Offset: 0x0008239C
		public static bool operator !=(StyleTransformOrigin lhs, StyleTransformOrigin rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000841B8 File Offset: 0x000823B8
		public static implicit operator StyleTransformOrigin(StyleKeyword keyword)
		{
			return new StyleTransformOrigin(keyword);
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x000841D0 File Offset: 0x000823D0
		public static implicit operator StyleTransformOrigin(TransformOrigin v)
		{
			return new StyleTransformOrigin(v);
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x000841E8 File Offset: 0x000823E8
		public bool Equals(StyleTransformOrigin other)
		{
			return other == this;
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x00084208 File Offset: 0x00082408
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleTransformOrigin)
			{
				StyleTransformOrigin styleTransformOrigin = (StyleTransformOrigin)obj;
				flag = this.Equals(styleTransformOrigin);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x00084234 File Offset: 0x00082434
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x00084268 File Offset: 0x00082468
		public override string ToString()
		{
			return this.DebugString<TransformOrigin>();
		}

		// Token: 0x04000FD5 RID: 4053
		private TransformOrigin m_Value;

		// Token: 0x04000FD6 RID: 4054
		private StyleKeyword m_Keyword;
	}
}
