using System;
using System.Diagnostics;
using Genkit.SimplexNoise;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200002C RID: 44
public class NoiseDesigner : MonoBehaviour
{
	// Token: 0x1700002F RID: 47
	// (get) Token: 0x0600009A RID: 154 RVA: 0x00004E50 File Offset: 0x00003050
	public int Layers
	{
		get
		{
			int num;
			try
			{
				num = Convert.ToInt32(GameObject.Find("LayersText").GetComponent<Text>().text);
			}
			catch (Exception)
			{
				num = 4;
			}
			return num;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x0600009B RID: 155 RVA: 0x00004E90 File Offset: 0x00003090
	public float OctaveStrength
	{
		get
		{
			float num;
			try
			{
				num = Convert.ToSingle(GameObject.Find("OctaveText").GetComponent<Text>().text);
			}
			catch (Exception)
			{
				num = 4f;
			}
			return num;
		}
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x0600009C RID: 156 RVA: 0x00004ED4 File Offset: 0x000030D4
	public float BaseStride
	{
		get
		{
			float num;
			try
			{
				num = Convert.ToSingle(GameObject.Find("BaseStrideText").GetComponent<Text>().text);
			}
			catch (Exception)
			{
				num = 0.001f;
			}
			return num;
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x0600009D RID: 157 RVA: 0x00004F18 File Offset: 0x00003118
	public float Cutoff
	{
		get
		{
			float num;
			try
			{
				num = Convert.ToSingle(GameObject.Find("CutoffText").GetComponent<Text>().text);
			}
			catch (Exception)
			{
				num = 0f;
			}
			return num;
		}
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x0600009E RID: 158 RVA: 0x00004F5C File Offset: 0x0000315C
	public int Bands
	{
		get
		{
			int num;
			try
			{
				num = Convert.ToInt32(GameObject.Find("BandsText").GetComponent<Text>().text);
			}
			catch (Exception)
			{
				num = 3;
			}
			return num;
		}
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x0600009F RID: 159 RVA: 0x00004F9C File Offset: 0x0000319C
	public int ZLevel
	{
		get
		{
			int num2;
			try
			{
				int num = (int)(GameObject.Find("LayerScroll").GetComponent<Scrollbar>().value * (float)this.depth);
				if (num < 0)
				{
					num = 0;
				}
				if (num > this.depth)
				{
					num = this.depth;
				}
				num2 = num;
			}
			catch (Exception)
			{
				num2 = 0;
			}
			return num2;
		}
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00004FF8 File Offset: 0x000031F8
	public void UpdateSlide()
	{
		if (this.LastRender == "Image")
		{
			this.UpdateImage();
		}
		if (this.LastRender == "Bands")
		{
			this.DrawBands();
		}
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x0000502C File Offset: 0x0000322C
	public void UpdateImage()
	{
		this.LastRender = "Image";
		Color[] array = new Color[this.width * this.height];
		int zlevel = this.ZLevel;
		float num = 0f;
		float cutoff = this.Cutoff;
		for (int i = 0; i < this.width; i++)
		{
			for (int j = 0; j < this.height; j++)
			{
				float num2 = this.NoiseData[i, j, zlevel];
				if (num2 < cutoff)
				{
					num2 = 0f;
				}
				else
				{
					num2 = (num2 - cutoff) * (1f / (1f - cutoff));
				}
				array[i + j * this.width] = new Color(num2 * 0.6f, num2 * 0.6f, num2 * 1f);
				if (num2 > num)
				{
					num = num2;
				}
			}
		}
		RawImage component = GameObject.Find("NoiseMap").GetComponent<RawImage>();
		if (component.texture == null)
		{
			component.texture = new Texture2D(this.width, this.height, TextureFormat.RGBA32, false);
			component.texture.filterMode = FilterMode.Point;
		}
		((Texture2D)component.texture).SetPixels(array);
		((Texture2D)component.texture).Apply();
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00005174 File Offset: 0x00003374
	public void DrawBands()
	{
		this.LastRender = "Bands";
		Color[] array = new Color[this.width * this.height];
		float num = 0f;
		float num2 = 1f;
		int zlevel = this.ZLevel;
		for (int i = 0; i < this.width; i++)
		{
			for (int j = 0; j < this.height; j++)
			{
				float num3 = this.NoiseData[i, j, zlevel];
				if (num3 < this.Cutoff)
				{
					num3 = 0f;
				}
				else
				{
					num3 = (num3 - this.Cutoff) * (1f / (1f - this.Cutoff));
				}
				array[i + j * this.width] = new Color(num3 * 0.6f, num3 * 0.6f, num3 * 1f);
				if (num3 > num)
				{
					num = num3;
				}
				if (num3 < num2)
				{
					num2 = num3;
				}
			}
		}
		global::UnityEngine.Debug.Log("Cel=" + num.ToString());
		global::UnityEngine.Debug.Log("Flo=" + num2.ToString());
		int bands = this.Bands;
		for (int k = 0; k < this.width; k++)
		{
			for (int l = 0; l < this.height; l++)
			{
				this.NoiseData[k, l, zlevel];
				int num4 = (int)((array[k + l * this.width].b - num2) / ((num - num2) / (float)bands));
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num4 > bands - 1)
				{
					num4 = bands - 1;
				}
				if (num4 == 0)
				{
					array[k + l * this.width] = Color.red;
				}
				if (num4 == 1)
				{
					array[k + l * this.width] = Color.yellow;
				}
				if (num4 == 2)
				{
					array[k + l * this.width] = Color.green;
				}
				if (num4 == 3)
				{
					array[k + l * this.width] = Color.blue;
				}
				if (num4 == 4)
				{
					array[k + l * this.width] = Color.magenta;
				}
				if (num4 == 5)
				{
					array[k + l * this.width] = Color.cyan;
				}
			}
		}
		RawImage component = GameObject.Find("NoiseMap").GetComponent<RawImage>();
		if (component.texture == null)
		{
			component.texture = new Texture2D(this.width, this.height, TextureFormat.RGBA32, false);
			component.texture.filterMode = FilterMode.Point;
		}
		((Texture2D)component.texture).SetPixels(array);
		((Texture2D)component.texture).Apply();
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x00005434 File Offset: 0x00003634
	public void GenerateNoise(string Type)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		LayeredNoise layeredNoise = null;
		if (Type == "Linear")
		{
			layeredNoise = LayeredNoise.CreateLinearOctiveLayers(this.Layers, this.OctaveStrength, this.BaseStride, null);
		}
		if (Type == "Crazy")
		{
			layeredNoise = LayeredNoise.CreateCrazyOctiveLayers(this.Layers, this.OctaveStrength, this.BaseStride, null);
		}
		layeredNoise.BaseSampleMultiple = 0.05f;
		this.NoiseData = layeredNoise.Generate3D(this.width, this.height, this.depth);
		stopwatch.Stop();
		this.UpdateImage();
		global::UnityEngine.Debug.Log(string.Format("Time taken to generate noise: {0} ms", stopwatch.ElapsedMilliseconds));
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x000054EA File Offset: 0x000036EA
	private void Start()
	{
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x000054EC File Offset: 0x000036EC
	private void Update()
	{
	}

	// Token: 0x040000D0 RID: 208
	private int width = 240;

	// Token: 0x040000D1 RID: 209
	private int height = 75;

	// Token: 0x040000D2 RID: 210
	private int depth = 30;

	// Token: 0x040000D3 RID: 211
	private string LastRender = "Image";

	// Token: 0x040000D4 RID: 212
	private float[,,] NoiseData;
}
