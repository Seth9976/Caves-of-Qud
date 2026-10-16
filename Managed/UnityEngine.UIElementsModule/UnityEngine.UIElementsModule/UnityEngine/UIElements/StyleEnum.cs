using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements
{
	// Token: 0x02000469 RID: 1129
	public struct StyleEnum<T> : IStyleValue<T>, IEquatable<StyleEnum<T>> where T : struct, IConvertible
	{
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x00082A70 File Offset: 0x00080C70
		// (set) Token: 0x0600233E RID: 9022 RVA: 0x00082A9B File Offset: 0x00080C9B
		public T value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(T);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x0600233F RID: 9023 RVA: 0x00082AAC File Offset: 0x00080CAC
		// (set) Token: 0x06002340 RID: 9024 RVA: 0x00082AC4 File Offset: 0x00080CC4
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

		// Token: 0x06002341 RID: 9025 RVA: 0x00082ACE File Offset: 0x00080CCE
		public StyleEnum(T v)
		{
			this = new StyleEnum<T>(v, StyleKeyword.Undefined);
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x00082ADC File Offset: 0x00080CDC
		public StyleEnum(StyleKeyword keyword)
		{
			this = new StyleEnum<T>(default(T), keyword);
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x00082AFB File Offset: 0x00080CFB
		internal StyleEnum(T v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x00082B0C File Offset: 0x00080D0C
		public static bool operator ==(StyleEnum<T> lhs, StyleEnum<T> rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && UnsafeUtility.EnumEquals<T>(lhs.m_Value, rhs.m_Value);
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x00082B40 File Offset: 0x00080D40
		public static bool operator !=(StyleEnum<T> lhs, StyleEnum<T> rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x00082B5C File Offset: 0x00080D5C
		public static implicit operator StyleEnum<T>(StyleKeyword keyword)
		{
			return new StyleEnum<T>(keyword);
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x00082B74 File Offset: 0x00080D74
		public static implicit operator StyleEnum<T>(T v)
		{
			return new StyleEnum<T>(v);
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x00082B8C File Offset: 0x00080D8C
		public bool Equals(StyleEnum<T> other)
		{
			return other == this;
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x00082BAC File Offset: 0x00080DAC
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleEnum<T>)
			{
				StyleEnum<T> styleEnum = (StyleEnum<T>)obj;
				flag = this.Equals(styleEnum);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x00082BD8 File Offset: 0x00080DD8
		public override int GetHashCode()
		{
			return (UnsafeUtility.EnumToInt<T>(this.m_Value) * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x00082C04 File Offset: 0x00080E04
		public override string ToString()
		{
			return this.DebugString<T>();
		}

		// Token: 0x04000FBF RID: 4031
		private T m_Value;

		// Token: 0x04000FC0 RID: 4032
		private StyleKeyword m_Keyword;
	}
}
