using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000051 RID: 81
public class ParticleAnimator : MonoBehaviour, CombatJuice.ICombatJuiceAnimator
{
	// Token: 0x06000293 RID: 659 RVA: 0x0000DB23 File Offset: 0x0000BD23
	public void Finish()
	{
		if (this.after != null)
		{
			this.after();
			this.after = null;
		}
	}

	// Token: 0x06000294 RID: 660 RVA: 0x0000DB40 File Offset: 0x0000BD40
	public void Play(bool loop = false, Action after = null, string name = null, string objectId = null)
	{
		this.after = after;
		this.loop = loop;
		this.deltaTime = 0f;
		this.Start();
		for (int i = 0; i < this.systems.Length; i++)
		{
			this.systems[i].Play(true);
		}
	}

	// Token: 0x06000295 RID: 661 RVA: 0x0000DB90 File Offset: 0x0000BD90
	private void Start()
	{
		if (this.systems == null)
		{
			this.systems = base.GetComponentsInChildren<ParticleSystem>();
		}
		for (int i = 0; i < this.systems.Length; i++)
		{
			this.systems[i].main.loop = this.loop;
			this.systems[i].time = 0f;
		}
	}

	// Token: 0x06000296 RID: 662 RVA: 0x0000DBF4 File Offset: 0x0000BDF4
	private void Update()
	{
		this.deltaTime += Time.deltaTime;
		if (!this.systems.Any<ParticleSystem>((ParticleSystem s) => s.isPlaying))
		{
			this.Finish();
		}
	}

	// Token: 0x06000297 RID: 663 RVA: 0x0000DC48 File Offset: 0x0000BE48
	public void Stop()
	{
		if (this.systems == null)
		{
			return;
		}
		int i = 0;
		int num = this.systems.Length;
		while (i < num)
		{
			this.systems[i].Stop();
			i++;
		}
	}

	// Token: 0x04000183 RID: 387
	public bool loop;

	// Token: 0x04000184 RID: 388
	public float deltaTime;

	// Token: 0x04000185 RID: 389
	private Action after;

	// Token: 0x04000186 RID: 390
	private ParticleSystem[] systems;
}
