using System;
using UnityEngine;

// Token: 0x02000027 RID: 39
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Sharpen")]
public class CC_Sharpen : CC_Base
{
	// Token: 0x0600008F RID: 143 RVA: 0x00004B74 File Offset: 0x00002D74
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.strength == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}
		base.material.SetFloat("_PX", 1f / (float)Screen.width);
		base.material.SetFloat("_PY", 1f / (float)Screen.height);
		base.material.SetFloat("_Strength", this.strength);
		base.material.SetFloat("_Clamp", this.clamp);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040000C1 RID: 193
	[Range(0f, 5f)]
	public float strength = 0.6f;

	// Token: 0x040000C2 RID: 194
	[Range(0f, 1f)]
	public float clamp = 0.05f;
}
