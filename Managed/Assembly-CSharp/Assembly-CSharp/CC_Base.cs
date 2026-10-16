using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
[RequireComponent(typeof(Camera))]
[AddComponentMenu("")]
public class CC_Base : MonoBehaviour
{
	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000054 RID: 84 RVA: 0x000037DF File Offset: 0x000019DF
	public static bool IsLinear
	{
		get
		{
			return QualitySettings.activeColorSpace == ColorSpace.Linear;
		}
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000037E9 File Offset: 0x000019E9
	protected virtual void Start()
	{
		if (!this.shader || !this.shader.isSupported)
		{
			base.enabled = false;
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x06000056 RID: 86 RVA: 0x0000380C File Offset: 0x00001A0C
	protected Material material
	{
		get
		{
			if (this._material == null)
			{
				this._material = new Material(this.shader);
				this._material.hideFlags = HideFlags.HideAndDontSave;
			}
			return this._material;
		}
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00003840 File Offset: 0x00001A40
	protected virtual void OnDisable()
	{
		if (this._material)
		{
			global::UnityEngine.Object.DestroyImmediate(this._material);
		}
	}

	// Token: 0x04000042 RID: 66
	public Shader shader;

	// Token: 0x04000043 RID: 67
	protected Material _material;
}
