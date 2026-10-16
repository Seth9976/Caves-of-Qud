using System;
using UnityEngine;

// Token: 0x02000018 RID: 24
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Fast Vignette")]
public class CC_FastVignette : CC_Base
{
	// Token: 0x06000069 RID: 105 RVA: 0x00003EF8 File Offset: 0x000020F8
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetVector("_Data", new Vector4(this.center.x, this.center.y, this.sharpness * 0.01f, this.darkness * 0.02f));
		Graphics.Blit(source, destination, base.material, this.desaturate ? 1 : 0);
	}

	// Token: 0x0400006E RID: 110
	public Vector2 center = new Vector2(0.5f, 0.5f);

	// Token: 0x0400006F RID: 111
	[Range(-100f, 100f)]
	public float sharpness = 10f;

	// Token: 0x04000070 RID: 112
	[Range(0f, 100f)]
	public float darkness = 30f;

	// Token: 0x04000071 RID: 113
	public bool desaturate;
}
