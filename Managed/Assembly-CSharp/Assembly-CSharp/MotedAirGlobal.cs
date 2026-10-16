using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using UnityEngine;
using XRL.Rules;
using XRL.World.Parts;

// Token: 0x0200004F RID: 79
[ExecuteInEditMode]
public class MotedAirGlobal : MonoBehaviour
{
	// Token: 0x0600028B RID: 651 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
	private void Awake()
	{
		this.reset = true;
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x0600028C RID: 652 RVA: 0x0000D5C9 File Offset: 0x0000B7C9
	// (set) Token: 0x0600028D RID: 653 RVA: 0x0000D5D8 File Offset: 0x0000B7D8
	public static int wanted
	{
		get
		{
			return MotedAirGlobal._wanted.GetValueOrDefault();
		}
		set
		{
			if (value > 0 && MotedAirGlobal.globalSystem == null)
			{
				MotedAirGlobal._wanted = new int?(0);
				MotedAirGlobal.globalSystem = global::UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Particles/MotedAirGlobal")) as GameObject;
				MotedAirGlobal.globalSystem.transform.position = new Vector3(0f, 0f, 100f);
				MotedAirGlobal.globalSystem.SetActive(true);
			}
			MotedAirGlobal._wanted = new int?(value);
		}
	}

	// Token: 0x0600028E RID: 654 RVA: 0x0000D654 File Offset: 0x0000B854
	private void Update()
	{
		if (this.render == null)
		{
			this.render = this.system.GetComponent<ParticleSystemRenderer>();
		}
		if (MotedAirGlobal.wanted == 0)
		{
			if (this.render.enabled)
			{
				this.render.enabled = false;
				this.system.Stop();
			}
			return;
		}
		if (!this.render.enabled)
		{
			this.render.enabled = true;
			this.reset = true;
		}
		MotedAirGlobal.wanted = 0;
		this.t += Time.deltaTime;
		int particles = this.system.GetParticles(this.gos);
		if (this.reset || particles < this.count)
		{
			this.system.Stop();
			this.system.Clear();
			this.reset = false;
			this.count = 5000;
			this.system.Play();
			this.system.Emit(this.count);
			for (int i = 0; i < this.count; i++)
			{
				this.startPositions.Set(i, new Vector3((float)Stat.Random(-635, 635), (float)Stat.Random(-295, 295)));
				this.pulseDuration.Set(i, (float)Stat.Random(20, 600));
				this.pulseStart.Set(i, Stat.Random(0f, this.pulseDuration[i]));
				this.color.Set(i, ConsoleLib.Console.ColorUtility.colorFromChar(Crayons.GetRandomColor(null)[0]));
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

	// Token: 0x04000176 RID: 374
	private static int? _wanted;

	// Token: 0x04000177 RID: 375
	private static GameObject globalSystem;

	// Token: 0x04000178 RID: 376
	public ParticleSystem system;

	// Token: 0x04000179 RID: 377
	public bool reset;

	// Token: 0x0400017A RID: 378
	public int count;

	// Token: 0x0400017B RID: 379
	private Dictionary<int, Vector3> startPositions = new Dictionary<int, Vector3>();

	// Token: 0x0400017C RID: 380
	private Dictionary<int, float> pulseDuration = new Dictionary<int, float>();

	// Token: 0x0400017D RID: 381
	private Dictionary<int, float> pulseStart = new Dictionary<int, float>();

	// Token: 0x0400017E RID: 382
	private Dictionary<int, Color32> color = new Dictionary<int, Color32>();

	// Token: 0x0400017F RID: 383
	private ParticleSystem.Particle[] gos = new ParticleSystem.Particle[5000];

	// Token: 0x04000180 RID: 384
	private ParticleSystemRenderer render;

	// Token: 0x04000181 RID: 385
	private bool init;

	// Token: 0x04000182 RID: 386
	public float t;
}
