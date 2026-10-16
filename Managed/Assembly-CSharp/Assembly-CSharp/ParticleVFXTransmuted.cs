using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using Kobold;
using UnityEngine;
using XRL;
using XRL.World;
using XRL.World.Parts;

// Token: 0x0200005B RID: 91
[ExecuteAlways]
public class ParticleVFXTransmuted : MonoBehaviour, CombatJuice.ICombatJuiceConfigurable
{
	// Token: 0x060002BD RID: 701 RVA: 0x0000FA44 File Offset: 0x0000DC44
	public void configure(string configurationString)
	{
		string[] array = configurationString.Split(';', StringSplitOptions.None);
		this.texture = SpriteManager.GetUnitySprite(array[0]).texture;
		this.startColor = ConsoleLib.Console.ColorUtility.ColorFromString(array[1]);
		this.startDetail = ConsoleLib.Console.ColorUtility.ColorFromString(array[2]);
		this.textureOut = SpriteManager.GetUnitySprite(array[3]).texture;
		this.endColor = ConsoleLib.Console.ColorUtility.ColorFromString(array[4]);
		this.endDetail = ConsoleLib.Console.ColorUtility.ColorFromString(array[5]);
		this.reset = true;
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0000FAC0 File Offset: 0x0000DCC0
	private void Awake()
	{
		this.reset = true;
	}

	// Token: 0x060002BF RID: 703 RVA: 0x0000FACC File Offset: 0x0000DCCC
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
			this.startPositions.Clear();
			this.endPositions.Clear();
			this.startColors.Clear();
			this.endColors.Clear();
			this.distances.Clear();
			this.angles.Clear();
			this.count = 0;
			for (int i = 0; i < this.texture.width; i++)
			{
				for (int j = 0; j < this.texture.height; j++)
				{
					if (this.texture.GetPixel(i, j).a > 0f)
					{
						this.startPositions.Add(this.count, new Vector3((float)i, (float)j) + new Vector3(-7.5f, -11.5f));
						this.startColors.Add(this.count, ((double)this.texture.GetPixel(i, j).r < 0.5) ? this.startColor : this.startDetail);
						this.count++;
					}
				}
			}
			int num = 0;
			for (int k = 0; k < this.textureOut.width; k++)
			{
				for (int l = 0; l < this.textureOut.height; l++)
				{
					if (this.textureOut.GetPixel(k, l).a > 0f)
					{
						this.endPositions.Add(num, new Vector3((float)k, (float)l) + new Vector3(-7.5f, -11.5f));
						this.endColors.Add(num, ((double)this.textureOut.GetPixel(k, l).r < 0.5) ? this.endColor : this.endDetail);
						num++;
					}
				}
			}
			if (this.count < num)
			{
				for (int m = this.count; m < num; m++)
				{
					this.startPositions.Add(m, this.startPositions[m % this.count]);
					this.startColors.Add(m, this.startColors[m % this.count]);
				}
				this.count = num;
			}
			else
			{
				for (int n = num; n < this.count; n++)
				{
					this.endPositions.Add(n, this.endPositions[n % num]);
					this.endColors.Add(n, this.endColors[n % num]);
				}
			}
			Debug.Log(this.count);
			Debug.Log(num);
			for (int num2 = 0; num2 < this.count; num2++)
			{
				this.distances.Add(num2, global::UnityEngine.Random.Range(4, 32));
				this.angles.Add(num2, global::UnityEngine.Random.Range(-6.28f, 6.28f));
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
				if (this.backingSprite != null && this.t >= this.duration && this.backingSprite.activeInHierarchy)
				{
					global::UnityEngine.GameObject gameObject = this.backingSprite;
					if (gameObject != null)
					{
						gameObject.SetActive(false);
					}
				}
			}
			int particles = this.system.GetParticles(this.gos);
			int num3 = 0;
			while (num3 < this.count && num3 < particles)
			{
				Vector3 vector = this.startPositions[num3];
				Vector3 vector2 = this.endPositions[num3];
				Vector3 vector3 = Vector3.Lerp(this.startPositions[num3], this.endPositions[num3], this.t / this.duration);
				float num4 = this.duration / 2f;
				float num5 = this.t;
				if (num5 >= this.duration / 2f)
				{
					num5 -= this.duration / 2f;
				}
				if (this.t < this.duration / 2f)
				{
					float num6 = Mathf.Lerp(0f + this.angles[num3], 6.2831855f + this.angles[num3], Easing.QuadraticEaseOut(this.t / this.duration));
					float num7 = Mathf.Lerp(0f, (float)this.distances[num3], Easing.QuadraticEaseInOut(num5 / num4));
					double num8 = (double)vector3.x + (double)num7 * Math.Cos((double)num6);
					double num9 = (double)vector3.y + (double)num7 * Math.Sin((double)num6);
					this.gos[num3].position = new Vector3((float)num8, (float)num9, this.gos[num3].position.z);
				}
				else
				{
					float num10 = Mathf.Lerp(0f + this.angles[num3], 6.2831855f + this.angles[num3], Easing.QuadraticEaseOut(this.t / this.duration));
					float num11 = Mathf.Lerp((float)this.distances[num3], 0f, Easing.QuadraticEaseIn(num5 / num4));
					double num12 = (double)vector3.x + (double)num11 * Math.Cos((double)num10);
					double num13 = (double)vector3.y + (double)num11 * Math.Sin((double)num10);
					this.gos[num3].position = new Vector3((float)num12, (float)num13, this.gos[num3].position.z);
				}
				this.gos[num3].startColor = Color.Lerp(this.startColors[num3], this.endColors[num3], this.t / this.duration);
				num3++;
			}
			this.system.SetParticles(this.gos);
		}
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x0001017C File Offset: 0x0000E37C
	public static void Play(global::XRL.World.GameObject From, global::XRL.World.GameObject To)
	{
		Render render = From.Render;
		Render render2 = To.Render;
		ColorChars colorChars = render.getColorChars();
		ColorChars colorChars2 = render2.getColorChars();
		StringBuilder stringBuilder = The.StringBuilder.Append(render.Tile).Append(';').Append(colorChars.foreground)
			.Append(';')
			.Append((colorChars.detail == '\0') ? 'k' : colorChars.detail)
			.Append(';')
			.Append(render2.Tile)
			.Append(';')
			.Append(colorChars2.foreground)
			.Append(';')
			.Append((colorChars2.detail == '\0') ? 'k' : colorChars2.detail);
		CombatJuice.playPrefabAnimation(To, "Deaths/DeathVFXTransmuted", From.ID, global::XRL.World.Event.FinalizeString(stringBuilder), null, true);
	}

	// Token: 0x040001EF RID: 495
	public global::UnityEngine.GameObject backingSprite;

	// Token: 0x040001F0 RID: 496
	public bool reset = true;

	// Token: 0x040001F1 RID: 497
	public int count;

	// Token: 0x040001F2 RID: 498
	public float delay;

	// Token: 0x040001F3 RID: 499
	public float duration = 0.5f;

	// Token: 0x040001F4 RID: 500
	public float delaycount;

	// Token: 0x040001F5 RID: 501
	public Texture2D texture;

	// Token: 0x040001F6 RID: 502
	public Texture2D textureOut;

	// Token: 0x040001F7 RID: 503
	public ParticleSystem system;

	// Token: 0x040001F8 RID: 504
	private ParticleSystem.Particle[] gos = new ParticleSystem.Particle[384];

	// Token: 0x040001F9 RID: 505
	private Dictionary<int, Vector3> startPositions = new Dictionary<int, Vector3>();

	// Token: 0x040001FA RID: 506
	private Dictionary<int, Color> startColors = new Dictionary<int, Color>();

	// Token: 0x040001FB RID: 507
	private Dictionary<int, Vector3> endPositions = new Dictionary<int, Vector3>();

	// Token: 0x040001FC RID: 508
	private Dictionary<int, Color> endColors = new Dictionary<int, Color>();

	// Token: 0x040001FD RID: 509
	private Dictionary<int, int> distances = new Dictionary<int, int>();

	// Token: 0x040001FE RID: 510
	private Dictionary<int, float> angles = new Dictionary<int, float>();

	// Token: 0x040001FF RID: 511
	public Color startColor;

	// Token: 0x04000200 RID: 512
	public Color startDetail;

	// Token: 0x04000201 RID: 513
	public Color endColor;

	// Token: 0x04000202 RID: 514
	public Color endDetail;

	// Token: 0x04000203 RID: 515
	private float t;
}
