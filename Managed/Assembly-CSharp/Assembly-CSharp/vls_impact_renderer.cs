using System;
using Kobold;
using UnityEngine;

// Token: 0x02000123 RID: 291
[ExecuteInEditMode]
public class vls_impact_renderer : MonoBehaviour, CombatJuice.ICombatJuiceAnimator
{
	// Token: 0x06000B3F RID: 2879 RVA: 0x0004ACBC File Offset: 0x00048EBC
	public void Update()
	{
		this.t += Time.deltaTime;
		if (this.t > this.duration)
		{
			this.t = this.duration;
		}
		this.missileVfx.transform.localPosition = new Vector3(0f, Mathf.Lerp(900f, 0f, this.t / this.duration), 0f);
		if (this.t >= this.duration)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x0004AD4C File Offset: 0x00048F4C
	public void Play(bool loop = false, Action after = null, string name = null, string objectId = null)
	{
		this.duration = 0.5f;
		this.t = 0f;
		this.missileVfx.transform.localPosition = new Vector3(0f, 900f, 0f);
		if (this._rocketTexture == null)
		{
			this._rocketTexture = SpriteManager.GetTextureInfo(this.rocketTexture, true);
		}
		this.rocketRenderer.textureInfo = null;
		this.rocketRenderer.shader = SpriteManager.GetShaderMode(0);
		this.rocketRenderer.textureInfo = this._rocketTexture;
		this.rocketRenderer.backcolor = new Color(0f, 0f, 0f, 0f);
		this.rocketRenderer.color = new Color(0f, 1f, 1f, 1f);
		this.rocketRenderer.detailcolor = new Color(1f, 0f, 0f, 1f);
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x0004AE4D File Offset: 0x0004904D
	public void Stop()
	{
	}

	// Token: 0x040007AF RID: 1967
	public GameObject missileVfx;

	// Token: 0x040007B0 RID: 1968
	public ex3DSprite2 rocketRenderer;

	// Token: 0x040007B1 RID: 1969
	private exTextureInfo _rocketTexture;

	// Token: 0x040007B2 RID: 1970
	public string rocketTexture = "Assets_Content_Textures_Combat3C_rocket.png";

	// Token: 0x040007B3 RID: 1971
	public float t;

	// Token: 0x040007B4 RID: 1972
	public float duration = 1f;
}
