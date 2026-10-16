using System;
using UnityEngine;

// Token: 0x02000122 RID: 290
[ExecuteInEditMode]
public class slug_renderer : BaseMissileWeaponVFX
{
	// Token: 0x06000B3B RID: 2875 RVA: 0x0004AA00 File Offset: 0x00048C00
	public override void configure(MissileWeaponVFXConfiguration configuration, int path, ref float duration)
	{
		base.configure(configuration, path, ref duration);
		this.slugObject.SetActive(false);
		this.trailRenderer.Clear();
		if (configuration.paths[path].first != null && configuration.paths[path].last != null)
		{
			this.startPos = GameManager.Instance.getTileCenter(configuration.paths[path].first.X, configuration.paths[path].first.Y, 0);
			this.endPos = GameManager.Instance.getTileCenter(configuration.paths[path].last.X, configuration.paths[path].last.Y, 0);
			float num = 0.02f;
			this.dur = Vector3.Distance(this.startPos, this.endPos) / 24f * num;
			this.delay = (float)path * 0.02f;
			this.dur += this.delay;
			base.transform.position = this.startPos;
			this.trailRenderer.Clear();
			if (this.dur > duration)
			{
				duration = this.dur;
			}
			Color color;
			if (configuration.paths[path].getConfigValue("slugColor", null) != null && ColorUtility.TryParseHtmlString(configuration.paths[path].getConfigValue("slugColor", null), out color))
			{
				this.slugRenderer.color = color;
			}
			Color color2;
			if (configuration.paths[path].getConfigValue("trailColor", null) != null && ColorUtility.TryParseHtmlString(configuration.paths[path].getConfigValue("trailColor", null), out color2))
			{
				this.trailRenderer.startColor = color2;
			}
		}
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x0004ABDD File Offset: 0x00048DDD
	public override void pool()
	{
		this.trailRenderer.Clear();
		this.slugObject.SetActive(false);
		base.pool();
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x0004ABFC File Offset: 0x00048DFC
	public override void OnUpdate(float t, float duration)
	{
		if (t >= this.dur && this.slugObject.activeInHierarchy)
		{
			base.transform.position = this.endPos;
		}
		else if (t > this.dur)
		{
			this.trailRenderer.Clear();
			this.slugObject.SetActive(false);
			base.gameObject.SetActive(false);
		}
		if (t > this.delay)
		{
			if (!this.slugObject.activeInHierarchy)
			{
				this.slugObject.SetActive(true);
				return;
			}
			base.transform.position = Vector3.Lerp(this.startPos, this.endPos, (t - this.delay) / (this.dur - this.delay));
		}
	}

	// Token: 0x040007A8 RID: 1960
	public GameObject slugObject;

	// Token: 0x040007A9 RID: 1961
	public SpriteRenderer slugRenderer;

	// Token: 0x040007AA RID: 1962
	public TrailRenderer trailRenderer;

	// Token: 0x040007AB RID: 1963
	private Vector3 startPos;

	// Token: 0x040007AC RID: 1964
	private Vector3 endPos;

	// Token: 0x040007AD RID: 1965
	public float dur;

	// Token: 0x040007AE RID: 1966
	public float delay;
}
