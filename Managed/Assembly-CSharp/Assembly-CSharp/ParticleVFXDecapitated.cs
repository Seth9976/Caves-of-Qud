using System;
using ConsoleLib.Console;
using Kobold;
using UnityEngine;

// Token: 0x02000057 RID: 87
[ExecuteAlways]
public class ParticleVFXDecapitated : MonoBehaviour, CombatJuice.ICombatJuiceConfigurable
{
	// Token: 0x060002AD RID: 685 RVA: 0x0000EB9C File Offset: 0x0000CD9C
	public void configure(string configurationString)
	{
		string[] array = configurationString.Split(';', StringSplitOptions.None);
		this.color = ConsoleLib.Console.ColorUtility.ColorFromString(array[1]);
		this.detail = ConsoleLib.Console.ColorUtility.ColorFromString(array[2]);
		Texture2D texture = SpriteManager.GetUnitySprite(array[0]).texture;
		Color[] pixels = texture.GetPixels();
		Color[] pixels2 = texture.GetPixels();
		for (int i = 0; i < texture.width; i++)
		{
			for (int j = 0; j < texture.height; j++)
			{
				int num = i + j * texture.width;
				if (j > 16)
				{
					pixels2[num] = new Color(0f, 0f, 0f, 0f);
					if (pixels[num].a > 0f)
					{
						if ((double)pixels[num].r < 0.5)
						{
							pixels[num] = this.color;
						}
						else
						{
							pixels[num] = this.detail;
						}
					}
				}
				else
				{
					pixels[num] = new Color(0f, 0f, 0f, 0f);
					if (pixels2[num].a > 0f)
					{
						if ((double)pixels2[num].r < 0.5)
						{
							pixels2[num] = this.color;
						}
						else
						{
							pixels2[num] = this.detail;
						}
					}
				}
			}
		}
		this.textureTop = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);
		this.textureTop.filterMode = global::UnityEngine.FilterMode.Point;
		this.textureTop.SetPixels(pixels);
		this.textureTop.Apply();
		this.textureBottom = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);
		this.textureBottom.filterMode = global::UnityEngine.FilterMode.Point;
		this.textureBottom.SetPixels(pixels2);
		this.textureBottom.Apply();
		this.spriteTop = Sprite.Create(this.textureTop, new Rect(0f, 0f, (float)this.textureTop.width, (float)this.textureTop.height), new Vector2(0.5f, 0.5f), 1f);
		this.spriteBottom = Sprite.Create(this.textureBottom, new Rect(0f, 0f, (float)this.textureBottom.width, (float)this.textureBottom.height), new Vector2(0.5f, 0.5f), 1f);
		this.spriteRendererTop.sprite = this.spriteTop;
		this.spriteRendererBottom.sprite = this.spriteBottom;
		this.spriteRendererTop.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.spriteRendererBottom.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.spriteRendererTop.gameObject.SetActive(true);
		this.spriteRendererBottom.gameObject.SetActive(true);
		this.reset = true;
	}

	// Token: 0x060002AE RID: 686 RVA: 0x0000EEB5 File Offset: 0x0000D0B5
	private void Awake()
	{
		this.reset = true;
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0000EEC0 File Offset: 0x0000D0C0
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
			this.spriteRendererTop.gameObject.transform.localPosition = Vector3.Lerp(this.topStart, this.topEnd, this.t / this.duration);
			this.spriteRendererBottom.gameObject.transform.localPosition = Vector3.Lerp(this.bottomStart, this.bottomEnd, this.t / this.duration);
			return;
		}
		if (this.spriteRendererTop.gameObject.activeInHierarchy)
		{
			this.spriteRendererTop.gameObject.SetActive(false);
		}
		if (this.spriteRendererBottom.gameObject.activeInHierarchy)
		{
			this.spriteRendererBottom.gameObject.SetActive(false);
		}
	}

	// Token: 0x040001B7 RID: 439
	public bool reset = true;

	// Token: 0x040001B8 RID: 440
	public int count;

	// Token: 0x040001B9 RID: 441
	public float delay;

	// Token: 0x040001BA RID: 442
	public float duration = 0.5f;

	// Token: 0x040001BB RID: 443
	public float delaycount;

	// Token: 0x040001BC RID: 444
	public Texture2D textureTop;

	// Token: 0x040001BD RID: 445
	public Texture2D textureBottom;

	// Token: 0x040001BE RID: 446
	public Sprite spriteTop;

	// Token: 0x040001BF RID: 447
	public Sprite spriteBottom;

	// Token: 0x040001C0 RID: 448
	public SpriteRenderer spriteRendererTop;

	// Token: 0x040001C1 RID: 449
	public SpriteRenderer spriteRendererBottom;

	// Token: 0x040001C2 RID: 450
	public ParticleSystem system;

	// Token: 0x040001C3 RID: 451
	public Vector3 topStart = new Vector3(0f, 0f, 0f);

	// Token: 0x040001C4 RID: 452
	public Vector3 topEnd = new Vector3(6f, 1f, 0f);

	// Token: 0x040001C5 RID: 453
	public Vector3 bottomStart = new Vector3(0f, 0f, 0f);

	// Token: 0x040001C6 RID: 454
	public Vector3 bottomEnd = new Vector3(-6f, -1f, 0f);

	// Token: 0x040001C7 RID: 455
	public Color color;

	// Token: 0x040001C8 RID: 456
	public Color detail;

	// Token: 0x040001C9 RID: 457
	private float t;
}
