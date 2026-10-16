using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Double Vision")]
public class CC_DoubleVision : CC_Base
{
	// Token: 0x06000067 RID: 103 RVA: 0x00003E4C File Offset: 0x0000204C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.amount == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}
		base.material.SetVector("_Displace", new Vector2(this.displace.x / (float)Screen.width, this.displace.y / (float)Screen.height));
		base.material.SetFloat("_Amount", this.amount);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400006C RID: 108
	public Vector2 displace = new Vector2(0.7f, 0f);

	// Token: 0x0400006D RID: 109
	[Range(0f, 1f)]
	public float amount = 1f;
}
