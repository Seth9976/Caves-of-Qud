using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class CanvasSize : MonoBehaviour
{
	// Token: 0x0600004D RID: 77 RVA: 0x000035DA File Offset: 0x000017DA
	private void Start()
	{
	}

	// Token: 0x0600004E RID: 78 RVA: 0x000035DC File Offset: 0x000017DC
	private void Update()
	{
		CanvasSize.updated = CanvasSize.width != this.canvasRect.rect.width || CanvasSize.height != this.canvasRect.rect.height;
		CanvasSize.width = this.canvasRect.rect.width;
		CanvasSize.height = this.canvasRect.rect.height;
	}

	// Token: 0x04000034 RID: 52
	public RectTransform canvasRect;

	// Token: 0x04000035 RID: 53
	public static float width = 1248f;

	// Token: 0x04000036 RID: 54
	public static float height = 768f;

	// Token: 0x04000037 RID: 55
	public static bool updated = true;
}
