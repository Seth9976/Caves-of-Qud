using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
[ExecuteAlways]
public class ParticleSnap : MonoBehaviour
{
	// Token: 0x0600029D RID: 669 RVA: 0x0000DD79 File Offset: 0x0000BF79
	private void Start()
	{
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0000DD7C File Offset: 0x0000BF7C
	private void Update()
	{
		int i = 0;
		int num = this.particles.GetParticles(this.gos);
		while (i < num)
		{
			Vector3 position = this.gos[i].position;
			int num2 = Mathf.RoundToInt(position.x);
			int num3 = Mathf.RoundToInt(position.y);
			this.gos[i].position = new Vector3((float)num2, (float)num3);
			i++;
		}
		this.particles.SetParticles(this.gos, num);
	}

	// Token: 0x0400018C RID: 396
	public ParticleSystem particles;

	// Token: 0x0400018D RID: 397
	private ParticleSystem.Particle[] gos = new ParticleSystem.Particle[300];
}
