using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ConsoleLib.Console;
using Qud.UI;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace XRL.UI
{
	// Token: 0x02001491 RID: 5265
	public class CyberneticsTerminal
	{
		// Token: 0x0600CEF0 RID: 52976 RVA: 0x004B52BC File Offset: 0x004B34BC
		public static void ShowTerminal(GameObject Terminal, GameObject Subject, GameObject Actor = null, TerminalScreen StartingScreen = null)
		{
			SoundManager.PlayUISound("Sounds/Interact/sfx_interact_computerterminal", 1f, false, false, SoundRequest.SoundEffectType.None);
			CyberneticsTerminal.Instance = new CyberneticsTerminal();
			CyberneticsTerminal.Instance._ShowTerminal(Terminal, Subject, Actor, StartingScreen);
			CyberneticsTerminal.Instance = null;
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x0600CEF1 RID: 52977 RVA: 0x004B52EE File Offset: 0x004B34EE
		public int LicensesRemaining
		{
			get
			{
				return this.Licenses - this.LicensesUsed;
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x0600CEF2 RID: 52978 RVA: 0x004B52FD File Offset: 0x004B34FD
		// (set) Token: 0x0600CEF3 RID: 52979 RVA: 0x004B5308 File Offset: 0x004B3508
		public TerminalScreen CurrentScreen
		{
			get
			{
				return this._CurrentScreen;
			}
			set
			{
				this._CurrentScreen = value;
				if (this._CurrentScreen == null)
				{
					return;
				}
				this.TopLine = 0;
				this.Selected = 0;
				this.Position = 0;
				this._CurrentScreen.Update();
				this.Wedges.Clear();
				this.Credits = 0;
				this.Subject.ForeachInventoryAndEquipment(delegate(GameObject GO)
				{
					CyberneticsCreditWedge cyberneticsCreditWedge;
					if (GO.TryGetPart<CyberneticsCreditWedge>(out cyberneticsCreditWedge) && cyberneticsCreditWedge.Credits > 0)
					{
						this.Credits += cyberneticsCreditWedge.Credits * GO.Count;
						this.Wedges.Add(cyberneticsCreditWedge);
					}
				});
				this.Licenses = this.Subject.GetIntProperty("CyberneticsLicenses", 0);
				this.FreeLicenses = this.Subject.GetIntProperty("FreeCyberneticsLicenses", 0);
				this.Implants.Clear();
				this.Terminal.CurrentCell.ForeachAdjacentCell(delegate(Cell C)
				{
					C.ForeachObjectWithPart("Container", delegate(GameObject GO)
					{
						GO.Inventory.ForeachObject(delegate(GameObject obj2)
						{
							if (obj2.IsImplant && obj2.Understood())
							{
								this.Implants.Add(obj2);
							}
						});
					});
				}, true);
				this.Subject.Inventory.ForeachObject(delegate(GameObject obj2)
				{
					if (obj2.IsImplant && obj2.Understood())
					{
						this.Implants.Add(obj2);
					}
				});
				this.LicensesUsed = 0;
				this.Subject.Body.ForeachInstalledCybernetics(delegate(GameObject obj2)
				{
					if (obj2.IsImplant)
					{
						this.LicensesUsed += obj2.GetPart<CyberneticsBaseItem>().Cost;
					}
				});
				if (this.Implants.Count > 1)
				{
					this.Implants.Sort(new Comparison<GameObject>(this.SortImplants));
				}
			}
		}

		// Token: 0x0600CEF4 RID: 52980 RVA: 0x004B5428 File Offset: 0x004B3628
		public int SortImplants(GameObject A, GameObject B)
		{
			CyberneticsBaseItem part = A.GetPart<CyberneticsBaseItem>();
			int num = ((part != null) ? part.Cost : 0);
			CyberneticsBaseItem part2 = B.GetPart<CyberneticsBaseItem>();
			int num2 = ((part2 != null) ? part2.Cost : 0);
			bool flag = A.HasTag("CyberneticsOneOnly") && this.Subject.HasInstalledCybernetics(A.Blueprint, null);
			bool flag2 = B.HasTag("CyberneticsOneOnly") && this.Subject.HasInstalledCybernetics(B.Blueprint, null);
			bool flag3 = num > this.LicensesRemaining;
			bool flag4 = num2 > this.LicensesRemaining;
			int num3 = (flag || flag3).CompareTo(flag2 || flag4);
			if (num3 != 0)
			{
				return num3;
			}
			int num4 = num.CompareTo(num2);
			if (num4 != 0)
			{
				return -num4;
			}
			return A.GetCachedDisplayNameForSort().CompareTo(B.GetCachedDisplayNameForSort());
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x0600CEF5 RID: 52981 RVA: 0x004B54F0 File Offset: 0x004B36F0
		public string CurrentText
		{
			get
			{
				return this.CurrentScreen.RenderedText;
			}
		}

		// Token: 0x0600CEF6 RID: 52982 RVA: 0x004B5500 File Offset: 0x004B3700
		public void _ShowTerminal(GameObject Terminal, GameObject Subject, GameObject Actor = null, TerminalScreen StartingScreen = null)
		{
			this.Terminal = Terminal;
			this.Subject = Subject;
			this.Actor = Actor ?? Subject;
			if (StartingScreen == null)
			{
				StartingScreen = (this.Authorized ? new CyberneticsScreenMainMenu() : new CyberneticsScreenGoodbye(true));
			}
			this.CurrentScreen = StartingScreen;
			GameManager.Instance.PushGameView("CyberneticsTerminalScreen", true);
			if (Options.ModernUI)
			{
				bool result = CyberneticsTerminalScreen.ShowCyberneticsTerminal(this).Result;
				return;
			}
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
				if (this.CurrentScreen != null)
				{
					string text = null;
					this.CurrentScreen.BeforeRender(scrapBuffer2, ref text);
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
				while (num6 < this.Position && num6 < this.CurrentScreen.RenderedText.Length)
				{
					if (num3 > num2 - 2)
					{
						if (num5 >= this.TopLine)
						{
							num4++;
						}
						num3 = num + 2;
						num5++;
						if (num5 >= this.TopLine + 19)
						{
							scrapBuffer2.Goto(num + 2, num4);
							scrapBuffer2.Write("<more...>", true, false, false, null, -1);
							break;
						}
					}
					if (this.CurrentText[num6] != '\r')
					{
						if (this.CurrentText[num6] == '&')
						{
							if (num6 < this.CurrentScreen.RenderedText.Length - 1 && this.CurrentText[num6 + 1] == '&')
							{
								num6++;
								if (num5 >= this.TopLine)
								{
									scrapBuffer2.Goto(num3, num4);
									scrapBuffer2.Write(text2 + "&&", true, false, false, null, -1);
								}
								num3++;
							}
							else
							{
								text2 = "";
								text2 += this.CurrentText.Substring(num6, 1);
								num6++;
								text2 += this.CurrentText.Substring(num6, 1);
							}
						}
						else if (this.CurrentText[num6] == '\n')
						{
							if (num5 >= this.TopLine)
							{
								num4++;
							}
							num3 = num + 2;
							num5++;
							if (num5 >= this.TopLine + 19)
							{
								scrapBuffer2.Goto(num + 2, num4);
								scrapBuffer2.Write("<more...>", true, false, false, null, -1);
								break;
							}
						}
						else
						{
							if (num5 >= this.TopLine)
							{
								scrapBuffer2.Goto(num3, num4);
								scrapBuffer2.Write(text2 + this.CurrentText.Substring(num6, 1).ToUpper(), true, false, false, null, -1);
							}
							num3++;
						}
					}
					num6++;
				}
				if (num6 >= this.CurrentScreen.RenderedText.Length)
				{
					this.CurrentScreen.TextComplete();
				}
				if (stopwatch.ElapsedMilliseconds % 1000L > 500L)
				{
					scrapBuffer2.Write("_", true, false, false, null, -1);
				}
				Popup._TextConsole.DrawBuffer(scrapBuffer2, null, false);
				if (Keyboard.kbhit())
				{
					this.CurrentScreen.TextComplete();
					Keys keys = Keyboard.getvk(true, false, false);
					if (this.Position < this.CurrentScreen.RenderedText.Length + 1)
					{
						this.Position = this.CurrentScreen.RenderedText.Length + 1;
						Keyboard.ClearInput(false, false);
					}
					else
					{
						bool flag2 = false;
						if (this.CurrentScreen.HackOption != -1 && keys == (Keys.LButton | Keys.MButton | Keys.Back | Keys.Control))
						{
							this.Selected = this.CurrentScreen.HackOption;
							flag2 = true;
						}
						else if (keys >= Keys.A && keys <= Keys.Z)
						{
							int num7 = keys - Keys.A;
							if (this.CurrentScreen.HackOption == -1)
							{
								if (num7 < this.CurrentScreen.Options.Count)
								{
									this.Selected = num7;
									flag2 = true;
								}
							}
							else if (num7 < this.CurrentScreen.Options.Count - 1)
							{
								this.Selected = ((num7 >= this.CurrentScreen.HackOption) ? (num7 - 1) : num7);
								flag2 = true;
							}
						}
						if (flag2)
						{
							this.CurrentScreen.Update();
							if (this.TopLine < this.CurrentScreen.OptionLines[this.Selected])
							{
								this.TopLine = this.CurrentScreen.OptionLines[this.Selected];
							}
							if (this.TopLine + 19 > this.CurrentScreen.OptionLines[this.Selected])
							{
								this.TopLine = this.CurrentScreen.OptionLines[this.Selected] - 19;
							}
							if (this.TopLine < 0)
							{
								this.TopLine = 0;
							}
							keys = Keys.Space;
						}
						if (keys == Keys.Escape)
						{
							this.CurrentScreen.Back();
						}
						if (keys == Keys.Space || keys == Keys.Enter)
						{
							this.CurrentScreen.Activate();
						}
						if (this.CurrentScreen == null)
						{
							flag = true;
						}
						if (keys == Keys.Prior)
						{
							if (this.Selected < 10)
							{
								this.Selected = 0;
							}
							else
							{
								this.Selected -= 10;
							}
							if (this.Selected < 0)
							{
								this.Selected = this.CurrentScreen.Options.Count - 1;
							}
							this.CurrentScreen.Update();
							if (this.TopLine < this.CurrentScreen.OptionLines[this.Selected])
							{
								this.TopLine = this.CurrentScreen.OptionLines[this.Selected];
							}
							if (this.TopLine + 18 > this.CurrentScreen.OptionLines[this.Selected])
							{
								this.TopLine = this.CurrentScreen.OptionLines[this.Selected] - 18;
							}
							if (this.TopLine < 0)
							{
								this.TopLine = 0;
							}
						}
						else if (keys == Keys.Next)
						{
							if (this.Selected < this.CurrentScreen.Options.Count - 11)
							{
								this.Selected += 10;
							}
							else
							{
								this.Selected = this.CurrentScreen.Options.Count - 1;
							}
							if (this.Selected >= this.CurrentScreen.Options.Count)
							{
								this.Selected = 0;
							}
							this.CurrentScreen.Update();
							if (this.TopLine < this.CurrentScreen.OptionLines[this.Selected])
							{
								this.TopLine = this.CurrentScreen.OptionLines[this.Selected];
							}
							if (this.TopLine + 18 > this.CurrentScreen.OptionLines[this.Selected])
							{
								this.TopLine = this.CurrentScreen.OptionLines[this.Selected] - 18;
							}
							if (this.TopLine < 0)
							{
								this.TopLine = 0;
							}
						}
						else if (keys == Keys.NumPad8)
						{
							this.Selected--;
							if (this.Selected < 0)
							{
								this.Selected = this.CurrentScreen.Options.Count - 1;
							}
							this.CurrentScreen.Update();
							if (this.TopLine < this.CurrentScreen.OptionLines[this.Selected])
							{
								this.TopLine = this.CurrentScreen.OptionLines[this.Selected];
							}
							if (this.TopLine + 18 > this.CurrentScreen.OptionLines[this.Selected])
							{
								this.TopLine = this.CurrentScreen.OptionLines[this.Selected] - 18;
							}
							if (this.TopLine < 0)
							{
								this.TopLine = 0;
							}
						}
						else if (keys == Keys.NumPad2)
						{
							this.Selected++;
							if (this.Selected >= this.CurrentScreen.Options.Count)
							{
								this.Selected = 0;
							}
							this.CurrentScreen.Update();
							if (this.TopLine < this.CurrentScreen.OptionLines[this.Selected])
							{
								this.TopLine = this.CurrentScreen.OptionLines[this.Selected];
							}
							if (this.TopLine + 18 > this.CurrentScreen.OptionLines[this.Selected])
							{
								this.TopLine = this.CurrentScreen.OptionLines[this.Selected] - 18;
							}
							if (this.TopLine < 0)
							{
								this.TopLine = 0;
							}
						}
					}
				}
				else
				{
					Thread.Sleep(7);
				}
			}
			GameManager.Instance.PopGameView(false);
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x0600CEF7 RID: 52983 RVA: 0x004B5DF8 File Offset: 0x004B3FF8
		public bool Authorized
		{
			get
			{
				return this.IsAuthorized(this.Subject);
			}
		}

		// Token: 0x0600CEF8 RID: 52984 RVA: 0x004B5E06 File Offset: 0x004B4006
		public bool IsAuthorized(GameObject Object)
		{
			return this.HackActive || (Object != null && Object.IsTrueKin());
		}

		// Token: 0x0600CEF9 RID: 52985 RVA: 0x004B5E20 File Offset: 0x004B4020
		public bool AttemptHack()
		{
			int num = this.SecurityHardeningLevel + this.SecurityAlertLevel;
			GameObject terminal = this.Terminal;
			int num2 = 4 + num;
			int num3 = 2 + num * 2;
			GameObject subject = this.Subject;
			HackingSifrah hackingSifrah = new HackingSifrah(terminal, num2, num3, (subject != null) ? subject.Stat("Intelligence", 0) : 0);
			hackingSifrah.HandlerID = this.Terminal.ID;
			hackingSifrah.HandlerPartName = "CyberneticsTerminal2";
			hackingSifrah.Play(this.Terminal);
			return !hackingSifrah.InterfaceExitRequested && this.HackActive;
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x0600CEFA RID: 52986 RVA: 0x004B5E9D File Offset: 0x004B409D
		public int HackLevel
		{
			get
			{
				GameObject terminal = this.Terminal;
				if (terminal == null)
				{
					return 0;
				}
				return terminal.GetIntProperty("HackLevel", 0);
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x0600CEFB RID: 52987 RVA: 0x004B5EB8 File Offset: 0x004B40B8
		public int SecurityHardeningLevel
		{
			get
			{
				if (this.Terminal == null)
				{
					return 0;
				}
				if (!this.Terminal.HasIntProperty("SecurityHardeningLevel"))
				{
					this.Terminal.SetIntProperty("SecurityHardeningLevel", Stat.Random(0, 4), false);
				}
				return this.Terminal.GetIntProperty("SecurityHardeningLevel", 0);
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x0600CEFC RID: 52988 RVA: 0x004B5F0B File Offset: 0x004B410B
		public int SecurityAlertLevel
		{
			get
			{
				GameObject terminal = this.Terminal;
				if (terminal == null)
				{
					return 0;
				}
				return terminal.GetIntProperty("SecurityAlertLevel", 0);
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x0600CEFD RID: 52989 RVA: 0x004B5F24 File Offset: 0x004B4124
		public bool HackActive
		{
			get
			{
				return this.HackLevel > this.SecurityAlertLevel;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x0600CEFE RID: 52990 RVA: 0x004B5F34 File Offset: 0x004B4134
		// (set) Token: 0x0600CEFF RID: 52991 RVA: 0x004B5F50 File Offset: 0x004B4150
		public bool LowLevelHack
		{
			get
			{
				GameObject terminal = this.Terminal;
				return terminal != null && terminal.GetIntProperty("LowLevelHack", 0) > 0;
			}
			set
			{
				if (value)
				{
					GameObject terminal = this.Terminal;
					if (terminal == null)
					{
						return;
					}
					terminal.SetIntProperty("LowLevelHack", 1, false);
					return;
				}
				else
				{
					GameObject terminal2 = this.Terminal;
					if (terminal2 == null)
					{
						return;
					}
					terminal2.RemoveIntProperty("LowLevelHack");
					return;
				}
			}
		}

		// Token: 0x0600CF00 RID: 52992 RVA: 0x004B5F84 File Offset: 0x004B4184
		public bool CheckSecurity(int AlertChance, TerminalScreen Screen, int Times = 1)
		{
			bool hackActive = this.HackActive;
			if (this.HackActive)
			{
				if (this.LowLevelHack)
				{
					AlertChance -= 5;
					if (AlertChance <= 0 && 50.in100())
					{
						AlertChance = 1;
					}
				}
				for (int i = 0; i < Times; i++)
				{
					if (AlertChance.in100())
					{
						this.Terminal.ModIntProperty("SecurityAlertLevel", 1, false);
						break;
					}
				}
			}
			if (this.Authorized)
			{
				if (hackActive && !this.HackActive)
				{
					this.Subject = this.Actor;
					this.CurrentScreen = new CyberneticsScreenMainMenu();
				}
				else
				{
					this.CurrentScreen = Screen;
				}
				return true;
			}
			this.CurrentScreen = new CyberneticsScreenGoodbye(true);
			return false;
		}

		// Token: 0x0600CF01 RID: 52993 RVA: 0x004B6028 File Offset: 0x004B4228
		public void GetPossibleSubjects(List<GameObject> Store)
		{
			if (Store == null)
			{
				return;
			}
			Store.Add(this.Actor);
			this.GetPossibleSubjects(Store, this.Terminal.CurrentCell);
			Cell currentCell = this.Terminal.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			currentCell.ForeachAdjacentCell(delegate(Cell cell)
			{
				this.GetPossibleSubjects(Store, cell);
			}, true);
		}

		// Token: 0x0600CF02 RID: 52994 RVA: 0x004B609C File Offset: 0x004B429C
		public List<GameObject> GetPossibleSubjects()
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.GetPossibleSubjects(list);
			return list;
		}

		// Token: 0x0600CF03 RID: 52995 RVA: 0x004B60B8 File Offset: 0x004B42B8
		private void GetPossibleSubjects(List<GameObject> Store, Cell Cell)
		{
			if (Cell == null || Store == null)
			{
				return;
			}
			int i = 0;
			int count = Cell.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = Cell.Objects[i];
				if (gameObject.IsCreature && gameObject.Body != null && gameObject.IsAlliedTowards(this.Actor) && !gameObject.IsInStasis() && this.Terminal.PhaseMatches(gameObject) && !Store.Contains(gameObject))
				{
					Store.Add(gameObject);
				}
				i++;
			}
		}

		// Token: 0x0600CF04 RID: 52996 RVA: 0x004B6135 File Offset: 0x004B4335
		public int GetPossibleSubjectCount()
		{
			return this.GetPossibleSubjects().Count;
		}

		// Token: 0x0600CF05 RID: 52997 RVA: 0x004B6144 File Offset: 0x004B4344
		public void GetAuthorizedSubjects(List<GameObject> Store)
		{
			if (Store == null)
			{
				return;
			}
			Store.Add(this.Actor);
			this.GetAuthorizedSubjects(Store, this.Terminal.CurrentCell);
			Cell currentCell = this.Terminal.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			currentCell.ForeachAdjacentCell(delegate(Cell cell)
			{
				this.GetAuthorizedSubjects(Store, cell);
			}, true);
		}

		// Token: 0x0600CF06 RID: 52998 RVA: 0x004B61B8 File Offset: 0x004B43B8
		public List<GameObject> GetAuthorizedSubjects()
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.GetAuthorizedSubjects(list);
			return list;
		}

		// Token: 0x0600CF07 RID: 52999 RVA: 0x004B61D4 File Offset: 0x004B43D4
		private void GetAuthorizedSubjects(List<GameObject> Store, Cell Cell)
		{
			if (Cell == null || Store == null)
			{
				return;
			}
			int i = 0;
			int count = Cell.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = Cell.Objects[i];
				if (gameObject.IsCreature && this.IsAuthorized(gameObject) && gameObject.Body != null && gameObject.IsAlliedTowards(this.Actor) && !gameObject.IsInStasis() && this.Terminal.PhaseMatches(gameObject) && !Store.Contains(gameObject))
				{
					Store.Add(gameObject);
				}
				i++;
			}
		}

		// Token: 0x0600CF08 RID: 53000 RVA: 0x004B625A File Offset: 0x004B445A
		public int GetAuthorizedSubjectCount()
		{
			return this.GetAuthorizedSubjects().Count;
		}

		// Token: 0x0400505B RID: 20571
		public static CyberneticsTerminal Instance;

		// Token: 0x0400505C RID: 20572
		public List<CyberneticsCreditWedge> Wedges = new List<CyberneticsCreditWedge>();

		// Token: 0x0400505D RID: 20573
		public List<GameObject> Implants = new List<GameObject>();

		// Token: 0x0400505E RID: 20574
		public int Licenses;

		// Token: 0x0400505F RID: 20575
		public int FreeLicenses;

		// Token: 0x04005060 RID: 20576
		public int LicensesUsed;

		// Token: 0x04005061 RID: 20577
		public int Credits;

		// Token: 0x04005062 RID: 20578
		public int Selected;

		// Token: 0x04005063 RID: 20579
		public int Position;

		// Token: 0x04005064 RID: 20580
		public int TopLine;

		// Token: 0x04005065 RID: 20581
		public GameObject Terminal;

		// Token: 0x04005066 RID: 20582
		public GameObject Subject;

		// Token: 0x04005067 RID: 20583
		public GameObject Actor;

		// Token: 0x04005068 RID: 20584
		public TerminalScreen _CurrentScreen;
	}
}
