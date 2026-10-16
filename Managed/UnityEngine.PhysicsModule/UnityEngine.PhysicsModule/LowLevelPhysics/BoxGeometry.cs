using System;

namespace UnityEngine.LowLevelPhysics
{
	// Token: 0x02000059 RID: 89
	public struct BoxGeometry : IGeometry
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0000BF04 File Offset: 0x0000A104
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x0000BF1C File Offset: 0x0000A11C
		public Vector3 HalfExtents
		{
			get
			{
				return this.m_HalfExtents;
			}
			set
			{
				this.m_HalfExtents = value;
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0000BF1C File Offset: 0x0000A11C
		public BoxGeometry(Vector3 halfExtents)
		{
			this.m_HalfExtents = halfExtents;
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0000BF26 File Offset: 0x0000A126
		public GeometryType GeometryType
		{
			get
			{
				return GeometryType.Box;
			}
		}

		// Token: 0x04000171 RID: 369
		private Vector3 m_HalfExtents;
	}
}
