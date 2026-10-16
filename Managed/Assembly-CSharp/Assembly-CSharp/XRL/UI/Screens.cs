using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Qud.UI;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x020014D3 RID: 5331
	public class Screens
	{
		// Token: 0x0600D0D6 RID: 53462 RVA: 0x004D6E20 File Offset: 0x004D5020
		private Screens()
		{
			Screens.ScreenList = new List<IScreen>();
			Screens.ScreenList.Add(new SkillsAndPowersScreen());
			Screens.ScreenList.Add(new StatusScreen());
			Screens.ScreenList.Add(new InventoryScreen());
			Screens.ScreenList.Add(new EquipmentScreen());
			Screens.ScreenList.Add(new FactionsScreen());
			Screens.ScreenList.Add(new QuestLog());
			Screens.ScreenList.Add(new JournalScreen());
			Screens.ScreenList.Add(new TinkeringScreen());
			Screens.PopupScreens = new Dictionary<string, IScreen>();
			Screens.PopupScreens.Add("Factions", new FactionsScreen());
		}

		// Token: 0x0600D0D7 RID: 53463 RVA: 0x004D6ED3 File Offset: 0x004D50D3
		public static void Awake()
		{
		}

		// Token: 0x0600D0D8 RID: 53464 RVA: 0x004D6ED8 File Offset: 0x004D50D8
		public static void ShowPopup(string Screen, GameObject GO)
		{
			if (Screen == "Factions")
			{
				Screens.CurrentScreen = 4;
			}
			if (Options.ModernCharacterSheet && Options.ModernUI)
			{
				int num = 0;
				if (Screens.CurrentScreen == 0)
				{
					num = 0;
				}
				if (Screens.CurrentScreen == 1)
				{
					num = 1;
				}
				if (Screens.CurrentScreen == 2)
				{
					num = 2;
				}
				if (Screens.CurrentScreen == 3)
				{
					num = 2;
				}
				if (Screens.CurrentScreen == 4)
				{
					num = 6;
				}
				if (Screens.CurrentScreen == 5)
				{
					num = 5;
				}
				if (Screens.CurrentScreen == 6)
				{
					num = 4;
				}
				if (Screens.CurrentScreen == 7)
				{
					num = 3;
				}
				ScreenReturn result = StatusScreensScreen.show(num, GO).Result;
				return;
			}
			Screens.PopupScreens[Screen].Show(GO);
		}

		// Token: 0x0600D0D9 RID: 53465 RVA: 0x004D6F78 File Offset: 0x004D5178
		public static void Show(GameObject GO)
		{
			if (Options.ModernUI && Options.ModernCharacterSheet)
			{
				int num = 0;
				if (Screens.CurrentScreen == 0)
				{
					num = 0;
				}
				if (Screens.CurrentScreen == 1)
				{
					num = 1;
				}
				if (Screens.CurrentScreen == 2)
				{
					num = 2;
				}
				if (Screens.CurrentScreen == 3)
				{
					num = 2;
				}
				if (Screens.CurrentScreen == 4)
				{
					num = 6;
				}
				if (Screens.CurrentScreen == 5)
				{
					num = 5;
				}
				if (Screens.CurrentScreen == 6)
				{
					num = 4;
				}
				if (Screens.CurrentScreen == 7)
				{
					num = 3;
				}
				ScreenReturn result = StatusScreensScreen.show(num, GO).Result;
				return;
			}
			GameManager.Instance.PushGameView("StatusScreens", true);
			Screens.OldBuffer.Copy(TextConsole.CurrentBuffer);
			ScreenReturn screenReturn;
			while ((screenReturn = Screens.ScreenList[Screens.CurrentScreen].Show(GO)) != ScreenReturn.Exit)
			{
				if (screenReturn == ScreenReturn.Next)
				{
					Screens.CurrentScreen++;
				}
				if (screenReturn == ScreenReturn.Previous)
				{
					Screens.CurrentScreen--;
				}
				if (Screens.CurrentScreen < 0)
				{
					Screens.CurrentScreen = Screens.ScreenList.Count - 1;
				}
				if (Screens.CurrentScreen > Screens.ScreenList.Count - 1)
				{
					Screens.CurrentScreen = 0;
				}
			}
			Popup._TextConsole.DrawBuffer(Screens.OldBuffer, null, false);
			GameManager.Instance.PopGameView(false);
		}

		// Token: 0x0400525F RID: 21087
		public static Screens _Screens = new Screens();

		// Token: 0x04005260 RID: 21088
		public static int CurrentScreen = 0;

		// Token: 0x04005261 RID: 21089
		private static List<IScreen> ScreenList;

		// Token: 0x04005262 RID: 21090
		private static Dictionary<string, IScreen> PopupScreens;

		// Token: 0x04005263 RID: 21091
		public static ScreenBuffer OldBuffer = ScreenBuffer.create(80, 25);
	}
}
