using System;
using Kobold;
using UnityEngine;

// Token: 0x02000121 RID: 289
[ExecuteInEditMode]
public class rocket_renderer : BaseMissileWeaponVFX
{
	// Token: 0x06000B37 RID: 2871 RVA: 0x0004A678 File Offset: 0x00048878
	public override void configure(MissileWeaponVFXConfiguration configuration, int path, ref float duration)
	{
		if (this._rocketTexture == null)
		{
			this._rocketTexture = SpriteManager.GetTextureInfo(this.rocketTexture, true);
		}
		this.rocketObject.SetActive(true);
		base.configure(configuration, path, ref duration);
		this.rocketRenderer.textureInfo = null;
		this.rocketRenderer.shader = SpriteManager.GetShaderMode(0);
		if (configuration.paths[path].first != null && configuration.paths[path].last != null)
		{
			this.startPos = GameManager.Instance.getTileCenter(configuration.paths[path].first.X, configuration.paths[path].first.Y, 0);
			this.endPos = GameManager.Instance.getTileCenter(configuration.paths[path].last.X, configuration.paths[path].last.Y, 0);
			this.endPos += new Vector3((float)global::UnityEngine.Random.Range(-4, 4), (float)global::UnityEngine.Random.Range(-6, 6));
			if (this.startPos != this.endPos)
			{
				Vector3 normalized = (this.endPos - this.startPos).normalized;
				this.originalRotation = Mathf.Atan2(normalized.y, normalized.x) * 57.29578f - 90f;
			}
			base.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, this.originalRotation);
			float num = 0.02f;
			this.dur = Vector3.Distance(this.startPos, this.endPos) / 24f * num;
			base.transform.position = this.startPos;
			duration = this.dur;
			this.rocketRenderer.textureInfo = this._rocketTexture;
			this.rocketRenderer.backcolor = new Color(0f, 0f, 0f, 0f);
			Color color;
			if (configuration.paths[path].getConfigValue("bodyColor", null) != null && ColorUtility.TryParseHtmlString(configuration.paths[path].getConfigValue("bodyColor", null), out color))
			{
				this._bodyColor = color;
			}
			Color color2;
			if (configuration.paths[path].getConfigValue("stripeColor", null) != null && ColorUtility.TryParseHtmlString(configuration.paths[path].getConfigValue("stripeColor", null), out color2))
			{
				this._stripeColor = color2;
			}
		}
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x0004A91A File Offset: 0x00048B1A
	public override void pool()
	{
		this.rocketObject.SetActive(false);
		base.pool();
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x0004A930 File Offset: 0x00048B30
	public override void OnUpdate(float t, float duration)
	{
		if (t <= this.dur && this.rocketObject.activeInHierarchy)
		{
			base.transform.position = this.endPos;
			this.rocketRenderer.detailcolor = this._bodyColor;
			this.rocketRenderer.color = this._stripeColor;
		}
		else
		{
			this.rocketObject.SetActive(false);
			base.gameObject.SetActive(false);
		}
		if (t > this.delay)
		{
			if (!this.rocketObject.activeInHierarchy)
			{
				this.rocketObject.SetActive(true);
				return;
			}
			base.transform.position = Vector3.Lerp(this.startPos, this.endPos, (t - this.delay) / (this.dur - this.delay));
		}
	}

	// Token: 0x0400079D RID: 1949
	public GameObject rocketObject;

	// Token: 0x0400079E RID: 1950
	public ex3DSprite2 rocketRenderer;

	// Token: 0x0400079F RID: 1951
	private exTextureInfo _rocketTexture;

	// Token: 0x040007A0 RID: 1952
	public string rocketTexture;

	// Token: 0x040007A1 RID: 1953
	private Vector3 startPos;

	// Token: 0x040007A2 RID: 1954
	private Vector3 endPos;

	// Token: 0x040007A3 RID: 1955
	public float dur;

	// Token: 0x040007A4 RID: 1956
	public float delay;

	// Token: 0x040007A5 RID: 1957
	public float originalRotation;

	// Token: 0x040007A6 RID: 1958
	private Color _bodyColor;

	// Token: 0x040007A7 RID: 1959
	private Color _stripeColor;
}
