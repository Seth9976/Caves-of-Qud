using System;
using ConsoleLib.Console;
using Kobold;
using UnityEngine;

// Token: 0x0200005A RID: 90
[ExecuteAlways]
public class ParticleVFXThrowFast : MonoBehaviour, CombatJuice.ICombatJuiceConfigurable
{
	// Token: 0x060002B9 RID: 697 RVA: 0x0000F70C File Offset: 0x0000D90C
	public void configure(string configurationString)
	{
		string[] array = configurationString.Split(';', StringSplitOptions.None);
		exTextureInfo textureInfo = SpriteManager.GetTextureInfo(array[0], true);
		this.color = ConsoleLib.Console.ColorUtility.ColorFromString(array[1]);
		this.detail = ConsoleLib.Console.ColorUtility.ColorFromString(array[2]);
		this.start = GameManager.Instance.GetCellCenter(Convert.ToInt32(array[3].Split(',', StringSplitOptions.None)[0]), Convert.ToInt32(array[3].Split(',', StringSplitOptions.None)[1]), 1);
		this.end = GameManager.Instance.GetCellCenter(Convert.ToInt32(array[4].Split(',', StringSplitOptions.None)[0]), Convert.ToInt32(array[4].Split(',', StringSplitOptions.None)[1]), 1);
		ConsoleLib.Console.ColorUtility.ColorFromString("k");
		this.spriteRenderer.textureInfo = textureInfo;
		this.spriteRenderer.detailcolor = this.detail;
		this.spriteRenderer.color = this.color;
		this.spriteRenderer.backcolor = new Color(0f, 0f, 0f, 0f);
		this.spriteRenderer.transform.localPosition = new Vector3(0f, 0f, -1f);
		this.spriteRenderer.gameObject.SetActive(true);
		base.gameObject.transform.position = this.start;
		this.reset = true;
	}

	// Token: 0x060002BA RID: 698 RVA: 0x0000F862 File Offset: 0x0000DA62
	private void Awake()
	{
		this.reset = true;
	}

	// Token: 0x060002BB RID: 699 RVA: 0x0000F86C File Offset: 0x0000DA6C
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
			base.gameObject.transform.position = Vector3.Lerp(this.start, this.end, this.t / this.duration);
			base.gameObject.transform.position += new Vector3(0f, Mathf.Sin(this.t / this.duration * 3.1415927f) * 2f, 0f);
			this.spriteRenderer.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(0f, 180f, this.t / this.duration));
		}
	}

	// Token: 0x040001E3 RID: 483
	public bool reset = true;

	// Token: 0x040001E4 RID: 484
	public int count;

	// Token: 0x040001E5 RID: 485
	public float delay;

	// Token: 0x040001E6 RID: 486
	public float duration = 0.4f;

	// Token: 0x040001E7 RID: 487
	public float delaycount;

	// Token: 0x040001E8 RID: 488
	public ex3DSprite2 spriteRenderer;

	// Token: 0x040001E9 RID: 489
	public ParticleSystem system;

	// Token: 0x040001EA RID: 490
	public Vector3 start = new Vector3(0f, 0f, 0f);

	// Token: 0x040001EB RID: 491
	public Vector3 end = new Vector3(6f, 1f, 0f);

	// Token: 0x040001EC RID: 492
	public Color color;

	// Token: 0x040001ED RID: 493
	public Color detail;

	// Token: 0x040001EE RID: 494
	private float t;
}
