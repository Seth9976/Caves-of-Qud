using System;
using System.Collections.Generic;

// Token: 0x020000DA RID: 218
public class ControlSorterByLevel : IComparer<exUIControl>
{
	// Token: 0x06000828 RID: 2088 RVA: 0x000347D4 File Offset: 0x000329D4
	public int Compare(exUIControl _a, exUIControl _b)
	{
		int num = 0;
		int num2 = 0;
		exUIControl exUIControl = _a.parent;
		while (exUIControl)
		{
			num++;
			exUIControl = exUIControl.parent;
		}
		exUIControl = _b.parent;
		while (exUIControl)
		{
			num2++;
			exUIControl = exUIControl.parent;
		}
		return num - num2;
	}
}
