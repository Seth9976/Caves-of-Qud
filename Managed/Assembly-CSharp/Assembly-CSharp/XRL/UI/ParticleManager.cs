using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ConsoleLib.Console;
using UnityEngine;
using XRL.Messages;
using XRL.Rules;

namespace XRL.UI
{
	// Token: 0x020014C8 RID: 5320
	public class ParticleManager
	{
		// Token: 0x0600D075 RID: 53365 RVA: 0x004CFBDF File Offset: 0x004CDDDF
		public void reset()
		{
			this.Banners = new CleanQueue<BannerText>();
			this.Particles = new List<TextParticle>();
			this.TileParticles = new List<TileParticle>();
			this.RadialParticles = new List<RadialTextParticle>();
			this.SinusoidalParticles = new List<SinusoidalTextParticle>();
		}

		// Token: 0x0600D076 RID: 53366 RVA: 0x004CFC18 File Offset: 0x004CDE18
		public void Init(TextConsole TextConsole_, ScreenBuffer ScreenBuffer_)
		{
			this._TextConsole = TextConsole_;
			this._ScreenBuffer = ScreenBuffer_;
		}

		// Token: 0x0600D077 RID: 53367 RVA: 0x004CFC28 File Offset: 0x004CDE28
		public void AddBanner(string Text, BannerTextType Type)
		{
			BannerText bannerText = new BannerText();
			bannerText.Block = new TextBlock(Text, 40, 25, false, 0);
			bannerText.Type = Type;
			bannerText.x = 40 - bannerText.Block.Width / 2;
			bannerText.y = 23 - bannerText.Block.Height;
			this.Banners.Enqueue(bannerText);
			MessageQueue.AddPlayerMessage(Text, null, true);
		}

		// Token: 0x0600D078 RID: 53368 RVA: 0x004CFC94 File Offset: 0x004CDE94
		public void AddRadial(string Text, float x, float y, float r, float d, float rdel, float ddel)
		{
			this.AddRadial(Text, x, y, r, d, rdel, ddel, 999, 0L);
		}

		// Token: 0x0600D079 RID: 53369 RVA: 0x004CFCBC File Offset: 0x004CDEBC
		public void Add(string Text, float x, float y, float xDel, float yDel)
		{
			this.Add(Text, x, y, xDel, yDel, 999, 0f, 0f, 0L);
		}

		// Token: 0x0600D07A RID: 53370 RVA: 0x004CFCE8 File Offset: 0x004CDEE8
		public void Add(string Text, float x, float y, float xDel, float yDel, int Life)
		{
			this.Add(Text, x, y, xDel, yDel, Life, 0f, 0f, 0L);
		}

		// Token: 0x0600D07B RID: 53371 RVA: 0x004CFD10 File Offset: 0x004CDF10
		public void Add(string Text, float x, float y, float xDel, float yDel, int Life, float xDelDel, float yDelDel, long delayMS = 0L)
		{
			if (this.Particles.Count > 1000)
			{
				return;
			}
			if (Text == "@" && Options.GetOption("OptionDisableTextWarpEffects", "") == "Yes")
			{
				Text = "*";
			}
			TextParticle textParticle;
			if (ParticleManager.textParticlesQueue.Count > 0)
			{
				textParticle = ParticleManager.textParticlesQueue.Dequeue();
			}
			else
			{
				textParticle = new TextParticle();
			}
			textParticle.x = x;
			textParticle.y = y;
			textParticle.xDel = xDel;
			textParticle.yDel = yDel;
			textParticle.xDelDel = xDelDel;
			textParticle.yDelDel = yDelDel;
			textParticle.Text = Text;
			textParticle.TextBlock = new TextBlock(Text, 12, 8, false, 0);
			textParticle.Life = Life;
			textParticle.DelayMS = delayMS;
			this.Particles.Add(textParticle);
		}

		// Token: 0x0600D07C RID: 53372 RVA: 0x004CFDE4 File Offset: 0x004CDFE4
		public void AddTile(string Tile, string ColorString, string DetailColor, float x, float y, float xDel, float yDel, int Life, float xDelDel, float yDelDel, bool HFlip = false, bool VFlip = false, long delayMS = 0L)
		{
			if (this.Particles.Count > 1000)
			{
				return;
			}
			TileParticle tileParticle;
			if (ParticleManager.tileParticlesQueue.Count > 0)
			{
				tileParticle = ParticleManager.tileParticlesQueue.Dequeue();
			}
			else
			{
				tileParticle = new TileParticle();
			}
			tileParticle.x = x;
			tileParticle.y = y;
			tileParticle.xDel = xDel;
			tileParticle.yDel = yDel;
			tileParticle.xDelDel = xDelDel;
			tileParticle.yDelDel = yDelDel;
			tileParticle.Tile = Tile;
			tileParticle.ColorString = ColorString;
			tileParticle.DetailColor = DetailColor;
			tileParticle.Life = Life;
			tileParticle.bHFlipped = HFlip;
			tileParticle.bVFlipped = VFlip;
			tileParticle.DelayMS = delayMS;
			this.TileParticles.Add(tileParticle);
		}

		// Token: 0x0600D07D RID: 53373 RVA: 0x004CFE98 File Offset: 0x004CE098
		public void AddSinusoidal(string Text, float x, float y, float amp, float sinstart, float sinincrement, float xamp, float yamp, float xdel, float ydel, int Life, long delayMS = 0L)
		{
			if (this.SinusoidalParticles.Count > 100)
			{
				return;
			}
			if (Text == "@" && Options.GetOption("OptionDisableTextWarpEffects", "") == "Yes")
			{
				Text = "*";
			}
			SinusoidalTextParticle sinusoidalTextParticle;
			if (ParticleManager.sinParticlesQueue.Count > 0)
			{
				sinusoidalTextParticle = ParticleManager.sinParticlesQueue.Dequeue();
			}
			else
			{
				sinusoidalTextParticle = new SinusoidalTextParticle();
			}
			sinusoidalTextParticle.x = x;
			sinusoidalTextParticle.y = y;
			sinusoidalTextParticle.amp = amp;
			sinusoidalTextParticle.xamp = xamp;
			sinusoidalTextParticle.yamp = yamp;
			sinusoidalTextParticle.sinstart = sinstart;
			sinusoidalTextParticle.sinincrement = sinincrement;
			sinusoidalTextParticle.ydel = ydel;
			sinusoidalTextParticle.xdel = xdel;
			sinusoidalTextParticle.Text = Text;
			sinusoidalTextParticle.TextBlock = new TextBlock(Text, 12, 8, false, 0);
			sinusoidalTextParticle.Life = Life;
			sinusoidalTextParticle.DelayMS = delayMS;
			this.SinusoidalParticles.Add(sinusoidalTextParticle);
		}

		// Token: 0x0600D07E RID: 53374 RVA: 0x004CFF80 File Offset: 0x004CE180
		public void AddRadial(string Text, float x, float y, float r, float d, float rdel, float ddel, int Life, long delayMS = 0L)
		{
			if (this.RadialParticles.Count > 100)
			{
				return;
			}
			if (Text == "@" && Options.GetOption("OptionDisableTextWarpEffects", "") == "Yes")
			{
				Text = "*";
			}
			RadialTextParticle radialTextParticle;
			if (ParticleManager.radParticlesQueue.Count > 0)
			{
				radialTextParticle = ParticleManager.radParticlesQueue.Dequeue();
			}
			else
			{
				radialTextParticle = new RadialTextParticle();
			}
			radialTextParticle.x = x;
			radialTextParticle.y = y;
			radialTextParticle.r = r;
			radialTextParticle.d = d;
			radialTextParticle.rdel = rdel;
			radialTextParticle.ddel = ddel;
			radialTextParticle.Text = Text;
			radialTextParticle.TextBlock = new TextBlock(Text, 12, 8, false, 0);
			radialTextParticle.Life = Life;
			radialTextParticle.DelayMS = delayMS;
			this.RadialParticles.Add(radialTextParticle);
		}

		// Token: 0x0600D07F RID: 53375 RVA: 0x004D0050 File Offset: 0x004CE250
		public void Render(ScreenBuffer Buffer)
		{
			if (!Options.UseTextParticleVFX)
			{
				return;
			}
			if (this.Removed.Count > 0)
			{
				this.Removed.Clear();
			}
			if (this.TileRemoved.Count > 0)
			{
				this.TileRemoved.Clear();
			}
			if (this.RadialRemoved.Count > 0)
			{
				this.RadialRemoved.Clear();
			}
			if (this.SinRemoved.Count > 0)
			{
				this.SinRemoved.Clear();
			}
			Math.Max(1, Options.ThrottleAnimationRate / 20);
			int num = 0;
			while (num < this.Particles.Count && this.Particles[num].DelayMS <= 0L)
			{
				TextParticle textParticle = this.Particles[num];
				int num2 = Convert.ToInt32(textParticle.x);
				int num3 = Convert.ToInt32(textParticle.y);
				if (num2 < 0 || num2 > 79 || num3 < 0 || num3 > 24)
				{
					this.Removed.Add(textParticle);
				}
				if (num2 < 0)
				{
					num2 = 0;
				}
				if (num2 > 79)
				{
					num2 = 79;
				}
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num3 > 24)
				{
					num3 = 24;
				}
				if (textParticle.Life <= 0)
				{
					this.Removed.Add(textParticle);
				}
				Buffer.Goto(num2, num3);
				if (textParticle.Text == "firefly")
				{
					Buffer.Buffer[num2, num3].Char = 'ú';
					if (textParticle.Life < 10)
					{
						Buffer.Buffer[num2, num3].SetColorsFromOldCharCode(ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Blue, TextColor.Black));
					}
					else if (textParticle.Life < 20)
					{
						Buffer.Buffer[num2, num3].SetColorsFromOldCharCode(ConsoleLib.Console.ColorUtility.MakeColor(ConsoleLib.Console.ColorUtility.Bright(TextColor.Blue), TextColor.Black));
					}
					else if (textParticle.Life < 30)
					{
						Buffer.Buffer[num2, num3].SetColorsFromOldCharCode(ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Cyan, TextColor.Black));
					}
					else if (textParticle.Life < 40)
					{
						Buffer.Buffer[num2, num3].SetColorsFromOldCharCode(ConsoleLib.Console.ColorUtility.MakeColor(ConsoleLib.Console.ColorUtility.Bright(TextColor.Cyan), TextColor.Black));
					}
					else if (textParticle.Life < 50)
					{
						Buffer.Buffer[num2, num3].SetColorsFromOldCharCode(ConsoleLib.Console.ColorUtility.MakeColor(ConsoleLib.Console.ColorUtility.Bright(TextColor.Blue), TextColor.Black));
					}
					else if (textParticle.Life < 60)
					{
						Buffer.Buffer[num2, num3].SetColorsFromOldCharCode(ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Blue, TextColor.Black));
					}
				}
				else if (textParticle.Text[0] == '@')
				{
					int num4 = num2;
					int num5 = num3;
					if ((float)num2 < textParticle.x)
					{
						num4--;
					}
					if ((float)num2 > textParticle.x)
					{
						num4++;
					}
					if ((float)num3 < textParticle.y)
					{
						num5--;
					}
					if ((float)num3 > textParticle.y)
					{
						num5++;
					}
					if (num4 < 0)
					{
						num4 = 0;
					}
					if (num4 > 79)
					{
						num4 = 79;
					}
					if (num5 < 0)
					{
						num5 = 0;
					}
					if (num5 > 24)
					{
						num5 = 24;
					}
					if ((float)num2 != textParticle.x || (float)num3 != textParticle.y)
					{
						Buffer.Buffer[num2, num3].Copy(Buffer.Buffer[num4, num5]);
					}
				}
				else
				{
					Buffer.Write(textParticle.Text, true, false, false, null, -1);
				}
				num++;
			}
			int num6 = 0;
			while (num6 < this.TileParticles.Count && this.TileParticles[num6].DelayMS <= 0L)
			{
				TileParticle tileParticle = this.TileParticles[num6];
				int num7 = Convert.ToInt32(tileParticle.x);
				int num8 = Convert.ToInt32(tileParticle.y);
				if (num7 < 0 || num7 > 79 || num8 < 0 || num8 > 24)
				{
					this.TileRemoved.Add(tileParticle);
				}
				else if (tileParticle.Life <= 0)
				{
					this.TileRemoved.Add(tileParticle);
				}
				else
				{
					Buffer.Goto(num7, num8);
					if (!string.IsNullOrEmpty(tileParticle.Tile))
					{
						ConsoleChar consoleChar = Buffer.Buffer[num7, num8];
						consoleChar.Tile = tileParticle.Tile;
						if (!string.IsNullOrEmpty(tileParticle.ColorString))
						{
							int num9 = tileParticle.ColorString.LastIndexOf('&');
							Color color;
							if (num9 != -1 && ConsoleLib.Console.ColorUtility.ColorMap.TryGetValue(tileParticle.ColorString[num9 + 1], out color))
							{
								consoleChar.Foreground = color;
							}
							else
							{
								consoleChar.Foreground = The.Color.DarkBlack;
							}
							num9 = tileParticle.ColorString.LastIndexOf('^');
							if (num9 != -1 && ConsoleLib.Console.ColorUtility.ColorMap.TryGetValue(tileParticle.ColorString[num9 + 1], out color))
							{
								consoleChar.Background = color;
							}
							else
							{
								consoleChar.Background = The.Color.DarkBlack;
							}
						}
						if (!string.IsNullOrEmpty(tileParticle.DetailColor))
						{
							consoleChar.Detail = ConsoleLib.Console.ColorUtility.ColorMap[tileParticle.DetailColor[0]];
						}
						consoleChar.HFlip = tileParticle.bHFlipped;
						consoleChar.VFlip = tileParticle.bVFlipped;
					}
				}
				num6++;
			}
			int num10 = 0;
			while (num10 < this.RadialParticles.Count && this.RadialParticles[num10].DelayMS <= 0L)
			{
				RadialTextParticle radialTextParticle = this.RadialParticles[num10];
				int num11 = Convert.ToInt32(radialTextParticle.x) + (int)((double)radialTextParticle.d * Math.Cos((double)radialTextParticle.r));
				int num12 = Convert.ToInt32(radialTextParticle.y) + (int)((double)radialTextParticle.d * Math.Sin((double)radialTextParticle.r));
				if (radialTextParticle.Life <= 0)
				{
					this.RadialRemoved.Add(radialTextParticle);
				}
				if (radialTextParticle.d < 1f)
				{
					this.RadialRemoved.Add(radialTextParticle);
				}
				if (num11 >= 0 && num11 <= 79 && num12 >= 0 && num12 <= 24)
				{
					Buffer.Goto(num11, num12);
					if (radialTextParticle.Text[0] == '@')
					{
						int num13 = num11;
						int num14 = num12;
						if ((float)num11 < radialTextParticle.x)
						{
							num13--;
						}
						if ((float)num11 > radialTextParticle.x)
						{
							num13++;
						}
						if ((float)num12 < radialTextParticle.y)
						{
							num14--;
						}
						if ((float)num12 > radialTextParticle.y)
						{
							num14++;
						}
						if (num13 < 0)
						{
							num13 = 0;
						}
						if (num13 > 79)
						{
							num13 = 79;
						}
						if (num14 < 0)
						{
							num14 = 0;
						}
						if (num14 > 24)
						{
							num14 = 24;
						}
						if ((float)num11 != radialTextParticle.x || (float)num12 != radialTextParticle.y)
						{
							Buffer.Buffer[num11, num12].Char = Buffer.Buffer[num13, num14].Char;
							Buffer.Buffer[num11, num12].Tile = Buffer.Buffer[num13, num14].Tile;
							Buffer.Buffer[num11, num12].Foreground = Buffer.Buffer[num13, num14].Foreground;
							Buffer.Buffer[num11, num12].Background = Buffer.Buffer[num13, num14].Background;
						}
					}
					else
					{
						Buffer.Write(radialTextParticle.Text, true, false, false, null, -1);
					}
				}
				num10++;
			}
			int num15 = 0;
			while (num15 < this.SinusoidalParticles.Count && this.SinusoidalParticles[num15].DelayMS <= 0L)
			{
				SinusoidalTextParticle sinusoidalTextParticle = this.SinusoidalParticles[num15];
				int num16 = Convert.ToInt32(sinusoidalTextParticle.x) + (int)(Math.Sin((double)sinusoidalTextParticle.sinstart) * (double)sinusoidalTextParticle.amp * (double)sinusoidalTextParticle.xamp);
				int num17 = Convert.ToInt32(sinusoidalTextParticle.y) + (int)(Math.Sin((double)sinusoidalTextParticle.sinstart) * (double)sinusoidalTextParticle.amp * (double)sinusoidalTextParticle.yamp);
				if (sinusoidalTextParticle.Life <= 0)
				{
					this.SinRemoved.Add(sinusoidalTextParticle);
				}
				if (num16 >= 0 && num16 <= 79 && num17 >= 0 && num17 <= 24)
				{
					Buffer.Goto(num16, num17);
					if (sinusoidalTextParticle.Text[0] == '@')
					{
						int num18 = num16;
						int num19 = num17;
						if ((float)num16 < sinusoidalTextParticle.x)
						{
							num18--;
						}
						if ((float)num16 > sinusoidalTextParticle.x)
						{
							num18++;
						}
						if ((float)num17 < sinusoidalTextParticle.y)
						{
							num19--;
						}
						if ((float)num17 > sinusoidalTextParticle.y)
						{
							num19++;
						}
						if (num18 < 0)
						{
							num18 = 0;
						}
						if (num18 > 79)
						{
							num18 = 79;
						}
						if (num19 < 0)
						{
							num19 = 0;
						}
						if (num19 > 24)
						{
							num19 = 24;
						}
						if ((float)num16 != sinusoidalTextParticle.x || (float)num17 != sinusoidalTextParticle.y)
						{
							Buffer.Buffer[num16, num17].Char = Buffer.Buffer[num18, num19].Char;
							Buffer.Buffer[num16, num17].Tile = Buffer.Buffer[num18, num19].Tile;
							Buffer.Buffer[num16, num17].Foreground = Buffer.Buffer[num18, num19].Foreground;
							Buffer.Buffer[num16, num17].Background = Buffer.Buffer[num18, num19].Background;
						}
					}
					else
					{
						Buffer.Write(sinusoidalTextParticle.Text, true, false, false, null, -1);
					}
				}
				num15++;
			}
			if (this.Banners.Count > 0)
			{
				BannerText bannerText = this.Banners.Peek();
				string text = "&w";
				string text2 = "&W";
				string text3 = "&Y";
				if (bannerText.Type == BannerTextType.GoldenText)
				{
					text = "&w";
					text2 = "&W";
					text3 = "&Y";
				}
				if (bannerText.Complete)
				{
					bannerText.Life--;
					bannerText.Complete = true;
					int num20 = 0;
					using (List<string>.Enumerator enumerator = bannerText.Block.Lines.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text4 = enumerator.Current;
							Buffer.Goto(bannerText.x, bannerText.y + num20);
							Buffer.Write(text4, true, false, false, null, -1);
							num20++;
						}
						goto IL_0C5D;
					}
				}
				bannerText.Complete = true;
				int num21 = 0;
				foreach (string text5 in bannerText.Block.Lines)
				{
					string text6 = ConsoleLib.Console.ColorUtility.StripFormatting(text5);
					Buffer.Goto(bannerText.x, bannerText.y + num21);
					for (int i = 0; i < text6.Length; i++)
					{
						int num22 = i / 4 * 3;
						int num23 = i / 4 * 3 + 1;
						int num24 = i / 4 * 3 + 2;
						if (bannerText.Time < num22)
						{
							bannerText.Complete = false;
							break;
						}
						if (bannerText.Time >= num24)
						{
							this.SB.Length = 0;
							this.SB.Append(text3);
							this.SB.Append(text6[i]);
							Buffer.Write(this.SB.ToString(), true, false, false, null, -1);
						}
						else if (bannerText.Time >= num23)
						{
							this.SB.Length = 0;
							this.SB.Append(text2);
							this.SB.Append(text6[i]);
							Buffer.Write(this.SB.ToString(), true, false, false, null, -1);
						}
						else
						{
							this.SB.Length = 0;
							this.SB.Append(text);
							this.SB.Append("*");
							Buffer.Write(this.SB.ToString(), true, false, false, null, -1);
							this.AddRadial("&Y.", (float)Buffer.X, (float)Buffer.Y, (float)Stat.RandomCosmetic(0, 5), (float)Stat.RandomCosmetic(5, 10), 0f, 0.05f * (float)Stat.RandomCosmetic(3, 7), 15, 0L);
						}
					}
					num21++;
				}
				bannerText.Time++;
				IL_0C5D:
				if (bannerText.Complete && bannerText.Life <= 0)
				{
					this.Banners.Dequeue();
				}
			}
			foreach (TextParticle textParticle2 in this.Removed)
			{
				ParticleManager.textParticlesQueue.Enqueue(textParticle2);
				this.Particles.Remove(textParticle2);
			}
			foreach (TileParticle tileParticle2 in this.TileRemoved)
			{
				ParticleManager.tileParticlesQueue.Enqueue(tileParticle2);
				this.TileParticles.Remove(tileParticle2);
			}
			foreach (RadialTextParticle radialTextParticle2 in this.RadialRemoved)
			{
				ParticleManager.radParticlesQueue.Enqueue(radialTextParticle2);
				this.RadialParticles.Remove(radialTextParticle2);
			}
			foreach (SinusoidalTextParticle sinusoidalTextParticle2 in this.SinRemoved)
			{
				ParticleManager.sinParticlesQueue.Enqueue(sinusoidalTextParticle2);
				this.SinusoidalParticles.Remove(sinusoidalTextParticle2);
			}
		}

		// Token: 0x0600D080 RID: 53376 RVA: 0x004D0E9C File Offset: 0x004CF09C
		public void Frame()
		{
			if (ParticleManager.frameTimer == null)
			{
				ParticleManager.frameTimer = new Stopwatch();
				ParticleManager.frameTimer.Reset();
				ParticleManager.frameTimer.Start();
			}
			long num = ParticleManager.frameTimer.ElapsedMilliseconds - ParticleManager.lastElapsed;
			ParticleManager.lastElapsed = ParticleManager.frameTimer.ElapsedMilliseconds;
			ParticleManager.accumulator += num;
			if (ParticleManager.accumulator > 128L)
			{
				ParticleManager.accumulator = 128L;
			}
			if (ParticleManager.accumulator >= 16L)
			{
				int num2 = (int)(ParticleManager.accumulator / 16L);
				ParticleManager.accumulator -= (long)(16 * num2);
				if (this.Removed.Count > 0)
				{
					this.Removed.Clear();
				}
				if (this.TileRemoved.Count > 0)
				{
					this.TileRemoved.Clear();
				}
				if (this.RadialRemoved.Count > 0)
				{
					this.RadialRemoved.Clear();
				}
				if (this.SinRemoved.Count > 0)
				{
					this.SinRemoved.Clear();
				}
				foreach (TextParticle textParticle in this.Particles)
				{
					if (textParticle.DelayMS > 0L)
					{
						textParticle.DelayMS -= num;
					}
					else
					{
						textParticle.Life -= num2;
						textParticle.x += textParticle.xDel * (float)num2;
						textParticle.y += textParticle.yDel * (float)num2;
						textParticle.xDel += textParticle.xDelDel * (float)num2;
						textParticle.yDel += textParticle.yDelDel * (float)num2;
						if (textParticle.x < 0f)
						{
							this.Removed.Add(textParticle);
						}
						else if (textParticle.x > 79f)
						{
							this.Removed.Add(textParticle);
						}
						else if (textParticle.y < 0f)
						{
							this.Removed.Add(textParticle);
						}
						else if (textParticle.y > 24f)
						{
							this.Removed.Add(textParticle);
						}
					}
				}
				foreach (TileParticle tileParticle in this.TileParticles)
				{
					if (tileParticle.DelayMS > 0L)
					{
						tileParticle.DelayMS -= num;
					}
					else
					{
						tileParticle.Life -= num2;
						tileParticle.x += tileParticle.xDel * (float)num2;
						tileParticle.y += tileParticle.yDel * (float)num2;
						tileParticle.xDel += tileParticle.xDelDel * (float)num2;
						tileParticle.yDel += tileParticle.yDelDel * (float)num2;
						if (tileParticle.x < 0f)
						{
							this.TileRemoved.Add(tileParticle);
						}
						else if (tileParticle.x > 79f)
						{
							this.TileRemoved.Add(tileParticle);
						}
						else if (tileParticle.y < 0f)
						{
							this.TileRemoved.Add(tileParticle);
						}
						else if (tileParticle.y > 24f)
						{
							this.TileRemoved.Add(tileParticle);
						}
					}
				}
				foreach (RadialTextParticle radialTextParticle in this.RadialParticles)
				{
					if (radialTextParticle.DelayMS > 0L)
					{
						radialTextParticle.DelayMS -= num;
					}
					else
					{
						radialTextParticle.Life -= num2;
						radialTextParticle.r += radialTextParticle.rdel * (float)num2;
						radialTextParticle.d += radialTextParticle.ddel * (float)num2;
						if (radialTextParticle.d < 1f)
						{
							this.RadialRemoved.Add(radialTextParticle);
						}
						while (radialTextParticle.r < 0f)
						{
							radialTextParticle.r += 6.28f;
						}
						while ((double)radialTextParticle.r > 6.28)
						{
							radialTextParticle.r -= 6.28f;
						}
					}
				}
				foreach (SinusoidalTextParticle sinusoidalTextParticle in this.SinusoidalParticles)
				{
					if (sinusoidalTextParticle.DelayMS > 0L)
					{
						sinusoidalTextParticle.DelayMS -= num;
					}
					else
					{
						sinusoidalTextParticle.Life -= num2;
						sinusoidalTextParticle.x += sinusoidalTextParticle.xdel * (float)num2;
						sinusoidalTextParticle.y += sinusoidalTextParticle.ydel * (float)num2;
						sinusoidalTextParticle.sinstart += sinusoidalTextParticle.sinincrement * (float)num2;
						if (sinusoidalTextParticle.x < 0f)
						{
							this.SinRemoved.Add(sinusoidalTextParticle);
						}
						else if (sinusoidalTextParticle.x > 79f)
						{
							this.SinRemoved.Add(sinusoidalTextParticle);
						}
						else if (sinusoidalTextParticle.y < 0f)
						{
							this.SinRemoved.Add(sinusoidalTextParticle);
						}
						else if (sinusoidalTextParticle.y > 24f)
						{
							this.SinRemoved.Add(sinusoidalTextParticle);
						}
						while (sinusoidalTextParticle.sinstart < 0f)
						{
							sinusoidalTextParticle.sinstart += 6.28f;
						}
						while ((double)sinusoidalTextParticle.sinstart > 6.28)
						{
							sinusoidalTextParticle.sinstart -= 6.28f;
						}
					}
				}
				foreach (TextParticle textParticle2 in this.Removed)
				{
					this.Particles.Remove(textParticle2);
				}
				foreach (TileParticle tileParticle2 in this.TileRemoved)
				{
					this.TileParticles.Remove(tileParticle2);
				}
				foreach (SinusoidalTextParticle sinusoidalTextParticle2 in this.SinRemoved)
				{
					this.SinusoidalParticles.Remove(sinusoidalTextParticle2);
				}
				foreach (RadialTextParticle radialTextParticle2 in this.RadialRemoved)
				{
					this.RadialParticles.Remove(radialTextParticle2);
				}
			}
		}

		// Token: 0x04005223 RID: 21027
		public TextConsole _TextConsole;

		// Token: 0x04005224 RID: 21028
		public ScreenBuffer _ScreenBuffer;

		// Token: 0x04005225 RID: 21029
		public CleanQueue<BannerText> Banners = new CleanQueue<BannerText>();

		// Token: 0x04005226 RID: 21030
		public List<TextParticle> Particles = new List<TextParticle>();

		// Token: 0x04005227 RID: 21031
		public List<TileParticle> TileParticles = new List<TileParticle>();

		// Token: 0x04005228 RID: 21032
		public List<RadialTextParticle> RadialParticles = new List<RadialTextParticle>();

		// Token: 0x04005229 RID: 21033
		public List<SinusoidalTextParticle> SinusoidalParticles = new List<SinusoidalTextParticle>();

		// Token: 0x0400522A RID: 21034
		[NonSerialized]
		public static CleanQueue<TextParticle> textParticlesQueue = new CleanQueue<TextParticle>();

		// Token: 0x0400522B RID: 21035
		[NonSerialized]
		public static CleanQueue<TileParticle> tileParticlesQueue = new CleanQueue<TileParticle>();

		// Token: 0x0400522C RID: 21036
		[NonSerialized]
		public static CleanQueue<SinusoidalTextParticle> sinParticlesQueue = new CleanQueue<SinusoidalTextParticle>();

		// Token: 0x0400522D RID: 21037
		[NonSerialized]
		public static CleanQueue<RadialTextParticle> radParticlesQueue = new CleanQueue<RadialTextParticle>();

		// Token: 0x0400522E RID: 21038
		private StringBuilder SB = new StringBuilder(1024);

		// Token: 0x0400522F RID: 21039
		private List<TextParticle> Removed = new List<TextParticle>();

		// Token: 0x04005230 RID: 21040
		private List<TileParticle> TileRemoved = new List<TileParticle>();

		// Token: 0x04005231 RID: 21041
		private List<RadialTextParticle> RadialRemoved = new List<RadialTextParticle>();

		// Token: 0x04005232 RID: 21042
		private List<SinusoidalTextParticle> SinRemoved = new List<SinusoidalTextParticle>();

		// Token: 0x04005233 RID: 21043
		private static Stopwatch frameTimer = null;

		// Token: 0x04005234 RID: 21044
		private static long lastElapsed = 0L;

		// Token: 0x04005235 RID: 21045
		private static long accumulator = 0L;
	}
}
