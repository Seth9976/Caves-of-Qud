using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Frost")]
public class CC_Frost : CC_Base
{
	// Token: 0x0600006B RID: 107 RVA: 0x00003F94 File Offset: 0x00002194
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.scale == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}
		base.material.SetFloat("_Scale", this.scale);
		base.material.SetFloat("_Sharpness", this.sharpness * 0.01f);
		base.material.SetFloat("_Darkness", this.darkness * 0.02f);
		Graphics.Blit(source, destination, base.material, this.enableVignette ? 1 : 0);
	}

	// Token: 0x04000072 RID: 114
	[Range(0f, 16f)]
	public float scale = 1.2f;

	// Token: 0x04000073 RID: 115
	[Range(-100f, 100f)]
	public float sharpness = 40f;

	// Token: 0x04000074 RID: 116
	[Range(0f, 100f)]
	public float darkness = 35f;

	// Token: 0x04000075 RID: 117
	public bool enableVignette = true;
}
