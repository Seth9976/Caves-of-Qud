using System;
using Unity.Collections;

namespace UnityEngine
{
	// Token: 0x02000220 RID: 544
	internal struct TransformDispatchData : IDisposable
	{
		// Token: 0x06001DC6 RID: 7622 RVA: 0x000391F4 File Offset: 0x000373F4
		public void Dispose()
		{
			this.transformedID.Dispose();
			this.parentID.Dispose();
			this.localToWorldMatrices.Dispose();
			this.positions.Dispose();
			this.rotations.Dispose();
			this.scales.Dispose();
		}

		// Token: 0x040008D9 RID: 2265
		public NativeArray<int> transformedID;

		// Token: 0x040008DA RID: 2266
		public NativeArray<int> parentID;

		// Token: 0x040008DB RID: 2267
		public NativeArray<Matrix4x4> localToWorldMatrices;

		// Token: 0x040008DC RID: 2268
		public NativeArray<Vector3> positions;

		// Token: 0x040008DD RID: 2269
		public NativeArray<Quaternion> rotations;

		// Token: 0x040008DE RID: 2270
		public NativeArray<Vector3> scales;
	}
}
