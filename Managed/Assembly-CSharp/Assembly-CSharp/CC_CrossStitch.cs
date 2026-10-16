using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Cross Stitch")]
public class CC_CrossStitch : CC_Base
{
	// Token: 0x06000065 RID: 101 RVA: 0x00003D7C File Offset: 0x00001F7C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_StitchSize", (float)this.size);
		base.material.SetFloat("_Brightness", this.brightness);
		int num = (this.invert ? 1 : 0);
		if (this.pixelize)
		{
			num += 2;
			base.material.SetFloat("_Scale", (float)(base.GetComponent<Camera>().pixelWidth / this.size));
			base.material.SetFloat("_Ratio", (float)(base.GetComponent<Camera>().pixelWidth / base.GetComponent<Camera>().pixelHeight));
		}
		Graphics.Blit(source, destination, base.material, num);
	}

	// Token: 0x04000068 RID: 104
	[Range(1f, 128f)]
	public int size = 8;

	// Token: 0x04000069 RID: 105
	public float brightness = 1.5f;

	// Token: 0x0400006A RID: 106
	public bool invert;

	// Token: 0x0400006B RID: 107
	public bool pixelize = true;
}
