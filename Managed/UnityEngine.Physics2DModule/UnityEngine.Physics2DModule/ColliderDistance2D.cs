using System;

namespace UnityEngine
{
	// Token: 0x02000018 RID: 24
	public struct ColliderDistance2D
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000764C File Offset: 0x0000584C
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00007664 File Offset: 0x00005864
		public Vector2 pointA
		{
			get
			{
				return this.m_PointA;
			}
			set
			{
				this.m_PointA = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00007670 File Offset: 0x00005870
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00007688 File Offset: 0x00005888
		public Vector2 pointB
		{
			get
			{
				return this.m_PointB;
			}
			set
			{
				this.m_PointB = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00007694 File Offset: 0x00005894
		public Vector2 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600023B RID: 571 RVA: 0x000076AC File Offset: 0x000058AC
		// (set) Token: 0x0600023C RID: 572 RVA: 0x000076C4 File Offset: 0x000058C4
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000076D0 File Offset: 0x000058D0
		public bool isOverlapped
		{
			get
			{
				return this.m_Distance < 0f;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600023E RID: 574 RVA: 0x000076F0 File Offset: 0x000058F0
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000770B File Offset: 0x0000590B
		public bool isValid
		{
			get
			{
				return this.m_IsValid != 0;
			}
			set
			{
				this.m_IsValid = (value ? 1 : 0);
			}
		}

		// Token: 0x04000060 RID: 96
		private Vector2 m_PointA;

		// Token: 0x04000061 RID: 97
		private Vector2 m_PointB;

		// Token: 0x04000062 RID: 98
		private Vector2 m_Normal;

		// Token: 0x04000063 RID: 99
		private float m_Distance;

		// Token: 0x04000064 RID: 100
		private int m_IsValid;
	}
}
