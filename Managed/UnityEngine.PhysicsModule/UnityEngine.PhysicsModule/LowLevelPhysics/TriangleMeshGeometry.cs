using System;

namespace UnityEngine.LowLevelPhysics
{
	// Token: 0x0200005D RID: 93
	public struct TriangleMeshGeometry : IGeometry
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0000BFFC File Offset: 0x0000A1FC
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x0000C014 File Offset: 0x0000A214
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

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0000C020 File Offset: 0x0000A220
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x0000C038 File Offset: 0x0000A238
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

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0000C042 File Offset: 0x0000A242
		public GeometryType GeometryType
		{
			get
			{
				return GeometryType.TriangleMesh;
			}
		}

		// Token: 0x0400017C RID: 380
		private Vector3 m_Scale;

		// Token: 0x0400017D RID: 381
		private Quaternion m_Rotation;

		// Token: 0x0400017E RID: 382
		private byte m_MeshFlags;

		// Token: 0x0400017F RID: 383
		private byte pad1;

		// Token: 0x04000180 RID: 384
		private short pad2;

		// Token: 0x04000181 RID: 385
		private IntPtr m_TriangleMesh;

		// Token: 0x04000182 RID: 386
		private uint pad3;
	}
}
