using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Photo Filter")]
public class CC_PhotoFilter : CC_Base
{
	// Token: 0x06000085 RID: 133 RVA: 0x000048D0 File Offset: 0x00002AD0
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.density == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}
		base.material.SetColor("_RGB", this.color);
		base.material.SetFloat("_Density", this.density);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040000B5 RID: 181
	public Color color = new Color(1f, 0.5f, 0.2f, 1f);

	// Token: 0x040000B6 RID: 182
	[Range(0f, 1f)]
	public float density = 0.35f;
}
