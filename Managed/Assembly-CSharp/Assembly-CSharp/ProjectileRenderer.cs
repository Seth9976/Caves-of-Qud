using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Kobold;
using UnityEngine;

// Token: 0x0200011D RID: 285
[ExecuteInEditMode]
public class ProjectileRenderer : BaseMissileWeaponVFX
{
	// Token: 0x06000B20 RID: 2848 RVA: 0x00048AC0 File Offset: 0x00046CC0
	public void LoadFields(Dictionary<string, string> Values)
	{
		string text;
		if (this.TryGetInheritable(Values, "Tile", "RenderTile", out text))
		{
			this.Projectile.textureInfo = SpriteManager.GetTextureInfo(text, true);
			this.Projectile.shader = SpriteManager.GetShaderMode(this.Projectile.textureInfo.ShaderMode);
		}
		if (this.TryGetInheritable(Values, "Foreground", "RenderForeground", out text))
		{
			this.Projectile.color = (this.Foreground = this.ParseColor(text));
		}
		if (this.TryGetInheritable(Values, "Detail", "RenderDetail", out text))
		{
			this.Projectile.detailcolor = (this.Detail = this.ParseColor(text));
		}
		if (Values.TryGetValue("Orientation", out text))
		{
			this.Orientation = this.ParseSingle(text, ',');
		}
		if (Values.TryGetValue("Rotation", out text))
		{
			this.Rotation = this.ParseSingle(text, ',');
		}
		if (Values.TryGetValue("Scale", out text))
		{
			this.Scale = Vector3.one * this.ParseSingle(text, ',');
		}
		if (this.TryGetInheritable(Values, "Flip", "RenderFlip", out text))
		{
			char c = char.ToUpperInvariant(text[0]);
			if (c == 'H')
			{
				this.Scale = new Vector3(this.Scale.x * -1f, this.Scale.y, this.Scale.z);
			}
			else if (c == 'V')
			{
				this.Scale = new Vector3(this.Scale.x, this.Scale.y * -1f, this.Scale.z);
			}
			else if (c == 'B')
			{
				this.Scale = new Vector3(this.Scale.x * -1f, this.Scale.y * -1f, this.Scale.z);
			}
		}
		if (Values.TryGetValue("Speed", out text))
		{
			this.Speed = this.ParseSingle(text, ',');
		}
		if (Values.TryGetValue("Rate", out text))
		{
			this.Rate = this.ParseSingle(text, ',');
		}
		if (Values.TryGetValue("Arc", out text))
		{
			this.Arc = this.ParseSingle(text, ',');
		}
		if (Values.TryGetValue("Slant", out text))
		{
			string text2 = text.ToLowerInvariant();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
			if (num <= 890022063U)
			{
				if (num <= 231893180U)
				{
					if (num != 184981848U)
					{
						if (num != 231893180U)
						{
							goto IL_039F;
						}
						if (!(text2 == "absolute"))
						{
							goto IL_039F;
						}
						this.Slant = 2;
						goto IL_039F;
					}
					else if (!(text2 == "false"))
					{
						goto IL_039F;
					}
				}
				else if (num != 873244444U)
				{
					if (num != 890022063U)
					{
						goto IL_039F;
					}
					if (!(text2 == "0"))
					{
						goto IL_039F;
					}
				}
				else
				{
					if (!(text2 == "1"))
					{
						goto IL_039F;
					}
					goto IL_037D;
				}
			}
			else if (num <= 2166136261U)
			{
				if (num != 1303515621U)
				{
					if (num != 2166136261U)
					{
						goto IL_039F;
					}
					if (text2 == null)
					{
						goto IL_039F;
					}
					if (text2.Length != 0)
					{
						goto IL_039F;
					}
				}
				else
				{
					if (!(text2 == "true"))
					{
						goto IL_039F;
					}
					goto IL_037D;
				}
			}
			else if (num != 2252008588U)
			{
				if (num != 2268786207U)
				{
					goto IL_039F;
				}
				if (!(text2 == "absolutey"))
				{
					goto IL_039F;
				}
				this.Slant = 4;
				goto IL_039F;
			}
			else
			{
				if (!(text2 == "absolutex"))
				{
					goto IL_039F;
				}
				this.Slant = 3;
				goto IL_039F;
			}
			this.Slant = 0;
			goto IL_039F;
			IL_037D:
			this.Slant = 1;
		}
		IL_039F:
		if (Values.TryGetValue("Curve", out text))
		{
			this.Curve = this.ParseSingle(text, ',');
		}
		if (Values.TryGetValue("Easing", out text))
		{
			Easing.Functions functions;
			if (!(text == "SineIn"))
			{
				if (!(text == "SineOut"))
				{
					if (!(text == "SineInOut"))
					{
						functions = Easing.Functions.SineEaseInOut;
					}
					else
					{
						functions = Easing.Functions.SineEaseInOut;
					}
				}
				else
				{
					functions = Easing.Functions.SineEaseOut;
				}
			}
			else
			{
				functions = Easing.Functions.SineEaseIn;
			}
			this.Interpolation = functions;
		}
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x00048EE8 File Offset: 0x000470E8
	public void LoadTrail(Dictionary<string, string> Values)
	{
		string text;
		if (this.TryGetInheritable(Values, "TrailColor", "RenderForeground", out text))
		{
			this.Trail.startColor = this.ParseColor(text);
			this.Trail.endColor = this.Trail.startColor.WithAlpha(0f);
		}
		if (this.TryGetInheritable(Values, "TrailStartColor", "RenderForeground", out text))
		{
			this.Trail.startColor = this.ParseColor(text);
		}
		if (this.TryGetInheritable(Values, "TrailEndColor", "RenderDetail", out text))
		{
			this.Trail.endColor = this.ParseColor(text);
			if (!text.IsNullOrEmpty() && (text[0] != '#' || text.Length != 9))
			{
				this.Trail.endColor = this.Trail.endColor.WithAlpha(0f);
			}
		}
		if (Values.TryGetValue("TrailWidth", out text))
		{
			this.Trail.startWidth = this.ParseSingle(text, ',');
			this.Trail.endWidth = this.Trail.startWidth;
		}
		if (Values.TryGetValue("TrailStartWidth", out text))
		{
			this.Trail.startWidth = this.ParseSingle(text, ',');
		}
		if (Values.TryGetValue("TrailEndWidth", out text))
		{
			this.Trail.endWidth = this.ParseSingle(text, ',');
		}
		if (Values.TryGetValue("TrailOrigin", out text))
		{
			ReadOnlySpan<char> readOnlySpan;
			ReadOnlySpan<char> readOnlySpan2;
			text.AsDelimitedSpans(',', out readOnlySpan, out readOnlySpan2);
			float num;
			float num2;
			if (float.TryParse(readOnlySpan, out num) && float.TryParse(readOnlySpan2, out num2))
			{
				this.Trail.transform.localPosition = (this.TrailOrigin = new Vector3(num, num2, 10f));
			}
		}
		if (Values.TryGetValue("TrailLife", out text))
		{
			this.Trail.time = this.ParseSingle(text, ',');
			if (this.Trail.time <= 0f)
			{
				this.Trail.enabled = false;
			}
		}
		if (Values.TryGetValue("TrailOrientation", out text))
		{
			this.Trail.transform.localRotation = Quaternion.Euler(0f, 0f, this.ParseSingle(text, ','));
		}
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x00049130 File Offset: 0x00047330
	public void LoadParticles(Dictionary<string, string> Values)
	{
		ParticleSystem.MainModule main = this.Particles.main;
		ParticleSystem.ShapeModule shape = this.Particles.shape;
		ParticleSystem.ColorOverLifetimeModule colorOverLifetime = this.Particles.colorOverLifetime;
		ParticleSystem.EmissionModule emission = this.Particles.emission;
		string text;
		if (Values.TryGetValue("ParticleShape", out text) && text == "Rectangle")
		{
			shape.shapeType = ParticleSystemShapeType.Rectangle;
		}
		if (Values.TryGetValue("ParticleShapePosition", out text))
		{
			shape.position = this.ParseVector(text, ',', ';');
		}
		if (Values.TryGetValue("ParticleShapeRotation", out text))
		{
			shape.rotation = this.ParseVector(text, ',', ';');
		}
		if (Values.TryGetValue("ParticleShapeScale", out text))
		{
			shape.scale = this.ParseVector(text, ',', ';');
		}
		if (Values.TryGetValue("ParticleLife", out text))
		{
			float num = this.ParseSingle(text, ',');
			if (num > 0f)
			{
				main.startLifetime = new ParticleSystem.MinMaxCurve(num);
			}
		}
		if (Values.TryGetValue("ParticleRate", out text))
		{
			float num2 = this.ParseSingle(text, ',');
			if (num2 > 0f)
			{
				emission.rateOverTime = new ParticleSystem.MinMaxCurve(num2);
				this.ParticleDuration = 1f;
			}
		}
		if (Values.TryGetValue("ParticleGravity", out text))
		{
			main.gravityModifier = new ParticleSystem.MinMaxCurve(this.ParseSingle(text, ','));
		}
		if (Values.TryGetValue("ParticleSpherize", out text))
		{
			shape.sphericalDirectionAmount = this.ParseSingle(text, ',');
		}
		if (this.TryGetInheritable(Values, "ParticleColor", "RenderForeground", out text))
		{
			Color color = this.ParseColor(text);
			colorOverLifetime.color = new ParticleSystem.MinMaxGradient(color, color.WithAlpha(1f));
		}
		if (this.TryGetInheritable(Values, "ParticleStartColor", "RenderForeground", out text))
		{
			Color color2 = this.ParseColor(text);
			colorOverLifetime.color = new ParticleSystem.MinMaxGradient(color2, color2.WithAlpha(1f));
		}
		if (this.TryGetInheritable(Values, "ParticleEndColor", "RenderDetail", out text))
		{
			Color color3 = this.ParseColor(text);
			if (!text.IsNullOrEmpty() && (text[0] != '#' || text.Length != 9))
			{
				color3 = color3.WithAlpha(1f);
			}
			colorOverLifetime.color = new ParticleSystem.MinMaxGradient(colorOverLifetime.color.colorMin, color3);
		}
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x000493B0 File Offset: 0x000475B0
	public override void configure(MissileWeaponVFXConfiguration configuration, int path, ref float duration)
	{
		base.configure(configuration, path, ref duration);
		this.Object.SetActive(false);
		this.Reset();
		MissileWeaponVFXConfiguration.MissileVFXPathDefinition missileVFXPathDefinition = configuration.paths[path];
		if (missileVFXPathDefinition.first != null && missileVFXPathDefinition.last != null)
		{
			this.Start = GameManager.Instance.getTileCenter(missileVFXPathDefinition.first.X, missileVFXPathDefinition.first.Y, 0);
			this.End = GameManager.Instance.getTileCenter(missileVFXPathDefinition.last.X, missileVFXPathDefinition.last.Y, 0);
		}
		this.LoadFields(missileVFXPathDefinition.projectileVFXConfiguration);
		this.LoadTrail(missileVFXPathDefinition.projectileVFXConfiguration);
		this.LoadParticles(missileVFXPathDefinition.projectileVFXConfiguration);
		float num = Vector3.Distance(this.Start, this.End) / this.Speed;
		duration = num;
		Transform transform = base.transform;
		this.Projectile.transform.localPosition = new Vector3(0f, 0f, -10f);
		this.Delay = (float)path * (this.Rate / 1000f);
		duration += this.Delay;
		transform.position = this.Start;
		float num2 = this.Orientation;
		if (this.Start != this.End)
		{
			Vector3 normalized = (this.End - this.Start).normalized;
			num2 += Mathf.Atan2(normalized.y, normalized.x) * 57.29578f - 90f;
		}
		this.StartRotation = num2;
		this.EndRotation = num2 + this.Rotation * num;
		transform.localRotation = Quaternion.Euler(0f, 0f, num2);
		transform.localScale = this.Scale;
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x00049578 File Offset: 0x00047778
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
				if (Value == "Foreground")
				{
					return this.Foreground;
				}
				if (Value == "Detail")
				{
					return this.Detail;
				}
				ConsoleLib.Console.ColorUtility.ColorMap.TryGetValue(Value[0], out color);
			}
		}
		return color;
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x000495E8 File Offset: 0x000477E8
	public ParticleSystem.MinMaxCurve ParseCurve(ReadOnlySpan<char> Value)
	{
		return default(ParticleSystem.MinMaxCurve);
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x00049600 File Offset: 0x00047800
	public Vector3 ParseVector(ReadOnlySpan<char> Value, char SplitVector = ',', char SplitSingle = ';')
	{
		Vector3 vector = default(Vector3);
		if (Value.Length != 0)
		{
			ReadOnlySpan<char> readOnlySpan;
			ReadOnlySpan<char> readOnlySpan2;
			ReadOnlySpan<char> readOnlySpan3;
			Value.Split(',', out readOnlySpan, out readOnlySpan2, out readOnlySpan3);
			vector = new Vector3(this.ParseSingle(readOnlySpan, SplitSingle), this.ParseSingle(readOnlySpan2, SplitSingle), this.ParseSingle(readOnlySpan3, SplitSingle));
		}
		return vector;
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x0004964C File Offset: 0x0004784C
	public unsafe float ParseSingle(ReadOnlySpan<char> Value, char Split = ',')
	{
		float num = 0f;
		if (Value.Length != 0)
		{
			ReadOnlySpan<char> readOnlySpan;
			ReadOnlySpan<char> readOnlySpan2;
			Value.Split(Split, out readOnlySpan, out readOnlySpan2);
			float num2;
			float num3;
			if (readOnlySpan2.Length == 0)
			{
				if (*readOnlySpan[0] == 63)
				{
					float.TryParse(readOnlySpan.Slice(1), out num);
					if (global::UnityEngine.Random.value >= 0.5f)
					{
						num *= -1f;
					}
				}
				else
				{
					float.TryParse(readOnlySpan, out num);
				}
			}
			else if (float.TryParse(readOnlySpan, out num2) && float.TryParse(readOnlySpan2, out num3))
			{
				num = global::UnityEngine.Random.Range(num2, num3);
			}
		}
		return num;
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x000496D8 File Offset: 0x000478D8
	public bool TryGetInheritable(Dictionary<string, string> Values, string Key, string InheritKey, out string Value)
	{
		return Values.TryGetValue(Key, out Value) && (Value != "Inherit" || Values.TryGetValue(InheritKey, out Value));
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x00049704 File Offset: 0x00047904
	public void Reset()
	{
		this.Trail.Clear();
		this.Foreground = default(Color);
		this.Detail = default(Color);
		this.Speed = 0f;
		this.Orientation = 0f;
		this.StartRotation = 0f;
		this.EndRotation = 0f;
		this.Arc = 0f;
		this.Slant = 0;
		this.Curve = 0f;
		this.Interpolation = Easing.Functions.Linear;
		this.Trail.enabled = true;
		this.Trail.startColor = Color.white;
		this.Trail.endColor = Color.white.WithAlpha(0f);
		this.Trail.startWidth = (this.Trail.endWidth = 1f);
		this.Trail.transform.localPosition = default(Vector3);
		this.Trail.time = 0.1f;
		this.Particles.gameObject.SetActive(false);
		this.ParticleDuration = 0f;
		ParticleSystem.MainModule main = this.Particles.main;
		ParticleSystem.ShapeModule shape = this.Particles.shape;
		ParticleSystem.ColorOverLifetimeModule colorOverLifetime = this.Particles.colorOverLifetime;
		ParticleSystem.EmissionModule emission = this.Particles.emission;
		main.gravityModifier = 0f;
		emission.rateOverTime = 0f;
		colorOverLifetime.color = new ParticleSystem.MinMaxGradient(Color.white, Color.white.WithAlpha(0f));
		shape.shapeType = ParticleSystemShapeType.Rectangle;
		shape.position = Vector3.zero;
		shape.rotation = Vector3.zero;
		shape.scale = new Vector3(16f, 24f, 1f);
		shape.randomDirectionAmount = 0f;
		shape.sphericalDirectionAmount = 0.1f;
		shape.randomPositionAmount = 0f;
		base.transform.localScale = (this.Scale = Vector3.one);
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x0004990D File Offset: 0x00047B0D
	public override void pool()
	{
		this.Object.SetActive(false);
		this.Reset();
		base.pool();
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x00049928 File Offset: 0x00047B28
	public override void start()
	{
		base.start();
		if (this.ParticleDuration > 0f)
		{
			ParticleSystem.EmissionModule emission = this.Particles.emission;
			this.Particles.gameObject.SetActive(true);
			if (!this.Particles.isStopped)
			{
				this.Particles.Stop();
			}
			this.Particles.Play();
			emission.enabled = true;
		}
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x00049990 File Offset: 0x00047B90
	public override void OnUpdate(float t, float duration)
	{
		this.Projectile.color = this.Foreground;
		this.Projectile.detailcolor = this.Detail;
		this.Projectile.backcolor = Color.clear;
		if (t >= duration && this.Object.activeInHierarchy)
		{
			base.transform.position = this.End;
		}
		else if (t > duration)
		{
			this.Trail.Clear();
			this.Particles.gameObject.SetActive(false);
			this.Object.SetActive(false);
			base.gameObject.SetActive(false);
		}
		if (t > this.Delay)
		{
			if (!this.Object.activeInHierarchy)
			{
				this.Object.SetActive(true);
				return;
			}
			float num = (t - this.Delay) / (duration - this.Delay);
			float num2 = Mathf.Sin(num * 3.1415927f);
			Vector3 vector = Vector3.Lerp(this.Start, this.End, Easing.Interpolate(num, this.Interpolation));
			if (this.Slant == 0)
			{
				vector += new Vector3(this.Curve * num2, this.Arc * num2, 0f);
			}
			else
			{
				float num3 = this.Start.y - this.End.y;
				float num4 = this.Start.x - this.End.x;
				int num5 = this.Slant;
				if (num5 == 2 || num5 == 3)
				{
					num3 = Math.Abs(num3);
				}
				num5 = this.Slant;
				if (num5 == 2 || num5 == 4)
				{
					num4 = Math.Abs(num4);
				}
				vector += new Vector3(num3 / 24f * this.Curve * num2, num4 / 16f * this.Arc * num2, 0f);
			}
			base.transform.position = vector;
			base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(this.StartRotation, this.EndRotation, num));
		}
	}

	// Token: 0x0400076F RID: 1903
	public GameObject Object;

	// Token: 0x04000770 RID: 1904
	public ex3DSprite2 Projectile;

	// Token: 0x04000771 RID: 1905
	public TrailRenderer Trail;

	// Token: 0x04000772 RID: 1906
	public ParticleSystem Particles;

	// Token: 0x04000773 RID: 1907
	public Vector3 TrailOrigin;

	// Token: 0x04000774 RID: 1908
	public Color Foreground;

	// Token: 0x04000775 RID: 1909
	public Color Detail;

	// Token: 0x04000776 RID: 1910
	public float Speed;

	// Token: 0x04000777 RID: 1911
	public float Orientation;

	// Token: 0x04000778 RID: 1912
	public float Rotation;

	// Token: 0x04000779 RID: 1913
	public float Arc;

	// Token: 0x0400077A RID: 1914
	public float Curve;

	// Token: 0x0400077B RID: 1915
	public int Slant;

	// Token: 0x0400077C RID: 1916
	public float Rate;

	// Token: 0x0400077D RID: 1917
	private Vector3 Start;

	// Token: 0x0400077E RID: 1918
	private Vector3 End;

	// Token: 0x0400077F RID: 1919
	private Vector3 Scale = Vector3.one;

	// Token: 0x04000780 RID: 1920
	private float StartRotation;

	// Token: 0x04000781 RID: 1921
	private float EndRotation;

	// Token: 0x04000782 RID: 1922
	private float Delay;

	// Token: 0x04000783 RID: 1923
	private Easing.Functions Interpolation;

	// Token: 0x04000784 RID: 1924
	public float ParticleDuration;
}
