using System;

// Token: 0x020000D5 RID: 213
public class exUIPointEvent : exUIEvent
{
	// Token: 0x1700010E RID: 270
	// (get) Token: 0x0600081E RID: 2078 RVA: 0x00034543 File Offset: 0x00032743
	public bool isTouch
	{
		get
		{
			return !this.isMouse;
		}
	}

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x0600081F RID: 2079 RVA: 0x0003454E File Offset: 0x0003274E
	public exUIPointInfo mainPoint
	{
		get
		{
			return this.pointInfos[0];
		}
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x0003455C File Offset: 0x0003275C
	public bool GetMouseButton(int _id)
	{
		if (this.isMouse)
		{
			for (int i = 0; i < this.pointInfos.Length; i++)
			{
				if (this.pointInfos[i].id == _id)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x04000594 RID: 1428
	public bool altKey;

	// Token: 0x04000595 RID: 1429
	public bool ctrlKey;

	// Token: 0x04000596 RID: 1430
	public bool metaKey;

	// Token: 0x04000597 RID: 1431
	public bool isMouse;

	// Token: 0x04000598 RID: 1432
	public exUIPointInfo[] pointInfos;
}
