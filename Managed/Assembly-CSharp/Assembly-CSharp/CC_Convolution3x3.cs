using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Convolution Matrix 3x3")]
public class CC_Convolution3x3 : CC_Base
{
	// Token: 0x06000063 RID: 99 RVA: 0x00003C60 File Offset: 0x00001E60
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_PX", 1f / (float)Screen.width);
		base.material.SetFloat("_PY", 1f / (float)Screen.height);
		base.material.SetFloat("_Amount", this.amount);
		base.material.SetVector("_KernelT", this.kernelTop / this.divisor);
		base.material.SetVector("_KernelM", this.kernelMiddle / this.divisor);
		base.material.SetVector("_KernelB", this.kernelBottom / this.divisor);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000063 RID: 99
	public Vector3 kernelTop = Vector3.zero;

	// Token: 0x04000064 RID: 100
	public Vector3 kernelMiddle = Vector3.up;

	// Token: 0x04000065 RID: 101
	public Vector3 kernelBottom = Vector3.zero;

	// Token: 0x04000066 RID: 102
	public float divisor = 1f;

	// Token: 0x04000067 RID: 103
	[Range(0f, 1f)]
	public float amount = 1f;
}
