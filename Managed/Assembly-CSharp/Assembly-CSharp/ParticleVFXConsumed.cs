using System;
using ConsoleLib.Console;
using Kobold;
using UnityEngine;

// Token: 0x02000056 RID: 86
[ExecuteAlways]
public class ParticleVFXConsumed : MonoBehaviour, CombatJuice.ICombatJuiceConfigurable
{
	// Token: 0x060002A9 RID: 681 RVA: 0x0000E918 File Offset: 0x0000CB18
	public void configure(string configurationString)
	{
		string[] array = configurationString.Split(';', StringSplitOptions.None);
		exTextureInfo textureInfo = SpriteManager.GetTextureInfo(array[0], true);
		this.color = ConsoleLib.Console.ColorUtility.ColorFromString(array[1]);
		this.detail = ConsoleLib.Console.ColorUtility.ColorFromString(array[2]);
		this.spriteRenderer.textureInfo = textureInfo;
		this.spriteRenderer.detailcolor = this.detail;
		this.spriteRenderer.color = this.color;
		this.spriteRenderer.backcolor = new Color(0f, 0f, 0f, 0f);
		this.spriteRenderer.transform.localPosition = new Vector3(0f, 0f, -1f);
		this.spriteRenderer.gameObject.SetActive(true);
		this.reset = true;
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0000E9E3 File Offset: 0x0000CBE3
	private void Awake()
	{
		this.reset = true;
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0000E9EC File Offset: 0x0000CBEC
	private void Update()
	{
		if (this.reset)
		{
			this.system.Stop();
			this.system.Clear();
			this.reset = false;
			this.count = 0;
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
			}
			if ((double)this.t < 0.25)
			{
				this.spriteRenderer.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(0f, 98f, this.t / this.duration));
			}
			else
			{
				this.spriteRenderer.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(98f, -120f, this.t / this.duration));
			}
			float num = Mathf.Lerp(1f, 0f, this.t / this.duration);
			this.spriteRenderer.gameObject.transform.localScale = new Vector3(num, num, 1f);
		}
	}

	// Token: 0x040001AD RID: 429
	public bool reset = true;

	// Token: 0x040001AE RID: 430
	public int count;

	// Token: 0x040001AF RID: 431
	public float delay;

	// Token: 0x040001B0 RID: 432
	public float duration = 0.5f;

	// Token: 0x040001B1 RID: 433
	public float delaycount;

	// Token: 0x040001B2 RID: 434
	public ex3DSprite2 spriteRenderer;

	// Token: 0x040001B3 RID: 435
	public ParticleSystem system;

	// Token: 0x040001B4 RID: 436
	public Color color;

	// Token: 0x040001B5 RID: 437
	public Color detail;

	// Token: 0x040001B6 RID: 438
	private float t;
}
