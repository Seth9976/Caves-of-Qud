using System;
using UnityEngine;

// Token: 0x020000DE RID: 222
public class exHotPoint
{
	// Token: 0x17000110 RID: 272
	// (get) Token: 0x06000830 RID: 2096 RVA: 0x00034956 File Offset: 0x00032B56
	public bool isTouch
	{
		get
		{
			return !this.isMouse;
		}
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x00034961 File Offset: 0x00032B61
	public bool GetMouseButton(int _id)
	{
		return this.isMouse && this.id == _id;
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x00034978 File Offset: 0x00032B78
	public void Reset()
	{
		this.active = false;
		this.pressDown = false;
		this.pressUp = false;
		this.pos = Vector2.zero;
		this.delta = Vector2.zero;
		this.worldPos = Vector3.zero;
		this.worldDelta = Vector3.zero;
		this.hover = null;
		this.pressed = null;
	}

	// Token: 0x0400059C RID: 1436
	public int id = -1;

	// Token: 0x0400059D RID: 1437
	public bool active;

	// Token: 0x0400059E RID: 1438
	public bool pressDown;

	// Token: 0x0400059F RID: 1439
	public bool pressUp;

	// Token: 0x040005A0 RID: 1440
	public Vector2 pos = Vector2.zero;

	// Token: 0x040005A1 RID: 1441
	public Vector2 delta = Vector2.zero;

	// Token: 0x040005A2 RID: 1442
	public Vector3 worldPos = Vector3.zero;

	// Token: 0x040005A3 RID: 1443
	public Vector3 worldDelta = Vector3.zero;

	// Token: 0x040005A4 RID: 1444
	public exUIControl hover;

	// Token: 0x040005A5 RID: 1445
	public exUIControl pressed;

	// Token: 0x040005A6 RID: 1446
	public bool isMouse;
}
