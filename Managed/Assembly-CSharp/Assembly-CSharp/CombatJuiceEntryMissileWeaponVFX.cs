using System;
using System.Collections.Generic;

// Token: 0x0200011A RID: 282
public class CombatJuiceEntryMissileWeaponVFX : CombatJuiceEntry
{
	// Token: 0x06000B14 RID: 2836 RVA: 0x00047D78 File Offset: 0x00045F78
	public void configure(MissileWeaponVFXConfiguration config)
	{
		this.config = config;
		this.duration = 1.5f;
		this.t = 0f;
		MissileWeaponVFXConfiguration.MissileVFXPathDefinition missileVFXPathDefinition;
		string text;
		int num;
		if (config.paths.TryGetValue(0, out missileVFXPathDefinition) && missileVFXPathDefinition.projectileVFXConfiguration.TryGetValue("Duration", out text) && int.TryParse(text, out num))
		{
			config.duration = (float)num;
		}
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x00047DD8 File Offset: 0x00045FD8
	public override bool canFinishUpToTurn()
	{
		return false;
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x00047DDC File Offset: 0x00045FDC
	public override void start()
	{
		this.duration = this.config.duration;
		this.t = 0f;
		foreach (KeyValuePair<int, MissileWeaponVFXConfiguration.MissileVFXPathDefinition> keyValuePair in this.config.paths)
		{
			if (keyValuePair.Value.used)
			{
				BaseMissileWeaponVFX component = PooledPrefabManager.Instantiate(keyValuePair.Value.projectileVFX, "Prefabs/").GetComponent<BaseMissileWeaponVFX>();
				component.configure(this.config, keyValuePair.Key, ref this.duration);
				this.obj.Add(component);
			}
		}
		base.start();
		foreach (BaseMissileWeaponVFX baseMissileWeaponVFX in this.obj)
		{
			baseMissileWeaponVFX.start();
		}
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x00047EE0 File Offset: 0x000460E0
	public override void update()
	{
		base.update();
		foreach (BaseMissileWeaponVFX baseMissileWeaponVFX in this.obj)
		{
			baseMissileWeaponVFX.OnUpdate(this.t, this.duration);
		}
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x00047F44 File Offset: 0x00046144
	public override void finish()
	{
		if (!this.finished)
		{
			this.finished = true;
			foreach (BaseMissileWeaponVFX baseMissileWeaponVFX in this.obj)
			{
				baseMissileWeaponVFX.pool();
			}
			this.obj.Clear();
			if (this.config != null)
			{
				MissileWeaponVFXConfiguration.repool(this.config);
			}
			base.finish();
		}
	}

	// Token: 0x0400075A RID: 1882
	private MissileWeaponVFXConfiguration config;

	// Token: 0x0400075B RID: 1883
	private List<BaseMissileWeaponVFX> obj = new List<BaseMissileWeaponVFX>();
}
