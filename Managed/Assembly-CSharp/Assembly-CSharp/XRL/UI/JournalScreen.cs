using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x020014A4 RID: 5284
	public class JournalScreen : IScreen
	{
		// Token: 0x0600CF7D RID: 53117 RVA: 0x004BF921 File Offset: 0x004BDB21
		public static string GetTabDisplayName(string Tab)
		{
			if (Tab == JournalScreen.STR_SULTANS)
			{
				return JournalScreen.GetSultansDisplayName();
			}
			return Tab;
		}

		// Token: 0x0600CF7E RID: 53118 RVA: 0x004BF938 File Offset: 0x004BDB38
		public unsafe static string GetSultansDisplayName()
		{
			string sultanTerm = HistoryAPI.GetSultanTerm();
			if (sultanTerm.IsNullOrEmpty())
			{
				return JournalScreen.STR_SULTANS;
			}
			if (sultanTerm != JournalScreen.SultanTerm)
			{
				int num = sultanTerm.Length + 10;
				checked
				{
					Span<char> span = new Span<char>(stackalloc byte[unchecked((UIntPtr)num) * 2], num);
					sultanTerm.CopyTo(0, span, 0, sultanTerm.Length);
					*span[0] = char.ToUpper(*span[0]);
					JournalScreen.STR_SULTANS.CopyTo(6, span, sultanTerm.Length, 10);
					JournalScreen.SultanTabName = new string(span);
					JournalScreen.SultanTerm = sultanTerm;
				}
			}
			return JournalScreen.SultanTabName;
		}

		// Token: 0x0600CF7F RID: 53119 RVA: 0x004BF9D4 File Offset: 0x004BDBD4
		public void Memorize(int Tab, bool Tabinate = false)
		{
			JournalScreen.TabMemory[] stock = JournalScreen.TabMemory.Stock;
			JournalScreen.TabMemory tabMemory;
			if ((tabMemory = stock[Tab]) == null)
			{
				tabMemory = (stock[Tab] = new JournalScreen.TabMemory());
			}
			JournalScreen.TabMemory tabMemory2 = tabMemory;
			tabMemory2.Memorize(this.selectedCategory, this.currentTopLine, this.cursorPosition);
			if (Tabinate)
			{
				tabMemory2.Selected = this.selectedCategory;
			}
		}

		// Token: 0x0600CF80 RID: 53120 RVA: 0x004BFA24 File Offset: 0x004BDC24
		public void Recall(int Tab, bool Tabinate = false)
		{
			JournalScreen.TabMemory[] stock = JournalScreen.TabMemory.Stock;
			JournalScreen.TabMemory tabMemory;
			if ((tabMemory = stock[Tab]) == null)
			{
				tabMemory = (stock[Tab] = new JournalScreen.TabMemory());
			}
			JournalScreen.TabMemory tabMemory2 = tabMemory;
			if (Tabinate)
			{
				this.selectedCategory = tabMemory2.Selected;
			}
			tabMemory2.Recall(this.selectedCategory, out this.currentTopLine, out this.cursorPosition);
		}

		// Token: 0x0600CF81 RID: 53121 RVA: 0x004BFA74 File Offset: 0x004BDC74
		public static List<IBaseJournalEntry> GetRawEntriesFor(string Tab, string Category = null)
		{
			int num = Tab.GetHashCode() ^ JournalAPI.Count;
			if (Category != null)
			{
				num ^= Category.GetHashCode();
			}
			if (JournalScreen.LastHash == num)
			{
				return JournalScreen.LastRaw;
			}
			JournalScreen.LastHash = num;
			JournalScreen.LastRaw.Clear();
			if (Tab == JournalScreen.STR_CHRONOLOGY)
			{
				JournalScreen.LastRaw.AddRange(JournalAPI.Accomplishments.Where<JournalAccomplishment>((JournalAccomplishment c) => c.Revealed));
			}
			else if (Tab == JournalScreen.STR_OBSERVATIONS)
			{
				JournalScreen.LastRaw.AddRange(JournalAPI.Observations.Where<JournalObservation>((JournalObservation c) => c.Revealed));
			}
			else if (Tab == JournalScreen.STR_SULTANS)
			{
				JournalScreen.LastRaw.AddRange(from c in JournalAPI.SultanNotes
					where c.Revealed && (c.SultanID == Category || Category == null)
					orderby c.EventID, c.Has("sultan") descending
					select c);
			}
			else if (Tab == JournalScreen.STR_VILLAGES)
			{
				JournalScreen.LastRaw.AddRange(JournalAPI.VillageNotes.Where<JournalVillageNote>((JournalVillageNote c) => c.Revealed && (c.VillageID == Category || Category == null)));
			}
			else if (Tab == JournalScreen.STR_LOCATIONS)
			{
				JournalScreen.LastRaw.AddRange(from c in JournalAPI.MapNotes
					where c.Revealed && (c.Category == Category || Category == null)
					select c into x
					orderby x.Tracked descending, x.Visited, x.Text, x.Time
					select x);
			}
			else if (Tab == JournalScreen.STR_GENERAL)
			{
				JournalScreen.LastRaw.AddRange(JournalAPI.GeneralNotes.Where<JournalGeneralNote>((JournalGeneralNote x) => x.Revealed));
			}
			else if (Tab == JournalScreen.STR_RECIPES)
			{
				JournalScreen.LastRaw.AddRange(JournalAPI.RecipeNotes.Where<JournalRecipeNote>((JournalRecipeNote x) => x.Revealed));
			}
			return JournalScreen.LastRaw;
		}

		// Token: 0x0600CF82 RID: 53122 RVA: 0x004BFD51 File Offset: 0x004BDF51
		public static void ResetRawHash()
		{
			JournalScreen.LastHash = 0;
		}

		// Token: 0x0600CF83 RID: 53123 RVA: 0x004BFD5C File Offset: 0x004BDF5C
		public void UpdateEntries(string selectedTab, GameObject GO)
		{
			this.displayLines.Clear();
			this.entries.Clear();
			this.entryForDisplayLine.Clear();
			this.categories.Clear();
			if (this.selectedCategory == null)
			{
				if (selectedTab == JournalScreen.STR_VILLAGES)
				{
					if (JournalAPI.GetKnownNotesForVillage("Joppa").Count > 0)
					{
						this.displayLines.Add("Joppa");
						this.categories.Add("Joppa");
					}
					if (JournalAPI.GetKnownNotesForVillage("Kyakukya").Count > 0)
					{
						this.displayLines.Add("Kyakukya");
						this.categories.Add("Kyakukya");
					}
					if (JournalAPI.GetKnownNotesForVillage("The Yd Freehold").Count > 0)
					{
						this.displayLines.Add("The Yd Freehold");
						this.categories.Add("The Yd Freehold");
					}
					foreach (HistoricEntity historicEntity in HistoryAPI.GetKnownVillages())
					{
						this.displayLines.Add(historicEntity.GetCurrentSnapshot().GetProperty("name", historicEntity.id));
						this.categories.Add(historicEntity.id);
					}
					if (this.displayLines.Count == 0)
					{
						this.displayLines.Add("{{K|You have no knowledge of any villages.}}");
					}
					return;
				}
				if (selectedTab == JournalScreen.STR_SULTANS)
				{
					foreach (HistoricEntity historicEntity2 in HistoryAPI.GetKnownSultans())
					{
						this.displayLines.Add(historicEntity2.GetCurrentSnapshot().GetProperty("name", historicEntity2.id));
						this.categories.Add(historicEntity2.id);
					}
					if (this.displayLines.Count == 0)
					{
						this.displayLines.Add("{{K|You have no knowledge of the sultans.}}");
					}
					return;
				}
				if (selectedTab == JournalScreen.STR_LOCATIONS)
				{
					foreach (string text in JournalAPI.GetMapNoteCategories())
					{
						string text2 = (JournalAPI.GetCategoryMapNoteToggle(text) ? "[{{G|X}}] " : "[ ] ") + text;
						this.displayLines.Add(text2);
						this.categories.Add(text);
					}
					if (this.displayLines.Count == 0)
					{
						this.displayLines.Add("{{K|You have no map notes.}}");
					}
					return;
				}
			}
			int num = 0;
			foreach (IBaseJournalEntry baseJournalEntry in JournalScreen.GetRawEntriesFor(selectedTab, this.selectedCategory))
			{
				JournalScreen.JournalEntry journalEntry = new JournalScreen.JournalEntry();
				journalEntry.entry = baseJournalEntry.GetDisplayText();
				if (Options.DebugInternals && selectedTab == JournalScreen.STR_CHRONOLOGY && baseJournalEntry is JournalAccomplishment)
				{
					string muralText = (baseJournalEntry as JournalAccomplishment).MuralText;
					JournalScreen.JournalEntry journalEntry2 = journalEntry;
					journalEntry2.entry = journalEntry2.entry + "\n\n{{internals|Ã " + muralText.Replace("=name=", GO.BaseDisplayNameStripped) + "}}";
				}
				journalEntry.baseEntry = baseJournalEntry;
				journalEntry.entryAPIPosition = num;
				this.entries.Add(journalEntry);
				num++;
			}
			int num2 = 0;
			if (selectedTab == JournalScreen.STR_SULTANS && this.selectedCategory != null)
			{
				this.displayLines.Add("[History of " + HistoryAPI.GetEntityName(this.selectedCategory) + "]");
				this.displayLines.Add("");
				this.entryForDisplayLine.Add(0);
				this.entryForDisplayLine.Add(0);
				num2 += 2;
			}
			if (selectedTab == JournalScreen.STR_VILLAGES && this.selectedCategory != null)
			{
				this.displayLines.Add("[History of " + HistoryAPI.GetEntityName(this.selectedCategory) + "]");
				this.displayLines.Add("");
				this.entryForDisplayLine.Add(0);
				this.entryForDisplayLine.Add(0);
				num2 += 2;
			}
			int num3 = 75;
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			for (int i = 0; i < this.entries.Count; i++)
			{
				int num4 = 0;
				List<string> list;
				if (selectedTab == JournalScreen.STR_CHRONOLOGY)
				{
					JournalAccomplishment journalAccomplishment = (JournalAccomplishment)this.entries[i].baseEntry;
					stringBuilder.Clear().Append(this.entries[i].entry);
					if (journalAccomplishment.Category == "player")
					{
						stringBuilder.Insert(0, "@ ");
					}
					else
					{
						stringBuilder.Insert(0, journalAccomplishment.Tradable ? "{{G|$}} " : "{{K|$}} ");
					}
					list = StringFormat.ClipTextToArray(stringBuilder.ToString(), num3, out num4, true, true, true, false);
				}
				else if (selectedTab == JournalScreen.STR_LOCATIONS)
				{
					JournalMapNote journalMapNote = (JournalMapNote)this.entries[i].baseEntry;
					stringBuilder.Clear().Append(journalMapNote.Tracked ? "[{{G|X}}] " : "[ ] ").Append(Grammar.InitCapWithFormatting(this.entries[i].entry));
					if (journalMapNote.Category == "player")
					{
						stringBuilder.Insert(0, "@ ");
					}
					else
					{
						stringBuilder.Insert(0, journalMapNote.Visited ? "{{K|?}} " : "{{G|?}} ");
						stringBuilder.Insert(0, journalMapNote.Tradable ? "{{G|$}} " : "{{K|$}} ");
					}
					list = StringFormat.ClipTextToArray(stringBuilder.ToString(), num3, out num4, true, true, true, false);
				}
				else
				{
					stringBuilder.Clear().Append("{{").Append(this.entries[i].baseEntry.Tradable ? 'G' : 'K')
						.Append("|$}} ");
					bool flag = this.entries[i].baseEntry.Has("sultanTombPropaganda");
					if (flag)
					{
						stringBuilder.Append("{{w|[tomb engraving] ");
					}
					stringBuilder.Append(this.entries[i].entry);
					if (flag)
					{
						stringBuilder.Append("}}");
					}
					list = StringFormat.ClipTextToArray(stringBuilder.ToString(), num3, out num4, true, true, true, false);
				}
				this.entries[i].topLine = num2;
				this.entries[i].lines = list.Count;
				this.displayLines.AddRange(list);
				if (i < this.entries.Count - 1)
				{
					this.entries[i].lines++;
					this.displayLines.Add("");
				}
				for (int j = 0; j < list.Count + 1; j++)
				{
					this.entryForDisplayLine.Add(i);
				}
				num2 += this.entries[i].lines;
			}
			if (this.displayLines.Count == 0)
			{
				if (selectedTab == JournalScreen.STR_CHRONOLOGY)
				{
					this.displayLines.Add("{{K|You have no history. That's pretty weird to be honest.}}");
				}
				if (selectedTab == JournalScreen.STR_OBSERVATIONS)
				{
					this.displayLines.Add("{{K|You have made no observations.}}");
				}
				if (selectedTab == JournalScreen.STR_LOCATIONS)
				{
					this.displayLines.Add("{{K|You have made no map notes. Hit " + JournalScreen.ADD_NEW_KEYBIND + " to add a new one.}}");
				}
				if (selectedTab == JournalScreen.STR_GENERAL)
				{
					this.displayLines.Add("{{K|You have made no general notes. Hit " + JournalScreen.ADD_NEW_KEYBIND + " to add a new one.}}");
				}
				if (selectedTab == JournalScreen.STR_RECIPES)
				{
					this.displayLines.Add("{{K|You have learned no recipes.}}");
				}
			}
		}

		// Token: 0x0600CF84 RID: 53124 RVA: 0x004C057C File Offset: 0x004BE77C
		public static bool HandleInsert(string selectedTab, GameObject GO)
		{
			if (selectedTab == JournalScreen.STR_CHRONOLOGY || selectedTab == JournalScreen.STR_GENERAL || selectedTab == JournalScreen.STR_LOCATIONS)
			{
				string text = Popup.AskString("Entry text", "", "Sounds/UI/ui_notification", null, null, 2147483646, 0, false, true, null);
				if (!string.IsNullOrEmpty(text))
				{
					if (selectedTab == JournalScreen.STR_CHRONOLOGY)
					{
						JournalAPI.AddAccomplishment(text, null, null, null, "player", MuralCategory.Generic, MuralWeight.Nil, null, -1L, true);
						return true;
					}
					if (selectedTab == JournalScreen.STR_GENERAL)
					{
						JournalAPI.AddGeneralNote(text, null, -1L, true);
						return true;
					}
					if (selectedTab == JournalScreen.STR_LOCATIONS)
					{
						JournalAPI.AddMapNote(GO.CurrentZone.ZoneID, text, "Miscellaneous", null, null, true, true, -1L, false);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600CF85 RID: 53125 RVA: 0x004C0650 File Offset: 0x004BE850
		public static bool HandleDelete(string selectedTab, IBaseJournalEntry entry, GameObject GO)
		{
			if (selectedTab == JournalScreen.STR_CHRONOLOGY)
			{
				if ((entry as JournalAccomplishment).Category == "player")
				{
					if (Popup.ShowYesNo("Are you sure you want to delete this entry?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
					{
						JournalAPI.DeleteAccomplishment(entry as JournalAccomplishment);
						return true;
					}
				}
				else
				{
					Popup.Show("You can't delete automatically recorded chronology entries.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			if (selectedTab == JournalScreen.STR_GENERAL && Popup.ShowYesNo("Are you sure you want to delete this entry?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
			{
				JournalAPI.DeleteGeneralNote(entry as JournalGeneralNote);
				return true;
			}
			if (selectedTab == JournalScreen.STR_LOCATIONS && Popup.ShowYesNo("Are you sure you want to delete this entry?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
			{
				JournalAPI.DeleteMapNote(entry as JournalMapNote);
				return true;
			}
			if (selectedTab == JournalScreen.STR_RECIPES)
			{
				JournalRecipeNote journalRecipeNote = entry as JournalRecipeNote;
				if (Popup.ShowYesNo("Are you sure you want to delete {{y|" + journalRecipeNote.Recipe.DisplayName + "}}?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
				{
					JournalAPI.DeleteRecipeNote(entry as JournalRecipeNote);
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x0600CF86 RID: 53126 RVA: 0x004C075E File Offset: 0x004BE95E
		public static string ADD_NEW_KEYBIND
		{
			get
			{
				return ControlManager.getCommandInputDescription("CmdInsert", true, false, ControlManager.InputDeviceType.Unknown, false);
			}
		}

		// Token: 0x0600CF87 RID: 53127 RVA: 0x004C0770 File Offset: 0x004BE970
		public ScreenReturn Show(GameObject GO)
		{
			GameManager.Instance.PushGameView("Journal", true);
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			Keys keys = Keys.None;
			bool flag = false;
			string[] array = new string[]
			{
				JournalScreen.STR_LOCATIONS,
				JournalScreen.STR_OBSERVATIONS,
				JournalScreen.STR_SULTANS,
				JournalScreen.STR_VILLAGES,
				JournalScreen.STR_CHRONOLOGY,
				JournalScreen.STR_GENERAL,
				JournalScreen.STR_RECIPES
			};
			JournalScreen.TabMemory.Stock = new JournalScreen.TabMemory[array.Length];
			int num = 0;
			this.cursorPosition = 0;
			this.currentTopLine = 0;
			this.selectedCategory = null;
			this.UpdateEntries(array[0], GO);
			string text = "< {{W|7}} Quests | Tinkering {{W|9}} >";
			if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
			{
				text = string.Concat(new string[]
				{
					"< {{W|",
					ControlManager.getCommandInputDescription("Page Left", false, false, ControlManager.InputDeviceType.Unknown, false),
					"}} Quests | Tinkering {{W|",
					ControlManager.getCommandInputDescription("Page Right", false, false, ControlManager.InputDeviceType.Unknown, false),
					"}} >"
				});
			}
			while (!flag)
			{
				Event.ResetPool(false);
				scrapBuffer.Clear();
				scrapBuffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
				scrapBuffer.Goto(35, 0);
				scrapBuffer.Write("[ {{W|Journal}} ]", true, false, false, null, -1);
				scrapBuffer.Goto(1, 2);
				int num2 = Math.Max(num - 4, 0);
				while (num2 < array.Length && num2 < Math.Max(num - 4, 0) + 5)
				{
					string tabDisplayName = JournalScreen.GetTabDisplayName(array[num2]);
					if (num == num2)
					{
						scrapBuffer.Write("  ", true, false, false, null, -1);
						scrapBuffer.Write("{{W|" + tabDisplayName + "}}", true, false, false, null, -1);
					}
					else
					{
						scrapBuffer.Write("  ", true, false, false, null, -1);
						scrapBuffer.Write("{{K|" + tabDisplayName + "}}", true, false, false, null, -1);
					}
					num2++;
				}
				if (num > 4)
				{
					scrapBuffer.Goto(1, 2);
					scrapBuffer.Write("{{G|<<}}", true, false, false, null, -1);
				}
				if (num <= 4)
				{
					scrapBuffer.Goto(76, 2);
					scrapBuffer.Write("{{G|>>}}", true, false, false, null, -1);
				}
				scrapBuffer.Goto(60, 0);
				scrapBuffer.Write(" {{W|" + ControlManager.getCommandInputDescription("Cancel", false, false, ControlManager.InputDeviceType.Unknown, false) + "}} to exit ", true, false, false, null, -1);
				scrapBuffer.Goto(79 - ColorUtility.StripFormatting(text).Length, 24);
				scrapBuffer.Write(text, true, false, false, null, -1);
				if (array[num] == JournalScreen.STR_RECIPES)
				{
					scrapBuffer.Goto(2, 24);
					if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
					{
						scrapBuffer.Write(" {{W|" + ControlManager.getCommandInputDescription("CmdDelete", false, false, ControlManager.InputDeviceType.Unknown, false) + "}} - Delete ", true, false, false, null, -1);
					}
					else
					{
						scrapBuffer.Write(" {{W|del}} - Delete ", true, false, false, null, -1);
					}
				}
				if (array[num] == JournalScreen.STR_CHRONOLOGY || array[num] == JournalScreen.STR_GENERAL || array[num] == JournalScreen.STR_LOCATIONS)
				{
					scrapBuffer.Goto(2, 24);
					scrapBuffer.Write(string.Concat(new string[]
					{
						" {{W|",
						ControlManager.getCommandInputDescription("CmdInsert", false, false, ControlManager.InputDeviceType.Unknown, false),
						"}} Add {{W|",
						ControlManager.getCommandInputDescription("CmdDelete", false, false, ControlManager.InputDeviceType.Unknown, false),
						"}} - Delete "
					}), true, false, false, null, -1);
					if (array[num] == JournalScreen.STR_LOCATIONS)
					{
						scrapBuffer.Write("{{W|" + ControlManager.getCommandInputDescription("V Positive", false, false, ControlManager.InputDeviceType.Unknown, false) + "}} - Rename Location", true, false, false, null, -1);
					}
				}
				int num3 = 4;
				int num4 = 23;
				int num5 = num4 - num3 + 1;
				int num6 = this.currentTopLine;
				int num7 = 0;
				int num8 = num3;
				while (num8 <= num4 && num6 < this.displayLines.Count)
				{
					if (num8 - num3 == this.cursorPosition)
					{
						scrapBuffer.Goto(2, num8);
						scrapBuffer.Write("{{Y|>}}", true, false, false, null, -1);
					}
					scrapBuffer.Goto(3, num8);
					scrapBuffer.Write(this.displayLines[num6], true, false, false, null, -1);
					num6++;
					num7++;
					num8++;
				}
				if (this.displayLines.Count > num5)
				{
					int num9 = (int)((float)num5 / (float)this.displayLines.Count * 23f);
					int num10 = (int)((float)this.currentTopLine / (float)this.displayLines.Count * 23f);
					scrapBuffer.Fill(79, 1, 79, 23, 177, ColorUtility.MakeColor(ColorUtility.Bright(TextColor.Black), TextColor.Black), false, false);
					scrapBuffer.Fill(79, 1 + num10, 79, 1 + num10 + num9, 177, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black), false, false);
				}
				Popup._TextConsole.DrawBuffer(scrapBuffer, null, false);
				keys = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
				if (keys == Keys.NumPad7 || (keys == Keys.NumPad9 && Keyboard.RawCode != Keys.Prior && Keyboard.RawCode != Keys.Next))
				{
					flag = true;
				}
				if (keys == Keys.Escape || keys == Keys.NumPad5)
				{
					if ((array[num] == JournalScreen.STR_SULTANS || array[num] == JournalScreen.STR_VILLAGES || array[num] == JournalScreen.STR_LOCATIONS) && this.selectedCategory != null)
					{
						this.Memorize(num, false);
						this.selectedCategory = null;
						this.Recall(num, false);
						this.UpdateEntries(array[num], GO);
					}
					else
					{
						flag = true;
					}
				}
				if ((keys == Keys.N || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:V Positive")) && array[num] == JournalScreen.STR_LOCATIONS)
				{
					Zone parentZone = GO.CurrentCell.ParentZone;
					if (parentZone.IsWorldMap())
					{
						Popup.Show("You cannot do that on the world map.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else if (parentZone.HasProperName && !parentZone.NamedByPlayer)
					{
						Popup.Show("This place already has a name.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else
					{
						string displayName = parentZone.DisplayName;
						string baseDisplayName = parentZone.BaseDisplayName;
						bool namedByPlayer = parentZone.NamedByPlayer;
						string text2 = Popup.AskString(namedByPlayer ? ("Enter a new name for " + baseDisplayName + ".") : "Enter a name for this location.", "", "Sounds/UI/ui_notification", null, null, 40, 0, false, true, null);
						if (!string.IsNullOrEmpty(text2))
						{
							if (namedByPlayer)
							{
								Popup.Show(string.Concat(new string[] { "You stop calling this location '", baseDisplayName, "' and start calling it '", text2, "'." }), null, "Sounds/UI/ui_notification", true, true, true, true, null);
								JournalAPI.AddAccomplishment(string.Concat(new string[] { "You stopped calling a location '", baseDisplayName, "' and start calling it '", text2, "'." }), string.Concat(new string[]
								{
									"In ",
									Calendar.GetMonth(),
									" of ",
									Calendar.GetYear().ToString(),
									", =name= commanded ",
									GO.GetPronounProvider(false).PossessiveAdjective,
									" cartographers to change the name of ",
									Grammar.GetProsaicZoneName(parentZone),
									" in the world atlas to ",
									text2,
									"."
								}), string.Concat(new string[]
								{
									"In =year=, =name= won a decisive victory against the combined forces of ",
									JournalAPI.GetLandmarkNearest(parentZone).Text,
									" at the bloody Battle of ",
									baseDisplayName,
									". As a result of the battle, ",
									baseDisplayName,
									" was so <spice.elements.",
									The.Player.GetMythicDomain(5, null),
									".ruinReason> that it was renamed ",
									text2,
									"."
								}), null, "general", MuralCategory.DoesBureaucracy, MuralWeight.Low, null, -1L, true);
							}
							else
							{
								Popup.Show("You start calling this location '" + text2 + "'.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
								JournalAPI.AddAccomplishment("You started calling a location '" + text2 + "'.", string.Concat(new string[]
								{
									"In ",
									Calendar.GetMonth(),
									" of ",
									Calendar.GetYear().ToString(),
									", =name= commanded ",
									GO.GetPronounProvider(false).PossessiveAdjective,
									" cartographers to change the name of ",
									Grammar.GetProsaicZoneName(parentZone),
									" in the world atlas to ",
									text2,
									"."
								}), string.Concat(new string[]
								{
									"In =year=, =name= won a decisive victory against the combined forces of ",
									JournalAPI.GetLandmarkNearest(parentZone).Text,
									" at a bloody battle near ",
									Grammar.GetProsaicZoneName(parentZone),
									". As a result of the battle, ",
									Grammar.GetProsaicZoneName(parentZone),
									" was so <spice.elements.",
									The.Player.GetMythicDomain(5, null),
									".ruinReason> that it was renamed ",
									text2,
									"."
								}), null, "general", MuralCategory.DoesBureaucracy, MuralWeight.Low, null, -1L, true);
							}
							parentZone.IncludeContextInZoneDisplay = true;
							parentZone.IncludeStratumInZoneDisplay = false;
							parentZone.NamedByPlayer = true;
							parentZone.HasProperName = true;
							parentZone.BaseDisplayName = text2;
							JournalAPI.AddMapNote(parentZone.ZoneID, text2, "Named Locations", null, null, true, true, -1L, false);
							this.UpdateEntries(array[num], GO);
							int num11 = Math.Max(0, this.displayLines.Count - num5);
							this.currentTopLine = num11 + 100;
						}
					}
				}
				int num12 = this.currentTopLine + this.cursorPosition;
				if (keys == Keys.Tab)
				{
					if (array[num] == JournalScreen.STR_LOCATIONS && this.selectedCategory == null && num12 < this.categories.Count)
					{
						JournalAPI.SetCategoryMapNoteToggle(this.categories[num12], !JournalAPI.GetCategoryMapNoteToggle(this.categories[num12]));
						this.UpdateEntries(array[num], GO);
					}
					else if (array[num] == JournalScreen.STR_LOCATIONS && this.selectedCategory != null && num12 < this.entryForDisplayLine.Count)
					{
						(this.entries[this.entryForDisplayLine[num12]].baseEntry as JournalMapNote).Tracked = !(this.entries[this.entryForDisplayLine[num12]].baseEntry as JournalMapNote).Tracked;
						this.UpdateEntries(array[num], GO);
					}
				}
				if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdDelete" && JournalScreen.HandleDelete(array[num], this.entries[this.entryForDisplayLine[num12]].baseEntry, GO))
				{
					this.UpdateEntries(array[num], GO);
				}
				if ((keys == Keys.Oemplus || keys == Keys.Add || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdInsert")) && JournalScreen.HandleInsert(array[num], GO))
				{
					this.UpdateEntries(array[num], GO);
					int num13 = Math.Max(0, this.displayLines.Count - num5);
					this.currentTopLine = num13 + 100;
				}
				if (keys == Keys.Space || keys == Keys.Enter)
				{
					if ((array[num] == JournalScreen.STR_SULTANS || array[num] == JournalScreen.STR_VILLAGES || array[num] == JournalScreen.STR_LOCATIONS) && this.selectedCategory == null)
					{
						if (this.categories.Count > num12)
						{
							this.Memorize(num, false);
							this.selectedCategory = this.categories[num12];
							this.Recall(num, false);
							this.UpdateEntries(array[num], GO);
						}
					}
					else if (this.selectedCategory != null && array[num] == JournalScreen.STR_LOCATIONS)
					{
						(this.entries[this.entryForDisplayLine[num12]].baseEntry as JournalMapNote).Tracked = !(this.entries[this.entryForDisplayLine[num12]].baseEntry as JournalMapNote).Tracked;
						this.UpdateEntries(array[num], GO);
					}
				}
				if (keys == Keys.NumPad8)
				{
					if (this.cursorPosition == 0)
					{
						if (this.currentTopLine > 0)
						{
							this.currentTopLine--;
						}
					}
					else
					{
						this.cursorPosition--;
					}
				}
				if (keys == Keys.NumPad2)
				{
					if (this.cursorPosition >= num4 - num3)
					{
						this.currentTopLine++;
					}
					else
					{
						this.cursorPosition++;
					}
				}
				if (keys == Keys.Next)
				{
					this.currentTopLine += num5;
					this.cursorPosition += num5;
				}
				else if (keys == Keys.Prior)
				{
					this.currentTopLine -= num5;
					this.cursorPosition -= num5;
				}
				else if (keys == Keys.Home)
				{
					this.currentTopLine = (this.cursorPosition = 0);
				}
				else if (keys == Keys.End)
				{
					this.currentTopLine = (this.cursorPosition = int.MaxValue);
				}
				int num14 = Math.Max(0, this.displayLines.Count - num5);
				if (this.currentTopLine < 0)
				{
					this.currentTopLine = 0;
				}
				if (this.currentTopLine > num14)
				{
					this.currentTopLine = num14;
				}
				if (this.cursorPosition >= num7)
				{
					this.cursorPosition = num7 - 1;
				}
				if (this.cursorPosition < 0)
				{
					this.cursorPosition = 0;
				}
				if (this.cursorPosition + this.currentTopLine > this.displayLines.Count)
				{
					this.cursorPosition = this.displayLines.Count - this.currentTopLine;
				}
				if (keys == Keys.NumPad4)
				{
					this.Memorize(num, true);
					num--;
					if (num < 0)
					{
						num = array.Length - 1;
					}
					this.selectedCategory = null;
					this.Recall(num, true);
					this.UpdateEntries(array[num], GO);
				}
				else if (keys == Keys.NumPad6)
				{
					this.Memorize(num, true);
					num++;
					if (num >= array.Length)
					{
						num = 0;
					}
					this.selectedCategory = null;
					this.Recall(num, true);
					this.UpdateEntries(array[num], GO);
				}
			}
			JournalScreen.TabMemory.Stock = null;
			GameManager.Instance.PopGameView(false);
			if (keys == Keys.NumPad7 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Page Left"))
			{
				return ScreenReturn.Previous;
			}
			if (keys == Keys.NumPad9 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Page Right"))
			{
				return ScreenReturn.Next;
			}
			if (GO.OnWorldMap())
			{
				GO.CurrentZone.Activated();
			}
			return ScreenReturn.Exit;
		}

		// Token: 0x040050B4 RID: 20660
		public static readonly string STR_LOCATIONS = "Locations";

		// Token: 0x040050B5 RID: 20661
		public static readonly string STR_CHRONOLOGY = "Chronology";

		// Token: 0x040050B6 RID: 20662
		public static readonly string STR_OBSERVATIONS = "Gossip and Lore";

		// Token: 0x040050B7 RID: 20663
		public static readonly string STR_SULTANS = "Sultan Histories";

		// Token: 0x040050B8 RID: 20664
		public static readonly string STR_VILLAGES = "Village Histories";

		// Token: 0x040050B9 RID: 20665
		public static readonly string STR_GENERAL = "General Notes";

		// Token: 0x040050BA RID: 20666
		public static readonly string STR_RECIPES = "Recipes";

		// Token: 0x040050BB RID: 20667
		private static string SultanTabName = JournalScreen.STR_SULTANS;

		// Token: 0x040050BC RID: 20668
		private static string SultanTerm = "sultan";

		// Token: 0x040050BD RID: 20669
		private List<string> categories = new List<string>();

		// Token: 0x040050BE RID: 20670
		private List<JournalScreen.JournalEntry> entries = new List<JournalScreen.JournalEntry>();

		// Token: 0x040050BF RID: 20671
		private List<string> displayLines = new List<string>();

		// Token: 0x040050C0 RID: 20672
		private List<int> entryForDisplayLine = new List<int>();

		// Token: 0x040050C1 RID: 20673
		private int cursorPosition;

		// Token: 0x040050C2 RID: 20674
		private int currentTopLine;

		// Token: 0x040050C3 RID: 20675
		public string selectedCategory;

		// Token: 0x040050C4 RID: 20676
		private static int LastHash;

		// Token: 0x040050C5 RID: 20677
		private static List<IBaseJournalEntry> LastRaw = new List<IBaseJournalEntry>();

		// Token: 0x02001DAD RID: 7597
		[Serializable]
		public class JournalEntry
		{
			// Token: 0x040073A3 RID: 29603
			public IBaseJournalEntry baseEntry;

			// Token: 0x040073A4 RID: 29604
			public int entryAPIPosition;

			// Token: 0x040073A5 RID: 29605
			public string entry;

			// Token: 0x040073A6 RID: 29606
			public int topLine;

			// Token: 0x040073A7 RID: 29607
			public int lines;
		}

		// Token: 0x02001DAE RID: 7598
		private class CursorMemory
		{
			// Token: 0x040073A8 RID: 29608
			public int Position;

			// Token: 0x040073A9 RID: 29609
			public int Cursor;
		}

		// Token: 0x02001DAF RID: 7599
		private class TabMemory : JournalScreen.CursorMemory
		{
			// Token: 0x0600F4F4 RID: 62708 RVA: 0x005544A4 File Offset: 0x005526A4
			public void Memorize(string Category, int Position, int Cursor)
			{
				JournalScreen.CursorMemory cursorMemory = this;
				if (Category != null && !this.Categories.TryGetValue(Category, out cursorMemory))
				{
					cursorMemory = (this.Categories[Category] = new JournalScreen.CursorMemory());
				}
				cursorMemory.Position = Position;
				cursorMemory.Cursor = Cursor;
			}

			// Token: 0x0600F4F5 RID: 62709 RVA: 0x005544E8 File Offset: 0x005526E8
			public void Recall(string Category, out int Position, out int Cursor)
			{
				if (Category == null)
				{
					Position = this.Position;
					Cursor = this.Cursor;
					return;
				}
				JournalScreen.CursorMemory cursorMemory;
				if (this.Categories.TryGetValue(Category, out cursorMemory))
				{
					Position = cursorMemory.Position;
					Cursor = cursorMemory.Cursor;
					return;
				}
				Position = 0;
				Cursor = 0;
			}

			// Token: 0x040073AA RID: 29610
			public static JournalScreen.TabMemory[] Stock;

			// Token: 0x040073AB RID: 29611
			public string Selected;

			// Token: 0x040073AC RID: 29612
			public Dictionary<string, JournalScreen.CursorMemory> Categories = new Dictionary<string, JournalScreen.CursorMemory>();
		}
	}
}
