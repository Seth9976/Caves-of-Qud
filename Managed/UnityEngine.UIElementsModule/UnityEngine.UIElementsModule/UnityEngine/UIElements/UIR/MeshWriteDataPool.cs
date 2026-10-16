using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200065D RID: 1629
	internal class MeshWriteDataPool : ImplicitPool<MeshWriteData>
	{
		// Token: 0x06002F99 RID: 12185 RVA: 0x000B164E File Offset: 0x000AF84E
		public MeshWriteDataPool()
			: base(MeshWriteDataPool.k_CreateAction, null, 100, 1000)
		{
		}

		// Token: 0x040016ED RID: 5869
		private static readonly Func<MeshWriteData> k_CreateAction = () => new MeshWriteData();
	}
}
