using System;
using UnityEngine;

// Token: 0x02000156 RID: 342
public class UIArrowMove : MonoBehaviour
{
	// Token: 0x06000DA0 RID: 3488 RVA: 0x00058709 File Offset: 0x00056909
	private void Start()
	{
		if (this.arrowRect == null)
		{
			this.arrowRect = base.GetComponent<RectTransform>();
		}
		this.initialPosition = this.arrowRect.anchoredPosition;
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x00058738 File Offset: 0x00056938
	private void Update()
	{
		float num = Mathf.Sin(Time.time * this.moveSpeed) * this.moveDistance;
		if (this.moveHorizontally)
		{
			this.arrowRect.anchoredPosition = new Vector2(this.initialPosition.x + num, this.initialPosition.y);
			return;
		}
		this.arrowRect.anchoredPosition = new Vector2(this.initialPosition.x, this.initialPosition.y + num);
	}

	// Token: 0x0400096A RID: 2410
	public RectTransform arrowRect;

	// Token: 0x0400096B RID: 2411
	public float moveSpeed = 2f;

	// Token: 0x0400096C RID: 2412
	public float moveDistance = 50f;

	// Token: 0x0400096D RID: 2413
	public bool moveHorizontally = true;

	// Token: 0x0400096E RID: 2414
	private Vector2 initialPosition;
}
