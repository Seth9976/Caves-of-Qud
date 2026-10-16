using System;
using UnityEngine;

// Token: 0x0200002E RID: 46
public class EnsureInRect : MonoBehaviour
{
	// Token: 0x17000037 RID: 55
	// (get) Token: 0x060000AC RID: 172 RVA: 0x00005683 File Offset: 0x00003883
	public RectTransform rectTransform
	{
		get
		{
			if (this._rectTransform == null)
			{
				this._rectTransform = base.GetComponent<RectTransform>();
			}
			return this._rectTransform;
		}
	}

	// Token: 0x060000AD RID: 173 RVA: 0x000056A5 File Offset: 0x000038A5
	private void Start()
	{
	}

	// Token: 0x060000AE RID: 174 RVA: 0x000056A8 File Offset: 0x000038A8
	private void LateUpdate()
	{
		this.enclosingRect.GetWorldCorners(this.enclosingCorners);
		this.rectTransform.GetWorldCorners(this.corners);
		if (this.corners[2].x > this.enclosingCorners[2].x)
		{
			this.rectTransform.position += new Vector3(this.enclosingCorners[2].x - this.corners[2].x, 0f, 0f);
		}
		if (this.corners[3].y < this.enclosingCorners[3].y)
		{
			this.rectTransform.position -= new Vector3(0f, this.corners[3].y - this.enclosingCorners[3].y, 0f);
		}
		if (this.corners[1].x < this.enclosingCorners[1].x)
		{
			this.rectTransform.position += new Vector3(this.enclosingCorners[0].x - this.corners[0].x, 0f, 0f);
		}
		if (this.corners[1].y > this.enclosingCorners[1].y)
		{
			this.rectTransform.position -= new Vector3(0f, this.corners[1].y - this.enclosingCorners[1].y, 0f);
		}
	}

	// Token: 0x040000D7 RID: 215
	public Canvas canvas;

	// Token: 0x040000D8 RID: 216
	public RectTransform enclosingRect;

	// Token: 0x040000D9 RID: 217
	private RectTransform _rectTransform;

	// Token: 0x040000DA RID: 218
	private Vector3[] enclosingCorners = new Vector3[4];

	// Token: 0x040000DB RID: 219
	private Vector3[] corners = new Vector3[4];
}
