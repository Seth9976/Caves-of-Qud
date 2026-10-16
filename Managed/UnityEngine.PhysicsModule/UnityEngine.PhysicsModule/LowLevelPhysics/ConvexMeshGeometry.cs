using System;

namespace UnityEngine.LowLevelPhysics
{
	// Token: 0x0200005C RID: 92
	public struct ConvexMeshGeometry : IGeometry
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0000BFB0 File Offset: 0x0000A1B0
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x0000BFC8 File Offset: 0x0000A1C8
		public Vector3 Scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0000BFD4 File Offset: 0x0000A1D4
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0000BFEC File Offset: 0x0000A1EC
		public Quaternion ScaleAxisRotation
		{
			get
			{
				return this.m_Rotation;
			}
			set
			{
				this.m_Rotation = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0000BFF6 File Offset: 0x0000A1F6
		public GeometryType GeometryType
		{
			get
			{
				return GeometryType.ConvexMesh;
			}
		}

		// Token: 0x04000175 RID: 373
		private Vector3 m_Scale;

		// Token: 0x04000176 RID: 374
		private Quaternion m_Rotation;

		// Token: 0x04000177 RID: 375
		private IntPtr m_ConvexMesh;

		// Token: 0x04000178 RID: 376
		private byte m_MeshFlags;

		// Token: 0x04000179 RID: 377
		private byte pad1;

		// Token: 0x0400017A RID: 378
		private short pad2;

		// Token: 0x0400017B RID: 379
		private uint pad3;
	}
}
