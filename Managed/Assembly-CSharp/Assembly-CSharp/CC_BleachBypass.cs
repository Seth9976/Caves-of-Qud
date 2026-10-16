using System;
using UnityEngine;

// Token: 0x02000010 RID: 16
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Bleach Bypass")]
public class CC_BleachBypass : CC_Base
{
	// Token: 0x06000059 RID: 89 RVA: 0x00003862 File Offset: 0x00001A62
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

	// Token: 0x04000044 RID: 68
	[Range(0f, 1f)]
	public float amount = 1f;
}
