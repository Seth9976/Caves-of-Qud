using System;
using System.Globalization;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000137 RID: 311
	public struct InternedString : IEquatable<InternedString>, IComparable<InternedString>
	{
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x00052D8A File Offset: 0x00050F8A
		public int length
		{
			get
			{
				string stringLowerCase = this.m_StringLowerCase;
				if (stringLowerCase == null)
				{
					return 0;
				}
				return stringLowerCase.Length;
			}
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00052D9D File Offset: 0x00050F9D
		public InternedString(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				this.m_StringOriginalCase = null;
				this.m_StringLowerCase = null;
				return;
			}
			this.m_StringOriginalCase = string.Intern(text);
			this.m_StringLowerCase = string.Intern(text.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00052DD8 File Offset: 0x00050FD8
		public bool IsEmpty()
		{
			return this.m_StringLowerCase == null;
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00052DE3 File Offset: 0x00050FE3
		public string ToLower()
		{
			return this.m_StringLowerCase;
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00052DEC File Offset: 0x00050FEC
		public override bool Equals(object obj)
		{
			if (obj is InternedString)
			{
				InternedString internedString = (InternedString)obj;
				return this.Equals(internedString);
			}
			string text = obj as string;
			if (text == null)
			{
				return false;
			}
			if (this.m_StringLowerCase == null)
			{
				return string.IsNullOrEmpty(text);
			}
			return string.Equals(this.m_StringLowerCase, text.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00052E41 File Offset: 0x00051041
		public bool Equals(InternedString other)
		{
			return this.m_StringLowerCase == other.m_StringLowerCase;
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00052E51 File Offset: 0x00051051
		public int CompareTo(InternedString other)
		{
			return string.Compare(this.m_StringLowerCase, other.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00052E65 File Offset: 0x00051065
		public override int GetHashCode()
		{
			if (this.m_StringLowerCase == null)
			{
				return 0;
			}
			return this.m_StringLowerCase.GetHashCode();
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00052E7C File Offset: 0x0005107C
		public override string ToString()
		{
			return this.m_StringOriginalCase ?? string.Empty;
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00052E8D File Offset: 0x0005108D
		public static bool operator ==(InternedString a, InternedString b)
		{
			return a.Equals(b);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00052E97 File Offset: 0x00051097
		public static bool operator !=(InternedString a, InternedString b)
		{
			return !a.Equals(b);
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00052EA4 File Offset: 0x000510A4
		public static bool operator ==(InternedString a, string b)
		{
			return string.Compare(a.m_StringLowerCase, b, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00052EB6 File Offset: 0x000510B6
		public static bool operator !=(InternedString a, string b)
		{
			return string.Compare(a.m_StringLowerCase, b, StringComparison.InvariantCultureIgnoreCase) != 0;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x00052EC8 File Offset: 0x000510C8
		public static bool operator ==(string a, InternedString b)
		{
			return string.Compare(a, b.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00052EDA File Offset: 0x000510DA
		public static bool operator !=(string a, InternedString b)
		{
			return string.Compare(a, b.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase) != 0;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00052EEC File Offset: 0x000510EC
		public static bool operator <(InternedString left, InternedString right)
		{
			return string.Compare(left.m_StringLowerCase, right.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase) < 0;
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00052F03 File Offset: 0x00051103
		public static bool operator >(InternedString left, InternedString right)
		{
			return string.Compare(left.m_StringLowerCase, right.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase) > 0;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00052F1A File Offset: 0x0005111A
		public static implicit operator string(InternedString str)
		{
			return str.ToString();
		}

		// Token: 0x0400071A RID: 1818
		private readonly string m_StringOriginalCase;

		// Token: 0x0400071B RID: 1819
		private readonly string m_StringLowerCase;
	}
}
