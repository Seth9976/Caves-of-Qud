using System;
using UnityEngine;
using UnityEngine.UI;
using XRL.UI;

// Token: 0x02000039 RID: 57
public class FontSizeUIScalar : MonoBehaviour
{
	// Token: 0x060001F7 RID: 503 RVA: 0x0000A4EC File Offset: 0x000086EC
	private void Start()
	{
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x0000A4F0 File Offset: 0x000086F0
	private void Update()
	{
		if (this.lastScale != Options.StageScaleRaw)
		{
			this.lastScale = Options.StageScaleRaw;
			if (this.text != null)
			{
				this.text.fontSize = (int)((double)this.target / Options.StageScale);
			}
		}
	}

	// Token: 0x04000109 RID: 265
	public global::UnityEngine.UI.Text text;

	// Token: 0x0400010A RID: 266
	public int target;

	// Token: 0x0400010B RID: 267
	private string lastScale;
}
