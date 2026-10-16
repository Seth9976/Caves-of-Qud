using System;
using UnityEngine;

// Token: 0x02000116 RID: 278
public class PooledPrefabTemporary : MonoBehaviour
{
	// Token: 0x06000AF0 RID: 2800 RVA: 0x000474A2 File Offset: 0x000456A2
	public void Update()
	{
		if (this.isTemporary)
		{
			this.timeToLive -= Time.deltaTime;
			if (this.timeToLive <= 0f)
			{
				PooledPrefabManager.Return(base.gameObject);
			}
		}
	}

	// Token: 0x0400074A RID: 1866
	public bool isTemporary;

	// Token: 0x0400074B RID: 1867
	public float timeToLive;
}
