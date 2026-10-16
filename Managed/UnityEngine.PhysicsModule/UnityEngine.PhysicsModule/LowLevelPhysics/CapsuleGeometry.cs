using System;

namespace UnityEngine.LowLevelPhysics
{
	// Token: 0x0200005B RID: 91
	public struct CapsuleGeometry : IGeometry
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0000BF54 File Offset: 0x0000A154
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x0000BF6C File Offset: 0x0000A16C
		public float Radius
		{
			get
			{
				return this.m_Radius;
			}
			set
			{
				this.m_Radius = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0000BF78 File Offset: 0x0000A178
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x0000BF90 File Offset: 0x0000A190
		public float HalfLength
		{
			get
			{
				return this.m_HalfLength;
			}
			set
			{
				this.m_HalfLength = value;
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0000BF9A File Offset: 0x0000A19A
		public CapsuleGeometry(float radius, float halfLength)
		{
			this.m_Radius = radius;
			this.m_HalfLength = halfLength;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0000BFAB File Offset: 0x0000A1AB
		public GeometryType GeometryType
		{
			get
			{
				return GeometryType.Capsule;
			}
		}

		// Token: 0x04000173 RID: 371
		private float m_Radius;

		// Token: 0x04000174 RID: 372
		private float m_HalfLength;
	}
}
