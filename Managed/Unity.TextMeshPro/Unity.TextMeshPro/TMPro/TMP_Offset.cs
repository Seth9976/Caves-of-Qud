using System;

namespace TMPro
{
	// Token: 0x02000017 RID: 23
	public struct TMP_Offset
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002AC1 File Offset: 0x00000CC1
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002AC9 File Offset: 0x00000CC9
		public float left
		{
			get
			{
				return this.m_Left;
			}
			set
			{
				this.m_Left = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002AD2 File Offset: 0x00000CD2
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002ADA File Offset: 0x00000CDA
		public float right
		{
			get
			{
				return this.m_Right;
			}
			set
			{
				this.m_Right = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002AE3 File Offset: 0x00000CE3
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002AEB File Offset: 0x00000CEB
		public float top
		{
			get
			{
				return this.m_Top;
			}
			set
			{
				this.m_Top = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002AF4 File Offset: 0x00000CF4
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002AFC File Offset: 0x00000CFC
		public float bottom
		{
			get
			{
				return this.m_Bottom;
			}
			set
			{
				this.m_Bottom = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002B05 File Offset: 0x00000D05
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002B0D File Offset: 0x00000D0D
		public float horizontal
		{
			get
			{
				return this.m_Left;
			}
			set
			{
				this.m_Left = value;
				this.m_Right = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002B1D File Offset: 0x00000D1D
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002B25 File Offset: 0x00000D25
		public float vertical
		{
			get
			{
				return this.m_Top;
			}
			set
			{
				this.m_Top = value;
				this.m_Bottom = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002B35 File Offset: 0x00000D35
		public static TMP_Offset zero
		{
			get
			{
				return TMP_Offset.k_ZeroOffset;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002B3C File Offset: 0x00000D3C
		public TMP_Offset(float left, float right, float top, float bottom)
		{
			this.m_Left = left;
			this.m_Right = right;
			this.m_Top = top;
			this.m_Bottom = bottom;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002B5B File Offset: 0x00000D5B
		public TMP_Offset(float horizontal, float vertical)
		{
			this.m_Left = horizontal;
			this.m_Right = horizontal;
			this.m_Top = vertical;
			this.m_Bottom = vertical;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002B79 File Offset: 0x00000D79
		public static bool operator ==(TMP_Offset lhs, TMP_Offset rhs)
		{
			return lhs.m_Left == rhs.m_Left && lhs.m_Right == rhs.m_Right && lhs.m_Top == rhs.m_Top && lhs.m_Bottom == rhs.m_Bottom;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002BB5 File Offset: 0x00000DB5
		public static bool operator !=(TMP_Offset lhs, TMP_Offset rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002BC1 File Offset: 0x00000DC1
		public static TMP_Offset operator *(TMP_Offset a, float b)
		{
			return new TMP_Offset(a.m_Left * b, a.m_Right * b, a.m_Top * b, a.m_Bottom * b);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002BFA File Offset: 0x00000DFA
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002C0D File Offset: 0x00000E0D
		public bool Equals(TMP_Offset other)
		{
			return base.Equals(other);
		}

		// Token: 0x04000039 RID: 57
		private float m_Left;

		// Token: 0x0400003A RID: 58
		private float m_Right;

		// Token: 0x0400003B RID: 59
		private float m_Top;

		// Token: 0x0400003C RID: 60
		private float m_Bottom;

		// Token: 0x0400003D RID: 61
		private static readonly TMP_Offset k_ZeroOffset = new TMP_Offset(0f, 0f, 0f, 0f);
	}
}
