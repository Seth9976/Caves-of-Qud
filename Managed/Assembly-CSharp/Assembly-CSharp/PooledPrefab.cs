using System;
using UnityEngine;

// Token: 0x02000112 RID: 274
public class PooledPrefab : MonoBehaviour
{
	// Token: 0x06000AD6 RID: 2774 RVA: 0x00046108 File Offset: 0x00044308
	public void Return()
	{
		PooledPrefabManager.Return(base.gameObject);
	}

	// Token: 0x04000744 RID: 1860
	public GameObject originalPrefab;
}
