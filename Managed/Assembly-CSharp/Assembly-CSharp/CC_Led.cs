using System;
using UnityEngine;

// Token: 0x0200001E RID: 30
[ExecuteInEditMode]
[AddComponentMenu("Colorful/LED")]
public class CC_Led : CC_Base
{
	// Token: 0x0600007B RID: 123 RVA: 0x00004420 File Offset: 0x00002620
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		int num = this.mode;
		if (num != 0)
		{
			if (num != 1)
			{
			}
			base.material.SetFloat("_Scale", (float)base.GetComponent<Camera>().pixelWidth / this.scale);
		}
		else
		{
			base.material.SetFloat("_Scale", this.scale);
		}
		base.material.SetFloat("_Ratio", this.automaticRatio ? ((float)(base.GetComponent<Camera>().pixelWidth / base.GetComponent<Camera>().pixelHeight)) : this.ratio);
		base.material.SetFloat("_Brightness", this.brightness);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000097 RID: 151
	[Range(1f, 255f)]
	public float scale = 80f;

	// Token: 0x04000098 RID: 152
	[Range(0f, 10f)]
	public float brightness = 1f;

	// Token: 0x04000099 RID: 153
	public bool automaticRatio;

	// Token: 0x0400009A RID: 154
	public float ratio = 1f;

	// Token: 0x0400009B RID: 155
	public int mode;
}
