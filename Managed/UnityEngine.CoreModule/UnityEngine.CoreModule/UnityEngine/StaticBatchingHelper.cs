using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001EB RID: 491
	[NativeHeader("Runtime/Graphics/Mesh/StaticBatching.h")]
	internal struct StaticBatchingHelper
	{
		// Token: 0x0600174D RID: 5965 RVA: 0x000283C4 File Offset: 0x000265C4
		[FreeFunction("StaticBatching::CombineMeshesForStaticBatching")]
		internal static void CombineMeshes(GameObject[] gos, GameObject staticBatchRoot)
		{
			StaticBatchingHelper.CombineMeshes_Injected(gos, Object.MarshalledUnityObject.Marshal<GameObject>(staticBatchRoot));
		}

		// Token: 0x0600174E RID: 5966
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CombineMeshes_Injected(GameObject[] gos, IntPtr staticBatchRoot);
	}
}
