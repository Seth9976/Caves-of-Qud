using System;
using Unity.Profiling;

namespace UnityEngine
{
	// Token: 0x020002D9 RID: 729
	public sealed class StaticBatchingUtility
	{
		// Token: 0x06002363 RID: 9059 RVA: 0x00045858 File Offset: 0x00043A58
		public static void Combine(GameObject staticBatchRoot)
		{
			using (StaticBatchingUtility.s_CombineMarker.Auto())
			{
				StaticBatchingUtility.CombineRoot(staticBatchRoot);
			}
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x0004589C File Offset: 0x00043A9C
		public static void Combine(GameObject[] gos, GameObject staticBatchRoot)
		{
			using (StaticBatchingUtility.s_CombineMarker.Auto())
			{
				StaticBatchingHelper.CombineMeshes(gos, staticBatchRoot);
			}
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000458E0 File Offset: 0x00043AE0
		private static void CombineRoot(GameObject staticBatchRoot)
		{
			bool flag = staticBatchRoot == null;
			MeshFilter[] array;
			if (flag)
			{
				array = (MeshFilter[])Object.FindObjectsByType(typeof(MeshFilter), FindObjectsSortMode.None);
			}
			else
			{
				array = staticBatchRoot.GetComponentsInChildren<MeshFilter>();
			}
			GameObject[] array2 = new GameObject[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = array[i].gameObject;
			}
			StaticBatchingHelper.CombineMeshes(array2, staticBatchRoot);
		}

		// Token: 0x04000A31 RID: 2609
		internal static ProfilerMarker s_CombineMarker = new ProfilerMarker("StaticBatching.Combine");
	}
}
