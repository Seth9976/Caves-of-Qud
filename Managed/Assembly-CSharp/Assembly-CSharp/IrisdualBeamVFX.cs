using System;
using ConsoleLib.Console;
using UnityEngine;
using XRL;

// Token: 0x0200011C RID: 284
[ExecuteInEditMode]
public class IrisdualBeamVFX : BaseMissileWeaponVFX
{
	// Token: 0x06000B1D RID: 2845 RVA: 0x00048518 File Offset: 0x00046718
	public override void OnUpdate(float t, float duration)
	{
		if (t > duration)
		{
			base.gameObject.SetActive(false);
			return;
		}
		int i = 0;
		int num = this.EvaluatedKeys.Length;
		while (i < num)
		{
			this.EvaluatedKeys[num - 1 - i].color = IrisdualBeamVFX.Gradient.Evaluate(Mathf.Abs(Mathf.Sin((float)i / 7f + t * 2f)));
			i++;
		}
		this.Evaluated.SetKeys(this.EvaluatedKeys, this.AlphaKeys);
		this.Fringe.colorGradient = this.Evaluated;
		this.Fringe.material.mainTextureOffset = new Vector2(-t * 2f, 0f);
		this.Core.material.mainTextureOffset = new Vector2(-t * 2f, 0f);
		float num2 = t + duration / 4f;
		int j = 0;
		int num3 = this.EvaluatedKeys.Length;
		while (j < num3)
		{
			this.EvaluatedKeys[num3 - 1 - j].color = IrisdualBeamVFX.Gradient.Evaluate(Mathf.Abs(Mathf.Sin((float)j / 7f + num2 * 2f))) + Color.gray;
			j++;
		}
		this.Evaluated.SetKeys(this.EvaluatedKeys, this.AlphaKeys);
		this.Core.colorGradient = this.Evaluated;
		float num4 = duration * 0.25f;
		if (t >= duration - num4)
		{
			this.Fringe.startWidth = 20f * (duration - t) / num4;
			this.Core.startWidth = 10f * (duration - t) / num4;
			this.Impact.emission.rateOverTimeMultiplier = 50f * (duration - t) / num4;
		}
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x000486DC File Offset: 0x000468DC
	public override void configure(MissileWeaponVFXConfiguration configuration, int path, ref float duration)
	{
		ConsoleLib.Console.ColorUtility.ColorCollection color = The.Color;
		MissileWeaponVFXConfiguration.MissileVFXPathDefinition missileVFXPathDefinition = configuration.paths[path];
		if (IrisdualBeamVFX.Gradient == null)
		{
			IrisdualBeamVFX.GradientKeys = new GradientColorKey[]
			{
				new GradientColorKey(color.Red, 0f),
				new GradientColorKey(color.Orange, 0.14f),
				new GradientColorKey(color.Yellow, 0.28f),
				new GradientColorKey(color.Green, 0.43f),
				new GradientColorKey(color.Cyan, 0.57f),
				new GradientColorKey(color.Blue, 0.71f),
				new GradientColorKey(color.Magenta, 0.86f),
				new GradientColorKey(color.Red, 1f)
			};
			IrisdualBeamVFX.Gradient = new Gradient();
			IrisdualBeamVFX.Gradient.SetKeys(IrisdualBeamVFX.GradientKeys, this.AlphaKeys);
		}
		base.configure(configuration, path, ref duration);
		if (missileVFXPathDefinition.first != null && missileVFXPathDefinition.last != null)
		{
			Vector3 vector = GameManager.Instance.getTileCenter(missileVFXPathDefinition.first.X, missileVFXPathDefinition.first.Y, 0) + new Vector3(0f, 4f);
			Vector3 tileCenter = GameManager.Instance.getTileCenter(missileVFXPathDefinition.last.X, missileVFXPathDefinition.last.Y, 0);
			base.transform.position = vector;
			this.Fringe.transform.position = tileCenter;
			this.Core.transform.position = tileCenter;
			if (this.Source)
			{
				this.Source.transform.position = vector;
			}
			if (this.Impact)
			{
				this.Impact.transform.position = tileCenter;
				this.Impact.emission.rateOverTimeMultiplier = 300f;
			}
			this.Fringe.positionCount = 8;
			this.Core.positionCount = 8;
			for (int i = 0; i < 8; i++)
			{
				this.Fringe.SetPosition(i, Vector3.Lerp(vector, tileCenter, (float)i / 7f));
				this.Core.SetPosition(i, Vector3.Lerp(vector, tileCenter, (float)i / 7f));
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
			if (this.Evaluated == null)
			{
				this.Evaluated = new Gradient();
			}
			this.Evaluated.mode = GradientMode.Blend;
			if (this.EvaluatedKeys == null)
			{
				this.EvaluatedKeys = new GradientColorKey[8];
			}
			Array.Copy(IrisdualBeamVFX.GradientKeys, this.EvaluatedKeys, 8);
			this.Evaluated.SetKeys(this.EvaluatedKeys, this.AlphaKeys);
			this.Fringe.colorGradient = this.Evaluated;
			this.Fringe.startWidth = 20f;
			this.Fringe.material = Resources.Load<Material>("Prefabs/MissileWeaponsEffects/BeamFringeMat");
			this.Core.colorGradient = this.Evaluated;
			this.Core.startWidth = 10f;
			this.Core.material = Resources.Load<Material>("Prefabs/MissileWeaponsEffects/BeamCoreMat");
		}
	}

	// Token: 0x04000764 RID: 1892
	public LineRenderer Core;

	// Token: 0x04000765 RID: 1893
	public LineRenderer Fringe;

	// Token: 0x04000766 RID: 1894
	public GameObject Source;

	// Token: 0x04000767 RID: 1895
	public GameObject SourceBlaze;

	// Token: 0x04000768 RID: 1896
	public ParticleSystem Impact;

	// Token: 0x04000769 RID: 1897
	public GameObject ImpactBlaze;

	// Token: 0x0400076A RID: 1898
	public Gradient Evaluated;

	// Token: 0x0400076B RID: 1899
	public GradientColorKey[] EvaluatedKeys;

	// Token: 0x0400076C RID: 1900
	public GradientAlphaKey[] AlphaKeys = new GradientAlphaKey[]
	{
		new GradientAlphaKey(1f, 0f),
		new GradientAlphaKey(1f, 1f)
	};

	// Token: 0x0400076D RID: 1901
	public static Gradient Gradient;

	// Token: 0x0400076E RID: 1902
	public static GradientColorKey[] GradientKeys;
}
