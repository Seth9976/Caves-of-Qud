using System;
using UnityEngine;

// Token: 0x0200001C RID: 28
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Halftone")]
public class CC_Halftone : CC_Base
{
	// Token: 0x06000071 RID: 113 RVA: 0x0000415C File Offset: 0x0000235C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Density", this.density);
		int num = 0;
		if (this.mode == 0)
		{
			if (this.antialiasing && this.showOriginal)
			{
				num = 3;
			}
			else if (this.antialiasing)
			{
				num = 1;
			}
			else if (this.showOriginal)
			{
				num = 2;
			}
		}
		else if (this.mode == 1)
		{
			num = (this.antialiasing ? 5 : 4);
		}
		Graphics.Blit(source, destination, base.material, num);
	}

	// Token: 0x0400007C RID: 124
	[Range(0f, 512f)]
	public float density = 64f;

	// Token: 0x0400007D RID: 125
	public int mode = 1;

	// Token: 0x0400007E RID: 126
	public bool antialiasing = true;

	// Token: 0x0400007F RID: 127
	public bool showOriginal;
}
