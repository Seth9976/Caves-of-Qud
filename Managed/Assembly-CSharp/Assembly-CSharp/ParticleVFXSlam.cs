using System;
using ConsoleLib.Console;
using Kobold;
using UnityEngine;

// Token: 0x02000058 RID: 88
[ExecuteAlways]
public class ParticleVFXSlam : MonoBehaviour, CombatJuice.ICombatJuiceConfigurable
{
	// Token: 0x060002B1 RID: 689 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
	public void configure(string configurationString)
	{
		string[] array = configurationString.Split(';', StringSplitOptions.None);
		exTextureInfo textureInfo = SpriteManager.GetTextureInfo(array[0], true);
		this.color = ConsoleLib.Console.ColorUtility.ColorFromString(array[1]);
		this.detail = ConsoleLib.Console.ColorUtility.ColorFromString(array[2]);
		this.start = GameManager.Instance.GetCellCenter(Convert.ToInt32(array[3].Split(',', StringSplitOptions.None)[0]), Convert.ToInt32(array[3].Split(',', StringSplitOptions.None)[1]), 1);
		this.end = GameManager.Instance.GetCellCenter(Convert.ToInt32(array[4].Split(',', StringSplitOptions.None)[0]), Convert.ToInt32(array[4].Split(',', StringSplitOptions.None)[1]), 1);
		Color color = ConsoleLib.Console.ColorUtility.ColorFromString("k");
		this.mask.color = color;
		this.mask.detailcolor = color;
		this.mask.backcolor = color;
		this.spriteRenderer.textureInfo = textureInfo;
		this.spriteRenderer.detailcolor = this.detail;
		this.spriteRenderer.color = this.color;
		this.spriteRenderer.backcolor = color;
		this.spriteRenderer.transform.localPosition = new Vector3(0f, 0f, -1f);
		this.spriteRenderer.gameObject.SetActive(true);
		base.gameObject.transform.position = this.start;
		this.reset = true;
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x0000F226 File Offset: 0x0000D426
	private void Awake()
	{
		this.reset = true;
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x0000F230 File Offset: 0x0000D430
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
			this.spriteRenderer.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(0f, 360f, this.t / this.duration));
			this.mask.transform.position = this.end;
		}
	}

	// Token: 0x040001CA RID: 458
	public bool reset = true;

	// Token: 0x040001CB RID: 459
	public int count;

	// Token: 0x040001CC RID: 460
	public float delay;

	// Token: 0x040001CD RID: 461
	public float duration = 0.05f;

	// Token: 0x040001CE RID: 462
	public float delaycount;

	// Token: 0x040001CF RID: 463
	public ex3DSprite2 mask;

	// Token: 0x040001D0 RID: 464
	public ex3DSprite2 spriteRenderer;

	// Token: 0x040001D1 RID: 465
	public ParticleSystem system;

	// Token: 0x040001D2 RID: 466
	public Vector3 start = new Vector3(0f, 0f, 0f);

	// Token: 0x040001D3 RID: 467
	public Vector3 end = new Vector3(6f, 1f, 0f);

	// Token: 0x040001D4 RID: 468
	public Color color;

	// Token: 0x040001D5 RID: 469
	public Color detail;

	// Token: 0x040001D6 RID: 470
	private float t;
}
