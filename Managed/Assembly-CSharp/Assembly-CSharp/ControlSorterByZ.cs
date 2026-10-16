using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class ControlSorterByZ : IComparer<exUIControl>
{
	// Token: 0x0600082E RID: 2094 RVA: 0x00034908 File Offset: 0x00032B08
	public int Compare(exUIControl _a, exUIControl _b)
	{
		int num = Mathf.CeilToInt(_a.transform.position.z - _b.transform.position.z);
		if (num != 0)
		{
			return num;
		}
		return _a.GetInstanceID() - _b.GetInstanceID();
	}
}
