using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000669 RID: 1641
	internal class ExtraRenderChainVEData : LinkedPoolItem<ExtraRenderChainVEData>
	{
		// Token: 0x06002FFF RID: 12287 RVA: 0x000B3F9F File Offset: 0x000B219F
		public void Reset()
		{
			Debug.Assert(this.extraMesh == null);
		}

		// Token: 0x04001771 RID: 6001
		public BasicNode<MeshHandle> extraMesh;
	}
}
