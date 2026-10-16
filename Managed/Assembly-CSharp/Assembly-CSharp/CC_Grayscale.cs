using System;
using UnityEngine;

// Token: 0x0200001B RID: 27
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Grayscale")]
public class CC_Grayscale : CC_Base
{
	// Token: 0x0600006F RID: 111 RVA: 0x000040CC File Offset: 0x000022CC
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.amount == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}
		base.material.SetVector("_Data", new Vector4(this.redLuminance, this.greenLuminance, this.blueLuminance, this.amount));
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000078 RID: 120
	[Range(0f, 1f)]
	public float redLuminance = 0.299f;

	// Token: 0x04000079 RID: 121
	[Range(0f, 1f)]
	public float greenLuminance = 0.587f;

	// Token: 0x0400007A RID: 122
	[Range(0f, 1f)]
	public float blueLuminance = 0.114f;

	// Token: 0x0400007B RID: 123
	[Range(0f, 1f)]
	public float amount = 1f;
}
