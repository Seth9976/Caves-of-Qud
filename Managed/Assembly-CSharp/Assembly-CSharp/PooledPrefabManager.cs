using System;
using System.Collections.Generic;
using QupKit;
using UnityEngine;

// Token: 0x02000115 RID: 277
public static class PooledPrefabManager
{
	// Token: 0x06000AE7 RID: 2791 RVA: 0x00047175 File Offset: 0x00045375
	public static void MakeTemporary(GameObject prefabToMakeTemporary, float timeToLive)
	{
		PooledPrefabTemporary component = prefabToMakeTemporary.GetComponent<PooledPrefabTemporary>();
		if (component == null)
		{
			Debug.Log("Didn't find temporary on: " + prefabToMakeTemporary.name);
		}
		component.isTemporary = true;
		component.timeToLive = timeToLive;
	}

	// Token: 0x06000AE8 RID: 2792 RVA: 0x000471A8 File Offset: 0x000453A8
	public static void Prewarm(GameObject prefabToPrewarm, int howMany)
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < howMany; i++)
		{
			list.Add(PooledPrefabManager.Instantiate(prefabToPrewarm));
		}
		for (int j = 0; j < howMany; j++)
		{
			PooledPrefabManager.Return(list[j]);
		}
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x000471EB File Offset: 0x000453EB
	public static GameObject InstantiateTemporary(GameObject prefab, float timeToLive)
	{
		GameObject gameObject = PooledPrefabManager.Instantiate(prefab);
		gameObject.GetComponent<PooledPrefabTemporary>().isTemporary = true;
		gameObject.GetComponent<PooledPrefabTemporary>().timeToLive = timeToLive;
		return gameObject;
	}

	// Token: 0x06000AEA RID: 2794 RVA: 0x0004720B File Offset: 0x0004540B
	public static GameObject Instantiate(string Prefab, string Folder = "Prefabs/")
	{
		return PooledPrefabManager.Instantiate(PrefabManager.Get(Prefab, Folder));
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x0004721C File Offset: 0x0004541C
	public static GameObject Instantiate(GameObject prefab)
	{
		if (!PooledPrefabManager.pools.ContainsKey(prefab))
		{
			PooledPrefabManager.pools.Add(prefab, new Queue<GameObject>());
		}
		GameObject gameObject = null;
		while (PooledPrefabManager.pools[prefab].Count > 0)
		{
			gameObject = PooledPrefabManager.pools[prefab].Dequeue();
			if (gameObject != null)
			{
				gameObject.SetActive(true);
				gameObject.SendMessage("PoolReset", SendMessageOptions.DontRequireReceiver);
				break;
			}
		}
		if (gameObject == null)
		{
			gameObject = global::UnityEngine.Object.Instantiate<GameObject>(prefab);
			gameObject.AddComponent<PooledPrefab>().originalPrefab = prefab;
			gameObject.AddComponent<PooledPrefabTemporary>();
			gameObject.SetActive(true);
		}
		gameObject.GetComponent<PooledPrefabTemporary>().isTemporary = false;
		return gameObject;
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x000472C4 File Offset: 0x000454C4
	public static GameObject InstantiateCanvasGroupStyle(GameObject prefab)
	{
		if (!PooledPrefabManager.pools.ContainsKey(prefab))
		{
			PooledPrefabManager.pools.Add(prefab, new Queue<GameObject>());
		}
		GameObject gameObject = null;
		while (PooledPrefabManager.pools[prefab].Count > 0)
		{
			gameObject = PooledPrefabManager.pools[prefab].Dequeue();
			if (gameObject != null)
			{
				CanvasGroup component = gameObject.GetComponent<CanvasGroup>();
				component.alpha = 1f;
				component.interactable = true;
				component.blocksRaycasts = true;
				gameObject.SendMessage("PoolReset", SendMessageOptions.DontRequireReceiver);
				break;
			}
		}
		if (gameObject == null)
		{
			gameObject = global::UnityEngine.Object.Instantiate<GameObject>(prefab);
			gameObject.AddComponent<PooledPrefab>().originalPrefab = prefab;
			gameObject.AddComponent<PooledPrefabTemporary>();
		}
		gameObject.GetComponent<PooledPrefabTemporary>().isTemporary = false;
		return gameObject;
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x0004737C File Offset: 0x0004557C
	public static void ReturnCanvasGroupStyle(GameObject pooledPrefab)
	{
		pooledPrefab.SendMessage("OnReturnToPool", SendMessageOptions.DontRequireReceiver);
		if (pooledPrefab.GetComponent<PooledPrefab>() == null)
		{
			global::UnityEngine.Object.Destroy(pooledPrefab);
			return;
		}
		if (pooledPrefab.activeInHierarchy)
		{
			if (pooledPrefab.GetComponent<PooledPrefabTemporary>() != null)
			{
				pooledPrefab.GetComponent<PooledPrefabTemporary>().isTemporary = false;
			}
			GameObject originalPrefab = pooledPrefab.GetComponent<PooledPrefab>().originalPrefab;
			pooledPrefab.transform.parent = null;
			CanvasGroup component = pooledPrefab.GetComponent<CanvasGroup>();
			component.alpha = 0f;
			component.interactable = false;
			component.blocksRaycasts = false;
			PooledPrefabManager.pools[originalPrefab].Enqueue(pooledPrefab);
		}
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x00047414 File Offset: 0x00045614
	public static void Return(GameObject pooledPrefab)
	{
		if (pooledPrefab == null)
		{
			return;
		}
		pooledPrefab.SendMessage("OnReturnToPool", SendMessageOptions.DontRequireReceiver);
		if (pooledPrefab.GetComponent<PooledPrefab>() == null)
		{
			global::UnityEngine.Object.Destroy(pooledPrefab);
			return;
		}
		if (pooledPrefab.GetComponent<PooledPrefabTemporary>() != null)
		{
			pooledPrefab.GetComponent<PooledPrefabTemporary>().isTemporary = false;
		}
		GameObject originalPrefab = pooledPrefab.GetComponent<PooledPrefab>().originalPrefab;
		pooledPrefab.transform.SetParent(null);
		pooledPrefab.SetActive(false);
		PooledPrefabManager.pools[originalPrefab].Enqueue(pooledPrefab);
	}

	// Token: 0x04000749 RID: 1865
	private static Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();
}
