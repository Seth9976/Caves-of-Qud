using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Genkit;
using UnityEngine;
using XRL;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World;

namespace ConsoleLib.Console
{
	// Token: 0x02001544 RID: 5444
	public class ScreenBuffer
	{
		// Token: 0x0600D441 RID: 54337 RVA: 0x004ED781 File Offset: 0x004EB981
		public ConsoleChar get(int x, int y)
		{
			if (x < 0)
			{
				return null;
			}
			if (y < 0)
			{
				return null;
			}
			if (x >= this._Width)
			{
				return null;
			}
			if (y >= this._Height)
			{
				return null;
			}
			return this.Buffer[x, y];
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x0600D442 RID: 54338 RVA: 0x004ED7B2 File Offset: 0x004EB9B2
		public ConsoleChar CurrentChar
		{
			get
			{
				return this.Buffer[this._X, this._Y];
			}
		}

		// Token: 0x0600D443 RID: 54339 RVA: 0x004ED7CB File Offset: 0x004EB9CB
		public ScreenBuffer WithBase()
		{
			XRLCore.Core.RenderBaseToBuffer(this);
			return this;
		}

		// Token: 0x0600D444 RID: 54340 RVA: 0x004ED7D9 File Offset: 0x004EB9D9
		public ScreenBuffer WithMap()
		{
			XRLCore.Core.RenderMapToBuffer(this);
			return this;
		}

		// Token: 0x0600D445 RID: 54341 RVA: 0x004ED7E7 File Offset: 0x004EB9E7
		public ScreenBuffer RenderBase()
		{
			return this.WithBase();
		}

		// Token: 0x0600D446 RID: 54342 RVA: 0x004ED7EF File Offset: 0x004EB9EF
		public ScreenBuffer Draw()
		{
			XRLCore._Console.DrawBuffer(this, null, false);
			return this;
		}

		// Token: 0x0600D447 RID: 54343 RVA: 0x004ED800 File Offset: 0x004EBA00
		public ScreenBuffer ScrollUp()
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (i == this.Height - 1)
					{
						this.Buffer[j, i].Char = ' ';
						this.Buffer[j, i].SetForeground('y');
						this.Buffer[j, i].SetBackground('k');
					}
					else
					{
						this.Buffer[j, i].Copy(this.Buffer[j, i + 1]);
					}
				}
			}
			return this;
		}

		// Token: 0x0600D448 RID: 54344 RVA: 0x004ED8A0 File Offset: 0x004EBAA0
		public override string ToString()
		{
			this.TSB.Length = 0;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.TSB.Append(this.Buffer[j, i].Char.ToString());
				}
				this.TSB.Append("\n");
			}
			return this.TSB.ToString();
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x0600D449 RID: 54345 RVA: 0x004ED91D File Offset: 0x004EBB1D
		// (set) Token: 0x0600D44A RID: 54346 RVA: 0x004ED925 File Offset: 0x004EBB25
		public int X
		{
			get
			{
				return this._X;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				if (value >= this._Width)
				{
					value %= this._Width;
				}
				this._X = value;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x0600D44B RID: 54347 RVA: 0x004ED948 File Offset: 0x004EBB48
		// (set) Token: 0x0600D44C RID: 54348 RVA: 0x004ED950 File Offset: 0x004EBB50
		public int Y
		{
			get
			{
				return this._Y;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				if (value >= this._Width)
				{
					value %= this._Width;
				}
				this._Y = value;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x0600D44D RID: 54349 RVA: 0x004ED973 File Offset: 0x004EBB73
		// (set) Token: 0x0600D44E RID: 54350 RVA: 0x004ED97B File Offset: 0x004EBB7B
		public int Width
		{
			get
			{
				return this._Width;
			}
			set
			{
				this._Width = value;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x0600D44F RID: 54351 RVA: 0x004ED984 File Offset: 0x004EBB84
		// (set) Token: 0x0600D450 RID: 54352 RVA: 0x004ED98C File Offset: 0x004EBB8C
		public int Height
		{
			get
			{
				return this._Height;
			}
			set
			{
				this._Height = value;
			}
		}

		// Token: 0x0600D451 RID: 54353 RVA: 0x004ED995 File Offset: 0x004EBB95
		public static ScreenBuffer create(int w, int h)
		{
			return new ScreenBuffer(w, h);
		}

		// Token: 0x0600D452 RID: 54354 RVA: 0x004ED99E File Offset: 0x004EBB9E
		public static ScreenBuffer GetScrapBuffer1(bool bLoadFromCurrent = false)
		{
			return TextConsole.GetScrapBuffer1(bLoadFromCurrent);
		}

		// Token: 0x0600D453 RID: 54355 RVA: 0x004ED9A6 File Offset: 0x004EBBA6
		public static ScreenBuffer GetScrapBuffer2(bool bLoadFromCurrent = false)
		{
			return TextConsole.GetScrapBuffer2(bLoadFromCurrent);
		}

		// Token: 0x0600D454 RID: 54356 RVA: 0x004ED9B0 File Offset: 0x004EBBB0
		private ScreenBuffer(int w, int h)
		{
			this.Width = w;
			this.Height = h;
			this.Buffer = new ConsoleChar[w, h];
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					this.Buffer[i, j] = new ConsoleChar();
				}
			}
		}

		// Token: 0x0600D455 RID: 54357 RVA: 0x004EDA30 File Offset: 0x004EBC30
		public void Shake(int MS, int Duration, TextConsole Console)
		{
			int num = MS / Duration;
			ScreenBuffer screenBuffer = new ScreenBuffer(this.Width, this.Height);
			for (int i = 0; i < num; i++)
			{
				int num2 = Stat.Random(-1, 1);
				int num3 = Stat.Random(-1, 1);
				for (int j = 0; j < this.Height; j++)
				{
					for (int k = 0; k < this.Width; k++)
					{
						int num4 = k + num2;
						int num5 = j + num3;
						if (num4 >= 0 && num4 < this.Width && num5 >= 0 && num5 < this.Height)
						{
							ConsoleChar consoleChar = this[k, j];
							screenBuffer[num4, num5].Copy(consoleChar);
						}
						else
						{
							screenBuffer[k, j].Char = ' ';
						}
					}
				}
				Console.DrawBuffer(screenBuffer, null, false);
				Thread.Sleep(Duration);
			}
		}

		// Token: 0x17000CC5 RID: 3269
		public ConsoleChar this[int r, int c]
		{
			get
			{
				return this.Buffer[r, c];
			}
			set
			{
				if (r < this.Width && c < this.Height && r >= 0 && c >= 0)
				{
					this.Buffer[r, c] = value;
				}
			}
		}

		// Token: 0x17000CC6 RID: 3270
		public ConsoleChar this[Cell C]
		{
			get
			{
				return this.Buffer[C.X, C.Y];
			}
			set
			{
				this.Buffer[C.X, C.Y] = value;
			}
		}

		// Token: 0x17000CC7 RID: 3271
		public ConsoleChar this[Location2D L]
		{
			get
			{
				return this.Buffer[L.X, L.Y];
			}
			set
			{
				this.Buffer[L.X, L.Y] = value;
			}
		}

		// Token: 0x0600D45C RID: 54364 RVA: 0x004EDBAC File Offset: 0x004EBDAC
		public void Clear()
		{
			this.focusPosition = Point2D.invalid;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.Buffer[j, i].Clear();
				}
			}
		}

		// Token: 0x0600D45D RID: 54365 RVA: 0x004EDBF8 File Offset: 0x004EBDF8
		public void Clear(int X1, int Y1, int X2, int Y2)
		{
			for (int i = X1; i <= X2; i++)
			{
				for (int j = Y1; j <= Y2; j++)
				{
					this.Buffer[i, j].Clear();
				}
			}
		}

		// Token: 0x0600D45E RID: 54366 RVA: 0x004EDC30 File Offset: 0x004EBE30
		public ScreenBuffer Copy(ScreenBuffer Source)
		{
			if (Source != null)
			{
				int num = Math.Min(this.Height, Source.Height);
				int num2 = Math.Min(this.Width, Source.Width);
				ConsoleChar[,] buffer = this.Buffer;
				for (int i = 0; i < num; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						buffer[j, i].Copy(Source[j, i]);
					}
				}
				this.focusPosition = Source.focusPosition;
			}
			return this;
		}

		// Token: 0x0600D45F RID: 54367 RVA: 0x004EDCAB File Offset: 0x004EBEAB
		public ScreenBuffer Goto(int x, int y)
		{
			this.X = x;
			this.Y = y;
			return this;
		}

		// Token: 0x0600D460 RID: 54368 RVA: 0x004EDCBC File Offset: 0x004EBEBC
		public ScreenBuffer Goto(Cell C)
		{
			if (C != null)
			{
				this.X = C.X;
				this.Y = C.Y;
			}
			return this;
		}

		// Token: 0x0600D461 RID: 54369 RVA: 0x004EDCDA File Offset: 0x004EBEDA
		public ScreenBuffer Goto(global::XRL.World.GameObject obj)
		{
			if (obj != null)
			{
				this.Goto(obj.CurrentCell);
			}
			return this;
		}

		// Token: 0x0600D462 RID: 54370 RVA: 0x004EDCF0 File Offset: 0x004EBEF0
		public void Write(ushort c, ushort f, ushort b)
		{
			ushort num = ColorUtility.MakeColor(f, b);
			if (this._X < this.Width && this._X >= 0 && this._Y < this.Height && this._Y >= 0)
			{
				this[this._X, this._Y].Char = (char)c;
				this[this._X, this._Y].SetColorsFromOldCharCode(num);
			}
			if (this.X == this.Width - 1)
			{
				return;
			}
			int num2 = this.X;
			this.X = num2 + 1;
			if (this.X >= this.Width)
			{
				this.X = 0;
				num2 = this.Y;
				this.Y = num2 + 1;
			}
			if (this.Y >= this.Height)
			{
				this.Y = 0;
			}
		}

		// Token: 0x0600D463 RID: 54371 RVA: 0x004EDDC0 File Offset: 0x004EBFC0
		public void Write(short c)
		{
			if (this._X < this.Width && this._X >= 0 && this._Y < this.Height && this._Y >= 0)
			{
				ConsoleChar consoleChar = this[this._X, this._Y];
				consoleChar.Char = (char)c;
				consoleChar.Foreground = The.Color.Gray;
				consoleChar.Background = The.Color.Black;
			}
			int num = this.X;
			this.X = num + 1;
			if (this.X >= this.Width)
			{
				this.X = 0;
				num = this.Y;
				this.Y = num + 1;
			}
			if (this.Y >= this.Height)
			{
				this.Y = 0;
			}
		}

		// Token: 0x0600D464 RID: 54372 RVA: 0x004EDE80 File Offset: 0x004EC080
		public void Write(string Tile, string RenderString, string ColorString, string TileColor, char DetailColor, bool SuppressImposters = true, bool HFlip = false, bool VFlip = false)
		{
			if (this._X < 0 || this._X >= this.Width || this._Y < 0 || this._Y >= this.Height)
			{
				return;
			}
			ConsoleChar consoleChar = this[this._X, this._Y];
			string text = ColorString;
			consoleChar.HFlip = HFlip;
			consoleChar.VFlip = VFlip;
			if (!string.IsNullOrEmpty(Tile) && Options.UseTiles)
			{
				consoleChar.Tile = Tile;
				if (SuppressImposters)
				{
					ScreenBuffer.ImposterSuppression[this._X, this._Y] = true;
				}
				Color color;
				if (DetailColor == '\0')
				{
					consoleChar.Detail = The.Color.DarkBlack;
				}
				else if (ColorUtility.ColorMap.TryGetValue(DetailColor, out color))
				{
					consoleChar.Detail = color;
				}
				text = TileColor ?? text;
			}
			else if (!string.IsNullOrEmpty(RenderString))
			{
				consoleChar.Char = RenderString[0];
			}
			if (!string.IsNullOrEmpty(text))
			{
				int num = text.LastIndexOf('&');
				Color color2;
				if (num != -1 && ColorUtility.ColorMap.TryGetValue(text[num + 1], out color2))
				{
					consoleChar.Foreground = color2;
				}
				else
				{
					consoleChar.Foreground = The.Color.DarkBlack;
				}
				num = text.LastIndexOf('^');
				if (num != -1 && ColorUtility.ColorMap.TryGetValue(text[num + 1], out color2))
				{
					consoleChar.Background = color2;
				}
				else
				{
					consoleChar.Background = The.Color.DarkBlack;
				}
			}
			int num2 = this.X;
			this.X = num2 + 1;
			if (this.X >= this.Width)
			{
				this.X = 0;
				num2 = this.Y;
				this.Y = num2 + 1;
			}
			if (this.Y >= this.Height)
			{
				this.Y = 0;
			}
		}

		// Token: 0x0600D465 RID: 54373 RVA: 0x004EE030 File Offset: 0x004EC230
		public void Write(string Tile, string RenderString, string ColorString, string TileColor, string DetailColor, bool SuppressImposters = true, bool HFlip = false, bool VFlip = false)
		{
			this.Write(Tile, RenderString, ColorString, TileColor, string.IsNullOrEmpty(DetailColor) ? DetailColor[0] : '\0', SuppressImposters, HFlip, VFlip);
		}

		// Token: 0x0600D466 RID: 54374 RVA: 0x004EE064 File Offset: 0x004EC264
		public void Write(IRenderable r)
		{
			if (r == null)
			{
				this.Write("{{M|?}}", true, false, false, null, -1);
				return;
			}
			this.Write(r.getTile(), r.getRenderString(), r.getColorString(), r.getTileColor(), r.getDetailColor(), true, r.getHFlip(), r.getVFlip());
		}

		// Token: 0x0600D467 RID: 54375 RVA: 0x004EE0B8 File Offset: 0x004EC2B8
		public void Write(IRenderable r, string Tile = null, string RenderString = null, string ColorString = null, string TileColor = null, char? DetailColor = null, bool SuppressImposters = true, bool HFlip = false, bool VFlip = false)
		{
			if (r == null)
			{
				this.Write("{{M|?}}", true, false, false, null, -1);
				return;
			}
			this.Write(Tile ?? r.getTile(), RenderString ?? r.getRenderString(), ColorString ?? r.getColorString(), TileColor ?? r.getTileColor(), DetailColor ?? r.getDetailColor(), SuppressImposters, HFlip, VFlip);
		}

		// Token: 0x0600D468 RID: 54376 RVA: 0x004EE130 File Offset: 0x004EC330
		public static Color GetColorFromChar(char c)
		{
			Color grey = Color.grey;
			if (ColorUtility.ColorMap.TryGetValue(c, out grey))
			{
				return grey;
			}
			return Color.grey;
		}

		// Token: 0x0600D469 RID: 54377 RVA: 0x004EE15C File Offset: 0x004EC35C
		public static Color GetForeground(string s)
		{
			Color color = The.Color.Gray;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == '&')
				{
					i++;
					if (i >= s.Length)
					{
						break;
					}
					if (s[i] != '&')
					{
						try
						{
							color = ScreenBuffer.GetColorFromChar(s[i]);
						}
						catch
						{
						}
					}
				}
			}
			return color;
		}

		// Token: 0x0600D46A RID: 54378 RVA: 0x004EE1CC File Offset: 0x004EC3CC
		public static Color GetBackground(string s)
		{
			Color color = The.Color.DarkBlack;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == '^')
				{
					i++;
					if (i >= s.Length)
					{
						break;
					}
					if (s[i] != '^')
					{
						try
						{
							color = ScreenBuffer.GetColorFromChar(s[i]);
						}
						catch
						{
						}
					}
				}
			}
			return color;
		}

		// Token: 0x0600D46B RID: 54379 RVA: 0x004EE23C File Offset: 0x004EC43C
		public int WriteBlockWithNewlines(string[] strings, int MaxLines = 999, int StartLine = 0, bool drawIndicators = false)
		{
			return this.WriteBlockWithNewlines(string.Join("\n", strings), MaxLines, StartLine, drawIndicators);
		}

		// Token: 0x0600D46C RID: 54380 RVA: 0x004EE254 File Offset: 0x004EC454
		public int WriteBlockWithNewlines(string s, int MaxLines = 999, int StartLine = 0, bool drawIndicators = false)
		{
			if (string.IsNullOrEmpty(s))
			{
				return 1;
			}
			Color gray = The.Color.Gray;
			Color darkBlack = The.Color.DarkBlack;
			int x = this._X;
			int num = 1;
			int num2 = 0;
			int i = 0;
			while (i < s.Length)
			{
				if (s[i] != '&')
				{
					goto IL_01C8;
				}
				i++;
				if (i >= s.Length)
				{
					break;
				}
				if (s[i] != '&')
				{
					try
					{
						ColorUtility.ColorMap.TryGetValue(s[i], out gray);
						goto IL_04C1;
					}
					catch
					{
						goto IL_04C1;
					}
					goto IL_01C8;
				}
				if (this._X >= 0 && this._Y >= 0 && this._X < this.Width && this._X >= 0 && this._Y < this.Height && this._Y >= 0 && num2 >= StartLine)
				{
					this[this._X, this._Y].Char = '&';
					this[this._X, this._Y].Foreground = gray;
					this[this._X, this._Y].Background = darkBlack;
					this[this._X, this._Y].HFlip = false;
					this[this._X, this._Y].VFlip = false;
				}
				if (this.X == this.Width - 1)
				{
					return num;
				}
				int num3 = this.X;
				this.X = num3 + 1;
				if (this.X >= this.Width)
				{
					this.X = 0;
					if (num2 >= StartLine)
					{
						num3 = this.Y;
						this.Y = num3 + 1;
						break;
					}
					break;
				}
				else if (this.Y >= this.Height)
				{
					this.Y = 0;
					break;
				}
				IL_04C1:
				i++;
				continue;
				IL_01C8:
				if (s[i] == '^')
				{
					i++;
					if (i >= s.Length)
					{
						break;
					}
					if (s[i] == '^')
					{
						if (this._X >= 0 && this._Y >= 0 && this._X < this.Width && this._X >= 0 && this._Y < this.Height && this._Y >= 0 && num2 >= StartLine)
						{
							this[this._X, this._Y].Char = '^';
							this[this._X, this._Y].Foreground = gray;
							this[this._X, this._Y].Background = darkBlack;
							this[this._X, this._Y].HFlip = false;
							this[this._X, this._Y].VFlip = false;
						}
						if (this.X == this.Width - 1)
						{
							return num;
						}
						num3 = this.X;
						this.X = num3 + 1;
						if (this.X >= this.Width)
						{
							this.X = 0;
							if (num2 >= StartLine)
							{
								num3 = this.Y;
								this.Y = num3 + 1;
								break;
							}
							break;
						}
						else
						{
							if (this.Y >= this.Height)
							{
								this.Y = 0;
								break;
							}
							goto IL_04C1;
						}
					}
					else
					{
						try
						{
							ColorUtility.ColorMap.TryGetValue(s[i], out darkBlack);
							goto IL_04C1;
						}
						catch
						{
							goto IL_04C1;
						}
					}
				}
				if (s[i] == '\n')
				{
					if (num2 >= StartLine)
					{
						num3 = this.Y;
						this.Y = num3 + 1;
					}
					this.X = x;
					if (num2 >= StartLine)
					{
						num++;
					}
					num2++;
					if (num >= MaxLines)
					{
						return num;
					}
					goto IL_04C1;
				}
				else
				{
					if (this._X < this.Width && this._X >= 0 && this._Y < this.Height && this._Y >= 0 && num2 >= StartLine)
					{
						this[this._X, this._Y].Char = s[i];
						this[this._X, this._Y].Foreground = gray;
						this[this._X, this._Y].Background = darkBlack;
						this[this._X, this._Y].HFlip = false;
						this[this._X, this._Y].VFlip = false;
					}
					if (this.X == this.Width - 1)
					{
						return num;
					}
					num3 = this.X;
					this.X = num3 + 1;
					if (this.X >= this.Width)
					{
						this.X = 0;
						if (num2 >= StartLine)
						{
							num3 = this.Y;
							this.Y = num3 + 1;
						}
					}
					if (this.Y >= this.Height)
					{
						this.Y = 0;
						goto IL_04C1;
					}
					goto IL_04C1;
				}
			}
			return num;
		}

		// Token: 0x0600D46D RID: 54381 RVA: 0x004EE754 File Offset: 0x004EC954
		public ScreenBuffer WriteAt(int x, int y, string s, bool processMarkup = true)
		{
			int x2 = this.X;
			int y2 = this.Y;
			this.Goto(x, y);
			this.Write(s, processMarkup, false, false, null, -1);
			this.Goto(x2, y2);
			return this;
		}

		// Token: 0x0600D46E RID: 54382 RVA: 0x004EE790 File Offset: 0x004EC990
		public ScreenBuffer WriteAt(Cell C, string s, bool processMarkup = true)
		{
			int x = this.X;
			int y = this.Y;
			if (C != null)
			{
				this.Goto(C);
				this.Write(s, processMarkup, false, false, null, -1);
			}
			this.Goto(x, y);
			return this;
		}

		// Token: 0x0600D46F RID: 54383 RVA: 0x004EE7D0 File Offset: 0x004EC9D0
		public ScreenBuffer WriteAt(global::XRL.World.GameObject obj, string s, bool processMarkup = true)
		{
			int x = this.X;
			int y = this.Y;
			if (obj != null)
			{
				this.WriteAt(obj.CurrentCell, s, processMarkup);
			}
			this.Goto(x, y);
			return this;
		}

		// Token: 0x0600D470 RID: 54384 RVA: 0x004EE808 File Offset: 0x004ECA08
		public ScreenBuffer Write(string s, bool processMarkup = true, bool HFlip = false, bool VFlip = false, List<string> imposters = null, int maxCharsWritten = -1)
		{
			if (string.IsNullOrEmpty(s))
			{
				return this;
			}
			if (processMarkup)
			{
				s = Markup.Transform(s, false);
			}
			Color color = The.Color.DefaultForeground;
			Color color2 = The.Color.DefaultBackground;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == '&')
				{
					i++;
					if (i >= s.Length)
					{
						break;
					}
					Color color3;
					if (s[i] == '&')
					{
						if (this._X >= 0 && this._Y >= 0 && this._X < this.Width && this._X >= 0 && this._Y < this.Height && this._Y >= 0)
						{
							ConsoleChar consoleChar = this[this._X, this._Y];
							consoleChar.Char = '&';
							consoleChar.Foreground = color;
							consoleChar.Background = color2;
							consoleChar.HFlip = HFlip;
							consoleChar.VFlip = VFlip;
						}
						if (this.X == this.Width - 1)
						{
							return this;
						}
						if (--maxCharsWritten == 0)
						{
							return this;
						}
						int num = this.X;
						this.X = num + 1;
						if (this.X >= this.Width)
						{
							this.X = 0;
							num = this.Y;
							this.Y = num + 1;
							break;
						}
						if (this.Y >= this.Height)
						{
							this.Y = 0;
							break;
						}
					}
					else if (ColorUtility.ColorMap.TryGetValue(s[i], out color3))
					{
						color = color3;
					}
				}
				else if (s[i] == '^')
				{
					i++;
					if (i >= s.Length)
					{
						break;
					}
					Color color4;
					if (s[i] == '^')
					{
						if (this._X >= 0 && this._Y >= 0 && this._X < this.Width && this._X >= 0 && this._Y < this.Height && this._Y >= 0)
						{
							ConsoleChar consoleChar2 = this[this._X, this._Y];
							consoleChar2.Char = '^';
							consoleChar2.Foreground = color;
							consoleChar2.Background = color2;
							consoleChar2.HFlip = HFlip;
							consoleChar2.VFlip = VFlip;
						}
						if (this.X == this.Width - 1)
						{
							return this;
						}
						if (--maxCharsWritten == 0)
						{
							return this;
						}
						int num = this.X;
						this.X = num + 1;
						if (this.X >= this.Width)
						{
							this.X = 0;
							num = this.Y;
							this.Y = num + 1;
							break;
						}
						if (this.Y >= this.Height)
						{
							this.Y = 0;
							break;
						}
					}
					else if (ColorUtility.ColorMap.TryGetValue(s[i], out color4))
					{
						color2 = color4;
					}
				}
				else if (s[i] == '\n')
				{
					int num = this.Y;
					this.Y = num + 1;
					this.X = 0;
				}
				else
				{
					if (this._X < this.Width && this._X >= 0 && this._Y < this.Height && this._Y >= 0)
					{
						ConsoleChar consoleChar3 = this[this._X, this._Y];
						consoleChar3.Char = s[i];
						consoleChar3.Foreground = color;
						consoleChar3.Background = color2;
						consoleChar3.HFlip = HFlip;
						consoleChar3.VFlip = VFlip;
					}
					if (this.X == this.Width - 1)
					{
						return this;
					}
					if (--maxCharsWritten == 0)
					{
						return this;
					}
					int num = this.X;
					this.X = num + 1;
					if (this.X >= this.Width)
					{
						this.X = 0;
						num = this.Y;
						this.Y = num + 1;
					}
					if (this.Y >= this.Height)
					{
						this.Y = 0;
					}
				}
			}
			return this;
		}

		// Token: 0x0600D471 RID: 54385 RVA: 0x004EEBC4 File Offset: 0x004ECDC4
		public ScreenBuffer Write(StringBuilder s, int maxCharsWritten = -1)
		{
			if (s.Length == 0)
			{
				return this;
			}
			Markup.Transform(s, false);
			Color color = The.Color.DefaultForeground;
			Color color2 = The.Color.DefaultBackground;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == '&')
				{
					i++;
					if (i >= s.Length)
					{
						break;
					}
					Color color3;
					if (s[i] == '&')
					{
						if (this._X >= 0 && this._Y >= 0 && this._X < this.Width && this._X >= 0 && this._Y < this.Height && this._Y >= 0)
						{
							ConsoleChar consoleChar = this[this._X, this._Y];
							consoleChar.Char = '&';
							consoleChar.Foreground = color;
							consoleChar.Background = color2;
							consoleChar.HFlip = false;
							consoleChar.VFlip = false;
						}
						if (--maxCharsWritten == 0)
						{
							return this;
						}
						if (this.X == this.Width - 1)
						{
							return this;
						}
						int num = this.X;
						this.X = num + 1;
						if (this.X >= this.Width)
						{
							this.X = 0;
							num = this.Y;
							this.Y = num + 1;
							break;
						}
						if (this.Y >= this.Height)
						{
							this.Y = 0;
							break;
						}
					}
					else if (ColorUtility.ColorMap.TryGetValue(s[i], out color3))
					{
						color = color3;
					}
				}
				else if (s[i] == '^')
				{
					i++;
					if (i >= s.Length)
					{
						break;
					}
					Color color4;
					if (s[i] == '^')
					{
						if (this._X >= 0 && this._Y >= 0 && this._X < this.Width && this._X >= 0 && this._Y < this.Height && this._Y >= 0)
						{
							ConsoleChar consoleChar2 = this[this._X, this._Y];
							consoleChar2.Char = '^';
							consoleChar2.Foreground = color;
							consoleChar2.Background = color2;
							consoleChar2.HFlip = false;
							consoleChar2.VFlip = false;
						}
						if (--maxCharsWritten == 0)
						{
							return this;
						}
						if (this.X == this.Width - 1)
						{
							return this;
						}
						int num = this.X;
						this.X = num + 1;
						if (this.X >= this.Width)
						{
							this.X = 0;
							num = this.Y;
							this.Y = num + 1;
							break;
						}
						if (this.Y >= this.Height)
						{
							this.Y = 0;
							break;
						}
					}
					else if (ColorUtility.ColorMap.TryGetValue(s[i], out color4))
					{
						color2 = color4;
					}
				}
				else if (s[i] == '\n')
				{
					int num = this.Y;
					this.Y = num + 1;
					this.X = 0;
				}
				else
				{
					if (this._X < this.Width && this._X >= 0 && this._Y < this.Height && this._Y >= 0)
					{
						ConsoleChar consoleChar3 = this[this._X, this._Y];
						consoleChar3.Char = s[i];
						consoleChar3.Foreground = color;
						consoleChar3.Background = color2;
						consoleChar3.HFlip = false;
						consoleChar3.VFlip = false;
					}
					if (--maxCharsWritten == 0)
					{
						return this;
					}
					if (this.X == this.Width - 1)
					{
						return this;
					}
					int num = this.X;
					this.X = num + 1;
					if (this.X >= this.Width)
					{
						this.X = 0;
						num = this.Y;
						this.Y = num + 1;
					}
					if (this.Y >= this.Height)
					{
						this.Y = 0;
					}
				}
			}
			return this;
		}

		// Token: 0x0600D472 RID: 54386 RVA: 0x004EEF70 File Offset: 0x004ED170
		public void Fill(int x1, int y1, int x2, int y2, ushort Char, ushort Color, bool HFlip = false, bool VFlip = false)
		{
			if (x1 < 0)
			{
				x1 = 0;
			}
			if (y1 < 0)
			{
				y1 = 0;
			}
			if (x2 >= this.Width)
			{
				x2 = this.Width - 1;
			}
			if (y2 >= this.Height)
			{
				y2 = this.Height - 1;
			}
			if (y2 < 0)
			{
				return;
			}
			if (x2 < 0)
			{
				return;
			}
			if (x1 >= this.Width)
			{
				return;
			}
			if (y1 >= this.Height)
			{
				return;
			}
			for (int i = x1; i <= x2; i++)
			{
				for (int j = y1; j <= y2; j++)
				{
					if (i >= 0 && j >= 0 && i < 80 && j < 25)
					{
						this.Buffer[i, j].Char = (char)Char;
						this.Buffer[i, j].SetColorsFromOldCharCode(Color);
						this.Buffer[i, j].HFlip = HFlip;
						this.Buffer[i, j].VFlip = VFlip;
					}
				}
			}
		}

		// Token: 0x0600D473 RID: 54387 RVA: 0x004EF04C File Offset: 0x004ED24C
		public void BeveledBox(int x1, int y1, int x2, int y2, ushort ForeColor, ushort BackColor)
		{
			for (int i = x1 + 1; i < x2; i++)
			{
				this.Buffer[i, y1].Char = 'Ü';
				this.Buffer[i, y1].SetColorsFromOldCharCode(ColorUtility.MakeColor(BackColor, ForeColor));
				this.Buffer[i, y1].HFlip = false;
				this.Buffer[i, y1].VFlip = false;
				this.Buffer[i, y2].Char = 'Ü';
				this.Buffer[i, y2].SetColorsFromOldCharCode(ColorUtility.MakeColor(ForeColor, BackColor));
				this.Buffer[i, y2].HFlip = false;
				this.Buffer[i, y2].VFlip = false;
			}
			for (int j = y1 + 1; j < y2; j++)
			{
				this.Buffer[x1, j].Char = 'Ý';
				this.Buffer[x1, j].SetColorsFromOldCharCode(ColorUtility.MakeColor(ForeColor, BackColor));
				this.Buffer[x1, j].HFlip = false;
				this.Buffer[x1, j].VFlip = false;
				this.Buffer[x2, j].Char = 'Ý';
				this.Buffer[x2, j].SetColorsFromOldCharCode(ColorUtility.MakeColor(BackColor, ForeColor));
				this.Buffer[x2, j].HFlip = false;
				this.Buffer[x2, j].VFlip = false;
			}
			this.Buffer[x1, y1].Char = 'Ú';
			this.Buffer[x1, y1].SetColorsFromOldCharCode(ColorUtility.MakeColor(BackColor, ForeColor));
			this.Buffer[x1, y1].HFlip = false;
			this.Buffer[x1, y1].VFlip = false;
			this.Buffer[x1, y2].Char = 'À';
			this.Buffer[x1, y2].SetColorsFromOldCharCode(ColorUtility.MakeColor(BackColor, ForeColor));
			this.Buffer[x1, y2].HFlip = false;
			this.Buffer[x1, y2].VFlip = false;
			this.Buffer[x2, y1].Char = '¿';
			this.Buffer[x2, y1].SetColorsFromOldCharCode(ColorUtility.MakeColor(BackColor, ForeColor));
			this.Buffer[x2, y1].HFlip = false;
			this.Buffer[x2, y1].VFlip = false;
			this.Buffer[x2, y2].Char = 'Ù';
			this.Buffer[x2, y2].SetColorsFromOldCharCode(ColorUtility.MakeColor(BackColor, ForeColor));
			this.Buffer[x2, y2].HFlip = false;
			this.Buffer[x2, y2].VFlip = false;
		}

		// Token: 0x0600D474 RID: 54388 RVA: 0x004EF350 File Offset: 0x004ED550
		public void ThickSingleBox(int x1, int y1, int x2, int y2, ushort Color)
		{
			if (x1 < 0)
			{
				x1 = 0;
			}
			if (y1 < 0)
			{
				y1 = 0;
			}
			if (x2 >= this.Width)
			{
				x2 = this.Width - 1;
			}
			if (y2 >= this.Height)
			{
				y2 = this.Height - 1;
			}
			if (y2 < 0)
			{
				return;
			}
			if (x2 < 0)
			{
				return;
			}
			if (x1 >= this.Width)
			{
				return;
			}
			if (y1 >= this.Height)
			{
				return;
			}
			for (int i = x1 + 1; i < x2; i++)
			{
				this.Buffer[i, y1].Char = 'ß';
				this.Buffer[i, y1].SetColorsFromOldCharCode(Color);
				this.Buffer[i, y1].HFlip = false;
				this.Buffer[i, y1].VFlip = false;
				this.Buffer[i, y2].Char = 'Ü';
				this.Buffer[i, y2].SetColorsFromOldCharCode(Color);
				this.Buffer[i, y2].HFlip = false;
				this.Buffer[i, y2].VFlip = false;
			}
			for (int j = y1 + 1; j < y2; j++)
			{
				this.Buffer[x1, j].Char = 'Ý';
				this.Buffer[x1, j].SetColorsFromOldCharCode(Color);
				this.Buffer[x1, j].HFlip = false;
				this.Buffer[x1, j].VFlip = false;
				this.Buffer[x2, j].Char = 'Þ';
				this.Buffer[x2, j].SetColorsFromOldCharCode(Color);
				this.Buffer[x2, j].HFlip = false;
				this.Buffer[x2, j].VFlip = false;
			}
			this.Buffer[x1, y1].Char = 'Û';
			this.Buffer[x1, y1].SetColorsFromOldCharCode(Color);
			this.Buffer[x1, y1].HFlip = false;
			this.Buffer[x1, y1].VFlip = false;
			this.Buffer[x2, y1].Char = 'Û';
			this.Buffer[x2, y1].SetColorsFromOldCharCode(Color);
			this.Buffer[x2, y1].HFlip = false;
			this.Buffer[x2, y1].VFlip = false;
			this.Buffer[x1, y2].Char = 'Û';
			this.Buffer[x1, y2].SetColorsFromOldCharCode(Color);
			this.Buffer[x1, y2].HFlip = false;
			this.Buffer[x1, y2].VFlip = false;
			this.Buffer[x2, y2].Char = 'Û';
			this.Buffer[x2, y2].SetColorsFromOldCharCode(Color);
			this.Buffer[x2, y2].HFlip = false;
			this.Buffer[x2, y2].VFlip = false;
		}

		// Token: 0x0600D475 RID: 54389 RVA: 0x004EF670 File Offset: 0x004ED870
		public void SingleBox(int x1, int y1, int x2, int y2, ushort Color)
		{
			if (x1 < 0)
			{
				x1 = 0;
			}
			if (y1 < 0)
			{
				y1 = 0;
			}
			if (x2 >= this.Width)
			{
				x2 = this.Width - 1;
			}
			if (y2 >= this.Height)
			{
				y2 = this.Height - 1;
			}
			if (y2 < 0)
			{
				return;
			}
			if (x2 < 0)
			{
				return;
			}
			if (x1 >= this.Width)
			{
				return;
			}
			if (y1 >= this.Height)
			{
				return;
			}
			for (int i = x1 + 1; i < x2; i++)
			{
				if (i >= 0 && i < 80 && y1 >= 0 && y1 < 25)
				{
					this.Buffer[i, y1].Char = 'Ä';
					this.Buffer[i, y1].SetColorsFromOldCharCode(Color);
					this.Buffer[i, y1].HFlip = false;
					this.Buffer[i, y1].VFlip = false;
				}
				if (i >= 0 && i < 80 && y2 >= 0 && y2 < 25)
				{
					this.Buffer[i, y2].Char = 'Ä';
					this.Buffer[i, y2].SetColorsFromOldCharCode(Color);
					this.Buffer[i, y2].HFlip = false;
					this.Buffer[i, y2].VFlip = false;
				}
			}
			for (int j = y1 + 1; j < y2; j++)
			{
				if (x1 >= 0 && x1 < 80 && j >= 0 && j < 25)
				{
					this.Buffer[x1, j].Char = '³';
					this.Buffer[x1, j].SetColorsFromOldCharCode(Color);
					this.Buffer[x1, j].HFlip = false;
					this.Buffer[x1, j].VFlip = false;
				}
				if (x2 >= 0 && x2 < 80 && j >= 0 && j < 25)
				{
					this.Buffer[x2, j].Char = '³';
					this.Buffer[x2, j].SetColorsFromOldCharCode(Color);
					this.Buffer[x2, j].HFlip = false;
					this.Buffer[x2, j].VFlip = false;
				}
			}
			if (x1 >= 0 && x1 < 80 && y1 >= 0 && y1 < 25)
			{
				this.Buffer[x1, y1].Char = 'Ú';
				this.Buffer[x1, y1].SetColorsFromOldCharCode(Color);
				this.Buffer[x1, y1].HFlip = false;
				this.Buffer[x1, y1].VFlip = false;
			}
			if (x2 >= 0 && x2 < 80 && y1 >= 0 && y1 < 25)
			{
				this.Buffer[x2, y1].Char = '¿';
				this.Buffer[x2, y1].SetColorsFromOldCharCode(Color);
				this.Buffer[x2, y1].HFlip = false;
				this.Buffer[x2, y1].VFlip = false;
			}
			if (x1 >= 0 && x1 < 80 && y2 >= 0 && y2 < 25)
			{
				this.Buffer[x1, y2].Char = 'À';
				this.Buffer[x1, y2].SetColorsFromOldCharCode(Color);
				this.Buffer[x1, y2].HFlip = false;
				this.Buffer[x1, y2].VFlip = false;
			}
			if (x2 >= 0 && x2 < 80 && y2 >= 0 && y2 < 25)
			{
				this.Buffer[x2, y2].Char = 'Ù';
				this.Buffer[x2, y2].SetColorsFromOldCharCode(Color);
				this.Buffer[x2, y2].HFlip = false;
				this.Buffer[x2, y2].VFlip = false;
			}
		}

		// Token: 0x0600D476 RID: 54390 RVA: 0x004EFA24 File Offset: 0x004EDC24
		public void DoubleBox(int x1, int y1, int x2, int y2, ushort Color)
		{
			if (x1 < 0)
			{
				x1 = 0;
			}
			if (y1 < 0)
			{
				y1 = 0;
			}
			if (x2 >= this.Width)
			{
				x2 = this.Width - 1;
			}
			if (y2 >= this.Height)
			{
				y2 = this.Height - 1;
			}
			if (y2 < 0)
			{
				return;
			}
			if (x2 < 0)
			{
				return;
			}
			if (x1 >= this.Width)
			{
				return;
			}
			if (y1 >= this.Height)
			{
				return;
			}
			for (int i = x1 + 1; i < x2; i++)
			{
				this.Buffer[i, y1].Char = 'Í';
				this.Buffer[i, y1].SetColorsFromOldCharCode(Color);
				this.Buffer[i, y1].HFlip = false;
				this.Buffer[i, y1].VFlip = false;
				this.Buffer[i, y2].Char = 'Í';
				this.Buffer[i, y2].SetColorsFromOldCharCode(Color);
				this.Buffer[i, y2].HFlip = false;
				this.Buffer[i, y2].VFlip = false;
			}
			for (int j = y1 + 1; j < y2; j++)
			{
				this.Buffer[x1, j].Char = 'º';
				this.Buffer[x1, j].SetColorsFromOldCharCode(Color);
				this.Buffer[x1, y1].HFlip = false;
				this.Buffer[x1, y1].VFlip = false;
				this.Buffer[x2, j].Char = 'º';
				this.Buffer[x2, j].SetColorsFromOldCharCode(Color);
				this.Buffer[x2, y1].HFlip = false;
				this.Buffer[x2, y1].VFlip = false;
			}
			this.Buffer[x1, y1].Char = 'É';
			this.Buffer[x1, y1].SetColorsFromOldCharCode(Color);
			this.Buffer[x1, y1].HFlip = false;
			this.Buffer[x1, y1].VFlip = false;
			this.Buffer[x2, y1].Char = '»';
			this.Buffer[x2, y1].SetColorsFromOldCharCode(Color);
			this.Buffer[x2, y1].HFlip = false;
			this.Buffer[x2, y1].VFlip = false;
			this.Buffer[x1, y2].Char = 'È';
			this.Buffer[x1, y2].SetColorsFromOldCharCode(Color);
			this.Buffer[x1, y2].HFlip = false;
			this.Buffer[x1, y2].VFlip = false;
			this.Buffer[x2, y2].Char = '¼';
			this.Buffer[x2, y2].SetColorsFromOldCharCode(Color);
			this.Buffer[x2, y2].HFlip = false;
			this.Buffer[x2, y2].VFlip = false;
		}

		// Token: 0x0600D477 RID: 54391 RVA: 0x004EFD42 File Offset: 0x004EDF42
		public void Constrain(ref int X, ref int Y)
		{
			if (X < 0)
			{
				X = 0;
			}
			else if (X >= this.Width)
			{
				X = this.Width - 1;
			}
			if (Y < 0)
			{
				Y = 0;
				return;
			}
			if (Y >= this.Height)
			{
				Y = this.Height - 1;
			}
		}

		// Token: 0x0600D478 RID: 54392 RVA: 0x004EFD7F File Offset: 0x004EDF7F
		public void Constrain(ref int Left, ref int Right, ref int Top, ref int Bottom)
		{
			if (Left < 0)
			{
				Left = 0;
			}
			if (Right >= this.Width)
			{
				Right = this.Width - 1;
			}
			if (Top < 0)
			{
				Top = 0;
			}
			if (Bottom >= this.Height)
			{
				Bottom = this.Height - 1;
			}
		}

		// Token: 0x0600D479 RID: 54393 RVA: 0x004EFDBC File Offset: 0x004EDFBC
		public static void ClearImposterSuppression()
		{
			for (int i = 0; i < 25; i++)
			{
				for (int j = 0; j < 80; j++)
				{
					ScreenBuffer.ImposterSuppression[j, i] = false;
				}
			}
		}

		// Token: 0x0400551C RID: 21788
		public string ViewTag;

		// Token: 0x0400551D RID: 21789
		public Point2D focusPosition = Point2D.invalid;

		// Token: 0x0400551E RID: 21790
		public ConsoleChar[,] Buffer;

		// Token: 0x0400551F RID: 21791
		public int _Width;

		// Token: 0x04005520 RID: 21792
		public int _Height;

		// Token: 0x04005521 RID: 21793
		public int _X;

		// Token: 0x04005522 RID: 21794
		public int _Y;

		// Token: 0x04005523 RID: 21795
		public StringBuilder TSB = new StringBuilder(2000);

		// Token: 0x04005524 RID: 21796
		public static bool bLowContrast = false;

		// Token: 0x04005525 RID: 21797
		public static bool[,] ImposterSuppression = new bool[80, 25];
	}
}
