using System;
using ConsoleLib.Console;
using Kobold;
using UnityEngine;

// Token: 0x02000059 RID: 89
[ExecuteAlways]
public class ParticleVFXThrow : MonoBehaviour, CombatJuice.ICombatJuiceConfigurable
{
	// Token: 0x060002B5 RID: 693 RVA: 0x0000F3D4 File Offset: 0x0000D5D4
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

	// Token: 0x060002B6 RID: 694 RVA: 0x0000F52A File Offset: 0x0000D72A
	private void Awake()
	{
		this.reset = true;
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x0000F534 File Offset: 0x0000D734
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
			base.gameObject.transform.position += new Vector3(0f, Mathf.Sin(this.t / this.duration * 3.1415927f) * 16f, 0f);
			this.spriteRenderer.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(0f, 360f, this.t / this.duration));
		}
	}

	// Token: 0x040001D7 RID: 471
	public bool reset = true;

	// Token: 0x040001D8 RID: 472
	public int count;

	// Token: 0x040001D9 RID: 473
	public float delay;

	// Token: 0x040001DA RID: 474
	public float duration = 0.4f;

	// Token: 0x040001DB RID: 475
	public float delaycount;

	// Token: 0x040001DC RID: 476
	public ex3DSprite2 spriteRenderer;

	// Token: 0x040001DD RID: 477
	public ParticleSystem system;

	// Token: 0x040001DE RID: 478
	public Vector3 start = new Vector3(0f, 0f, 0f);

	// Token: 0x040001DF RID: 479
	public Vector3 end = new Vector3(6f, 1f, 0f);

	// Token: 0x040001E0 RID: 480
	public Color color;

	// Token: 0x040001E1 RID: 481
	public Color detail;

	// Token: 0x040001E2 RID: 482
	private float t;
}
