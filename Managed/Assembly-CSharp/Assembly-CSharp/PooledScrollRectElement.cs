using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000087 RID: 135
public abstract class PooledScrollRectElement<T> : MonoBehaviour
{
	// Token: 0x060004DB RID: 1243
	public abstract void Setup(int placement, List<T> allData);
}
