using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000D9 RID: 217
public class RaycastSorter : IComparer
{
	// Token: 0x06000826 RID: 2086 RVA: 0x00034788 File Offset: 0x00032988
	int IComparer.Compare(object _a, object _b)
	{
		if (!(_a is RaycastHit) || !(_b is RaycastHit))
		{
			return 0;
		}
		RaycastHit raycastHit = (RaycastHit)_a;
		RaycastHit raycastHit2 = (RaycastHit)_b;
		return (int)Mathf.Sign(raycastHit.distance - raycastHit2.distance);
	}
}
