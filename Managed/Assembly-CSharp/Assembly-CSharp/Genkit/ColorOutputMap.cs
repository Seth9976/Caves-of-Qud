using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.Rules;

namespace Genkit
{
	// Token: 0x02000394 RID: 916
	public class ColorOutputMap
	{
		// Token: 0x06002285 RID: 8837 RVA: 0x000AE358 File Offset: 0x000AC558
		public ColorOutputMap(int width, int height)
		{
			this.width = width;
			this.height = height;
			this.pixels = new Color32[width * height];
			for (int i = 0; i < width * height; i++)
			{
				this.pixels[i] = ColorOutputMap.WHITE;
			}
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x000AE3A6 File Offset: 0x000AC5A6
		public ColorOutputMap(WaveCollapseModelBase model)
		{
			this.pixels = model.GetResult();
			this.width = model.FMX;
			this.height = model.FMY;
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x000AE3D2 File Offset: 0x000AC5D2
		public ColorOutputMap(WaveCollapseFastModelBase model)
		{
			this.pixels = model.GetResult();
			this.width = model.FMX;
			this.height = model.FMY;
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x000AE3FE File Offset: 0x000AC5FE
		public ColorOutputMap(Color32[] pixels, int width, int height)
		{
			this.pixels = pixels;
			this.width = width;
			this.height = height;
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x000AE41C File Offset: 0x000AC61C
		public void HMirror()
		{
			for (int i = 0; i < this.width / 2; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					this.setPixel(this.width - i - 1, j, this.getPixel(i, j));
				}
			}
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x000AE468 File Offset: 0x000AC668
		public void VMirror()
		{
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height / 2; j++)
				{
					this.setPixel(i, this.height - j - 1, this.getPixel(i, j));
				}
			}
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000AE4B4 File Offset: 0x000AC6B4
		public void Trim(Color32 colorToTrim, Color32 colorToTrimTo)
		{
			int num = 1;
			while (num != 0)
			{
				num = 0;
				for (int i = 0; i < this.width; i++)
				{
					for (int j = 0; j < this.height; j++)
					{
						if (this.getPixel(i, j).Equals(colorToTrim))
						{
							int num2 = 0;
							if (i > 0 && this.getPixel(i - 1, j).Equals(colorToTrim))
							{
								num2++;
							}
							if (i < this.width - 1 && this.getPixel(i + 1, j).Equals(colorToTrim))
							{
								num2++;
							}
							if (j > 0 && this.getPixel(i, j - 1).Equals(colorToTrim))
							{
								num2++;
							}
							if (j < this.height - 1 && this.getPixel(i, j + 1).Equals(colorToTrim))
							{
								num2++;
							}
							if (num2 == 1)
							{
								num++;
								this.setPixel(i, j, colorToTrimTo);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x000AE5B4 File Offset: 0x000AC7B4
		public int CountColor(Color32 colorToCount)
		{
			int num = 0;
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					if (this.getPixel(i, j).Equals(colorToCount))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000AE5FD File Offset: 0x000AC7FD
		public List<ColorOutputMap> CarveSubmaps(List<Color32> colorsToCombine)
		{
			return this.CarveSubmaps(colorsToCombine, ColorOutputMap.WHITE);
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x000AE60C File Offset: 0x000AC80C
		public List<ColorOutputMap> CarveSubmaps(List<Color32> colorsToCombine, Color32 visitedColor)
		{
			List<ColorOutputMap> list = new List<ColorOutputMap>();
			ColorOutputMap colorOutputMap = this.Copy();
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					if (colorsToCombine.Contains(colorOutputMap.getPixel(i, j)))
					{
						List<Location2D> list2 = new List<Location2D>();
						List<Location2D> list3 = new List<Location2D>();
						Queue<Location2D> queue = new Queue<Location2D>();
						queue.Enqueue(Location2D.Get(i, j));
						int num = int.MaxValue;
						int num2 = int.MaxValue;
						int num3 = int.MinValue;
						int num4 = int.MinValue;
						int num5 = int.MaxValue;
						int num6 = int.MaxValue;
						int num7 = int.MinValue;
						int num8 = int.MinValue;
						while (queue.Count > 0)
						{
							Location2D location2D = queue.Dequeue();
							list2.Add(location2D);
							if (colorsToCombine.Contains(colorOutputMap.getPixel(location2D.X, location2D.Y)))
							{
								if (location2D.X < num)
								{
									if (colorOutputMap.getPixel(location2D.X, location2D.Y).Equals(ColorOutputMap.BLACK))
									{
										num5 = location2D.X;
									}
									num = location2D.X;
								}
								if (location2D.X > num3)
								{
									if (colorOutputMap.getPixel(location2D.X, location2D.Y).Equals(ColorOutputMap.BLACK))
									{
										num7 = location2D.X;
									}
									num3 = location2D.X;
								}
								if (location2D.Y < num2)
								{
									if (colorOutputMap.getPixel(location2D.X, location2D.Y).Equals(ColorOutputMap.BLACK))
									{
										num6 = location2D.Y;
									}
									num2 = location2D.Y;
								}
								if (location2D.Y > num4)
								{
									if (colorOutputMap.getPixel(location2D.X, location2D.Y).Equals(ColorOutputMap.BLACK))
									{
										num8 = location2D.Y;
									}
									num4 = location2D.Y;
								}
								list3.Add(location2D);
								colorOutputMap.setPixel(location2D.X, location2D.Y, visitedColor);
								if (location2D.X - 1 >= 0 && !list2.Contains(Location2D.Get(location2D.X - 1, location2D.Y)))
								{
									queue.Enqueue(Location2D.Get(location2D.X - 1, location2D.Y));
								}
								if (location2D.X + 1 < this.width && !list2.Contains(Location2D.Get(location2D.X + 1, location2D.Y)))
								{
									queue.Enqueue(Location2D.Get(location2D.X + 1, location2D.Y));
								}
								if (location2D.Y - 1 >= 0 && !list2.Contains(Location2D.Get(location2D.X, location2D.Y - 1)))
								{
									queue.Enqueue(Location2D.Get(location2D.X, location2D.Y - 1));
								}
								if (location2D.Y - 1 < this.height && !list2.Contains(Location2D.Get(location2D.X, location2D.Y + 1)))
								{
									queue.Enqueue(Location2D.Get(location2D.X, location2D.Y + 1));
								}
							}
						}
						int num9 = num3 - num + 1;
						int num10 = num4 - num2 + 1;
						ColorOutputMap colorOutputMap2 = new ColorOutputMap(num9, num10);
						foreach (Location2D location2D2 in list3)
						{
							colorOutputMap2.setPixel(location2D2.X - num, location2D2.Y - num2, this.getPixel(location2D2.X, location2D2.Y));
						}
						colorOutputMap2.extrawidth = 0;
						colorOutputMap2.extraheight = 0;
						if (num5 != 2147483647 && num7 != -2147483648)
						{
							colorOutputMap2.extrawidth = num7 - num5 + 1;
						}
						if (num6 != 2147483647 && num8 != -2147483648)
						{
							colorOutputMap2.extraheight = num8 - num6 + 1;
						}
						list.Add(colorOutputMap2);
					}
				}
			}
			return list;
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x000AEA28 File Offset: 0x000ACC28
		public ColorOutputMap Copy()
		{
			ColorOutputMap colorOutputMap = new ColorOutputMap(this.width, this.height);
			for (int i = 0; i < this.pixels.Length; i++)
			{
				colorOutputMap.pixels[i] = this.pixels[i];
			}
			return colorOutputMap;
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x000AEA74 File Offset: 0x000ACC74
		public void Paste(ColorOutputMap map, int xpos, int ypos)
		{
			int num = 0;
			while (num < map.width && xpos + num < this.width)
			{
				int num2 = 0;
				while (num2 < map.height && ypos + num2 < this.height)
				{
					this.setPixel(xpos + num, ypos + num2, map.getPixel(num, num2));
					num2++;
				}
				num++;
			}
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x000AEAD0 File Offset: 0x000ACCD0
		public Point2D findPixelWithColor(Color32 c)
		{
			List<Point2D> list = new List<Point2D>();
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					if (this.pixels[i + j * this.width].Equals(c))
					{
						list.Add(new Point2D(i, j));
					}
				}
			}
			if (list.Count > 0)
			{
				return list[Stat.Random(0, list.Count - 1)];
			}
			return Point2D.invalid;
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x000AEB54 File Offset: 0x000ACD54
		public Point2D getFirstPixelWithColor(Color32 c)
		{
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					if (this.pixels[i + j * this.width].Equals(c))
					{
						return new Point2D(i, j);
					}
				}
			}
			return Point2D.invalid;
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000AEBAD File Offset: 0x000ACDAD
		public Color32 getPixel(int x, int y)
		{
			if (x >= 0 && y >= 0 && x < this.width && y < this.height)
			{
				return this.pixels[x + y * this.width];
			}
			return Color.magenta;
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x000AEBE9 File Offset: 0x000ACDE9
		public void setPixel(int x, int y, Color32 c)
		{
			if (x >= 0 && y >= 0 && x < this.width && y < this.height)
			{
				this.pixels[x + y * this.width] = c;
			}
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000AEC1C File Offset: 0x000ACE1C
		public void ReplaceBorders(Color32 oldColor, Color32 newColor)
		{
			for (int i = 0; i < this.width; i++)
			{
				if (WaveCollapseTools.equals(this.getPixel(i, 0), oldColor))
				{
					this.setPixel(i, 0, newColor);
				}
				if (WaveCollapseTools.equals(this.getPixel(i, this.height - 1), oldColor))
				{
					this.setPixel(i, this.height - 1, newColor);
				}
			}
			for (int j = 1; j < this.height - 1; j++)
			{
				if (WaveCollapseTools.equals(this.getPixel(0, j), oldColor))
				{
					this.setPixel(0, j, newColor);
				}
				if (WaveCollapseTools.equals(this.getPixel(this.width - 1, j), oldColor))
				{
					this.setPixel(this.width - 1, j, newColor);
				}
			}
		}

		// Token: 0x04001150 RID: 4432
		public static readonly Color32 BLACK = new Color32(0, 0, 0, byte.MaxValue);

		// Token: 0x04001151 RID: 4433
		public static readonly Color32 RED = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);

		// Token: 0x04001152 RID: 4434
		public static readonly Color32 MAGENTA = new Color32(byte.MaxValue, byte.MaxValue, 0, byte.MaxValue);

		// Token: 0x04001153 RID: 4435
		public static readonly Color32 GREEN = new Color32(0, byte.MaxValue, 0, byte.MaxValue);

		// Token: 0x04001154 RID: 4436
		public static readonly Color32 BLUE = new Color32(0, 0, byte.MaxValue, byte.MaxValue);

		// Token: 0x04001155 RID: 4437
		public static readonly Color32 WHITE = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		// Token: 0x04001156 RID: 4438
		public static readonly Color32 YELLOW = new Color32(byte.MaxValue, byte.MaxValue, 0, byte.MaxValue);

		// Token: 0x04001157 RID: 4439
		public Color32[] pixels;

		// Token: 0x04001158 RID: 4440
		public int width;

		// Token: 0x04001159 RID: 4441
		public int height;

		// Token: 0x0400115A RID: 4442
		public int extrawidth;

		// Token: 0x0400115B RID: 4443
		public int extraheight;
	}
}
