using System;
using UnityEngine;

// Token: 0x02000021 RID: 33
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Negative")]
public class CC_Negative : CC_Base
{
	// Token: 0x06000083 RID: 131 RVA: 0x00004881 File Offset: 0x00002A81
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.amount == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}
		base.material.SetFloat("_Amount", this.amount);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040000B4 RID: 180
	[Range(0f, 1f)]
	public float amount = 1f;
}
