using System;
using UnityEngine;

// Token: 0x02000118 RID: 280
[ExecuteInEditMode]
public class BaseMissileWeaponVFX : MonoBehaviour
{
	// Token: 0x06000B03 RID: 2819 RVA: 0x00047AB1 File Offset: 0x00045CB1
	private void Start()
	{
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x00047AB3 File Offset: 0x00045CB3
	public virtual void OnUpdate(float t, float duration)
	{
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x00047AB5 File Offset: 0x00045CB5
	public virtual void configure(MissileWeaponVFXConfiguration configuration, int path, ref float duration)
	{
		this.configuration = configuration;
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x00047ABE File Offset: 0x00045CBE
	public virtual void start()
	{
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x00047AC0 File Offset: 0x00045CC0
	public virtual void pool()
	{
		base.gameObject.SetActive(false);
		if (this.configuration != null)
		{
			this.configuration = null;
			PooledPrefabManager.Return(base.gameObject);
		}
	}

	// Token: 0x04000756 RID: 1878
	private MissileWeaponVFXConfiguration configuration;
}
