using System;
using UnityEngine;

// Token: 0x02000014 RID: 20
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Contrast Vignette")]
public class CC_ContrastVignette : CC_Base
{
	// Token: 0x06000061 RID: 97 RVA: 0x00003B4C File Offset: 0x00001D4C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetVector("_Data", new Vector4(this.sharpness * 0.01f, this.darkness * 0.02f, this.contrast * 0.01f, this.edge * 0.01f));
		base.material.SetVector("_Coeffs", new Vector4(this.redCoeff, this.greenCoeff, this.blueCoeff, 1f));
		base.material.SetVector("_Center", this.center);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400005B RID: 91
	public Vector2 center = new Vector2(0.5f, 0.5f);

	// Token: 0x0400005C RID: 92
	[Range(-100f, 100f)]
	public float sharpness = 32f;

	// Token: 0x0400005D RID: 93
	[Range(0f, 100f)]
	public float darkness = 28f;

	// Token: 0x0400005E RID: 94
	[Range(0f, 200f)]
	public float contrast = 20f;

	// Token: 0x0400005F RID: 95
	[Range(0f, 1f)]
	public float redCoeff = 0.5f;

	// Token: 0x04000060 RID: 96
	[Range(0f, 1f)]
	public float greenCoeff = 0.5f;

	// Token: 0x04000061 RID: 97
	[Range(0f, 1f)]
	public float blueCoeff = 0.5f;

	// Token: 0x04000062 RID: 98
	[Range(0f, 200f)]
	public float edge;
}
