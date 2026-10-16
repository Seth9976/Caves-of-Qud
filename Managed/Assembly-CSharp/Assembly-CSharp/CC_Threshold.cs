using System;
using UnityEngine;

// Token: 0x02000028 RID: 40
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Threshold")]
public class CC_Threshold : CC_Base
{
	// Token: 0x06000091 RID: 145 RVA: 0x00004C28 File Offset: 0x00002E28
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Threshold", this.threshold / 255f);
		base.material.SetFloat("_Range", this.noiseRange / 255f);
		Graphics.Blit(source, destination, base.material, this.useNoise ? 1 : 0);
	}

	// Token: 0x040000C3 RID: 195
	[Range(1f, 255f)]
	public float threshold = 128f;

	// Token: 0x040000C4 RID: 196
	[Range(0f, 128f)]
	public float noiseRange = 48f;

	// Token: 0x040000C5 RID: 197
	public bool useNoise;
}
