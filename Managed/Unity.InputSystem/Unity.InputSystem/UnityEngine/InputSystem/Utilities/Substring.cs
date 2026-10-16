using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200014D RID: 333
	internal struct Substring : IComparable<Substring>, IEquatable<Substring>
	{
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00056DB1 File Offset: 0x00054FB1
		public bool isEmpty
		{
			get
			{
				return this.m_Length == 0;
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00056DBC File Offset: 0x00054FBC
		public Substring(string str)
		{
			this.m_String = str;
			this.m_Index = 0;
			if (str != null)
			{
				this.m_Length = str.Length;
				return;
			}
			this.m_Length = 0;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00056DE3 File Offset: 0x00054FE3
		public Substring(string str, int index, int length)
		{
			this.m_String = str;
			this.m_Index = index;
			this.m_Length = length;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00056DFA File Offset: 0x00054FFA
		public Substring(string str, int index)
		{
			this.m_String = str;
			this.m_Index = index;
			this.m_Length = str.Length - index;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00056E18 File Offset: 0x00055018
		public override bool Equals(object obj)
		{
			if (obj is Substring)
			{
				Substring substring = (Substring)obj;
				return this.Equals(substring);
			}
			string text = obj as string;
			return text != null && this.Equals(text);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00056E50 File Offset: 0x00055050
		public bool Equals(string other)
		{
			if (string.IsNullOrEmpty(other))
			{
				return this.m_Length == 0;
			}
			if (other.Length != this.m_Length)
			{
				return false;
			}
			for (int i = 0; i < this.m_Length; i++)
			{
				if (other[i] != this.m_String[this.m_Index + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00056EAF File Offset: 0x000550AF
		public bool Equals(Substring other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00056EBB File Offset: 0x000550BB
		public bool Equals(InternedString other)
		{
			return this.length == other.length && string.Compare(this.m_String, this.m_Index, other.ToString(), 0, this.length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00056EF7 File Offset: 0x000550F7
		public int CompareTo(Substring other)
		{
			return Substring.Compare(this, other, StringComparison.CurrentCulture);
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00056F08 File Offset: 0x00055108
		public static int Compare(Substring left, Substring right, StringComparison comparison)
		{
			if (left.m_Length == right.m_Length)
			{
				return string.Compare(left.m_String, left.m_Index, right.m_String, right.m_Index, left.m_Length, comparison);
			}
			if (left.m_Length < right.m_Length)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00056F5C File Offset: 0x0005515C
		public bool StartsWith(string str)
		{
			if (str.Length > this.length)
			{
				return false;
			}
			for (int i = 0; i < str.Length; i++)
			{
				if (this.m_String[this.m_Index + i] != str[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00056FA9 File Offset: 0x000551A9
		public string Substr(int index = 0, int length = -1)
		{
			if (length < 0)
			{
				length = this.length - index;
			}
			return this.m_String.Substring(this.m_Index + index, length);
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00056FCD File Offset: 0x000551CD
		public override string ToString()
		{
			if (this.m_String == null)
			{
				return string.Empty;
			}
			return this.m_String.Substring(this.m_Index, this.m_Length);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00056FF4 File Offset: 0x000551F4
		public override int GetHashCode()
		{
			if (this.m_String == null)
			{
				return 0;
			}
			if (this.m_Index == 0 && this.m_Length == this.m_String.Length)
			{
				return this.m_String.GetHashCode();
			}
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00057043 File Offset: 0x00055243
		public static bool operator ==(Substring a, Substring b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0005704D File Offset: 0x0005524D
		public static bool operator !=(Substring a, Substring b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0005705A File Offset: 0x0005525A
		public static bool operator ==(Substring a, InternedString b)
		{
			return a.Equals(b);
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00057064 File Offset: 0x00055264
		public static bool operator !=(Substring a, InternedString b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00057071 File Offset: 0x00055271
		public static bool operator ==(InternedString a, Substring b)
		{
			return b.Equals(a);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0005707B File Offset: 0x0005527B
		public static bool operator !=(InternedString a, Substring b)
		{
			return !b.Equals(a);
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00057088 File Offset: 0x00055288
		public static implicit operator Substring(string s)
		{
			return new Substring(s);
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x00057090 File Offset: 0x00055290
		public int length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x00057098 File Offset: 0x00055298
		public int index
		{
			get
			{
				return this.m_Index;
			}
		}

		// Token: 0x170004F2 RID: 1266
		public char this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_Length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.m_String[this.m_Index + index];
			}
		}

		// Token: 0x04000748 RID: 1864
		private readonly string m_String;

		// Token: 0x04000749 RID: 1865
		private readonly int m_Index;

		// Token: 0x0400074A RID: 1866
		private readonly int m_Length;
	}
}
