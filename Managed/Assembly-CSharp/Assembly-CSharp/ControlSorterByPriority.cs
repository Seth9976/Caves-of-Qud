using System;
using System.Collections.Generic;

// Token: 0x020000DB RID: 219
public class ControlSorterByPriority : IComparer<exUIControl>
{
	// Token: 0x0600082A RID: 2090 RVA: 0x0003482C File Offset: 0x00032A2C
	public int Compare(exUIControl _a, exUIControl _b)
	{
		int num = _a.priority;
		int num2 = _b.priority;
		if (_a == _b)
		{
			return 0;
		}
		if (!_a.gameObject.activeInHierarchy || !_a.activeInHierarchy)
		{
			num = -999;
		}
		if (!_b.gameObject.activeInHierarchy || !_b.activeInHierarchy)
		{
			num2 = -999;
		}
		if (num == num2)
		{
			return -1;
		}
		return num2 - num;
	}
}
