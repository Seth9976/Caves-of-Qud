using System;
using UnityEngine;

// Token: 0x0200012B RID: 299
public class StupidDropdownFix : MonoBehaviour
{
	// Token: 0x06000B5E RID: 2910 RVA: 0x0004BDB4 File Offset: 0x00049FB4
	private void Update()
	{
		if (this.c == null)
		{
			this.c = base.GetComponent<Canvas>();
		}
		if (this.c != null && !this.c.overrideSorting)
		{
			this.c.overrideSorting = true;
			this.c.sortingLayerName = "User Interface";
		}
	}

	// Token: 0x040007E0 RID: 2016
	private Canvas c;
}
