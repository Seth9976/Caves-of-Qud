using System;
using UnityEngine;

// Token: 0x02000024 RID: 36
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Posterize")]
public class CC_Posterize : CC_Base
{
	// Token: 0x06000089 RID: 137 RVA: 0x00004A1B File Offset: 0x00002C1B
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Levels", (float)this.levels);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040000BB RID: 187
	[Range(2f, 255f)]
	public int levels = 4;
}
