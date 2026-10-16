using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200046A RID: 1130
	public struct StyleFloat : IStyleValue<float>, IEquatable<StyleFloat>
	{
		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x00082C28 File Offset: 0x00080E28
		// (set) Token: 0x0600234D RID: 9037 RVA: 0x00082C4F File Offset: 0x00080E4F
		public float value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : 0f;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x0600234E RID: 9038 RVA: 0x00082C60 File Offset: 0x00080E60
		// (set) Token: 0x0600234F RID: 9039 RVA: 0x00082C78 File Offset: 0x00080E78
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

		// Token: 0x06002350 RID: 9040 RVA: 0x00082C82 File Offset: 0x00080E82
		public StyleFloat(float v)
		{
			this = new StyleFloat(v, StyleKeyword.Undefined);
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x00082C8E File Offset: 0x00080E8E
		public StyleFloat(StyleKeyword keyword)
		{
			this = new StyleFloat(0f, keyword);
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x00082C9E File Offset: 0x00080E9E
		internal StyleFloat(float v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x00082CB0 File Offset: 0x00080EB0
		public static bool operator ==(StyleFloat lhs, StyleFloat rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x00082CE4 File Offset: 0x00080EE4
		public static bool operator !=(StyleFloat lhs, StyleFloat rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x00082D00 File Offset: 0x00080F00
		public static implicit operator StyleFloat(StyleKeyword keyword)
		{
			return new StyleFloat(keyword);
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x00082D18 File Offset: 0x00080F18
		public static implicit operator StyleFloat(float v)
		{
			return new StyleFloat(v);
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x00082D30 File Offset: 0x00080F30
		public bool Equals(StyleFloat other)
		{
			return other == this;
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x00082D50 File Offset: 0x00080F50
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleFloat)
			{
				StyleFloat styleFloat = (StyleFloat)obj;
				flag = this.Equals(styleFloat);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x00082D7C File Offset: 0x00080F7C
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x00082DA8 File Offset: 0x00080FA8
		public override string ToString()
		{
			return this.DebugString<float>();
		}

		// Token: 0x04000FC1 RID: 4033
		private float m_Value;

		// Token: 0x04000FC2 RID: 4034
		private StyleKeyword m_Keyword;
	}
}
