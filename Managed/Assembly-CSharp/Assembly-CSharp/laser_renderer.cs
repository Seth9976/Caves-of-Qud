using System;
using UnityEngine;

// Token: 0x02000120 RID: 288
[ExecuteInEditMode]
public class laser_renderer : BaseMissileWeaponVFX
{
	// Token: 0x06000B35 RID: 2869 RVA: 0x0004A2CC File Offset: 0x000484CC
	public override void configure(MissileWeaponVFXConfiguration configuration, int path, ref float duration)
	{
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

	// Token: 0x0400079A RID: 1946
	public LineRenderer[] beamLayers;

	// Token: 0x0400079B RID: 1947
	public GameObject sourceVfx;

	// Token: 0x0400079C RID: 1948
	public GameObject impactVfx;
}
