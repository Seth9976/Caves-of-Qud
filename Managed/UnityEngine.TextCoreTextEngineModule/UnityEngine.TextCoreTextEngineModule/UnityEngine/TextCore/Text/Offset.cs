using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000057 RID: 87
	internal struct Offset
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0001FCD8 File Offset: 0x0001DED8
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0001FCF0 File Offset: 0x0001DEF0
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

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0001FCFC File Offset: 0x0001DEFC
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0001FD14 File Offset: 0x0001DF14
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

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0001FD20 File Offset: 0x0001DF20
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0001FD38 File Offset: 0x0001DF38
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

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0001FD44 File Offset: 0x0001DF44
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0001FD5C File Offset: 0x0001DF5C
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

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0001FD68 File Offset: 0x0001DF68
		public static Offset zero
		{
			get
			{
				return Offset.k_ZeroOffset;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0001FD7F File Offset: 0x0001DF7F
		public Offset(float left, float right, float top, float bottom)
		{
			this.m_Left = left;
			this.m_Right = right;
			this.m_Top = top;
			this.m_Bottom = bottom;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0001FDA0 File Offset: 0x0001DFA0
		public static bool operator ==(Offset lhs, Offset rhs)
		{
			return lhs.m_Left == rhs.m_Left && lhs.m_Right == rhs.m_Right && lhs.m_Top == rhs.m_Top && lhs.m_Bottom == rhs.m_Bottom;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0001FDF0 File Offset: 0x0001DFF0
		public static bool operator !=(Offset lhs, Offset rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0001FE0C File Offset: 0x0001E00C
		public static Offset operator *(Offset a, float b)
		{
			return new Offset(a.m_Left * b, a.m_Right * b, a.m_Top * b, a.m_Bottom * b);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0001FE44 File Offset: 0x0001E044
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0001FE68 File Offset: 0x0001E068
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0001FE8C File Offset: 0x0001E08C
		public bool Equals(Offset other)
		{
			return base.Equals(other);
		}

		// Token: 0x0400030A RID: 778
		private float m_Left;

		// Token: 0x0400030B RID: 779
		private float m_Right;

		// Token: 0x0400030C RID: 780
		private float m_Top;

		// Token: 0x0400030D RID: 781
		private float m_Bottom;

		// Token: 0x0400030E RID: 782
		private static readonly Offset k_ZeroOffset = new Offset(0f, 0f, 0f, 0f);
	}
}
