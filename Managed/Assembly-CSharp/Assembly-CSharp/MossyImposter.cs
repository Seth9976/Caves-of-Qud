using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Kobold;
using UnityEngine;

// Token: 0x0200004E RID: 78
[ExecuteAlways]
public class MossyImposter : MonoBehaviour, IConfigurableExtra
{
	// Token: 0x06000287 RID: 647 RVA: 0x0000D3EC File Offset: 0x0000B5EC
	public void Configure(string configurationString)
	{
		if (configurationString == this.lastConfig)
		{
			return;
		}
		this.lastConfig = configurationString;
		if (MossyImposter.sprites.ContainsKey(configurationString))
		{
			this.spriteRenderer.sprite = MossyImposter.sprites[configurationString];
			return;
		}
		string[] array = configurationString.Split('~', StringSplitOptions.None);
		int num = Convert.ToInt32(array[1]);
		this.texture = SpriteManager.GetUnitySprite(array[0]).texture;
		if (this.moss == null)
		{
			this.moss = new Texture2D(16, 24, TextureFormat.RGBA32, false);
		}
		global::System.Random random = new global::System.Random(num);
		int num2 = random.Next(-2, 2);
		for (int i = 0; i < this.texture.width; i++)
		{
			for (int j = 0; j < this.texture.height; j++)
			{
				if (this.texture.GetPixel(i, j).a != 0f && random.Next(j, 6 + j) + num2 < 12)
				{
					this.moss.SetPixel(i, j, ConsoleLib.Console.ColorUtility.colorFromChar('g'));
				}
				else
				{
					this.moss.SetPixel(i, j, Color.clear);
				}
			}
		}
		this.moss.Apply();
		this.moss.filterMode = global::UnityEngine.FilterMode.Point;
		this.spriteRenderer.sprite = Sprite.Create(this.moss, new Rect(0f, 0f, (float)this.moss.width, (float)this.moss.height), new Vector2(0.5f, 0.5f));
		MossyImposter.sprites.Add(configurationString, this.spriteRenderer.sprite);
	}

	// Token: 0x06000288 RID: 648 RVA: 0x0000D589 File Offset: 0x0000B789
	public void Update()
	{
		this.lastConfig = null;
		if (this.Test)
		{
			this.Test = false;
			this.Configure("Creatures/sw_bearman5.bmp~5");
		}
	}

	// Token: 0x0400016F RID: 367
	public SpriteRenderer spriteRenderer;

	// Token: 0x04000170 RID: 368
	public int count;

	// Token: 0x04000171 RID: 369
	public Texture2D texture;

	// Token: 0x04000172 RID: 370
	public Texture2D moss;

	// Token: 0x04000173 RID: 371
	public string lastConfig;

	// Token: 0x04000174 RID: 372
	private static Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

	// Token: 0x04000175 RID: 373
	public bool Test;
}
