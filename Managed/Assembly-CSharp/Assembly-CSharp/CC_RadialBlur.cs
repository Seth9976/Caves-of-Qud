using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Radial Blur")]
public class CC_RadialBlur : CC_Base
{
	// Token: 0x0600008D RID: 141 RVA: 0x00004ADC File Offset: 0x00002CDC
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Amount", this.amount);
		base.material.SetVector("_Center", this.center);
		if (this.amount == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}
		Graphics.Blit(source, destination, base.material, this.quality);
	}

	// Token: 0x040000BE RID: 190
	[Range(0f, 1f)]
	public float amount = 0.1f;

	// Token: 0x040000BF RID: 191
	public Vector2 center = new Vector2(0.5f, 0.5f);

	// Token: 0x040000C0 RID: 192
	public int quality = 1;
}
