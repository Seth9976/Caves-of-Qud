using System;

namespace UnityEngine.LowLevelPhysics
{
	// Token: 0x0200005A RID: 90
	public struct SphereGeometry : IGeometry
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0000BF2C File Offset: 0x0000A12C
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x0000BF44 File Offset: 0x0000A144
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

		// Token: 0x06000699 RID: 1689 RVA: 0x0000BF44 File Offset: 0x0000A144
		public SphereGeometry(float radius)
		{
			this.m_Radius = radius;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0000BF4E File Offset: 0x0000A14E
		public GeometryType GeometryType
		{
			get
			{
				return GeometryType.Sphere;
			}
		}

		// Token: 0x04000172 RID: 370
		private float m_Radius;
	}
}
