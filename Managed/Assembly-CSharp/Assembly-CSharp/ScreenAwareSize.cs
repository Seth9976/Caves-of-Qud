using System;
using UnityEngine;

// Token: 0x02000099 RID: 153
public class ScreenAwareSize : MonoBehaviour
{
	// Token: 0x06000524 RID: 1316 RVA: 0x00021768 File Offset: 0x0001F968
	public void Adjust()
	{
		this.lastCanvasX = CanvasSize.width;
		this.lastCanvasY = CanvasSize.height;
		if (this.widthPercentage > 0f)
		{
			base.GetComponent<RectTransform>().sizeDelta = new Vector2(CanvasSize.width * this.widthPercentage, base.GetComponent<RectTransform>().sizeDelta.y);
		}
		if (this.heightPercentage > 0f)
		{
			base.GetComponent<RectTransform>().sizeDelta = new Vector2(base.GetComponent<RectTransform>().sizeDelta.x, CanvasSize.height * this.heightPercentage);
		}
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x000217FD File Offset: 0x0001F9FD
	public void Awake()
	{
		this.Adjust();
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x00021805 File Offset: 0x0001FA05
	public void Update()
	{
		if (this.lastCanvasX != CanvasSize.width || this.lastCanvasY != CanvasSize.height)
		{
			this.Adjust();
		}
	}

	// Token: 0x040003F4 RID: 1012
	public float widthPercentage;

	// Token: 0x040003F5 RID: 1013
	public float heightPercentage;

	// Token: 0x040003F6 RID: 1014
	public float lastCanvasX = -1f;

	// Token: 0x040003F7 RID: 1015
	public float lastCanvasY = -1f;
}
