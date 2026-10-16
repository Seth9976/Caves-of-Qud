using System;
using System.Collections.Generic;
using Kobold;
using UnityEngine;
using XRL.World;
using XRL.World.Parts;

// Token: 0x02000054 RID: 84
[ExecuteAlways]
public class ParticleTeleportInVFX : MonoBehaviour, CombatJuice.ICombatJuiceConfigurable
{
	// Token: 0x060002A0 RID: 672 RVA: 0x0000DE14 File Offset: 0x0000C014
	public void configureWithObject(object args)
	{
		this.reset = true;
		ParticleTeleportInVFX.ConfigureObject configureObject = args as ParticleTeleportInVFX.ConfigureObject;
		if (configureObject == null)
		{
			MetricsManager.LogError(this, string.Format("Expected {0} but got {1}", "ConfigureObject", args.GetType()));
			return;
		}
		Render render = configureObject.BeingTeleported.Render;
		if (render == null)
		{
			MetricsManager.LogWarning("BeingTeleported is null");
			return;
		}
		this.texture = SpriteManager.GetUnitySprite(configureObject.BeingTeleported.Render.Tile).texture;
		Vector3 vector = this.initialLocalScale;
		if (render.getHFlip())
		{
			vector.x = -vector.x;
		}
		if (render.getVFlip())
		{
			vector.y = -vector.y;
		}
		base.transform.localScale = vector;
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x0000DEC7 File Offset: 0x0000C0C7
	public void configure(string configurationString)
	{
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x0000DEC9 File Offset: 0x0000C0C9
	private void Awake()
	{
		this.reset = true;
		this.initialLocalScale = base.transform.localScale;
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x0000DEE4 File Offset: 0x0000C0E4
	private void Update()
	{
		if (this.reset)
		{
			if (this.backingSprite != null)
			{
				this.backingSprite.SetActive(true);
			}
			this.system.Stop();
			this.system.Clear();
			this.reset = false;
			this.count = 0;
			this.startPositions.Clear();
			this.endPositions.Clear();
			this.distances.Clear();
			this.angles.Clear();
			for (int i = 0; i < this.texture.width; i++)
			{
				for (int j = 0; j < this.texture.height; j++)
				{
					if (this.texture.GetPixel(i, j).a > 0f)
					{
						this.endPositions.Add(this.count, new Vector3((float)i, (float)j) + new Vector3(-7.5f, -11.5f));
						this.startPositions.Add(this.count, this.endPositions[this.count] + new Vector3(global::UnityEngine.Random.Range(-24f, 24f), (float)global::UnityEngine.Random.Range(-24, 24), 0f));
						if (this.teleportingOut)
						{
							this.distances.Add(this.count, global::UnityEngine.Random.Range(64, 128));
							this.angles.Add(this.count, global::UnityEngine.Random.Range(0f, 1.2f));
						}
						else
						{
							this.distances.Add(this.count, global::UnityEngine.Random.Range(12, 256));
							this.angles.Add(this.count, global::UnityEngine.Random.Range(0f, 6.28f));
						}
						this.count++;
					}
				}
			}
			this.system.Play();
			this.system.Emit(this.count);
			this.t = 0f;
			this.delaycount = this.delay;
		}
		if (this.t < this.duration)
		{
			if (this.delaycount > 0f)
			{
				bool flag = false;
				if (this.delaycount == this.delay)
				{
					flag = true;
				}
				this.delaycount -= Time.deltaTime;
				if (!flag)
				{
					return;
				}
			}
			else
			{
				this.t += Time.deltaTime;
				if (this.backingSprite != null && (double)this.t >= 0.8 && this.backingSprite.activeInHierarchy)
				{
					global::UnityEngine.GameObject gameObject = this.backingSprite;
					if (gameObject != null)
					{
						gameObject.SetActive(false);
					}
				}
			}
			this.system.GetParticles(this.particles);
			for (int k = 0; k < this.count; k++)
			{
				float num;
				float num2;
				if (this.teleportingOut)
				{
					num = Mathf.Lerp(0f, -this.angles[k], this.t / this.duration);
					num2 = Mathf.Lerp(0f, (float)(-(float)this.distances[k]), this.t / this.duration);
				}
				else
				{
					num = Mathf.Lerp(this.angles[k], 0f, this.t / this.duration);
					num2 = Mathf.Lerp((float)this.distances[k], 0f, this.t / this.duration);
				}
				double num3 = (double)this.endPositions[k].x + (double)num2 * Math.Cos((double)num);
				double num4 = (double)this.endPositions[k].y + (double)num2 * Math.Sin((double)num);
				this.particles[k].position = new Vector3((float)num3, (float)num4, this.particles[k].position.z);
			}
			this.system.SetParticles(this.particles);
		}
	}

	// Token: 0x0400018E RID: 398
	public global::UnityEngine.GameObject backingSprite;

	// Token: 0x0400018F RID: 399
	public bool teleportingOut;

	// Token: 0x04000190 RID: 400
	public bool reset = true;

	// Token: 0x04000191 RID: 401
	public int count;

	// Token: 0x04000192 RID: 402
	public float delay;

	// Token: 0x04000193 RID: 403
	public float duration = 0.5f;

	// Token: 0x04000194 RID: 404
	public float delaycount;

	// Token: 0x04000195 RID: 405
	public Texture2D texture;

	// Token: 0x04000196 RID: 406
	public ParticleSystem system;

	// Token: 0x04000197 RID: 407
	private ParticleSystem.Particle[] particles = new ParticleSystem.Particle[384];

	// Token: 0x04000198 RID: 408
	private Dictionary<int, Vector3> startPositions = new Dictionary<int, Vector3>();

	// Token: 0x04000199 RID: 409
	private Dictionary<int, Vector3> endPositions = new Dictionary<int, Vector3>();

	// Token: 0x0400019A RID: 410
	private Dictionary<int, int> distances = new Dictionary<int, int>();

	// Token: 0x0400019B RID: 411
	private Dictionary<int, float> angles = new Dictionary<int, float>();

	// Token: 0x0400019C RID: 412
	private Vector3 initialLocalScale;

	// Token: 0x0400019D RID: 413
	private float t;

	// Token: 0x02001571 RID: 5489
	public class ConfigureObject
	{
		// Token: 0x0600D536 RID: 54582 RVA: 0x004F3438 File Offset: 0x004F1638
		public ConfigureObject(XRL.World.GameObject beingTeleported)
		{
			this.BeingTeleported = beingTeleported;
		}

		// Token: 0x0400564E RID: 22094
		public XRL.World.GameObject BeingTeleported;
	}
}
