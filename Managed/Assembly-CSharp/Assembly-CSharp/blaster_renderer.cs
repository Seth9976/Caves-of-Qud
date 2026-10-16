using System;
using UnityEngine;

// Token: 0x0200011F RID: 287
[ExecuteInEditMode]
public class blaster_renderer : BaseMissileWeaponVFX
{
	// Token: 0x06000B32 RID: 2866 RVA: 0x0004A10C File Offset: 0x0004830C
	public override void configure(MissileWeaponVFXConfiguration configuration, int path, ref float duration)
	{
		base.configure(configuration, path, ref duration);
		if (configuration.paths[path].first != null && configuration.paths[path].last != null)
		{
			this.startPos = GameManager.Instance.getTileCenter(configuration.paths[path].first.X, configuration.paths[path].first.Y, 0);
			this.endPos = GameManager.Instance.getTileCenter(configuration.paths[path].last.X, configuration.paths[path].last.Y, 0);
			float num = 0.05f;
			this.dur = Vector3.Distance(this.startPos, this.endPos) / 24f * num;
			if (this.dur > duration)
			{
				duration = this.dur;
			}
			this.trails.Stop();
			base.transform.position = this.startPos;
			this.trails.Play();
		}
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x0004A234 File Offset: 0x00048434
	public override void OnUpdate(float t, float duration)
	{
		if (t > this.dur)
		{
			base.gameObject.SetActive(false);
		}
		Debug.Log(string.Format("{0} {1} {2} {3}", new object[] { this.startPos, this.endPos, t, this.dur }));
		base.transform.position = Vector3.Lerp(this.startPos, this.endPos, t / this.dur);
	}

	// Token: 0x04000796 RID: 1942
	public ParticleSystem trails;

	// Token: 0x04000797 RID: 1943
	private Vector3 startPos;

	// Token: 0x04000798 RID: 1944
	private Vector3 endPos;

	// Token: 0x04000799 RID: 1945
	public float dur;
}
