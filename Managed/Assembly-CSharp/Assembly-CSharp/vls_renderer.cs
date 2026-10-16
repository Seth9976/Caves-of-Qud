using System;
using Kobold;
using UnityEngine;

// Token: 0x02000124 RID: 292
[ExecuteInEditMode]
public class vls_renderer : BaseMissileWeaponVFX
{
	// Token: 0x06000B43 RID: 2883 RVA: 0x0004AE70 File Offset: 0x00049070
	public override void configure(MissileWeaponVFXConfiguration configuration, int path, ref float duration)
	{
		this.missileVfx.transform.localPosition = new Vector3(0f, 0f, 0f);
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
		base.configure(configuration, path, ref duration);
		if (configuration.paths[path].first != null && configuration.paths[path].last != null)
		{
			base.transform.position = GameManager.Instance.getTileCenter(configuration.paths[path].first.X, configuration.paths[path].first.Y, 0) + new Vector3(0f, 4f);
			if (this.sourceVfx != null)
			{
				this.sourceVfx.transform.position = GameManager.Instance.getTileCenter(configuration.paths[path].first.X, configuration.paths[path].first.Y, 0) + new Vector3(0f, 4f);
			}
			if (this.impactVfx != null)
			{
				this.impactVfx.transform.position = GameManager.Instance.getTileCenter(configuration.paths[path].last.X, configuration.paths[path].last.Y, 0);
			}
			foreach (LineRenderer lineRenderer in this.beamLayers)
			{
				lineRenderer.transform.position = GameManager.Instance.getTileCenter(configuration.paths[path].last.X, configuration.paths[path].last.Y, 0);
				lineRenderer.GetComponent<LineRenderer>().SetPosition(0, GameManager.Instance.getTileCenter(configuration.paths[path].first.X, configuration.paths[path].first.Y, 0) + new Vector3(0f, 4f));
				lineRenderer.GetComponent<LineRenderer>().SetPosition(1, GameManager.Instance.getTileCenter(configuration.paths[path].last.X, configuration.paths[path].last.Y, 0));
			}
			MissileWeaponVFXConfiguration.MissileVFXPathDefinition missileVFXPathDefinition = configuration.paths[path];
			if (missileVFXPathDefinition.projectileVFXConfiguration == null)
			{
				missileVFXPathDefinition.projectileVFXConfiguration = "duration::0.25;;beamColor0::#FF0000;;beamColor1::#FF8000;;beamColor2::#FFFFFF".CachedDictionaryExpansion();
			}
			Color color;
			if (configuration.paths[path].getConfigValue("beamColor0", null) != null && ColorUtility.TryParseHtmlString(configuration.paths[path].getConfigValue("beamColor0", null), out color) && this.beamLayers.Length >= 1)
			{
				this.beamLayers[0].startColor = color;
				this.beamLayers[0].endColor = color;
			}
			Color color2;
			if (configuration.paths[path].getConfigValue("beamColor1", null) != null && ColorUtility.TryParseHtmlString(configuration.paths[path].getConfigValue("beamColor1", null), out color2) && this.beamLayers.Length >= 2)
			{
				this.beamLayers[1].startColor = color2;
				this.beamLayers[1].endColor = color2;
			}
			Color color3;
			if (configuration.paths[path].getConfigValue("beamColor2", null) != null && ColorUtility.TryParseHtmlString(configuration.paths[path].getConfigValue("beamColor2", null), out color3) && this.beamLayers.Length >= 3)
			{
				this.beamLayers[2].startColor = color3;
				this.beamLayers[2].endColor = color3;
			}
		}
	}

	// Token: 0x040007B5 RID: 1973
	public LineRenderer[] beamLayers;

	// Token: 0x040007B6 RID: 1974
	public GameObject sourceVfx;

	// Token: 0x040007B7 RID: 1975
	public GameObject impactVfx;

	// Token: 0x040007B8 RID: 1976
	public GameObject missileVfx;

	// Token: 0x040007B9 RID: 1977
	public ex3DSprite2 rocketRenderer;

	// Token: 0x040007BA RID: 1978
	private exTextureInfo _rocketTexture;

	// Token: 0x040007BB RID: 1979
	public string rocketTexture = "Assets_Content_Textures_Combat3C_rocket.png";
}
