using System;
using Kobold;
using UnityEngine;

// Token: 0x0200011E RID: 286
[ExecuteInEditMode]
public class arrow_renderer : BaseMissileWeaponVFX
{
	// Token: 0x06000B2E RID: 2862 RVA: 0x00049BA0 File Offset: 0x00047DA0
	public override void configure(MissileWeaponVFXConfiguration configuration, int path, ref float duration)
	{
		if (this._arrowTexture == null)
		{
			this._arrowTexture = SpriteManager.GetTextureInfo(this.arrowTexture, true);
		}
		if (this._impactTexture == null)
		{
			this._impactTexture = SpriteManager.GetTextureInfo(this.impactTexture, true);
		}
		this.arrowObject.SetActive(true);
		base.configure(configuration, path, ref duration);
		this.trailRenderer.Clear();
		this.arrowRenderer.textureInfo = null;
		this.arrowRenderer.shader = SpriteManager.GetShaderMode(0);
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
			this.delay = (float)path * 0.02f;
			this.dur += this.delay;
			base.transform.position = this.startPos;
			this.trailRenderer.Clear();
			if (this.dur + this.stickDur > duration)
			{
				duration = this.dur + this.stickDur;
			}
			this.arrowRenderer.textureInfo = this._arrowTexture;
			this.arrowRenderer.backcolor = new Color(0f, 0f, 0f, 0f);
			Color color;
			if (configuration.paths[path].getConfigValue("shaftColor", null) != null && ColorUtility.TryParseHtmlString(configuration.paths[path].getConfigValue("shaftColor", null), out color))
			{
				this._shaftColor = color;
			}
			Color color2;
			if (configuration.paths[path].getConfigValue("headColor", null) != null && ColorUtility.TryParseHtmlString(configuration.paths[path].getConfigValue("headColor", null), out color2))
			{
				this._headColor = color2;
			}
			Color color3;
			if (configuration.paths[path].getConfigValue("trailColor", null) != null && ColorUtility.TryParseHtmlString(configuration.paths[path].getConfigValue("trailColor", null), out color3))
			{
				this.trailRenderer.startColor = color3;
			}
		}
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x00049EF7 File Offset: 0x000480F7
	public override void pool()
	{
		this.trailRenderer.Clear();
		this.arrowObject.SetActive(false);
		base.pool();
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x00049F18 File Offset: 0x00048118
	public override void OnUpdate(float t, float duration)
	{
		if (t <= this.dur && this.arrowObject.activeInHierarchy)
		{
			base.transform.position = this.endPos;
			this.arrowRenderer.detailcolor = this._shaftColor;
			this.arrowRenderer.color = this._headColor;
		}
		else if (t <= this.dur + this.stickDur && this.arrowObject.activeInHierarchy)
		{
			base.transform.position = this.endPos;
			float num = (t - this.dur) % this.wobbleFrequency / this.wobbleFrequency * 6.2831855f;
			float num2 = (t - this.dur) / this.stickDur;
			base.transform.rotation = Quaternion.Euler(0f, 0f, this.originalRotation + Mathf.Lerp(this.wobbleAmplitude * Mathf.Sin(num), 0f, num2));
			if (this.arrowRenderer.textureInfo != this._impactTexture)
			{
				this.arrowRenderer.textureInfo = this._impactTexture;
				this.arrowRenderer.detailcolor = this._shaftColor;
				this.arrowRenderer.color = this._headColor;
			}
			this.trailRenderer.Clear();
		}
		else if (t > this.dur + this.stickDur)
		{
			this.trailRenderer.Clear();
			this.arrowObject.SetActive(false);
			base.gameObject.SetActive(false);
		}
		if (t > this.delay)
		{
			if (!this.arrowObject.activeInHierarchy)
			{
				this.arrowObject.SetActive(true);
				return;
			}
			base.transform.position = Vector3.Lerp(this.startPos, this.endPos, (t - this.delay) / (this.dur - this.delay));
		}
	}

	// Token: 0x04000785 RID: 1925
	public GameObject arrowObject;

	// Token: 0x04000786 RID: 1926
	public ex3DSprite2 arrowRenderer;

	// Token: 0x04000787 RID: 1927
	public TrailRenderer trailRenderer;

	// Token: 0x04000788 RID: 1928
	private exTextureInfo _arrowTexture;

	// Token: 0x04000789 RID: 1929
	public string arrowTexture;

	// Token: 0x0400078A RID: 1930
	private exTextureInfo _impactTexture;

	// Token: 0x0400078B RID: 1931
	public string impactTexture;

	// Token: 0x0400078C RID: 1932
	private Vector3 startPos;

	// Token: 0x0400078D RID: 1933
	private Vector3 endPos;

	// Token: 0x0400078E RID: 1934
	public float dur;

	// Token: 0x0400078F RID: 1935
	public float stickDur;

	// Token: 0x04000790 RID: 1936
	public float delay;

	// Token: 0x04000791 RID: 1937
	public float originalRotation;

	// Token: 0x04000792 RID: 1938
	public float wobbleAmplitude = 10f;

	// Token: 0x04000793 RID: 1939
	public float wobbleFrequency = 0.33f;

	// Token: 0x04000794 RID: 1940
	private Color _headColor;

	// Token: 0x04000795 RID: 1941
	private Color _shaftColor;
}
