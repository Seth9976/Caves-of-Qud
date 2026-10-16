using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using Genkit;
using Kobold;
using Qud.API;
using UnityEngine;
using UnityEngine.UI;
using XRL;
using XRL.Rules;
using XRL.UI;
using XRL.World;
using XRL.World.Parts;

// Token: 0x02000080 RID: 128
public class MapScrollerController : MonoBehaviour
{
	// Token: 0x060004AB RID: 1195 RVA: 0x0001EAF5 File Offset: 0x0001CCF5
	private void Start()
	{
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0001EAF8 File Offset: 0x0001CCF8
	public void SetHighlights(IEnumerable<Location2D> highlights)
	{
		if ((highlights == null || highlights.Count<Location2D>() == 0) && (this.highlights == null || this.highlights.Count == 0))
		{
			return;
		}
		if (highlights == null || this.highlights == null || highlights.Count<Location2D>() != this.highlights.Count)
		{
			this.highlightsUpdated = true;
		}
		else
		{
			this.highlightsUpdated = false;
			int num = 0;
			foreach (Location2D location2D in highlights)
			{
				if (this.highlights[num] != location2D)
				{
					this.highlightsUpdated = true;
					break;
				}
				num++;
			}
		}
		this.highlights.Clear();
		if (highlights != null)
		{
			this.highlights.AddRange(highlights);
		}
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x0001EBC8 File Offset: 0x0001CDC8
	public void SetPins(IEnumerable<MapScrollerController.MapPinData> data)
	{
		foreach (global::UnityEngine.GameObject gameObject in this.pins)
		{
			MapScrollerController.pinPool.Enqueue(gameObject);
			gameObject.SetActive(false);
		}
		this.pins.Clear();
		foreach (MapScrollerController.MapPinData mapPinData in data)
		{
			global::UnityEngine.GameObject gameObject2;
			if (MapScrollerController.pinPool.Count > 0)
			{
				gameObject2 = MapScrollerController.pinPool.Dequeue();
				gameObject2.SetActive(true);
			}
			else
			{
				gameObject2 = global::UnityEngine.Object.Instantiate<global::UnityEngine.GameObject>(this.pinPrefab);
			}
			this.pins.Add(gameObject2);
			gameObject2.transform.SetParent(this.mapTransform, false);
			gameObject2.GetComponent<MapScrollerPinItem>().SetData(mapPinData);
			gameObject2.GetComponent<RectTransform>().anchoredPosition = new Vector2((float)(mapPinData.x * 32 + 20), (float)(mapPinData.y * -48 + 24));
			gameObject2.SetActive(true);
		}
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x0001ECF8 File Offset: 0x0001CEF8
	public void RenderAccomplishment(JournalAccomplishment a)
	{
		int num = 144;
		int num2 = 120;
		if (this.mapTexture == null)
		{
			this.mapTexture = new Texture2D(num, num2, TextureFormat.RGBA32, false);
			this.mapTexture.filterMode = global::UnityEngine.FilterMode.Point;
			this.mapImage.sprite = Sprite.Create(this.mapTexture, new Rect(0f, 0f, (float)num, (float)num2), new Vector2(0f, 0f));
		}
		this.marginaliaText.SetText(a.GetDisplayText());
		this.marginaliaTime.SetText(Calendar.GetMarginaliaTime(a.Time));
		if (a.Screenshot == null)
		{
			return;
		}
		int num3 = 16;
		int num4 = 24;
		Color color = ConsoleLib.Console.ColorUtility.FromWebColor("041312");
		Color color2 = ConsoleLib.Console.ColorUtility.ColorMap['K'];
		if (MapScrollerController.mapPixels == null)
		{
			MapScrollerController.mapPixels = this.mapTexture.GetPixels();
		}
		for (int i = 0; i < MapScrollerController.mapPixels.Length; i++)
		{
			MapScrollerController.mapPixels[i] = new Color(0f, 0f, 0f, 0f);
		}
		for (int j = 0; j < 9; j++)
		{
			for (int k = 0; k < 5; k++)
			{
				SnapshotRenderable snapshotRenderable = a.Screenshot[j + k * 9];
				Sprite unitySprite = SpriteManager.GetUnitySprite(snapshotRenderable.GetSpriteName());
				ColorChars colorChars = snapshotRenderable.getColorChars();
				string spriteName = snapshotRenderable.GetSpriteName();
				Color color3 = ConsoleLib.Console.ColorUtility.colorFromChar(colorChars.foreground);
				Color color4 = ConsoleLib.Console.ColorUtility.colorFromChar(snapshotRenderable.getDetailColor());
				if (!MapScrollerController.texturePixels.ContainsKey(spriteName))
				{
					MapScrollerController.texturePixels.Add(spriteName, unitySprite.texture.GetPixels());
				}
				Color[] array = MapScrollerController.texturePixels[spriteName];
				for (int l = 0; l < num3; l++)
				{
					for (int m = 0; m < num4; m++)
					{
						Color color5 = (snapshotRenderable.getHFlip() ? array[num3 - l - 1 + m * num3] : array[l + m * num3]);
						Color color6 = color;
						int num5 = j * num3 + l;
						int num6 = (5 - k - 1) * num4 + m;
						float num7;
						if (this.highlights == null || this.highlights.Count == 0)
						{
							num7 = 0f;
						}
						else
						{
							float num8 = 0f;
							num7 = Mathf.Min(1f, Mathf.Max(0f, Mathf.Sqrt(num8) / 48f));
						}
						if (color5.a <= 0f)
						{
							color6 = color;
						}
						else if ((double)color5.r < 0.5)
						{
							color6 = Color.Lerp(color3, color2, num7);
						}
						else if ((double)color5.r > 0.5)
						{
							color6 = Color.Lerp(color4, color2, num7);
						}
						MapScrollerController.mapPixels[num5 + num6 * (9 * num3)] = color6;
					}
				}
			}
		}
		this.mapTexture.SetPixels(MapScrollerController.mapPixels);
		this.mapTexture.Apply();
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x0001F00C File Offset: 0x0001D20C
	public void RefreshMapCoda()
	{
		if (!this.wasCoda)
		{
			this.highlightsUpdated = true;
			this.wasCoda = false;
		}
		if (!this.highlightsUpdated)
		{
			return;
		}
		this.highlightsUpdated = false;
		this.highlights.Clear();
		Stat.ReseedFrom("CODAMAP", false);
		int num = Stat.Random(5, 75);
		int num2 = Stat.Random(5, 20);
		Location2D location2D = Location2D.Get(num, num2);
		this.highlights.Add(location2D);
		this.SetTarget(location2D.X, location2D.Y);
		int num3 = 1280;
		int num4 = 600;
		if (this.mapTexture == null)
		{
			this.mapTexture = new Texture2D(num3, num4, TextureFormat.RGBA32, false);
			this.mapTexture.filterMode = global::UnityEngine.FilterMode.Point;
			this.mapImage.sprite = Sprite.Create(this.mapTexture, new Rect(0f, 0f, (float)num3, (float)num4), new Vector2(0f, 0f));
		}
		The.Game.ZoneManager.GetZone("JoppaWorld");
		int num5 = 16;
		int num6 = 24;
		Color color = ConsoleLib.Console.ColorUtility.FromWebColor("041312");
		Color color2 = ConsoleLib.Console.ColorUtility.FromWebColor("041312");
		BallBag<string> ballBag = new BallBag<string>(0);
		Zone currentZone = The.Player.CurrentZone;
		if (((currentZone != null) ? currentZone.GetZoneProperty("Region", "") : null) == "Saltmarsh")
		{
			ballBag.Add("saltmarsh", 100);
			ballBag.Add("canyon", 10);
			ballBag.Add("hills", 10);
		}
		else
		{
			Zone currentZone2 = The.Player.CurrentZone;
			if (((currentZone2 != null) ? currentZone2.GetZoneProperty("Region", "") : null) == "DesertCanyon")
			{
				ballBag.Add("saltmarsh", 10);
				ballBag.Add("canyon", 100);
				ballBag.Add("hills", 10);
			}
			else
			{
				Zone currentZone3 = The.Player.CurrentZone;
				if (((currentZone3 != null) ? currentZone3.GetZoneProperty("Region", "") : null) == "Hills")
				{
					ballBag.Add("saltmarsh", 10);
					ballBag.Add("canyon", 10);
					ballBag.Add("hills", 100);
				}
			}
		}
		int count = this.highlights.Count;
		if (MapScrollerController.mapPixels == null)
		{
			MapScrollerController.mapPixels = this.mapTexture.GetPixels();
		}
		for (int i = 0; i < 80; i++)
		{
			for (int j = 0; j < 25; j++)
			{
				string text = "Text/32.bmp";
				int num7 = location2D.Distance(i, j);
				Color color3 = ConsoleLib.Console.ColorUtility.colorFromChar('g');
				Color color4 = ConsoleLib.Console.ColorUtility.colorFromChar('k');
				if (num7 == 0)
				{
					text = "Terrain/sw_joppa.bmp";
					color3 = ConsoleLib.Console.ColorUtility.colorFromChar(Crayons.GetRandomColor(null)[0]);
					color4 = ConsoleLib.Console.ColorUtility.colorFromChar('w');
				}
				else if (num7 < 4)
				{
					string text2 = ballBag.PluckOne();
					if (text2 == "saltmarsh")
					{
						text = "terrain/tile_swamp1.bmp";
						color3 = ConsoleLib.Console.ColorUtility.colorFromChar('g');
						color4 = ConsoleLib.Console.ColorUtility.colorFromChar('k');
					}
					else if (text2 == "canyon")
					{
						text = ((Stat.Random(1, 2) == 1) ? "Terrain/sw_canyon.bmp" : "Terrain/sw_hills2.bmp");
						color3 = ConsoleLib.Console.ColorUtility.colorFromChar('w');
						color4 = ConsoleLib.Console.ColorUtility.colorFromChar('k');
					}
					else if (text2 == "hills")
					{
						text = "Terrain/sw_hills.bmp";
						color3 = ConsoleLib.Console.ColorUtility.colorFromChar('y');
						color4 = ConsoleLib.Console.ColorUtility.colorFromChar('k');
					}
				}
				else
				{
					color3 = ConsoleLib.Console.ColorUtility.colorFromChar('k');
					color4 = ConsoleLib.Console.ColorUtility.colorFromChar('k');
				}
				Sprite unitySprite = SpriteManager.GetUnitySprite(text);
				if (!MapScrollerController.texturePixels.ContainsKey(text))
				{
					MapScrollerController.texturePixels.Add(text, unitySprite.texture.GetPixels());
				}
				Color[] array = MapScrollerController.texturePixels[text];
				for (int k = 0; k < num5; k++)
				{
					for (int l = 0; l < num6; l++)
					{
						Color color5 = array[k + l * num5];
						Color color6 = color;
						int num8 = i * num5 + k;
						int num9 = (24 - j) * num6 + l;
						float num10;
						if (this.highlights == null || count == 0)
						{
							num10 = 0f;
						}
						else
						{
							float num11 = float.MaxValue;
							for (int m = 0; m < this.highlights.Count; m++)
							{
								Location2D location2D2 = this.highlights[m];
								float num12 = (float)(location2D2.X * num5 + num5 / 2);
								float num13 = (float)(location2D2.Y * num6 + num6 / 4);
								int num14 = i * num5 + k;
								int num15 = j * (num6 + 1) - l;
								float num16 = (num12 - (float)num14) * (num12 - (float)num14) + (num13 - (float)num15) * (num13 - (float)num15);
								if (num16 < num11)
								{
									num11 = num16;
								}
							}
							if (num11 < 2400f)
							{
								num10 = Mathf.Min(1f, Mathf.Max(0f, Mathf.Sqrt(num11) / 48f));
							}
							else
							{
								num10 = 1f;
							}
						}
						if (color5.a <= 0f)
						{
							color6 = color;
						}
						else if ((double)color5.r < 0.5)
						{
							color6 = Color.Lerp(color3, color2, num10);
						}
						else if ((double)color5.r > 0.5)
						{
							color6 = Color.Lerp(color4, color2, num10);
						}
						MapScrollerController.mapPixels[num8 + num9 * (80 * num5)] = color6;
					}
				}
			}
		}
		this.mapTexture.SetPixels(MapScrollerController.mapPixels);
		this.mapTexture.Apply();
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x0001F590 File Offset: 0x0001D790
	public void RefreshMap()
	{
		if (CodaSystem.InCoda())
		{
			this.RefreshMapCoda();
			return;
		}
		if (!this.highlightsUpdated)
		{
			return;
		}
		this.highlightsUpdated = false;
		int num = 1280;
		int num2 = 600;
		if (this.mapTexture == null)
		{
			this.mapTexture = new Texture2D(num, num2, TextureFormat.RGBA32, false);
			this.mapTexture.filterMode = global::UnityEngine.FilterMode.Point;
			this.mapImage.sprite = Sprite.Create(this.mapTexture, new Rect(0f, 0f, (float)num, (float)num2), new Vector2(0f, 0f));
		}
		Zone zone = The.Game.ZoneManager.GetZone("JoppaWorld");
		int num3 = 16;
		int num4 = 24;
		Color color = ConsoleLib.Console.ColorUtility.FromWebColor("041312");
		Color color2 = ConsoleLib.Console.ColorUtility.ColorMap['K'];
		using (RenderEvent renderEvent = RenderEvent.Pool.Get())
		{
			int count = this.highlights.Count;
			if (MapScrollerController.mapPixels == null)
			{
				MapScrollerController.mapPixels = this.mapTexture.GetPixels();
			}
			for (int i = 0; i < 80; i++)
			{
				for (int j = 0; j < 25; j++)
				{
					zone.GetCell(i, j).Render(renderEvent, true, LightLevel.Light, true, false, false, null);
					Sprite unitySprite = SpriteManager.GetUnitySprite(renderEvent.GetSpriteName());
					if (!MapScrollerController.texturePixels.ContainsKey(renderEvent.GetSpriteName()))
					{
						MapScrollerController.texturePixels.Add(renderEvent.GetSpriteName(), unitySprite.texture.GetPixels());
					}
					Color[] array = MapScrollerController.texturePixels[renderEvent.GetSpriteName()];
					Color foregroundColor = renderEvent.GetForegroundColor();
					for (int k = 0; k < num3; k++)
					{
						for (int l = 0; l < num4; l++)
						{
							Color color3 = array[k + l * num3];
							Color color4 = color;
							int num5 = i * num3 + k;
							int num6 = (24 - j) * num4 + l;
							float num7;
							if (this.highlights == null || count == 0)
							{
								num7 = 0f;
							}
							else
							{
								float num8 = float.MaxValue;
								for (int m = 0; m < this.highlights.Count; m++)
								{
									Location2D location2D = this.highlights[m];
									if (location2D != null)
									{
										float num9 = (float)(location2D.X * num3 + num3 / 2);
										float num10 = (float)(location2D.Y * num4 + num4 / 4);
										int num11 = i * num3 + k;
										int num12 = j * (num4 + 1) - l;
										float num13 = (num9 - (float)num11) * (num9 - (float)num11) + (num10 - (float)num12) * (num10 - (float)num12);
										if (num13 < num8)
										{
											num8 = num13;
										}
									}
								}
								if (num8 < 2400f)
								{
									num7 = Mathf.Min(1f, Mathf.Max(0f, Mathf.Sqrt(num8) / 48f));
								}
								else
								{
									num7 = 1f;
								}
							}
							if (color3.a <= 0f)
							{
								color4 = color;
							}
							else if ((double)color3.r < 0.5)
							{
								color4 = Color.Lerp(foregroundColor, color2, num7);
							}
							else if ((double)color3.r > 0.5)
							{
								color4 = Color.Lerp(renderEvent.GetDetailColor(), color2, num7);
							}
							MapScrollerController.mapPixels[num5 + num6 * (80 * num3)] = color4;
						}
					}
				}
			}
			this.mapTexture.SetPixels(MapScrollerController.mapPixels);
			this.mapTexture.Apply();
		}
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x0001F928 File Offset: 0x0001DB28
	public void SetTarget(int x, int y)
	{
		this.mapTarget.anchoredPosition = new Vector2((float)(x * 32), (float)(y * -48));
		this.mapLerpStart = this.mapTransform.anchoredPosition;
		Vector2 sizeDelta = (base.transform as RectTransform).sizeDelta;
		this.mapLerpEnd = new Vector2(-((float)(x * 32 + 16) - sizeDelta.x / 2f), (float)(y * 48 + 24) - sizeDelta.y / 2f);
		this.mapLerpTime = this.MAP_LERP_DURATION;
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x0001F9B4 File Offset: 0x0001DBB4
	private void Update()
	{
		if (this.mapTransform != null)
		{
			if (this.mapLerpTime > 0f)
			{
				this.mapTransform.anchoredPosition = Vector2.Lerp(this.mapLerpStart, this.mapLerpEnd, Easing.CubicEaseInOut(this.MAP_LERP_DURATION - this.mapLerpTime));
				this.mapLerpTime -= Time.deltaTime;
			}
			float num = 512f;
			string text = ControlManager.ResolveAxisDirection("UI:DetailsNavigate");
			if (text != null && text.Contains("N"))
			{
				this.mapTransform.anchoredPosition += new Vector2(0f, -num * Time.deltaTime);
			}
			if (text != null && text.Contains("S"))
			{
				this.mapTransform.anchoredPosition += new Vector2(0f, num * Time.deltaTime);
			}
			if (text != null && text.Contains("E"))
			{
				this.mapTransform.anchoredPosition += new Vector2(-num * Time.deltaTime, 0f);
			}
			if (text != null && text.Contains("W"))
			{
				this.mapTransform.anchoredPosition += new Vector2(num * Time.deltaTime, 0f);
			}
		}
	}

	// Token: 0x04000397 RID: 919
	public RectTransform mapTarget;

	// Token: 0x04000398 RID: 920
	public RectTransform mapTransform;

	// Token: 0x04000399 RID: 921
	public Image mapImage;

	// Token: 0x0400039A RID: 922
	private float MAP_LERP_DURATION = 1.25f;

	// Token: 0x0400039B RID: 923
	private float mapLerpTime;

	// Token: 0x0400039C RID: 924
	private Vector2 mapLerpStart;

	// Token: 0x0400039D RID: 925
	private Vector2 mapLerpEnd;

	// Token: 0x0400039E RID: 926
	public global::UnityEngine.GameObject pinPrefab;

	// Token: 0x0400039F RID: 927
	public List<global::UnityEngine.GameObject> pins = new List<global::UnityEngine.GameObject>();

	// Token: 0x040003A0 RID: 928
	private Texture2D mapTexture;

	// Token: 0x040003A1 RID: 929
	private List<Location2D> highlights = new List<Location2D>(3000);

	// Token: 0x040003A2 RID: 930
	private static Queue<global::UnityEngine.GameObject> pinPool = new Queue<global::UnityEngine.GameObject>();

	// Token: 0x040003A3 RID: 931
	public UITextSkin marginaliaText;

	// Token: 0x040003A4 RID: 932
	public UITextSkin marginaliaTime;

	// Token: 0x040003A5 RID: 933
	private static Dictionary<string, Color[]> texturePixels = new Dictionary<string, Color[]>();

	// Token: 0x040003A6 RID: 934
	private static Color[] mapPixels = null;

	// Token: 0x040003A7 RID: 935
	private bool wasCoda;

	// Token: 0x040003A8 RID: 936
	public bool highlightsUpdated = true;

	// Token: 0x020015A1 RID: 5537
	public class MapPinData
	{
		// Token: 0x04005703 RID: 22275
		public int x;

		// Token: 0x04005704 RID: 22276
		public int y;

		// Token: 0x04005705 RID: 22277
		public string title;

		// Token: 0x04005706 RID: 22278
		public string details;
	}
}
