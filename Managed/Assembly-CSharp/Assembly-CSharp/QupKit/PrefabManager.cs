using System;
using System.Collections.Generic;
using UnityEngine;

namespace QupKit
{
	// Token: 0x020003A6 RID: 934
	public static class PrefabManager
	{
		// Token: 0x060022FC RID: 8956 RVA: 0x000B1DF4 File Offset: 0x000AFFF4
		public static GameObject Create(string Prefab, string Folder = "Prefabs/")
		{
			if (!PrefabManager.PrefabCache.ContainsKey(Prefab))
			{
				PrefabManager.PrefabCache.Add(Prefab, (GameObject)Resources.Load(Folder.IsNullOrEmpty() ? Prefab : (Folder + Prefab)));
			}
			return global::UnityEngine.Object.Instantiate<GameObject>(PrefabManager.PrefabCache[Prefab]);
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x000B1E48 File Offset: 0x000B0048
		public static GameObject Get(string Prefab, string Folder = "Prefabs")
		{
			if (!PrefabManager.PrefabCache.ContainsKey(Prefab))
			{
				PrefabManager.PrefabCache.Add(Prefab, (GameObject)Resources.Load(Folder.IsNullOrEmpty() ? Prefab : (Folder + Prefab)));
			}
			return PrefabManager.PrefabCache[Prefab];
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000B1E94 File Offset: 0x000B0094
		public static GameObject CreateRoot(string Prefab)
		{
			if (!PrefabManager.RootPrefabCache.ContainsKey(Prefab))
			{
				PrefabManager.RootPrefabCache.Add(Prefab, (GameObject)Resources.Load(Prefab));
			}
			return global::UnityEngine.Object.Instantiate<GameObject>(PrefabManager.RootPrefabCache[Prefab]);
		}

		// Token: 0x040011A1 RID: 4513
		private static Dictionary<string, GameObject> PrefabCache = new Dictionary<string, GameObject>();

		// Token: 0x040011A2 RID: 4514
		private static Dictionary<string, GameObject> RootPrefabCache = new Dictionary<string, GameObject>();
	}
}
