using System;
using System.Diagnostics;
using ConsoleLib.Console;
using Qud.UI;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x020014DA RID: 5338
	public class GenericTerminal
	{
		// Token: 0x0600D0FB RID: 53499 RVA: 0x004D9BF4 File Offset: 0x004D7DF4
		public static string ntoa(int n)
		{
			if (n == 1)
			{
				return "A";
			}
			if (n == 2)
			{
				return "B";
			}
			if (n == 3)
			{
				return "C";
			}
			if (n == 4)
			{
				return "D";
			}
			if (n == 5)
			{
				return "E";
			}
			if (n == 6)
			{
				return "F";
			}
			if (n == 7)
			{
				return "G";
			}
			if (n == 8)
			{
				return "H";
			}
			if (n == 9)
			{
				return "I";
			}
			if (n == 10)
			{
				return "J";
			}
			if (n == 11)
			{
				return "K";
			}
			if (n == 12)
			{
				return "L";
			}
			if (n == 13)
			{
				return "M";
			}
			if (n == 14)
			{
				return "N";
			}
			if (n == 15)
			{
				return "O";
			}
			return "?";
		}

		// Token: 0x0600D0FC RID: 53500 RVA: 0x004D9CA3 File Offset: 0x004D7EA3
		public static void ShowTerminal(GameObject Terminal, GameObject Object, GenericTerminalScreen startingScreen)
		{
			GenericTerminal.instance = new GenericTerminal();
			GenericTerminal.instance._ShowTerminal(Terminal, Object, startingScreen);
			GenericTerminal.instance = null;
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x0600D0FD RID: 53501 RVA: 0x004D9CC2 File Offset: 0x004D7EC2
		// (set) Token: 0x0600D0FE RID: 53502 RVA: 0x004D9CCA File Offset: 0x004D7ECA
		public GenericTerminalScreen currentScreen
		{
			get
			{
				return this._currentScreen;
			}
			set
			{
				this._currentScreen = value;
				if (this._currentScreen == null)
				{
					return;
				}
				this.nTopLine = 0;
				this.nSelected = 0;
				this.Position = 0;
				this._currentScreen.Update();
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x0600D0FF RID: 53503 RVA: 0x004D9CFC File Offset: 0x004D7EFC
		public string currentText
		{
			get
			{
				return this.currentScreen.renderedText;
			}
		}

		// Token: 0x0600D100 RID: 53504 RVA: 0x004D9D0C File Offset: 0x004D7F0C
		public void _ShowTerminal(GameObject Terminal, GameObject obj, GenericTerminalScreen startingScreen)
		{
			this.terminal = Terminal;
			this.obj = obj;
			this.currentScreen = startingScreen;
			GameManager.Instance.PushGameView("CyberneticsTerminalScreen", true);
			if (Options.ModernUI)
			{
				bool result = CyberneticsTerminalScreen.ShowGenericTerminal(this).Result;
				return;
			}
			try
			{
				TextConsole.LoadScrapBuffers();
				ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer2;
				ScreenBuffer scrapBuffer2 = TextConsole.ScrapBuffer2;
				bool flag = false;
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				Stopwatch stopwatch2 = new Stopwatch();
				stopwatch2.Start();
				int num = 3;
				int num2 = 76;
				while (!flag)
				{
					Event.ResetPool(true);
					scrapBuffer2.Copy(scrapBuffer);
					scrapBuffer2.Fill(num, 2, num2, 24, 32, ColorUtility.MakeColor(TextColor.Black, TextColor.Black), false, false);
					scrapBuffer2.BeveledBox(num, 2, num2, 24, ColorUtility.Bright(TextColor.Black), 1);
					scrapBuffer2.Goto(20, 2);
					string text = null;
					if (this.currentScreen != null)
					{
						this.currentScreen.BeforeRender(scrapBuffer2, ref text);
					}
					if (stopwatch2.ElapsedMilliseconds > 15L)
					{
						this.Position++;
						stopwatch2.Reset();
						stopwatch2.Start();
					}
					int num3 = num + 2;
					int num4 = 4;
					int num5 = 0;
					string text2 = "";
					int num6 = 0;
					while (num6 < this.Position && num6 < this.currentScreen.renderedText.Length)
					{
						if (num3 > num2 - 2)
						{
							if (num5 >= this.nTopLine)
							{
								num4++;
							}
							num3 = num + 2;
							num5++;
							if (num5 >= this.nTopLine + 19)
							{
								scrapBuffer2.Goto(num + 2, num4);
								scrapBuffer2.Write("<more...>", true, false, false, null, -1);
								break;
							}
						}
						if (this.currentText[num6] != '\r')
						{
							if (this.currentText[num6] == '&')
							{
								text2 = "";
								text2 += this.currentText.Substring(num6, 1);
								num6++;
								text2 += this.currentText.Substring(num6, 1);
							}
							else if (this.currentText[num6] == '\n')
							{
								if (num5 >= this.nTopLine)
								{
									num4++;
								}
								num3 = num + 2;
								num5++;
								if (num5 >= this.nTopLine + 19)
								{
									scrapBuffer2.Goto(num + 2, num4);
									scrapBuffer2.Write("<more...>", true, false, false, null, -1);
									break;
								}
							}
							else
							{
								if (num5 >= this.nTopLine)
								{
									scrapBuffer2.Goto(num3, num4);
									scrapBuffer2.Write(text2 + this.currentText.Substring(num6, 1).ToUpper(), true, false, false, null, -1);
								}
								num3++;
							}
						}
						num6++;
					}
					if (num6 >= this.currentScreen.renderedText.Length)
					{
						this.currentScreen.TextComplete();
					}
					if (stopwatch.ElapsedMilliseconds % 1000L > 500L)
					{
						scrapBuffer2.Write("_", true, false, false, null, -1);
					}
					Popup._TextConsole.DrawBuffer(scrapBuffer2, null, false);
					if (Keyboard.kbhit())
					{
						this.currentScreen.TextComplete();
						Keys keys = Keyboard.getvk(true, false, true);
						if (this.Position < this.currentScreen.renderedText.Length + 1)
						{
							this.Position = this.currentScreen.renderedText.Length + 1;
						}
						else
						{
							if (keys >= Keys.A && keys <= Keys.Z)
							{
								int num7 = keys - Keys.A;
								if (num7 < this.currentScreen.Options.Count)
								{
									this.nSelected = num7;
								}
								this.currentScreen.Update();
								if (this.nTopLine < this.currentScreen.OptionLines[this.nSelected])
								{
									this.nTopLine = this.currentScreen.OptionLines[this.nSelected];
								}
								if (this.nTopLine + 19 > this.currentScreen.OptionLines[this.nSelected])
								{
									this.nTopLine = this.currentScreen.OptionLines[this.nSelected] - 19;
								}
								if (this.nTopLine < 0)
								{
									this.nTopLine = 0;
								}
								keys = Keys.Space;
							}
							if (keys == Keys.Escape)
							{
								this.currentScreen.Back();
							}
							if (keys == Keys.Space || keys == Keys.Enter)
							{
								this.currentScreen.Activate();
							}
							if (this.currentScreen == null)
							{
								flag = true;
							}
							if (keys == Keys.Prior)
							{
								if (this.nSelected < 10)
								{
									this.nSelected = 0;
								}
								else
								{
									this.nSelected -= 10;
								}
								if (this.nSelected < 0)
								{
									this.nSelected = this.currentScreen.Options.Count - 1;
								}
								this.currentScreen.Update();
								if (this.nTopLine < this.currentScreen.OptionLines[this.nSelected])
								{
									this.nTopLine = this.currentScreen.OptionLines[this.nSelected];
								}
								if (this.nTopLine + 18 > this.currentScreen.OptionLines[this.nSelected])
								{
									this.nTopLine = this.currentScreen.OptionLines[this.nSelected] - 18;
								}
								if (this.nTopLine < 0)
								{
									this.nTopLine = 0;
								}
							}
							else if (keys == Keys.Next)
							{
								if (this.nSelected < this.currentScreen.Options.Count - 11)
								{
									this.nSelected += 10;
								}
								else
								{
									this.nSelected = this.currentScreen.Options.Count - 1;
								}
								if (this.nSelected >= this.currentScreen.Options.Count)
								{
									this.nSelected = 0;
								}
								this.currentScreen.Update();
								if (this.nTopLine < this.currentScreen.OptionLines[this.nSelected])
								{
									this.nTopLine = this.currentScreen.OptionLines[this.nSelected];
								}
								if (this.nTopLine + 18 > this.currentScreen.OptionLines[this.nSelected])
								{
									this.nTopLine = this.currentScreen.OptionLines[this.nSelected] - 18;
								}
								if (this.nTopLine < 0)
								{
									this.nTopLine = 0;
								}
							}
							else if (keys == Keys.NumPad8)
							{
								this.nSelected--;
								if (this.nSelected < 0)
								{
									this.nSelected = this.currentScreen.Options.Count - 1;
								}
								this.currentScreen.Update();
								if (this.nTopLine < this.currentScreen.OptionLines[this.nSelected])
								{
									this.nTopLine = this.currentScreen.OptionLines[this.nSelected];
								}
								if (this.nTopLine + 18 > this.currentScreen.OptionLines[this.nSelected])
								{
									this.nTopLine = this.currentScreen.OptionLines[this.nSelected] - 18;
								}
								if (this.nTopLine < 0)
								{
									this.nTopLine = 0;
								}
							}
							else if (keys == Keys.NumPad2)
							{
								this.nSelected++;
								if (this.nSelected >= this.currentScreen.Options.Count)
								{
									this.nSelected = 0;
								}
								this.currentScreen.Update();
								if (this.nTopLine < this.currentScreen.OptionLines[this.nSelected])
								{
									this.nTopLine = this.currentScreen.OptionLines[this.nSelected];
								}
								if (this.nTopLine + 18 > this.currentScreen.OptionLines[this.nSelected])
								{
									this.nTopLine = this.currentScreen.OptionLines[this.nSelected] - 18;
								}
								if (this.nTopLine < 0)
								{
									this.nTopLine = 0;
								}
							}
						}
					}
				}
			}
			finally
			{
				GameManager.Instance.PopGameView(false);
			}
		}

		// Token: 0x04005272 RID: 21106
		public static GenericTerminal instance;

		// Token: 0x04005273 RID: 21107
		public int nSelected;

		// Token: 0x04005274 RID: 21108
		public int Position;

		// Token: 0x04005275 RID: 21109
		public int nTopLine;

		// Token: 0x04005276 RID: 21110
		public GameObject terminal;

		// Token: 0x04005277 RID: 21111
		public GameObject obj;

		// Token: 0x04005278 RID: 21112
		public GenericTerminalScreen _currentScreen;
	}
}
