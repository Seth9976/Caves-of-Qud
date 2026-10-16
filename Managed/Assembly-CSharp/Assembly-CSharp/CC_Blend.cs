using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Blend")]
public class CC_Blend : CC_Base
{
	// Token: 0x0600005B RID: 91 RVA: 0x000038B0 File Offset: 0x00001AB0
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.texture == null || this.amount == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}
		base.material.SetTexture("_OverlayTex", this.texture);
		base.material.SetFloat("_Amount", this.amount);
		Graphics.Blit(source, destination, base.material, this.mode);
	}

	// Token: 0x04000045 RID: 69
	public Texture texture;

	// Token: 0x04000046 RID: 70
	[Range(0f, 1f)]
	public float amount = 1f;

	// Token: 0x04000047 RID: 71
	public int mode;
}
