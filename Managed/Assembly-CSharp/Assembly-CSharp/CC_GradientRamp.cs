using System;
using UnityEngine;

// Token: 0x0200001A RID: 26
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Gradient Ramp")]
public class CC_GradientRamp : CC_Base
{
	// Token: 0x0600006D RID: 109 RVA: 0x00004050 File Offset: 0x00002250
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.rampTexture == null || this.amount == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}
		base.material.SetTexture("_RampTex", this.rampTexture);
		base.material.SetFloat("_Amount", this.amount);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000076 RID: 118
	public Texture rampTexture;

	// Token: 0x04000077 RID: 119
	[Range(0f, 1f)]
	public float amount = 1f;
}
