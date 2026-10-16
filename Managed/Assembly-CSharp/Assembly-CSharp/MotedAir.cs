using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using UnityEngine;
using XRL.Rules;
using XRL.World.Parts;

// Token: 0x020000FC RID: 252
[ExecuteInEditMode]
public class MotedAir : MonoBehaviour
{
	// Token: 0x0600097E RID: 2430 RVA: 0x0003C38C File Offset: 0x0003A58C
	private void Awake()
	{
		if (!this.init)
		{
			this.init = true;
			this.reset = true;
		}
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x0003C3A4 File Offset: 0x0003A5A4
	private void Update()
	{
		this.t += Time.deltaTime;
		int particles = this.system.GetParticles(this.gos);
		if (this.reset || particles < this.count)
		{
			this.system.Stop();
			this.system.Clear();
			this.reset = false;
			if (this.count <= 0)
			{
				this.count = 5000;
			}
			this.system.Play();
			this.system.Emit(this.count);
			for (int i = 0; i < this.count; i++)
			{
				this.startPositions.Set(i, new Vector3((float)Stat.Random(-955, -955), (float)Stat.Random(-195, 195)));
				this.pulseDuration.Set(i, (float)Stat.Random(20, 600));
				this.pulseStart.Set(i, Stat.Random(0f, this.pulseDuration[i]));
				this.color.Add(i, ConsoleLib.Console.ColorUtility.colorFromChar(Crayons.GetRandomColor(null)[0]));
			}
			this.count = this.system.GetParticles(this.gos);
			for (int j = 0; j < this.count; j++)
			{
				this.gos[j].remainingLifetime = float.MaxValue;
				this.gos[j].startLifetime = float.MaxValue;
				this.gos[j].position = this.startPositions[j];
				this.gos[j].rotation = (float)Stat.Random(25, 600) / 300f;
				this.gos[j].startSize = (float)Stat.Random(25, 600) / 300f;
				this.gos[j].startColor = new Color32(this.color[j].r, this.color[j].g, this.color[j].b, (byte)(255f * ((1f + Mathf.Sin(3.1415927f * (this.t / (this.pulseStart[j] + this.pulseDuration[j])))) / 2f)));
			}
			this.system.SetParticles(this.gos);
		}
		int num = 0;
		while (num < this.count && num < particles)
		{
			float num2 = 6.2831855f * (this.t / this.pulseDuration[num]);
			float num3 = Mathf.Sin(6.2831855f * ((this.t + this.pulseStart[num]) / this.pulseDuration[num]));
			float num4 = (1f + num3) / 2f;
			Mathf.Sin(num2);
			this.gos[num].remainingLifetime = float.MaxValue;
			this.gos[num].position = this.startPositions[num];
			ParticleSystem.Particle[] array = this.gos;
			int num5 = num;
			array[num5].rotation = array[num5].rotation + (float)Stat.Random(0, 60) / 6000f;
			this.gos[num].startSize = (1f + num3) / 2f * 1.5f;
			this.gos[num].startColor = new Color32(this.color[num].r, this.color[num].g, this.color[num].b, (byte)(255f * num4 - (float)Stat.RandomCosmetic(0, 64)));
			num++;
		}
		this.system.SetParticles(this.gos);
	}

	// Token: 0x04000633 RID: 1587
	public ParticleSystem system;

	// Token: 0x04000634 RID: 1588
	public bool reset;

	// Token: 0x04000635 RID: 1589
	public int count;

	// Token: 0x04000636 RID: 1590
	private Dictionary<int, Vector3> startPositions = new Dictionary<int, Vector3>();

	// Token: 0x04000637 RID: 1591
	private Dictionary<int, float> pulseDuration = new Dictionary<int, float>();

	// Token: 0x04000638 RID: 1592
	private Dictionary<int, float> pulseStart = new Dictionary<int, float>();

	// Token: 0x04000639 RID: 1593
	private Dictionary<int, Color32> color = new Dictionary<int, Color32>();

	// Token: 0x0400063A RID: 1594
	private ParticleSystem.Particle[] gos = new ParticleSystem.Particle[4];

	// Token: 0x0400063B RID: 1595
	private bool init;

	// Token: 0x0400063C RID: 1596
	public float t;
}
