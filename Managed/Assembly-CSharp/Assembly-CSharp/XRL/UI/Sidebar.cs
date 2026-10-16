using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using Cysharp.Text;
using UnityEngine;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Text;

namespace XRL.UI
{
	// Token: 0x02001479 RID: 5241
	public class Sidebar
	{
		// Token: 0x0600CE54 RID: 52820 RVA: 0x004AEE78 File Offset: 0x004AD078
		public static void FormatToRTF(TextBuilder Source, TextBuilder Dest, string opacity = "FF")
		{
			bool flag = false;
			Dest.Clear();
			for (int i = 0; i < Source.Length; i++)
			{
				char c = Source[i];
				if (c == '&')
				{
					if (i < Source.Length - 1 && Source[i + 1] == '&')
					{
						i++;
						Dest.Append("&");
					}
					else
					{
						if (flag)
						{
							Dest.Append("</color>");
						}
						flag = true;
						i++;
						if (i < Source.Length)
						{
							Dest.Append("<color=#");
							if (!ConsoleLib.Console.ColorUtility.ColorMap.ContainsKey(Source[i]))
							{
								Debug.Log("Unknown color code: " + Source[i].ToString());
								Dest.Append("DD00DDFF>");
							}
							else
							{
								Color color = ConsoleLib.Console.ColorUtility.ColorMap[Source[i]];
								Dest.AppendHex2((int)Math.Min(color.r * 255f, 255f));
								Dest.AppendHex2((int)Math.Min(color.g * 255f, 255f));
								Dest.AppendHex2((int)Math.Min(color.b * 255f, 255f));
								Dest.Append(opacity);
								Dest.Append(">");
							}
						}
					}
				}
				else if (c == '^')
				{
					if (i < Source.Length - 1 && Source[i + 1] == '^')
					{
						i++;
						Dest.Append("^");
					}
					else
					{
						i++;
					}
				}
				else
				{
					int num;
					if (Sidebar.Codepage437Mapping.TryGetValue(c, out num))
					{
						c = (char)num;
					}
					Dest.Append(c);
				}
			}
			if (flag)
			{
				Dest.Append("</color>");
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x0600CE55 RID: 52821 RVA: 0x004AF036 File Offset: 0x004AD236
		// (set) Token: 0x0600CE56 RID: 52822 RVA: 0x004AF071 File Offset: 0x004AD271
		public static bool Hidden
		{
			get
			{
				if (Options.ShiftHidesSidebar && Keyboard.bShift)
				{
					GameManager instance = GameManager.Instance;
					if (((instance != null) ? instance.CurrentGameView : null) == "Stage")
					{
						return !Sidebar._Hidden;
					}
				}
				return Sidebar._Hidden;
			}
			set
			{
				Sidebar._Hidden = value;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x0600CE57 RID: 52823 RVA: 0x004AF079 File Offset: 0x004AD279
		// (set) Token: 0x0600CE58 RID: 52824 RVA: 0x004AF080 File Offset: 0x004AD280
		public static int SidebarState
		{
			get
			{
				return Sidebar._SidebarState;
			}
			set
			{
				The.Game.Player.Messages.Cache_0_12Valid = false;
				Sidebar._SidebarState = value;
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x0600CE59 RID: 52825 RVA: 0x004AF09D File Offset: 0x004AD29D
		// (set) Token: 0x0600CE5A RID: 52826 RVA: 0x004AF0B0 File Offset: 0x004AD2B0
		public static XRL.World.GameObject CurrentTarget
		{
			get
			{
				XRL.World.GameObject.Validate(ref Sidebar._CurrentTarget);
				return Sidebar._CurrentTarget;
			}
			set
			{
				if (value != Sidebar._CurrentTarget && (value == null || !value.IsPlayer()))
				{
					Sidebar._CurrentTarget = value;
					if (Sidebar._CurrentTarget != null && !AutoAct.Attacking && Sidebar._CurrentTarget.IsHostileTowards(The.Player))
					{
						AutoAct.Interrupt(null, null, Sidebar._CurrentTarget, true);
					}
				}
			}
		}

		// Token: 0x0600CE5B RID: 52827 RVA: 0x004AF104 File Offset: 0x004AD304
		public static void UpdateState()
		{
			XRL.World.GameObject player = The.Player;
			if (((player != null) ? player.Physics : null) == null)
			{
				return;
			}
			XRL.World.Parts.Physics physics = The.Player.Physics;
			if (physics != null && physics.CurrentCell != null)
			{
				if (physics.CurrentCell.Y > 10 && Sidebar.VState == "bottom")
				{
					Sidebar.SetSidebarVState("top");
				}
				if (physics.CurrentCell.Y < 8 && Sidebar.VState == "top")
				{
					Sidebar.SetSidebarVState("bottom");
				}
				if (physics.CurrentCell.X > 42 && Sidebar.State == "right")
				{
					Sidebar.SetSidebarState("left");
				}
				if (physics.CurrentCell.X < 38 && Sidebar.State == "left")
				{
					Sidebar.SetSidebarState("right");
				}
			}
		}

		// Token: 0x0600CE5C RID: 52828 RVA: 0x004AF1E8 File Offset: 0x004AD3E8
		public static bool AnyAutogotItems()
		{
			return Sidebar.AutogotItems.Count > 0;
		}

		// Token: 0x0600CE5D RID: 52829 RVA: 0x004AF1F7 File Offset: 0x004AD3F7
		public static void ClearAutogotItems()
		{
			Sidebar.AutogotItems.Clear();
		}

		// Token: 0x0600CE5E RID: 52830 RVA: 0x004AF203 File Offset: 0x004AD403
		public static void AddAutogotItem(XRL.World.GameObject GO)
		{
			if (!Sidebar.AutogotItems.ContainsKey(GO) && GO.Render != null)
			{
				Sidebar.AutogotItems.Add(GO, "<autogot> " + GO.ShortDisplayName);
			}
		}

		// Token: 0x0600CE5F RID: 52831 RVA: 0x004AF235 File Offset: 0x004AD435
		public static void SetSidebarVState(string _State)
		{
			Sidebar.VState = _State;
		}

		// Token: 0x0600CE60 RID: 52832 RVA: 0x004AF23D File Offset: 0x004AD43D
		public static void SetSidebarState(string _State)
		{
			Sidebar.State = _State;
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x0600CE61 RID: 52833 RVA: 0x004AF245 File Offset: 0x004AD445
		// (set) Token: 0x0600CE62 RID: 52834 RVA: 0x004AF251 File Offset: 0x004AD451
		[Obsolete]
		public static bool WaitingForHPWarning
		{
			get
			{
				return The.Core.HPWarning;
			}
			set
			{
				The.Core.HPWarning = value;
			}
		}

		// Token: 0x0600CE63 RID: 52835 RVA: 0x004AF260 File Offset: 0x004AD460
		public static void Update()
		{
			if (Sidebar.PlayerBody == null)
			{
				return;
			}
			if (Sidebar.PlayerBody.Physics == null)
			{
				Sidebar.PlayerBody = null;
				return;
			}
			Sidebar.sPlayerHPColor = Sidebar.PlayerBody.GetHPColor();
			if (Sidebar.PlayerBody.GetIntProperty("Analgesia", 0) > 0)
			{
				Sidebar.sPlayerHP = Strings.WoundLevel(Sidebar.PlayerBody);
				Sidebar.Analgesic = true;
			}
			else
			{
				Sidebar.Analgesic = false;
				int hitpoints = Sidebar.PlayerBody.hitpoints;
				int baseHitpoints = Sidebar.PlayerBody.baseHitpoints;
				if (hitpoints != Sidebar.LastHP || baseHitpoints != Sidebar.LastHPBase)
				{
					Sidebar.LastHP = hitpoints;
					Sidebar.LastHPBase = baseHitpoints;
					Sidebar.sPlayerHP = XRL.World.Event.NewStringBuilder(null).Append(Sidebar.sPlayerHPColor).Append(hitpoints)
						.Append(" &y/ ")
						.Append(Sidebar.LastHPBase)
						.ToString();
				}
			}
			int carriedWeight = Sidebar.PlayerBody.GetCarriedWeight();
			int maxCarriedWeight = Sidebar.PlayerBody.GetMaxCarriedWeight();
			if (carriedWeight != Sidebar.LastWeight || maxCarriedWeight != Sidebar.LastMaxWeight)
			{
				Sidebar.LastWeight = carriedWeight;
				Sidebar.LastMaxWeight = maxCarriedWeight;
				Sidebar.sWeight = XRL.World.Event.NewStringBuilder(null).Append('#').Append(carriedWeight)
					.Append('/')
					.Append(maxCarriedWeight)
					.ToString();
			}
		}

		// Token: 0x0600CE64 RID: 52836 RVA: 0x004AF389 File Offset: 0x004AD589
		public static void DrawMessageBox(ScreenBuffer _ScreenBuffer, int x1, int y1, int x2, int y2)
		{
		}

		// Token: 0x0600CE65 RID: 52837 RVA: 0x004AF38C File Offset: 0x004AD58C
		private static void PutTargetInSB()
		{
			string text = Sidebar.CurrentTarget.DisplayName;
			if (ConsoleLib.Console.ColorUtility.LengthExceptFormatting(Sidebar.SB) > 21)
			{
				text = ConsoleLib.Console.ColorUtility.ClipExceptFormatting(text, 21);
			}
			Sidebar.SB.Clear().Append("{{R|[{{y|").Append(text)
				.Append("}}]}}");
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x0600CE66 RID: 52838 RVA: 0x004AF3E0 File Offset: 0x004AD5E0
		// (set) Token: 0x0600CE67 RID: 52839 RVA: 0x004AF3E7 File Offset: 0x004AD5E7
		public static int CurrentXP
		{
			get
			{
				return Sidebar._CurrentXP;
			}
			set
			{
				if (Sidebar._CurrentXP != value)
				{
					Sidebar.bOverlayUpdated = true;
				}
				Sidebar._CurrentXP = value;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x0600CE68 RID: 52840 RVA: 0x004AF3FD File Offset: 0x004AD5FD
		// (set) Token: 0x0600CE69 RID: 52841 RVA: 0x004AF404 File Offset: 0x004AD604
		public static int NextXP
		{
			get
			{
				return Sidebar._NextXP;
			}
			set
			{
				if (Sidebar._NextXP != value)
				{
					Sidebar.bOverlayUpdated = true;
				}
				Sidebar._NextXP = value;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x0600CE6A RID: 52842 RVA: 0x004AF41A File Offset: 0x004AD61A
		// (set) Token: 0x0600CE6B RID: 52843 RVA: 0x004AF421 File Offset: 0x004AD621
		public static string sPlayerHP
		{
			get
			{
				return Sidebar._sPlayerHP;
			}
			set
			{
				if (Sidebar._sPlayerHP != value)
				{
					Sidebar.bOverlayUpdated = true;
				}
				Sidebar._sPlayerHP = value;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x0600CE6C RID: 52844 RVA: 0x004AF43C File Offset: 0x004AD63C
		// (set) Token: 0x0600CE6D RID: 52845 RVA: 0x004AF443 File Offset: 0x004AD643
		public static int CurrentHP
		{
			get
			{
				return Sidebar._CurrentHP;
			}
			set
			{
				if (Sidebar._CurrentHP != value)
				{
					Sidebar.bOverlayUpdated = true;
				}
				Sidebar._CurrentHP = value;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x0600CE6E RID: 52846 RVA: 0x004AF459 File Offset: 0x004AD659
		// (set) Token: 0x0600CE6F RID: 52847 RVA: 0x004AF460 File Offset: 0x004AD660
		public static int MaxHP
		{
			get
			{
				return Sidebar._MaxHP;
			}
			set
			{
				if (Sidebar._MaxHP != value)
				{
					Sidebar.bOverlayUpdated = true;
				}
				Sidebar._MaxHP = value;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x0600CE70 RID: 52848 RVA: 0x004AF476 File Offset: 0x004AD676
		// (set) Token: 0x0600CE71 RID: 52849 RVA: 0x004AF47D File Offset: 0x004AD67D
		public static string AbilityText
		{
			get
			{
				return Sidebar._AbilityText;
			}
			set
			{
				if (Sidebar._AbilityText != value)
				{
					Sidebar.bOverlayUpdated = true;
				}
				Sidebar._AbilityText = value;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x0600CE72 RID: 52850 RVA: 0x004AF498 File Offset: 0x004AD698
		// (set) Token: 0x0600CE73 RID: 52851 RVA: 0x004AF49F File Offset: 0x004AD69F
		public static string LeftText
		{
			get
			{
				return Sidebar._LeftText;
			}
			set
			{
				if (Sidebar._LeftText != value)
				{
					Sidebar.bOverlayUpdated = true;
				}
				Sidebar._LeftText = value;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x0600CE74 RID: 52852 RVA: 0x004AF4BA File Offset: 0x004AD6BA
		// (set) Token: 0x0600CE75 RID: 52853 RVA: 0x004AF4C1 File Offset: 0x004AD6C1
		public static string RightText
		{
			get
			{
				return Sidebar._RightText;
			}
			set
			{
				if (Sidebar._RightText != value)
				{
					Sidebar.bOverlayUpdated = true;
				}
				Sidebar._RightText = value;
			}
		}

		// Token: 0x0600CE76 RID: 52854 RVA: 0x004AF4DC File Offset: 0x004AD6DC
		public static string ToCP437(string s)
		{
			if (s == null)
			{
				return null;
			}
			if (Sidebar.Codepage437Inverse == null)
			{
				Sidebar.Codepage437Inverse = Sidebar.Codepage437Mapping.Where<KeyValuePair<char, int>>((KeyValuePair<char, int> kv) => kv.Value != 32).ToDictionary<KeyValuePair<char, int>, char, int>((KeyValuePair<char, int> kv) => (char)kv.Value, (KeyValuePair<char, int> kv) => (int)kv.Key);
			}
			string text = s;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
			{
				foreach (char c in s)
				{
					int num;
					if (Sidebar.Codepage437Inverse.TryGetValue(c, out num))
					{
						c = (char)num;
					}
					utf16ValueStringBuilder.Append(c);
				}
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x0600CE77 RID: 52855 RVA: 0x004AF5D4 File Offset: 0x004AD7D4
		public static string FromCP437(string s)
		{
			if (s == null)
			{
				return null;
			}
			string text = s;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
			{
				foreach (char c in s)
				{
					int num;
					if (Sidebar.Codepage437Mapping.TryGetValue(c, out num))
					{
						c = (char)num;
					}
					utf16ValueStringBuilder.Append(c);
				}
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x0600CE78 RID: 52856 RVA: 0x004AF654 File Offset: 0x004AD854
		public static string FormatToRTF(string s, string opacity = "FF")
		{
			if (s == null)
			{
				return "";
			}
			Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder();
			string text;
			try
			{
				Sidebar.FormatToRTF(s, ref utf16ValueStringBuilder, opacity);
				text = utf16ValueStringBuilder.ToString();
			}
			finally
			{
				utf16ValueStringBuilder.Dispose();
			}
			return text;
		}

		// Token: 0x0600CE79 RID: 52857 RVA: 0x004AF6A4 File Offset: 0x004AD8A4
		public static void FormatToRTF(string s, ref Utf16ValueStringBuilder sb, string opacity = "FF")
		{
			if (s == null)
			{
				return;
			}
			s = Markup.Transform(s, false);
			bool flag = false;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == '&')
				{
					if (i < s.Length - 1 && s[i + 1] == '&')
					{
						i++;
						sb.Append("&");
					}
					else
					{
						if (flag)
						{
							sb.Append("</color>");
						}
						flag = true;
						i++;
						if (i < s.Length)
						{
							sb.Append("<color=#");
							if (!ConsoleLib.Console.ColorUtility.ColorMap.ContainsKey(s[i]))
							{
								Debug.Log("Unknown color code: " + s[i].ToString());
								sb.Append("DD00DDFF>");
							}
							else
							{
								Color color = ConsoleLib.Console.ColorUtility.ColorMap[s[i]];
								sb.Append(((int)Math.Min(color.r * 255f, 255f)).ToString("X2"));
								sb.Append(((int)Math.Min(color.g * 255f, 255f)).ToString("X2"));
								sb.Append(((int)Math.Min(color.b * 255f, 255f)).ToString("X2"));
								sb.Append(opacity);
								sb.Append(">");
							}
						}
					}
				}
				else if (s[i] == '^')
				{
					if (i < s.Length - 1 && s[i + 1] == '^')
					{
						i++;
						sb.Append("^");
					}
					else
					{
						i++;
					}
				}
				else
				{
					char c = s[i];
					int num;
					if (Sidebar.Codepage437Mapping.TryGetValue(c, out num))
					{
						c = (char)num;
					}
					sb.Append(c);
				}
			}
			if (flag)
			{
				sb.Append("</color>");
			}
		}

		// Token: 0x0600CE7A RID: 52858 RVA: 0x004AF898 File Offset: 0x004ADA98
		public static void Render(ScreenBuffer _ScreenBuffer)
		{
			if (GameManager.bDraw == 23)
			{
				return;
			}
			if (Keyboard.bAlt)
			{
				return;
			}
			if (GameManager.bDraw == 24)
			{
				return;
			}
			if (Options.ModernUI)
			{
				if (Sidebar.LastRenderedTick == Sidebar.SidebarTick)
				{
					return;
				}
				Sidebar.LastRenderedTick = Sidebar.SidebarTick;
				Sidebar.PlayerBody = The.Player;
				if (Sidebar.PlayerBody != null)
				{
					if (The.Player.GetIntProperty("Analgesia", 0) > 0)
					{
						Sidebar.sPlayerHP = Strings.WoundLevel(The.Player);
						Sidebar.Analgesic = true;
					}
					else
					{
						Sidebar.Analgesic = false;
						int hitpoints = Sidebar.PlayerBody.hitpoints;
						int baseHitpoints = Sidebar.PlayerBody.baseHitpoints;
						if (hitpoints != Sidebar.LastHP || baseHitpoints != Sidebar.LastHPBase)
						{
							Sidebar.LastHP = hitpoints;
							Sidebar.LastHPBase = baseHitpoints;
							Sidebar.sPlayerHP = XRL.World.Event.NewStringBuilder(null).Append("&Y").Append(hitpoints)
								.Append(" / ")
								.Append(Sidebar.LastHPBase)
								.ToString();
						}
					}
					int statValue = Sidebar.PlayerBody.GetStatValue("Level", 0);
					int xpforLevel = Leveler.GetXPForLevel(statValue);
					Sidebar.CurrentXP = Sidebar.PlayerBody.GetStatValue("XP", 0);
					Sidebar.NextXP = Leveler.GetXPForLevel(statValue + 1);
					Sidebar.XPBarPercentage = (float)(Sidebar.CurrentXP - xpforLevel) / (float)(Sidebar.NextXP - xpforLevel);
					Sidebar.CurrentHP = Sidebar.PlayerBody.hitpoints;
					Sidebar.MaxHP = Sidebar.PlayerBody.baseHitpoints;
					Sidebar.SB.Length = 0;
				}
				return;
			}
			else
			{
				if (The.Player != Sidebar.PlayerBody)
				{
					Sidebar.PlayerBody = The.Player;
					if (Sidebar.PlayerBody == null)
					{
						return;
					}
					Sidebar.Update();
				}
				if (GameManager.bDraw == 25)
				{
					return;
				}
				if (GameManager.bDraw == 26)
				{
					return;
				}
				if (Sidebar.Hidden)
				{
					if (Sidebar.State == "left")
					{
						_ScreenBuffer.Goto(0, 0);
					}
					if (Sidebar.State == "right")
					{
						_ScreenBuffer.Goto(77, 0);
					}
					_ScreenBuffer.Write("[&W/&y]", true, false, false, null, -1);
					if (Sidebar.State == "left")
					{
						_ScreenBuffer.Goto(0, 1);
					}
					if (Sidebar.State == "right")
					{
						_ScreenBuffer.Goto(79 - Sidebar.sPlayerHP.LengthExceptFormatting(), 1);
					}
					_ScreenBuffer.Write(Sidebar.sPlayerHP, true, false, false, null, -1);
					if (Sidebar.State == "left")
					{
					}
					if (Sidebar.State == "right")
					{
					}
					if (Sidebar.CurrentTarget != null)
					{
						string text = Strings.WoundLevel(Sidebar.CurrentTarget);
						_ScreenBuffer.Goto(79 - text.LengthExceptFormatting() - 1, 2);
						_ScreenBuffer.Write(text, true, false, false, null, -1);
					}
					Sidebar.RenderCurrentCellPopup(_ScreenBuffer);
					return;
				}
				if (GameManager.bDraw == 27)
				{
					return;
				}
				int num = 60;
				int num2 = 61;
				if (Sidebar.State == "left")
				{
					num = 25;
					num2 = 0;
				}
				else if (Sidebar.State == "right")
				{
					num = 55;
					num2 = 56;
				}
				_ScreenBuffer.Goto(num, 0);
				for (int i = 0; i < 25; i++)
				{
					_ScreenBuffer.Goto(num, i);
					_ScreenBuffer.Write(179);
				}
				if (Sidebar.State == "left")
				{
					_ScreenBuffer.Fill(0, 0, num - 1, 24, 32, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Black, TextColor.Black), false, false);
				}
				if (Sidebar.State == "right")
				{
					_ScreenBuffer.Fill(num + 1, 0, 79, 24, 32, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Black, TextColor.Black), false, false);
				}
				if (GameManager.bDraw == 28)
				{
					return;
				}
				if (Sidebar.SidebarState == 3)
				{
					if (XRLCore.Core.HostileWalkObjects.Count > 0)
					{
						XRLCore.Core.HostileWalkObjects.Sort(new XRLCore.SortObjectBydistanceToPlayer());
						int num3 = 0;
						while (num3 < XRLCore.Core.HostileWalkObjects.Count && num3 < 14)
						{
							string text2 = XRLCore.Core.HostileWalkObjects[num3].DisplayName;
							if (text2.Length > 20)
							{
								text2 = text2.Substring(0, 20);
							}
							if (Sidebar.State == "left")
							{
								_ScreenBuffer.Goto(0, num3);
							}
							if (Sidebar.State == "right")
							{
								_ScreenBuffer.Goto(num + 1, num3);
							}
							XRL.World.GameObject gameObject = XRLCore.Core.HostileWalkObjects[num3];
							if (gameObject == Sidebar.CurrentTarget)
							{
								_ScreenBuffer.Write("^r" + text2, true, false, false, null, -1);
							}
							else
							{
								_ScreenBuffer.Write(text2, true, false, false, null, -1);
							}
							Sidebar.SB.Length = 0;
							Sidebar.SB.Append(gameObject.hitpoints);
							Sidebar.SB.Append("/");
							Sidebar.SB.Append(gameObject.baseHitpoints);
							Sidebar.SB.Append('\u0003');
							int num4 = 100;
							if (gameObject.baseHitpoints != 0)
							{
								num4 = gameObject.hitpoints * 100 / gameObject.baseHitpoints;
							}
							if (num4 < 15)
							{
								_ScreenBuffer.Write("&r", true, false, false, null, -1);
								_ScreenBuffer.Write(Sidebar.SB, -1);
							}
							else if (num4 < 33)
							{
								_ScreenBuffer.Write("&R", true, false, false, null, -1);
								_ScreenBuffer.Write(Sidebar.SB, -1);
							}
							else if (num4 < 66)
							{
								_ScreenBuffer.Write("&W", true, false, false, null, -1);
								_ScreenBuffer.Write(Sidebar.SB, -1);
							}
							else if (num4 < 100)
							{
								_ScreenBuffer.Write("&G", true, false, false, null, -1);
								_ScreenBuffer.Write(Sidebar.SB, -1);
							}
							else
							{
								_ScreenBuffer.Write("&Y", true, false, false, null, -1);
								_ScreenBuffer.Write(Sidebar.SB, -1);
							}
							num3++;
						}
					}
					if (XRLCore.Core.Game.Player.Messages.Cache_0_12Valid)
					{
						if (Sidebar.State == "left")
						{
							Text.DrawBottomToTop(_ScreenBuffer, Sidebar.MessageCache, 0, 16, num, 24);
						}
						else if (Sidebar.State == "right")
						{
							Text.DrawBottomToTop(_ScreenBuffer, Sidebar.MessageCache, num + 1, 16, 79, 24);
						}
					}
					else
					{
						StringBuilder lines = XRLCore.Core.Game.Player.Messages.GetLines(0, 12);
						if (Sidebar.State == "left")
						{
							Sidebar.MessageCache = Text.DrawBottomToTop(_ScreenBuffer, lines, 0, 16, num, 24);
						}
						else if (Sidebar.State == "right")
						{
							Sidebar.MessageCache = Text.DrawBottomToTop(_ScreenBuffer, lines, num + 1, 16, 79, 24);
						}
					}
					if (Sidebar.State == "left")
					{
						_ScreenBuffer.Goto(0, 14);
					}
					if (Sidebar.State == "right")
					{
						_ScreenBuffer.Goto(num + 2, 14);
					}
					_ScreenBuffer.Write(Sidebar.sPlayerHP, true, false, false, null, -1);
					if (Sidebar.State == "left")
					{
						_ScreenBuffer.Goto(0, 15);
					}
					if (Sidebar.State == "right")
					{
						_ScreenBuffer.Goto(num + 1, 15);
					}
					if (Sidebar.CurrentTarget != null)
					{
						_ScreenBuffer.Goto(num2 + 1, 12);
						Sidebar.PutTargetInSB();
						_ScreenBuffer.Write(Sidebar.SB, -1);
						_ScreenBuffer.Goto(num2 + 1, 13);
						_ScreenBuffer.Write(Strings.WoundLevel(Sidebar.CurrentTarget), true, false, false, null, -1);
					}
				}
				else
				{
					if (Sidebar.SidebarState == 2)
					{
						if (XRLCore.Core.Game.Player.Messages.Cache_0_12Valid)
						{
							if (Sidebar.State == "left")
							{
								Text.DrawBottomToTop(_ScreenBuffer, Sidebar.MessageCache, 0, 16, num, 24);
							}
							else if (Sidebar.State == "right")
							{
								Text.DrawBottomToTop(_ScreenBuffer, Sidebar.MessageCache, num + 1, 16, 79, 24);
							}
						}
						else
						{
							StringBuilder lines2 = XRLCore.Core.Game.Player.Messages.GetLines(0, 12);
							if (Sidebar.State == "left")
							{
								Sidebar.MessageCache = Text.DrawBottomToTop(_ScreenBuffer, lines2, 0, 16, num, 24);
							}
							else if (Sidebar.State == "right")
							{
								Sidebar.MessageCache = Text.DrawBottomToTop(_ScreenBuffer, lines2, num + 1, 16, 79, 24);
							}
						}
						if (Sidebar.CurrentTarget != null)
						{
							_ScreenBuffer.Goto(num2 + 1, 12);
							Sidebar.PutTargetInSB();
							_ScreenBuffer.Write(Sidebar.SB, -1);
							_ScreenBuffer.Goto(num2 + 1, 13);
							_ScreenBuffer.Write(Strings.WoundLevel(Sidebar.CurrentTarget), true, false, false, null, -1);
						}
						_ScreenBuffer.Goto(num2 + 15, 11);
						_ScreenBuffer.Write(Sidebar.sWeight, true, false, false, null, -1);
						_ScreenBuffer.Goto(num2 + 1, 11);
						_ScreenBuffer.Write("HP: ", true, false, false, null, -1);
						_ScreenBuffer.Write(Sidebar.sPlayerHP, true, false, false, null, -1);
						List<ActivatedAbilityEntry> list = new List<ActivatedAbilityEntry>();
						int num5 = 0;
						ActivatedAbilities activatedAbilities = The.Player.ActivatedAbilities;
						if (activatedAbilities == null)
						{
							goto IL_138C;
						}
						using (Dictionary<Guid, ActivatedAbilityEntry>.KeyCollection.Enumerator enumerator = activatedAbilities.AbilityByGuid.Keys.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Guid guid = enumerator.Current;
								if (num5 > 11)
								{
									break;
								}
								ActivatedAbilityEntry activatedAbilityEntry = activatedAbilities.AbilityByGuid[guid];
								if (!list.CleanContains(activatedAbilityEntry))
								{
									list.Add(activatedAbilityEntry);
									_ScreenBuffer.Goto(num2, num5);
									string text3 = "";
									string text4 = "y";
									if (activatedAbilityEntry.Toggleable)
									{
										if (!activatedAbilityEntry.ToggleState)
										{
											text4 = "r";
										}
										else
										{
											text4 = "g";
										}
									}
									else if (!activatedAbilityEntry.Enabled || activatedAbilityEntry.Cooldown > 0)
									{
										text4 = "K";
									}
									if (activatedAbilityEntry.Cooldown > 0)
									{
										text3 = string.Concat(new string[]
										{
											text3,
											"{{Y|({{C|",
											activatedAbilityEntry.CooldownRounds.ToString(),
											"}})-}}{{",
											text4,
											"|",
											activatedAbilityEntry.DisplayName,
											"}}"
										});
									}
									else
									{
										text3 = string.Concat(new string[] { text3, "{{", text4, "|", activatedAbilityEntry.DisplayName, "}}" });
									}
									if (ConsoleLib.Console.ColorUtility.LengthExceptFormatting(text3) > 25)
									{
										text3 = ConsoleLib.Console.ColorUtility.ClipExceptFormatting(text3, 25);
									}
									_ScreenBuffer.Write(text3, true, false, false, null, -1);
									num5++;
								}
							}
							goto IL_138C;
						}
					}
					if (Sidebar.SidebarState == 1)
					{
						if (XRLCore.Core.Game.Player.Messages.Cache_0_12Valid)
						{
							if (Sidebar.State == "left")
							{
								Text.DrawBottomToTop(_ScreenBuffer, Sidebar.MessageCache, 0, 1, num, 24);
							}
							else if (Sidebar.State == "right")
							{
								Text.DrawBottomToTop(_ScreenBuffer, Sidebar.MessageCache, num + 1, 1, 79, 24);
							}
						}
						else
						{
							StringBuilder lines3 = XRLCore.Core.Game.Player.Messages.GetLines(0, 12);
							if (Sidebar.State == "left")
							{
								Sidebar.MessageCache = Text.DrawBottomToTop(_ScreenBuffer, lines3, 0, 1, num, 24);
							}
							else if (Sidebar.State == "right")
							{
								Sidebar.MessageCache = Text.DrawBottomToTop(_ScreenBuffer, lines3, num + 1, 1, 79, 24);
							}
						}
						if (Sidebar.CurrentTarget != null && Sidebar.CurrentTarget.HasStat("Hitpoints"))
						{
							_ScreenBuffer.Goto(num2 + 1, 1);
							Sidebar.PutTargetInSB();
							_ScreenBuffer.Write(Sidebar.SB, -1);
							_ScreenBuffer.Goto(num2 + 1, 2);
							_ScreenBuffer.Write(Strings.WoundLevel(Sidebar.CurrentTarget), true, false, false, null, -1);
						}
						_ScreenBuffer.Goto(num2 + 15, 0);
						_ScreenBuffer.Write(Sidebar.sWeight, true, false, false, null, -1);
						_ScreenBuffer.Goto(num2 + 1, 0);
						_ScreenBuffer.Write("HP: " + Sidebar.sPlayerHP, true, false, false, null, -1);
					}
					else if (Sidebar.SidebarState == 0)
					{
						if (GameManager.bDraw == 29)
						{
							return;
						}
						if (GameManager.bDraw == 30)
						{
							return;
						}
						if (XRLCore.Core.Game.Player.Messages.Cache_0_12Valid)
						{
							if (Sidebar.State == "left")
							{
								Text.DrawBottomToTop(_ScreenBuffer, Sidebar.MessageCache, 0, 16, num, 24);
							}
							else if (Sidebar.State == "right")
							{
								Text.DrawBottomToTop(_ScreenBuffer, Sidebar.MessageCache, num + 1, 16, 79, 24);
							}
						}
						else
						{
							StringBuilder lines4 = XRLCore.Core.Game.Player.Messages.GetLines(0, 12);
							if (Sidebar.State == "left")
							{
								Sidebar.MessageCache = Text.DrawBottomToTop(_ScreenBuffer, lines4, 0, 16, num, 24);
							}
							else if (Sidebar.State == "right")
							{
								Sidebar.MessageCache = Text.DrawBottomToTop(_ScreenBuffer, lines4, num + 1, 16, 79, 24);
							}
						}
						if (GameManager.bDraw == 31)
						{
							return;
						}
						if (Sidebar.CurrentTarget != null && Sidebar.CurrentTarget.HasStat("Hitpoints"))
						{
							_ScreenBuffer.Goto(num2 + 1, 12);
							Sidebar.PutTargetInSB();
							_ScreenBuffer.Write(Sidebar.SB, -1);
							_ScreenBuffer.Goto(num2 + 1, 13);
							_ScreenBuffer.Write(Strings.WoundLevel(Sidebar.CurrentTarget), true, false, false, null, -1);
						}
						if (GameManager.bDraw == 32)
						{
							return;
						}
						_ScreenBuffer.Goto(num2 + 1, 0);
						if (Options.LocationIntseadOfName)
						{
							XRL.World.GameObject player = The.Player;
							if (((player != null) ? player.CurrentZone : null) != null)
							{
								_ScreenBuffer.Write(StringFormat.ClipLine(WorldFactory.Factory.ZoneDisplayName(The.Player.CurrentZone.ZoneID), 22, true, null), true, false, false, null, -1);
							}
							else
							{
								_ScreenBuffer.Write("unknown", true, false, false, null, -1);
							}
						}
						else if (The.Player.HasEffect<Dominated>())
						{
							if (("Dom: " + The.Player.DisplayName).Length < 24)
							{
								_ScreenBuffer.Write("Dom: " + The.Player.DisplayName, true, false, false, null, -1);
							}
							else
							{
								_ScreenBuffer.Write(("Dom: " + The.Player.DisplayName).Substring(0, 24), true, false, false, null, -1);
							}
						}
						else
						{
							_ScreenBuffer.Write(XRLCore.Core.Game.PlayerName, true, false, false, null, -1);
						}
						if (GameManager.bDraw == 33)
						{
							return;
						}
						_ScreenBuffer.Goto(num2 + 1, 1);
						StringBuilder stringBuilder = XRL.World.Event.NewStringBuilder(null);
						stringBuilder.Append("&YST&y: &C").Append(Sidebar.PlayerBody.Statistics["Strength"].Value);
						_ScreenBuffer.Write(stringBuilder, -1);
						_ScreenBuffer.Goto(num2 + 9, 1);
						stringBuilder.Length = 0;
						stringBuilder.Append("&WAG&y: &C").Append(Sidebar.PlayerBody.Statistics["Agility"].Value);
						_ScreenBuffer.Write(stringBuilder, -1);
						_ScreenBuffer.Goto(num2 + 17, 1);
						int num6 = Sidebar.PlayerBody.Speed;
						if (Sidebar.PlayerBody.Physics.Temperature < Sidebar.PlayerBody.Physics.FreezeTemperature && Sidebar.PlayerBody.Physics.Temperature > Sidebar.PlayerBody.Physics.BrittleTemperature)
						{
							num6 -= (int)(100.0 * (0.5 - (double)(Math.Abs((float)(Sidebar.PlayerBody.Physics.Temperature - Sidebar.PlayerBody.Physics.BrittleTemperature) / (float)(Sidebar.PlayerBody.Physics.FreezeTemperature - Sidebar.PlayerBody.Physics.BrittleTemperature)) * 0.5f)));
						}
						_ScreenBuffer.Write(XRL.World.Event.NewStringBuilder(null).AppendFormat("QN: {0}", num6), -1);
						_ScreenBuffer.Goto(num2 + 17, 2);
						_ScreenBuffer.Write(XRL.World.Event.NewStringBuilder(null).AppendFormat("MS: {0}", 100 - Sidebar.PlayerBody.Statistics["MoveSpeed"].Value + 100), -1);
						_ScreenBuffer.Goto(num2 + 1, 2);
						_ScreenBuffer.Write(XRL.World.Event.NewStringBuilder(null).AppendFormat("&RTO&y: &C{0}", Sidebar.PlayerBody.Statistics["Toughness"].Value), -1);
						_ScreenBuffer.Goto(num2 + 9, 2);
						_ScreenBuffer.Write(XRL.World.Event.NewStringBuilder(null).AppendFormat("&GWI&y: &C{0}", Sidebar.PlayerBody.Statistics["Willpower"].Value), -1);
						_ScreenBuffer.Goto(num2 + 17, 3);
						_ScreenBuffer.Write(XRL.World.Event.NewStringBuilder(null).AppendFormat("T: {0}", Sidebar.PlayerBody.Physics.Temperature), -1);
						_ScreenBuffer.Goto(num2 + 1, 3);
						_ScreenBuffer.Write(XRL.World.Event.NewStringBuilder(null).AppendFormat("&BIN&y: &C{0}", Sidebar.PlayerBody.Statistics["Intelligence"].Value), -1);
						_ScreenBuffer.Goto(num2 + 9, 3);
						_ScreenBuffer.Write(XRL.World.Event.NewStringBuilder(null).AppendFormat("&MEG&y: &C{0}", Sidebar.PlayerBody.Statistics["Ego"].Value), -1);
						_ScreenBuffer.Goto(num2 + 1, 4);
						_ScreenBuffer.Write(XRL.World.Event.NewStringBuilder(null).AppendFormat("AV: {0}", Sidebar.PlayerBody.Statistics["AV"].Value), -1);
						_ScreenBuffer.Goto(num2 + 9, 4);
						_ScreenBuffer.Write(XRL.World.Event.NewStringBuilder(null).AppendFormat("DV: {0}", Stats.GetCombatDV(Sidebar.PlayerBody)), -1);
						_ScreenBuffer.Goto(num2 + 17, 4);
						_ScreenBuffer.Write(XRL.World.Event.NewStringBuilder(null).AppendFormat("MA: {0}", Stats.GetCombatMA(Sidebar.PlayerBody)), -1);
						if (GameManager.bDraw == 34)
						{
							return;
						}
						_ScreenBuffer.Goto(num2 + 1, 5);
						_ScreenBuffer.Write(XRL.World.Event.NewStringBuilder(null).AppendFormat("XP: {0}&K / {1}", Sidebar.PlayerBody.Statistics["XP"].Value, Leveler.GetXPForLevel(Sidebar.PlayerBody.Statistics["Level"].Value + 1)), -1);
						Stomach part = Sidebar.PlayerBody.GetPart<Stomach>();
						if (part != null)
						{
							_ScreenBuffer.Goto(num2 + 1, 6);
							_ScreenBuffer.Write(part.WaterStatus(), true, false, false, null, -1);
							if (Sidebar.PlayerBody.Physics.CurrentCell != null && !Sidebar.PlayerBody.Physics.CurrentCell.ParentZone.IsWorldMap())
							{
								_ScreenBuffer.Write("&y,", true, false, false, null, -1);
								_ScreenBuffer.Write(part.FoodStatus(), true, false, false, null, -1);
							}
						}
						_ScreenBuffer.Goto(num2 + 15, 7);
						_ScreenBuffer.Write(Sidebar.sWeight, true, false, false, null, -1);
						_ScreenBuffer.Goto(num2 + 1, 7);
						_ScreenBuffer.Write("HP: ", true, false, false, null, -1);
						_ScreenBuffer.Write(Sidebar.sPlayerHP, true, false, false, null, -1);
						_ScreenBuffer.Goto(num2 + 1, 9);
						_ScreenBuffer.Write(Calendar.GetDay(), true, false, false, null, -1);
						_ScreenBuffer.Write(" of ", true, false, false, null, -1);
						_ScreenBuffer.Write(Calendar.GetMonth(), true, false, false, null, -1);
						_ScreenBuffer.Goto(num2 + 1, 10);
						_ScreenBuffer.Write(Calendar.GetTime(), true, false, false, null, -1);
						Sidebar.RenderMissleStatus(Sidebar.PlayerBody, num2 + 1, 11, _ScreenBuffer);
						if (GameManager.bDraw == 35)
						{
							return;
						}
					}
				}
				IL_138C:
				if (Options.GetOption("OptionShowSidebarAbilities", "") == "Yes")
				{
					Sidebar.RenderAbilityStatus(Sidebar.PlayerBody, num, 3, _ScreenBuffer);
				}
				_ScreenBuffer.Goto(num, 19);
				_ScreenBuffer.Write(193);
				_ScreenBuffer.Goto(num, 20);
				_ScreenBuffer.Write("&W*&y", true, false, false, null, -1);
				_ScreenBuffer.Goto(num, 21);
				_ScreenBuffer.Write("&W/&y", true, false, false, null, -1);
				_ScreenBuffer.Goto(num, 22);
				_ScreenBuffer.Write(194);
				if (GameManager.bDraw == 36)
				{
					return;
				}
				Sidebar.RenderCurrentCellPopup(_ScreenBuffer);
				int bDraw = GameManager.bDraw;
				return;
			}
		}

		// Token: 0x0600CE7B RID: 52859 RVA: 0x004B0CF0 File Offset: 0x004AEEF0
		private static void RenderAbilityStatus(XRL.World.GameObject Player, int xp, int yp, ScreenBuffer _Buffer)
		{
			if (Player.HasPart("ActivatedAbilities"))
			{
				ActivatedAbilities part = Player.GetPart<ActivatedAbilities>();
				if (part.AbilityByGuid != null && part.AbilityByGuid.Count > 0)
				{
					int num = yp;
					_Buffer.Goto(xp, num);
					_Buffer.Write("Á", true, false, false, null, -1);
					foreach (ActivatedAbilityEntry activatedAbilityEntry in part.GetAbilityListOrderedByPreference())
					{
						num++;
						_Buffer.Goto(xp, num);
						_Buffer.Write(activatedAbilityEntry.GetUITile());
					}
					num++;
					_Buffer.Goto(xp, num);
					_Buffer.Write("Â", true, false, false, null, -1);
				}
			}
		}

		// Token: 0x0600CE7C RID: 52860 RVA: 0x004B0DC0 File Offset: 0x004AEFC0
		private static void RenderMissleStatus(XRL.World.GameObject Player, int xp, int yp, ScreenBuffer _Buffer)
		{
			List<XRL.World.GameObject> missileWeapons = Player.GetMissileWeapons(null, null);
			if (missileWeapons != null && missileWeapons.Count > 0)
			{
				int num = 0;
				while (num < missileWeapons.Count && num < 4)
				{
					MissileWeapon part = missileWeapons[num].GetPart<MissileWeapon>();
					if (part != null)
					{
						string text = part.Status(null);
						if (ConsoleLib.Console.ColorUtility.LengthExceptFormatting(text) > 24)
						{
							text = ConsoleLib.Console.ColorUtility.ClipExceptFormatting(text, 24);
						}
						_Buffer.WriteAt(xp, yp, text, true);
						yp++;
						num++;
					}
				}
			}
		}

		// Token: 0x0600CE7D RID: 52861 RVA: 0x004B0E34 File Offset: 0x004AF034
		private static void RenderCurrentCellPopup(ScreenBuffer _Buffer)
		{
			if (Options.GetOption("OptionShowCurrentCellPopup", "") != "Yes")
			{
				return;
			}
			XRL.World.GameObject Player = The.Player;
			if (Player.Physics.CurrentCell == null)
			{
				return;
			}
			Sidebar.Objects.Clear();
			foreach (XRL.World.GameObject gameObject in Sidebar.AutogotItems.Keys)
			{
				Sidebar.Objects.Add(Sidebar.AutogotItems[gameObject]);
			}
			Cell currentCell = Player.CurrentCell;
			string text = "Physics";
			Predicate<XRL.World.GameObject> <>9__0;
			Predicate<XRL.World.GameObject> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = (XRL.World.GameObject GO) => GO != Player && GO.IsTakeable());
			}
			foreach (XRL.World.GameObject gameObject2 in currentCell.LoopObjectsWithPart(text, predicate))
			{
				Sidebar.Objects.Add(gameObject2.ShortDisplayName);
			}
			List<string> list = new List<string>(Sidebar.Objects.Count);
			int num = 0;
			int num2 = 0;
			foreach (string text2 in Sidebar.Objects)
			{
				num2++;
				if (num2 == 10)
				{
					list.Add("<more...>");
					break;
				}
				list.Add(text2 + "\n");
				string text3 = ConsoleLib.Console.ColorUtility.StripFormatting(text2);
				if (text3.Length > num)
				{
					num = text3.Length;
				}
			}
			num++;
			int num3;
			int num4;
			if ((Sidebar.Hidden && Sidebar.State == "left") || (!Sidebar.Hidden && Sidebar.State == "right"))
			{
				num3 = 0;
				if (Sidebar.VState == "bottom")
				{
					num4 = 24 - Sidebar.Objects.Count - 1;
				}
				else if (Sidebar.Hidden)
				{
					num4 = 3;
				}
				else
				{
					num4 = 0;
				}
			}
			else
			{
				num3 = 80 - num - 3;
				if (Sidebar.VState == "bottom")
				{
					num4 = 24 - Sidebar.Objects.Count - 1;
				}
				else if (Sidebar.Hidden)
				{
					num4 = 3;
				}
				else
				{
					num4 = 0;
				}
			}
			if (num > 0 && Sidebar.Objects.Count > 0)
			{
				_Buffer.Fill(num3, num4, num3 + num + 2, num4 + list.Count + 1, 32, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Black, TextColor.Black), false, false);
				_Buffer.ThickSingleBox(num3, num4, num3 + num + 2, num4 + list.Count + 1, ConsoleLib.Console.ColorUtility.MakeColor(ConsoleLib.Console.ColorUtility.Bright(TextColor.Black), TextColor.Black));
			}
			int num5 = 0;
			foreach (string text4 in list)
			{
				_Buffer.Goto(num3 + 2, num4 + 1 + num5);
				_Buffer.Write(text4, true, false, false, null, -1);
				num5++;
			}
		}

		// Token: 0x04004FF6 RID: 20470
		public static string State = "right";

		// Token: 0x04004FF7 RID: 20471
		public static string VState = "top";

		// Token: 0x04004FF8 RID: 20472
		public static bool _Hidden = false;

		// Token: 0x04004FF9 RID: 20473
		public static int _SidebarState = 0;

		// Token: 0x04004FFA RID: 20474
		public static XRL.World.GameObject _CurrentTarget = null;

		// Token: 0x04004FFB RID: 20475
		public static Dictionary<XRL.World.GameObject, string> AutogotItems = new Dictionary<XRL.World.GameObject, string>();

		// Token: 0x04004FFC RID: 20476
		private static XRL.World.GameObject PlayerBody = null;

		// Token: 0x04004FFD RID: 20477
		public static string sWeight = "";

		// Token: 0x04004FFE RID: 20478
		public static string sPlayerHPColor;

		// Token: 0x04004FFF RID: 20479
		public static int LastHP = -1;

		// Token: 0x04005000 RID: 20480
		public static int LastHPBase = -1;

		// Token: 0x04005001 RID: 20481
		public static int LastWeight = -1;

		// Token: 0x04005002 RID: 20482
		public static int LastMaxWeight = -1;

		// Token: 0x04005003 RID: 20483
		public static bool Analgesic = false;

		// Token: 0x04005004 RID: 20484
		public static StringBuilder SB = new StringBuilder(2048);

		// Token: 0x04005005 RID: 20485
		public static List<string> MessageCache = null;

		// Token: 0x04005006 RID: 20486
		public static float XPBarPercentage = 0f;

		// Token: 0x04005007 RID: 20487
		public static bool bOverlayUpdated = false;

		// Token: 0x04005008 RID: 20488
		public static int _CurrentXP = 0;

		// Token: 0x04005009 RID: 20489
		public static int _NextXP = 0;

		// Token: 0x0400500A RID: 20490
		public static string _sPlayerHP = "";

		// Token: 0x0400500B RID: 20491
		public static int _CurrentHP = 0;

		// Token: 0x0400500C RID: 20492
		public static int _MaxHP = 0;

		// Token: 0x0400500D RID: 20493
		public static string _AbilityText;

		// Token: 0x0400500E RID: 20494
		public static string _LeftText;

		// Token: 0x0400500F RID: 20495
		public static string _RightText;

		// Token: 0x04005010 RID: 20496
		public static Dictionary<char, int> Codepage437Mapping = new Dictionary<char, int>
		{
			{ '\0', 32 },
			{ '\u0001', 9786 },
			{ '\u0002', 9787 },
			{ '\u0003', 9829 },
			{ '\u0004', 9830 },
			{ '\u0005', 9827 },
			{ '\u0006', 9824 },
			{ '\a', 8226 },
			{ '\b', 9688 },
			{ '\t', 9675 },
			{ '\v', 9794 },
			{ '\f', 9792 },
			{ '\u000e', 9835 },
			{ '\u000f', 9788 },
			{ '\u0010', 9654 },
			{ '\u0011', 9664 },
			{ '\u0012', 8597 },
			{ '\u0013', 8252 },
			{ '\u0014', 182 },
			{ '\u0015', 167 },
			{ '\u0016', 9602 },
			{ '\u0017', 8616 },
			{ '\u0018', 8593 },
			{ '\u0019', 8595 },
			{ '\u001a', 8594 },
			{ '\u001b', 8592 },
			{ '\u001c', 8735 },
			{ '\u001d', 8596 },
			{ '\u001e', 9650 },
			{ '\u001f', 9660 },
			{ '\u0080', 199 },
			{ '\u0081', 252 },
			{ '\u0082', 233 },
			{ '\u0083', 226 },
			{ '\u0084', 228 },
			{ '\u0085', 224 },
			{ '\u0086', 229 },
			{ '\u0087', 231 },
			{ '\u0088', 234 },
			{ '\u0089', 235 },
			{ '\u008a', 232 },
			{ '\u008b', 239 },
			{ '\u008c', 238 },
			{ '\u008d', 236 },
			{ '\u008e', 196 },
			{ '\u008f', 197 },
			{ '\u0090', 201 },
			{ '\u0091', 230 },
			{ '\u0092', 198 },
			{ '\u0093', 244 },
			{ '\u0094', 246 },
			{ '\u0095', 242 },
			{ '\u0096', 251 },
			{ '\u0097', 249 },
			{ '\u0098', 255 },
			{ '\u0099', 214 },
			{ '\u009a', 220 },
			{ '\u009b', 162 },
			{ '\u009c', 163 },
			{ '\u009d', 165 },
			{ '\u009e', 8359 },
			{ '\u009f', 402 },
			{ '\u00a0', 225 },
			{ '¡', 237 },
			{ '¢', 243 },
			{ '£', 250 },
			{ '¤', 241 },
			{ '¥', 209 },
			{ '¦', 170 },
			{ '§', 186 },
			{ '\u00a8', 191 },
			{ '©', 8976 },
			{ 'ª', 172 },
			{ '«', 189 },
			{ '¬', 188 },
			{ '\u00ad', 161 },
			{ '®', 171 },
			{ '\u00af', 187 },
			{ '°', 9617 },
			{ '±', 9618 },
			{ '²', 9619 },
			{ '³', 9474 },
			{ '\u00b4', 9508 },
			{ 'µ', 9569 },
			{ '¶', 9570 },
			{ '·', 9558 },
			{ '\u00b8', 9557 },
			{ '¹', 9571 },
			{ 'º', 9553 },
			{ '»', 9559 },
			{ '¼', 9565 },
			{ '½', 9564 },
			{ '¾', 9563 },
			{ '¿', 9488 },
			{ 'À', 9492 },
			{ 'Á', 9524 },
			{ 'Â', 9516 },
			{ 'Ã', 9500 },
			{ 'Ä', 9472 },
			{ 'Å', 9532 },
			{ 'Æ', 9566 },
			{ 'Ç', 9567 },
			{ 'È', 9562 },
			{ 'É', 9556 },
			{ 'Ê', 9577 },
			{ 'Ë', 9574 },
			{ 'Ì', 9568 },
			{ 'Í', 9552 },
			{ 'Î', 9580 },
			{ 'Ï', 9575 },
			{ 'Ð', 9576 },
			{ 'Ñ', 9572 },
			{ 'Ò', 9573 },
			{ 'Ó', 9561 },
			{ 'Ô', 9560 },
			{ 'Õ', 9554 },
			{ 'Ö', 9555 },
			{ '×', 9579 },
			{ 'Ø', 9578 },
			{ 'Ù', 9496 },
			{ 'Ú', 9484 },
			{ 'Û', 9608 },
			{ 'Ü', 9604 },
			{ 'Ý', 9612 },
			{ 'Þ', 9616 },
			{ 'ß', 9600 },
			{ 'à', 945 },
			{ 'á', 223 },
			{ 'â', 915 },
			{ 'ã', 960 },
			{ 'ä', 931 },
			{ 'å', 963 },
			{ 'æ', 181 },
			{ 'ç', 964 },
			{ 'è', 934 },
			{ 'é', 920 },
			{ 'ê', 937 },
			{ 'ë', 948 },
			{ 'ì', 8734 },
			{ 'í', 966 },
			{ 'î', 949 },
			{ 'ï', 8745 },
			{ 'ð', 8801 },
			{ 'ñ', 177 },
			{ 'ò', 8805 },
			{ 'ó', 8804 },
			{ 'ô', 8992 },
			{ 'õ', 8993 },
			{ 'ö', 247 },
			{ '÷', 8776 },
			{ 'ø', 176 },
			{ 'ù', 8729 },
			{ 'ú', 183 },
			{ 'û', 8730 },
			{ 'ü', 8319 },
			{ 'ý', 178 },
			{ 'þ', 9632 },
			{ 'ÿ', 160 },
			{ '\u2007', 1 },
			{ '\ueea4', 164 },
			{ '\ueea6', 166 },
			{ '\ueea8', 168 },
			{ '\ueea9', 169 },
			{ '\ueead', 173 },
			{ '\ueeae', 174 },
			{ '\ueeaf', 175 },
			{ '\ueeb3', 179 },
			{ '\ueeb4', 180 },
			{ '\ueeb8', 184 },
			{ '\ueeb9', 185 },
			{ '\ueebe', 190 },
			{ '\ueec0', 192 },
			{ '\ueec1', 193 },
			{ '\ueec2', 194 },
			{ '\ueec3', 195 },
			{ '\ueec8', 200 },
			{ '\ueeca', 202 },
			{ '\ueecb', 203 },
			{ '\ueecc', 204 },
			{ '\ueecd', 205 },
			{ '\ueece', 206 },
			{ '\ueecf', 207 },
			{ '\ueed0', 208 },
			{ '\ueed2', 210 },
			{ '\ueed3', 211 },
			{ '\ueed4', 212 },
			{ '\ueed5', 213 },
			{ '\ueed7', 215 },
			{ '\ueed8', 216 },
			{ '\ueed9', 217 },
			{ '\ueeda', 218 },
			{ '\ueedb', 219 },
			{ '\ueedd', 221 },
			{ '\ueede', 222 },
			{ '\ueee3', 227 },
			{ '\ueef0', 240 },
			{ '\ueef5', 245 },
			{ '\ueef8', 248 },
			{ '\ueefd', 253 },
			{ '\ueefe', 254 }
		};

		// Token: 0x04005011 RID: 20497
		public static Dictionary<char, int> Codepage437Inverse;

		// Token: 0x04005012 RID: 20498
		public static long SidebarTick = 0L;

		// Token: 0x04005013 RID: 20499
		public static long LastRenderedTick = -1L;

		// Token: 0x04005014 RID: 20500
		[NonSerialized]
		public static List<string> Objects = new List<string>();
	}
}
