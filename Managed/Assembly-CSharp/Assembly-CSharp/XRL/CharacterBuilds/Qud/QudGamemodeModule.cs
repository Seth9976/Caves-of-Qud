using System;
using System.Collections.Generic;
using System.Linq;
using JoppaTutorial;
using XRL.CharacterBuilds.Qud.UI;
using XRL.Rules;
using XRL.UI;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x020005A5 RID: 1445
	public class QudGamemodeModule : QudEmbarkBuilderModule<QudGamemodeModuleData>
	{
		// Token: 0x06003DA8 RID: 15784 RVA: 0x0012E74F File Offset: 0x0012C94F
		public override bool IncludeInBuildCodes()
		{
			return false;
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x0012E752 File Offset: 0x0012C952
		public override string DataErrors()
		{
			QudGamemodeModuleData data = base.data;
			if (((data != null) ? data.Mode : null) == null)
			{
				return "No game mode selected.";
			}
			return null;
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x0012E770 File Offset: 0x0012C970
		public void SelectMode(string mode)
		{
			this.setData(new QudGamemodeModuleData
			{
				Mode = mode
			});
			if (mode == "Tutorial")
			{
				QudChartypeModuleData qudChartypeModuleData = new QudChartypeModuleData();
				TutorialManager.StartTutorial(new IntroTutorialStart());
				qudChartypeModuleData.type = "Pregen";
				this.builder.GetModule<QudChartypeModule>().setData(qudChartypeModuleData);
				this.builder.advance(false, true, false);
				return;
			}
			if (mode == "_Quickstart")
			{
				this.builder.info.GameSeed = Guid.NewGuid().ToString();
				QudGameBootModule.SeedGame(The.Game, this.builder.info);
				EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor = this.builder.activeWindow;
				EmbarkBuilder builder;
				do
				{
					this.builder.advance(false, false, false);
					if (this.builder.activeWindow == null || this.builder.activeWindow == embarkBuilderModuleWindowDescriptor)
					{
						return;
					}
					embarkBuilderModuleWindowDescriptor = this.builder.activeWindow;
					embarkBuilderModuleWindowDescriptor.window.DebugQuickstart(mode);
					builder = this.builder;
				}
				while (((builder != null) ? builder.activeWindow : null) != null);
				return;
			}
			if (mode == "Daily")
			{
				this.builder.info.GameSeed = DateTime.Now.ToLongDateString();
				Stat.ReseedFrom(this.builder.info.GameSeed, false);
				EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor2 = this.builder.activeWindow;
				string text;
				do
				{
					this.builder.advance(false, false, false);
					if (this.builder.activeWindow == null || this.builder.activeWindow == embarkBuilderModuleWindowDescriptor2)
					{
						break;
					}
					embarkBuilderModuleWindowDescriptor2 = this.builder.activeWindow;
					embarkBuilderModuleWindowDescriptor2.window.DailySelection();
					EmbarkBuilder builder2 = this.builder;
					if (builder2 == null)
					{
						text = null;
					}
					else
					{
						EmbarkBuilderModuleWindowDescriptor activeWindow = builder2.activeWindow;
						text = ((activeWindow != null) ? activeWindow.viewID : null);
					}
				}
				while (text != "Chargen/BuildSummary");
				List<KeyValuePair<string, StartingLocationData>> list = this.builder.GetModule<QudChooseStartingLocationModule>().startingLocations.Where<KeyValuePair<string, StartingLocationData>>(delegate(KeyValuePair<string, StartingLocationData> kv)
				{
					string excludeFromDaily = kv.Value.ExcludeFromDaily;
					return ((excludeFromDaily != null) ? excludeFromDaily.CompareExceptFormattingAndCase("Yes") : 1) != 0;
				}).ToList<KeyValuePair<string, StartingLocationData>>();
				this.builder.GetModule<QudChooseStartingLocationModule>().setData(new QudChooseStartingLocationModuleData(list.GetRandomElement<KeyValuePair<string, StartingLocationData>>(null).Key));
				return;
			}
			this.builder.advance(false, false, false);
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x0012E9B7 File Offset: 0x0012CBB7
		public string GetMode()
		{
			QudGamemodeModuleData data = base.data;
			if (data == null)
			{
				return null;
			}
			return data.Mode;
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x0012E9CC File Offset: 0x0012CBCC
		public QudGamemodeModule.GameModeDescriptor GetModeDescriptor()
		{
			QudGamemodeModule.GameModeDescriptor gameModeDescriptor = null;
			this.GameModes.TryGetValue(this.GetMode() ?? "", out gameModeDescriptor);
			return gameModeDescriptor;
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x0012E9FC File Offset: 0x0012CBFC
		public override object handleUIEvent(string id, object element)
		{
			if (id == QudChooseStartingLocationModuleWindow.EventNames.EID_GET_STARTING_LOCATION_SET)
			{
				QudGamemodeModuleData data = base.data;
				if (((data != null) ? data.Mode : null) == "Tutorial")
				{
					return "Tutorial";
				}
				return null;
			}
			else
			{
				if (!(id == EmbarkBuilder.EventNames.EditableGameModeQuery))
				{
					return base.handleUIEvent(id, element);
				}
				QudGamemodeModule.GameModeDescriptor modeDescriptor = this.GetModeDescriptor();
				bool? flag = ((modeDescriptor != null) ? new bool?(modeDescriptor.Editable) : null);
				if (flag == null)
				{
					return element;
				}
				return flag.GetValueOrDefault();
			}
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x0012EA89 File Offset: 0x0012CC89
		public void Back()
		{
			this.builder.back();
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06003DAF RID: 15791 RVA: 0x0012EA96 File Offset: 0x0012CC96
		public override Dictionary<string, Action<XmlDataHelper>> XmlNodes
		{
			get
			{
				Dictionary<string, Action<XmlDataHelper>> xmlNodes = base.XmlNodes;
				xmlNodes.Add("modes", new Action<XmlDataHelper>(this.HandleModesNode));
				return xmlNodes;
			}
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x0012EAB8 File Offset: 0x0012CCB8
		public override void Init()
		{
			base.Init();
			if (Options.ShowQuickstartOption)
			{
				if (!this.GameModes.Any<KeyValuePair<string, QudGamemodeModule.GameModeDescriptor>>((KeyValuePair<string, QudGamemodeModule.GameModeDescriptor> m) => m.Key == "_Quickstart"))
				{
					this.GameModes.Add("_Quickstart", new QudGamemodeModule.GameModeDescriptor
					{
						Description = "Start a new game with one button.",
						IconDetail = "R",
						IconForeground = "r",
						IconTile = "Abilities/abil_sprint_toggle_off.bmp",
						ID = "_Quickstart",
						Title = "Quickstart"
					});
				}
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06003DB1 RID: 15793 RVA: 0x0012EB58 File Offset: 0x0012CD58
		public Dictionary<string, Action<XmlDataHelper>> XmlModesNodes
		{
			get
			{
				return new Dictionary<string, Action<XmlDataHelper>>
				{
					{
						"mode",
						new Action<XmlDataHelper>(this.HandleModeNode)
					},
					{
						"icon",
						new Action<XmlDataHelper>(this.HandleModeIconNode)
					},
					{
						"description",
						new Action<XmlDataHelper>(this.HandleModeDescriptionNode)
					},
					{
						"stringgamestate",
						new Action<XmlDataHelper>(this.HandleStringGameStateNode)
					},
					{
						"boolgamestate",
						new Action<XmlDataHelper>(this.HandleBoolGameStateNode)
					},
					{
						"intgamestate",
						new Action<XmlDataHelper>(this.HandleIntGameStateNode)
					},
					{
						"int64gamestate",
						new Action<XmlDataHelper>(this.HandleInt64GameStateNode)
					},
					{
						"gamesystem",
						new Action<XmlDataHelper>(this.HandleGameSystemNode)
					}
				};
			}
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x0012EC22 File Offset: 0x0012CE22
		public void HandleModesNode(XmlDataHelper xml)
		{
			xml.HandleNodes(this.XmlModesNodes, null);
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x0012EC34 File Offset: 0x0012CE34
		protected void HandleModeNode(XmlDataHelper xml)
		{
			string attribute = xml.GetAttribute("ID");
			if (!this.GameModes.TryGetValue(attribute, out this.CurrentReadingGameModeDescriptor))
			{
				this.CurrentReadingGameModeDescriptor = new QudGamemodeModule.GameModeDescriptor
				{
					ID = attribute
				};
				this.GameModes.Add(attribute, this.CurrentReadingGameModeDescriptor);
			}
			this.CurrentReadingGameModeDescriptor.Title = xml.GetAttributeString("Title", this.CurrentReadingGameModeDescriptor.Title, false);
			this.CurrentReadingGameModeDescriptor.Editable = xml.GetAttributeBool("Editable", this.CurrentReadingGameModeDescriptor.Editable, false);
			xml.HandleNodes(this.XmlModesNodes, null);
			this.CurrentReadingGameModeDescriptor = null;
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x0012ECE0 File Offset: 0x0012CEE0
		protected void HandleModeIconNode(XmlDataHelper xml)
		{
			this.CurrentReadingGameModeDescriptor.IconTile = xml.GetAttributeString("Tile", this.CurrentReadingGameModeDescriptor.IconTile, false);
			this.CurrentReadingGameModeDescriptor.IconDetail = xml.GetAttributeString("Detail", this.CurrentReadingGameModeDescriptor.IconDetail, false);
			this.CurrentReadingGameModeDescriptor.IconForeground = xml.GetAttributeString("Foreground", this.CurrentReadingGameModeDescriptor.IconForeground, false);
			xml.DoneWithElement();
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x0012ED59 File Offset: 0x0012CF59
		protected void HandleModeDescriptionNode(XmlDataHelper xml)
		{
			this.CurrentReadingGameModeDescriptor.Description = xml.GetTextNode();
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x0012ED6C File Offset: 0x0012CF6C
		protected void HandleGameSystemNode(XmlDataHelper xml)
		{
			this.CurrentReadingGameModeDescriptor.gameSystems.Add(xml.GetAttribute("Class"));
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x0012ED8C File Offset: 0x0012CF8C
		protected void HandleStringGameStateNode(XmlDataHelper xml)
		{
			string attribute = xml.GetAttribute("Name");
			string attribute2 = xml.GetAttribute("Value");
			if (attribute == null)
			{
				xml.ParseWarning("No Name provided for stringgamestate node.");
				xml.DoneWithElement();
				return;
			}
			if (attribute2 == null)
			{
				xml.ParseWarning("No Value provided for stringgamestate node Name=" + attribute + ".");
				xml.DoneWithElement();
				return;
			}
			this.CurrentReadingGameModeDescriptor.stringGameStates[attribute] = attribute2;
			xml.DoneWithElement();
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x0012EE00 File Offset: 0x0012D000
		protected void HandleBoolGameStateNode(XmlDataHelper xml)
		{
			string attribute = xml.GetAttribute("Name");
			string attribute2 = xml.GetAttribute("Value");
			if (attribute == null)
			{
				xml.ParseWarning("No Name provided for boolgamestate node.");
				xml.DoneWithElement();
				return;
			}
			if (attribute2 == null)
			{
				xml.ParseWarning("No Value provided for boolgamestate node Name=" + attribute + ".");
				xml.DoneWithElement();
				return;
			}
			this.CurrentReadingGameModeDescriptor.boolGameStates[attribute] = attribute2.EqualsNoCase("TRUE");
			xml.DoneWithElement();
		}

		// Token: 0x06003DB9 RID: 15801 RVA: 0x0012EE7C File Offset: 0x0012D07C
		protected void HandleIntGameStateNode(XmlDataHelper xml)
		{
			string attribute = xml.GetAttribute("Name");
			string attribute2 = xml.GetAttribute("Value");
			if (attribute == null)
			{
				xml.ParseWarning("No Name provided for intgamestate node.");
				xml.DoneWithElement();
				return;
			}
			if (attribute2 == null)
			{
				xml.ParseWarning("No Value provided for intgamestate node Name=" + attribute + ".");
				xml.DoneWithElement();
				return;
			}
			int num;
			if (!int.TryParse(attribute2, out num))
			{
				xml.ParseWarning(string.Concat(new string[] { "Failed parsing '", attribute2, "' provided for Int32GameState '", attribute, "'." }));
				xml.DoneWithElement();
				return;
			}
			this.CurrentReadingGameModeDescriptor.intGameStates[attribute] = num;
			xml.DoneWithElement();
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x0012EF30 File Offset: 0x0012D130
		protected void HandleInt64GameStateNode(XmlDataHelper xml)
		{
			string attribute = xml.GetAttribute("Name");
			string attribute2 = xml.GetAttribute("Value");
			if (attribute == null)
			{
				xml.ParseWarning("No Name provided for int64gamestate node.");
				xml.DoneWithElement();
				return;
			}
			if (attribute2 == null)
			{
				xml.ParseWarning("No Value provided for int64gamestate node Name=" + attribute + ".");
				xml.DoneWithElement();
				return;
			}
			long num;
			if (!long.TryParse(attribute2, out num))
			{
				xml.ParseWarning(string.Concat(new string[] { "Failed parsing '", attribute2, "' provided for Int64GameState '", attribute, "'." }));
				xml.DoneWithElement();
				return;
			}
			this.CurrentReadingGameModeDescriptor.int64GameStates[attribute] = num;
			xml.DoneWithElement();
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x0012EFE4 File Offset: 0x0012D1E4
		public override void InitFromSeed(string seed)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x0012EFEC File Offset: 0x0012D1EC
		public override void bootGame(XRLGame game, EmbarkInfo info)
		{
			game.gameMode = info.getData<QudGamemodeModuleData>().Mode;
			foreach (KeyValuePair<string, string> keyValuePair in this.selectedMode.stringGameStates)
			{
				game.SetStringGameState(keyValuePair.Key, keyValuePair.Value);
			}
			foreach (KeyValuePair<string, bool> keyValuePair2 in this.selectedMode.boolGameStates)
			{
				game.SetBooleanGameState(keyValuePair2.Key, keyValuePair2.Value);
			}
			foreach (KeyValuePair<string, int> keyValuePair3 in this.selectedMode.intGameStates)
			{
				game.SetIntGameState(keyValuePair3.Key, keyValuePair3.Value);
			}
			foreach (KeyValuePair<string, long> keyValuePair4 in this.selectedMode.int64GameStates)
			{
				game.SetInt64GameState(keyValuePair4.Key, keyValuePair4.Value);
			}
			foreach (string text in this.selectedMode.gameSystems)
			{
				game.AddSystem(text);
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06003DBD RID: 15805 RVA: 0x0012F1B0 File Offset: 0x0012D3B0
		public QudGamemodeModule.GameModeDescriptor selectedMode
		{
			get
			{
				return this.GameModes[base.data.Mode];
			}
		}

		// Token: 0x04001EA9 RID: 7849
		public Dictionary<string, QudGamemodeModule.GameModeDescriptor> GameModes = new Dictionary<string, QudGamemodeModule.GameModeDescriptor>();

		// Token: 0x04001EAA RID: 7850
		protected QudGamemodeModule.GameModeDescriptor CurrentReadingGameModeDescriptor;

		// Token: 0x02001998 RID: 6552
		public class GameModeDescriptor
		{
			// Token: 0x04006658 RID: 26200
			public string ID;

			// Token: 0x04006659 RID: 26201
			public string Title;

			// Token: 0x0400665A RID: 26202
			public string IconTile;

			// Token: 0x0400665B RID: 26203
			public string IconForeground = "y";

			// Token: 0x0400665C RID: 26204
			public string IconDetail = "W";

			// Token: 0x0400665D RID: 26205
			public string Description;

			// Token: 0x0400665E RID: 26206
			public bool Editable = true;

			// Token: 0x0400665F RID: 26207
			public Dictionary<string, string> stringGameStates = new Dictionary<string, string>();

			// Token: 0x04006660 RID: 26208
			public Dictionary<string, int> intGameStates = new Dictionary<string, int>();

			// Token: 0x04006661 RID: 26209
			public Dictionary<string, long> int64GameStates = new Dictionary<string, long>();

			// Token: 0x04006662 RID: 26210
			public Dictionary<string, bool> boolGameStates = new Dictionary<string, bool>();

			// Token: 0x04006663 RID: 26211
			public List<string> gameSystems = new List<string>();
		}
	}
}
