using System;
using System.Collections.Generic;

// Token: 0x020000DC RID: 220
public class ControlSorterByPriority2 : IComparer<exUIControl>
{
	// Token: 0x0600082C RID: 2092 RVA: 0x0003489C File Offset: 0x00032A9C
	public int Compare(exUIControl _a, exUIControl _b)
	{
		int num = _a.priority;
		int num2 = _b.priority;
		exUIControl exUIControl = _a.parent;
		while (exUIControl)
		{
			num += exUIControl.priority;
			exUIControl = exUIControl.parent;
		}
		exUIControl = _b.parent;
		while (exUIControl)
		{
			num2 += exUIControl.priority;
			exUIControl = exUIControl.parent;
		}
		return num - num2;
	}
}
