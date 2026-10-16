using System;

namespace UnityEngine
{
	// Token: 0x02000129 RID: 297
	public struct BoundingSphere
	{
		// Token: 0x06000835 RID: 2101 RVA: 0x0000E1ED File Offset: 0x0000C3ED
		public BoundingSphere(Vector3 pos, float rad)
		{
			this.position = pos;
			this.radius = rad;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0000E1FE File Offset: 0x0000C3FE
		public BoundingSphere(Vector4 packedSphere)
		{
			this.position = new Vector3(packedSphere.x, packedSphere.y, packedSphere.z);
			this.radius = packedSphere.w;
		}

		// Token: 0x040003FE RID: 1022
		public Vector3 position;

		// Token: 0x040003FF RID: 1023
		public float radius;
	}
}
