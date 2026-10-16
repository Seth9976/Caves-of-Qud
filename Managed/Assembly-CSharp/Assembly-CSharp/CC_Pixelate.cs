using System;
using UnityEngine;

// Token: 0x02000023 RID: 35
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Pixelate")]
public class CC_Pixelate : CC_Base
{
	// Token: 0x06000087 RID: 135 RVA: 0x00004960 File Offset: 0x00002B60
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
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040000B7 RID: 183
	[Range(1f, 1024f)]
	public float scale = 80f;

	// Token: 0x040000B8 RID: 184
	public bool automaticRatio;

	// Token: 0x040000B9 RID: 185
	public float ratio = 1f;

	// Token: 0x040000BA RID: 186
	public int mode;
}
