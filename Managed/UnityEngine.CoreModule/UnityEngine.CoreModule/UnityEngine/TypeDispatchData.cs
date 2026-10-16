using System;
using Unity.Collections;

namespace UnityEngine
{
	// Token: 0x0200021F RID: 543
	internal struct TypeDispatchData : IDisposable
	{
		// Token: 0x06001DC5 RID: 7621 RVA: 0x000391D1 File Offset: 0x000373D1
		public void Dispose()
		{
			this.changed = null;
			this.changedID.Dispose();
			this.destroyedID.Dispose();
		}

		// Token: 0x040008D6 RID: 2262
		public Object[] changed;

		// Token: 0x040008D7 RID: 2263
		public NativeArray<int> changedID;

		// Token: 0x040008D8 RID: 2264
		public NativeArray<int> destroyedID;
	}
}
