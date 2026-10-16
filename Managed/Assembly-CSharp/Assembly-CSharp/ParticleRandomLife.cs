using System;
using UnityEngine;

// Token: 0x02000052 RID: 82
[ExecuteAlways]
public class ParticleRandomLife : MonoBehaviour, CombatJuice.ICombatJuiceConfigurable
{
	// Token: 0x06000299 RID: 665 RVA: 0x0000DC88 File Offset: 0x0000BE88
	public void configure(string configurationString)
	{
		this.reset = true;
	}

	// Token: 0x0600029A RID: 666 RVA: 0x0000DC91 File Offset: 0x0000BE91
	private void Awake()
	{
		this.reset = true;
	}

	// Token: 0x0600029B RID: 667 RVA: 0x0000DC9C File Offset: 0x0000BE9C
	private void Update()
	{
		if (!this.reset)
		{
			return;
		}
		foreach (ParticleSystem particleSystem in this.particles)
		{
			int num = particleSystem.GetParticles(this.gos);
			if (num > 0)
			{
				for (int j = 0; j < num; j++)
				{
					if (this.gos[j].remainingLifetime > 0f)
					{
						float num2 = global::UnityEngine.Random.Range(this.min, this.max);
						this.gos[j].remainingLifetime = num2;
						this.gos[j].startLifetime = num2;
					}
				}
				particleSystem.SetParticles(this.gos, num);
				this.reset = false;
			}
		}
	}

	// Token: 0x04000187 RID: 391
	public float min;

	// Token: 0x04000188 RID: 392
	public float max;

	// Token: 0x04000189 RID: 393
	public bool reset = true;

	// Token: 0x0400018A RID: 394
	public ParticleSystem[] particles;

	// Token: 0x0400018B RID: 395
	private ParticleSystem.Particle[] gos = new ParticleSystem.Particle[384];
}
