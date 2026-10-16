using System;
using ConsoleLib.Console;
using Kobold;
using UnityEngine;

// Token: 0x02000055 RID: 85
[ExecuteAlways]
public class ParticleVFXAnimated : MonoBehaviour, CombatJuice.ICombatJuiceConfigurable
{
	// Token: 0x060002A5 RID: 677 RVA: 0x0000E344 File Offset: 0x0000C544
	public void configure(string configurationString)
	{
		string[] array = configurationString.Split(';', StringSplitOptions.None);
		this.sprite = SpriteManager.GetTextureInfo(array[0], true);
		this.color = ConsoleLib.Console.ColorUtility.ColorFromString(array[1]);
		this.detail = ConsoleLib.Console.ColorUtility.ColorFromString(array[2]);
		Color color = ConsoleLib.Console.ColorUtility.ColorFromString("k");
		this.mask.color = color;
		this.mask.detailcolor = color;
		this.mask.backcolor = color;
		this.spriteRenderer.textureInfo = this.sprite;
		this.spriteRenderer.detailcolor = this.detail;
		this.spriteRenderer.color = this.color;
		this.spriteRenderer.backcolor = new Color(0f, 0f, 0f, 0f);
		this.spriteRenderer.transform.localPosition = new Vector3(0f, 0f, -1f);
		this.spriteRenderer.gameObject.SetActive(true);
		this.reset = true;
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0000E448 File Offset: 0x0000C648
	private void Awake()
	{
		this.reset = true;
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x0000E454 File Offset: 0x0000C654
	private void Update()
	{
		if (this.reset)
		{
			Color color = ConsoleLib.Console.ColorUtility.ColorFromString("k");
			this.mask.color = color;
			this.mask.detailcolor = color;
			this.mask.backcolor = color;
			this.spriteRenderer.textureInfo = this.sprite;
			this.spriteRenderer.detailcolor = this.detail;
			this.spriteRenderer.color = this.color;
			this.spriteRenderer.backcolor = new Color(0f, 0f, 0f, 0f);
			this.spriteRenderer.transform.localPosition = new Vector3(0f, 0f, -1f);
			this.spriteRenderer.gameObject.SetActive(true);
			if (this.system != null)
			{
				ParticleSystem particleSystem = this.system;
				if (particleSystem != null)
				{
					particleSystem.Stop();
				}
				ParticleSystem particleSystem2 = this.system;
				if (particleSystem2 != null)
				{
					particleSystem2.Clear();
				}
			}
			this.reset = false;
			if (this.system != null)
			{
				this.count = 0;
				ParticleSystem particleSystem3 = this.system;
				if (particleSystem3 != null)
				{
					particleSystem3.Play();
				}
				ParticleSystem particleSystem4 = this.system;
				if (particleSystem4 != null)
				{
					particleSystem4.Emit(this.count);
				}
			}
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
			if ((double)this.t < 0.5)
			{
				this.spriteRenderer.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 8f * Mathf.Sin(this.t * 18f * this.speed));
				return;
			}
			if (this.t > 1f && (double)this.t < 1.2)
			{
				this.spriteRenderer.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 4f * Mathf.Sin(this.t * 21f));
				if ((double)this.t < 1.1)
				{
					this.spriteRenderer.gameObject.transform.localPosition = new Vector3(0f, Mathf.Lerp(0f, 4f, (this.t - 1f) * 10f), 0f);
					return;
				}
				this.spriteRenderer.gameObject.transform.localPosition = new Vector3(0f, Mathf.Lerp(4f, 0f, (this.t - 1.1f) * 10f), 0f);
				return;
			}
			else if (this.t > 2f && this.t < 3f)
			{
				this.spriteRenderer.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 16f * Mathf.Sin(this.t * 12f));
				if ((double)this.t < 2.5)
				{
					this.spriteRenderer.gameObject.transform.localPosition = new Vector3(0f, Mathf.Lerp(0f, 8f, Easing.QuarticEaseOut((this.t - 2f) * 2f)), 0f);
					return;
				}
				this.spriteRenderer.gameObject.transform.localPosition = new Vector3(0f, Mathf.Lerp(8f, 0f, Easing.QuarticEaseIn((this.t - 2.5f) * 2f)), 0f);
				return;
			}
			else
			{
				this.spriteRenderer.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
				this.spriteRenderer.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
			}
		}
	}

	// Token: 0x0400019E RID: 414
	public bool reset = true;

	// Token: 0x0400019F RID: 415
	public int count;

	// Token: 0x040001A0 RID: 416
	public float delay;

	// Token: 0x040001A1 RID: 417
	public float duration = 0.05f;

	// Token: 0x040001A2 RID: 418
	public float delaycount;

	// Token: 0x040001A3 RID: 419
	public float speed = 1f;

	// Token: 0x040001A4 RID: 420
	public ex3DSprite2 mask;

	// Token: 0x040001A5 RID: 421
	public ex3DSprite2 spriteRenderer;

	// Token: 0x040001A6 RID: 422
	public ParticleSystem system;

	// Token: 0x040001A7 RID: 423
	public Vector3 start = new Vector3(0f, 0f, 0f);

	// Token: 0x040001A8 RID: 424
	public Vector3 end = new Vector3(0f, 0f, 0f);

	// Token: 0x040001A9 RID: 425
	public Color color;

	// Token: 0x040001AA RID: 426
	public Color detail;

	// Token: 0x040001AB RID: 427
	public exTextureInfo sprite;

	// Token: 0x040001AC RID: 428
	private float t;
}
