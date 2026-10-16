using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Genkit;
using UnityEngine;
using XRL.World;
using XRL.World.Parts;

// Token: 0x02000119 RID: 281
public class MissileWeaponVFXConfiguration
{
	// Token: 0x06000B09 RID: 2825 RVA: 0x00047AF0 File Offset: 0x00045CF0
	public static MissileWeaponVFXConfiguration next()
	{
		Queue<MissileWeaponVFXConfiguration> queue = MissileWeaponVFXConfiguration.pool;
		MissileWeaponVFXConfiguration missileWeaponVFXConfiguration;
		lock (queue)
		{
			if (MissileWeaponVFXConfiguration.pool.Count > 0)
			{
				missileWeaponVFXConfiguration = MissileWeaponVFXConfiguration.pool.Dequeue();
			}
			else
			{
				missileWeaponVFXConfiguration = new MissileWeaponVFXConfiguration();
			}
		}
		missileWeaponVFXConfiguration.reset();
		return missileWeaponVFXConfiguration;
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x00047B50 File Offset: 0x00045D50
	public static void repool(MissileWeaponVFXConfiguration configuration)
	{
		if (configuration == null)
		{
			return;
		}
		configuration.reset();
		Queue<MissileWeaponVFXConfiguration> queue = MissileWeaponVFXConfiguration.pool;
		lock (queue)
		{
			MissileWeaponVFXConfiguration.pool.Enqueue(configuration);
		}
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x00047BA0 File Offset: 0x00045DA0
	public void reset()
	{
		foreach (KeyValuePair<int, MissileWeaponVFXConfiguration.MissileVFXPathDefinition> keyValuePair in this.paths)
		{
			keyValuePair.Value.reset();
		}
		this.duration = 1.5f;
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x00047C04 File Offset: 0x00045E04
	public MissileWeaponVFXConfiguration.MissileVFXPathDefinition GetPath(int Index)
	{
		MissileWeaponVFXConfiguration.MissileVFXPathDefinition missileVFXPathDefinition;
		if (!this.paths.TryGetValue(Index, out missileVFXPathDefinition))
		{
			missileVFXPathDefinition = new MissileWeaponVFXConfiguration.MissileVFXPathDefinition();
			this.paths.Add(Index, missileVFXPathDefinition);
		}
		return missileVFXPathDefinition;
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x00047C35 File Offset: 0x00045E35
	public void addStep(int pathIndex, Location2D location)
	{
		MissileWeaponVFXConfiguration.MissileVFXPathDefinition path = this.GetPath(pathIndex);
		if (path == null)
		{
			return;
		}
		path.addStep(location);
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x00047C4C File Offset: 0x00045E4C
	public void setPathProjectileVFX(int pathIndex, string projectileVFX, string projectilVFXConfiguration)
	{
		MissileWeaponVFXConfiguration.MissileVFXPathDefinition path = this.GetPath(pathIndex);
		path.used = true;
		path.projectileVFX = projectileVFX;
		if (!projectilVFXConfiguration.IsNullOrEmpty())
		{
			foreach (KeyValuePair<string, string> keyValuePair in projectilVFXConfiguration.CachedDictionaryExpansion())
			{
				path.projectileVFXConfiguration.Set(keyValuePair.Key, keyValuePair.Value);
			}
		}
		string configValue = path.getConfigValue("duration", null);
		if (configValue != null && configValue != "")
		{
			float num;
			if (float.TryParse(configValue, out num))
			{
				if (num != this.duration)
				{
					this.duration = num;
					return;
				}
			}
			else
			{
				Debug.LogError("invalid duration specification: " + configValue);
			}
		}
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x00047D1C File Offset: 0x00045F1C
	public void setPathProjectileVFX(int pathIndex, Dictionary<string, string> Values)
	{
		this.GetPath(pathIndex).SetProjectileVFX(Values);
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x00047D2B File Offset: 0x00045F2B
	public void SetPathProjectileRender(int Index, XRL.World.GameObject Object)
	{
		this.GetPath(Index).SetProjectileRender(Object);
	}

	// Token: 0x04000757 RID: 1879
	private static Queue<MissileWeaponVFXConfiguration> pool = new Queue<MissileWeaponVFXConfiguration>();

	// Token: 0x04000758 RID: 1880
	public Dictionary<int, MissileWeaponVFXConfiguration.MissileVFXPathDefinition> paths = new Dictionary<int, MissileWeaponVFXConfiguration.MissileVFXPathDefinition>();

	// Token: 0x04000759 RID: 1881
	public float duration = 1.5f;

	// Token: 0x020015E8 RID: 5608
	public class MissileVFXPathDefinition
	{
		// Token: 0x0600D657 RID: 54871 RVA: 0x004F81EC File Offset: 0x004F63EC
		public string getConfigValue(string key, string defaultResult = null)
		{
			if (this.projectileVFXConfiguration == null)
			{
				return defaultResult;
			}
			string text;
			if (this.projectileVFXConfiguration.TryGetValue(key, out text))
			{
				return text;
			}
			return defaultResult;
		}

		// Token: 0x0600D658 RID: 54872 RVA: 0x004F8216 File Offset: 0x004F6416
		public bool TryGetValue(string Key, out string Result)
		{
			if (this.projectileVFXConfiguration == null)
			{
				Result = null;
				return false;
			}
			return this.projectileVFXConfiguration.TryGetValue(Key, out Result);
		}

		// Token: 0x0600D659 RID: 54873 RVA: 0x004F8232 File Offset: 0x004F6432
		public void addStep(Location2D step)
		{
			this.used = true;
			if (this.first == null)
			{
				this.first = step;
			}
			this.last = step;
			this.steps.Add(step);
		}

		// Token: 0x0600D65A RID: 54874 RVA: 0x004F8263 File Offset: 0x004F6463
		public void reset()
		{
			this.used = false;
			this.first = null;
			this.last = null;
			this.projectileVFX = null;
			this.projectileVFXConfiguration.Clear();
			this.steps.Clear();
		}

		// Token: 0x0600D65B RID: 54875 RVA: 0x004F8297 File Offset: 0x004F6497
		public void SetParameter(string Key, string Value)
		{
			this.projectileVFXConfiguration[Key] = Value;
		}

		// Token: 0x0600D65C RID: 54876 RVA: 0x004F82A8 File Offset: 0x004F64A8
		public void SetProjectileVFX(Dictionary<string, string> Values)
		{
			this.used = true;
			string text;
			if (Values.TryGetValue("Effect", out text))
			{
				this.projectileVFX = text;
			}
			foreach (KeyValuePair<string, string> keyValuePair in Values)
			{
				this.projectileVFXConfiguration[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		// Token: 0x0600D65D RID: 54877 RVA: 0x004F8328 File Offset: 0x004F6528
		public void SetProjectileRender(XRL.World.GameObject Object)
		{
			Render render = Object.Render;
			if (render == null)
			{
				return;
			}
			this.used = true;
			this.projectileVFXConfiguration["RenderTile"] = render.Tile;
			ColorChars colorChars = render.getColorChars();
			this.projectileVFXConfiguration["RenderForeground"] = colorChars.foreground.ToString();
			this.projectileVFXConfiguration["RenderDetail"] = colorChars.detail.ToString();
			if (!render.HFlip)
			{
				if (render.VFlip)
				{
					this.projectileVFXConfiguration["RenderFlip"] = "V";
				}
				return;
			}
			if (render.VFlip)
			{
				this.projectileVFXConfiguration["RenderFlip"] = "B";
				return;
			}
			this.projectileVFXConfiguration["RenderFlip"] = "H";
		}

		// Token: 0x04005852 RID: 22610
		public string projectileVFX;

		// Token: 0x04005853 RID: 22611
		public Dictionary<string, string> projectileVFXConfiguration = new Dictionary<string, string>();

		// Token: 0x04005854 RID: 22612
		public bool used;

		// Token: 0x04005855 RID: 22613
		public Location2D first;

		// Token: 0x04005856 RID: 22614
		public Location2D last;

		// Token: 0x04005857 RID: 22615
		public List<Location2D> steps = new List<Location2D>(105);
	}
}
