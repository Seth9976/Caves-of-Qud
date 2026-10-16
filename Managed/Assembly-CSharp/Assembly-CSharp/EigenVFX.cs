using System;
using ConsoleLib.Console;
using UnityEngine;

// Token: 0x0200011B RID: 283
[ExecuteInEditMode]
public class EigenVFX : BaseMissileWeaponVFX
{
	// Token: 0x06000B19 RID: 2841 RVA: 0x00047FC8 File Offset: 0x000461C8
	public override void OnUpdate(float t, float duration)
	{
		if (t > duration)
		{
			base.gameObject.SetActive(false);
			return;
		}
		float num = duration * 0.9f;
		if (t >= duration - num)
		{
			this.Right.startWidth = 20f * (duration - t) / num;
			this.Left.startWidth = 10f * (duration - t) / num;
			this.Impact.emission.rateOverTimeMultiplier = 50f * (duration - t) / num;
		}
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x00048040 File Offset: 0x00046240
	public Color ParseColor(string Value)
	{
		Color color = default(Color);
		if (!Value.IsNullOrEmpty())
		{
			if (Value[0] == '#')
			{
				global::UnityEngine.ColorUtility.TryParseHtmlString(Value, out color);
			}
			else
			{
				ConsoleLib.Console.ColorUtility.ColorMap.TryGetValue(Value[0], out color);
			}
		}
		return color;
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x00048088 File Offset: 0x00046288
	public override void configure(MissileWeaponVFXConfiguration configuration, int path, ref float duration)
	{
		MissileWeaponVFXConfiguration.MissileVFXPathDefinition missileVFXPathDefinition = configuration.paths[path];
		base.configure(configuration, path, ref duration);
		if (missileVFXPathDefinition.first != null && missileVFXPathDefinition.last != null)
		{
			duration = 0.25f;
			Vector3 vector = GameManager.Instance.getTileCenter(missileVFXPathDefinition.first.X, missileVFXPathDefinition.first.Y, 0) + new Vector3(0f, 4f);
			Vector3 tileCenter = GameManager.Instance.getTileCenter(missileVFXPathDefinition.last.X, missileVFXPathDefinition.last.Y, 0);
			base.transform.position = vector;
			this.Right.transform.position = tileCenter;
			this.Left.transform.position = tileCenter;
			if (this.Source)
			{
				this.Source.transform.position = vector;
			}
			if (this.Impact)
			{
				this.Impact.transform.position = tileCenter;
				this.Impact.emission.rateOverTimeMultiplier = 300f;
			}
			string text;
			if (missileVFXPathDefinition.projectileVFXConfiguration.TryGetValue("Color", out text))
			{
				this.Right.endColor = (this.Right.startColor = (this.Left.endColor = (this.Left.startColor = this.ParseColor(text))));
			}
			if (missileVFXPathDefinition.projectileVFXConfiguration.TryGetValue("LeftColor", out text))
			{
				this.Left.endColor = (this.Left.startColor = this.ParseColor(text));
			}
			if (missileVFXPathDefinition.projectileVFXConfiguration.TryGetValue("RightColor", out text))
			{
				this.Right.endColor = (this.Right.startColor = this.ParseColor(text));
			}
			if (missileVFXPathDefinition.projectileVFXConfiguration.TryGetValue("StartColor", out text))
			{
				this.Right.startColor = (this.Left.startColor = this.ParseColor(text));
			}
			if (missileVFXPathDefinition.projectileVFXConfiguration.TryGetValue("EndColor", out text))
			{
				this.Right.endColor = (this.Left.endColor = this.ParseColor(text));
			}
			if (missileVFXPathDefinition.projectileVFXConfiguration.TryGetValue("Tessellation", out text))
			{
				this.Tessellation = float.Parse(text);
			}
			if (missileVFXPathDefinition.projectileVFXConfiguration.TryGetValue("Width", out text))
			{
				this.Width = float.Parse(text);
			}
			float num = Vector3.Distance(vector, tileCenter) / 1280f;
			int num2 = Math.Max(3, Mathf.RoundToInt(this.Tessellation * num));
			this.Right.positionCount = num2;
			this.Left.positionCount = num2;
			Vector3 normalized = (tileCenter - vector).normalized;
			Quaternion quaternion = Quaternion.Euler(0f, 0f, Mathf.Atan2(normalized.y, normalized.x) * 57.29578f - 90f);
			int i = 0;
			int num3 = num2 - 1;
			while (i <= num3)
			{
				Vector3 vector2 = Vector3.Lerp(vector, tileCenter, (float)i / ((float)num2 - 1f));
				Vector3 vector3 = Vector3.zero;
				if (i > 0 && i < num3)
				{
					vector3 = ((i % 2 != 0) ? (quaternion * new Vector3(8f, 0f, 0f)) : (quaternion * new Vector3(-8f, 0f, 0f)));
				}
				this.Right.SetPosition(i, vector2 + vector3);
				this.Left.SetPosition(i, vector2 - vector3);
				i++;
			}
			if (path != 0)
			{
				if (this.Source)
				{
					this.Source.SetActive(false);
				}
				if (this.SourceBlaze)
				{
					this.SourceBlaze.SetActive(false);
				}
			}
			else
			{
				if (this.Source)
				{
					this.Source.SetActive(true);
				}
				if (this.SourceBlaze)
				{
					this.SourceBlaze.SetActive(true);
				}
			}
			this.Right.startWidth = 3f;
			this.Right.material = Resources.Load<Material>("Prefabs/MissileWeaponsEffects/BeamCoreMat");
			this.Left.startWidth = 3f;
			this.Left.material = Resources.Load<Material>("Prefabs/MissileWeaponsEffects/BeamCoreMat");
		}
	}

	// Token: 0x0400075C RID: 1884
	public LineRenderer Left;

	// Token: 0x0400075D RID: 1885
	public LineRenderer Right;

	// Token: 0x0400075E RID: 1886
	public GameObject Source;

	// Token: 0x0400075F RID: 1887
	public GameObject SourceBlaze;

	// Token: 0x04000760 RID: 1888
	public ParticleSystem Impact;

	// Token: 0x04000761 RID: 1889
	public GameObject ImpactBlaze;

	// Token: 0x04000762 RID: 1890
	public float Tessellation;

	// Token: 0x04000763 RID: 1891
	public float Width = 8f;
}
