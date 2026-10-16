using System;
using System.Collections.Generic;
using System.Linq;
using XRL.UI.Framework;
using XRL.World;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x0200059D RID: 1437
	public class QudChooseStartingLocationModule : QudEmbarkBuilderModule<QudChooseStartingLocationModuleData>
	{
		// Token: 0x06003D73 RID: 15731 RVA: 0x0012D181 File Offset: 0x0012B381
		public override bool shouldBeEnabled()
		{
			EmbarkBuilder builder = this.builder;
			object obj;
			if (builder == null)
			{
				obj = null;
			}
			else
			{
				QudSubtypeModule module = builder.GetModule<QudSubtypeModule>();
				if (module == null)
				{
					obj = null;
				}
				else
				{
					QudSubtypeModuleData data = module.data;
					obj = ((data != null) ? data.Subtype : null);
				}
			}
			return obj != null;
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x0012D1B0 File Offset: 0x0012B3B0
		public override bool shouldBeEditable()
		{
			EmbarkBuilder builder = this.builder;
			string text;
			if (builder == null)
			{
				text = null;
			}
			else
			{
				QudChartypeModule module = builder.GetModule<QudChartypeModule>();
				if (module == null)
				{
					text = null;
				}
				else
				{
					QudChartypeModuleData data = module.data;
					text = ((data != null) ? data.type : null);
				}
			}
			return text != "Daily";
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x0012D1E6 File Offset: 0x0012B3E6
		public override void InitFromSeed(string Seed)
		{
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x0012D1E8 File Offset: 0x0012B3E8
		public override void bootGame(XRLGame Game, EmbarkInfo Info)
		{
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06003D77 RID: 15735 RVA: 0x0012D1EA File Offset: 0x0012B3EA
		public override Dictionary<string, Action<XmlDataHelper>> XmlNodes
		{
			get
			{
				Dictionary<string, Action<XmlDataHelper>> xmlNodes = base.XmlNodes;
				xmlNodes.Add("locations", delegate(XmlDataHelper xml)
				{
					xml.HandleNodes(this.XmlNodeHandlers, null);
				});
				return xmlNodes;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06003D78 RID: 15736 RVA: 0x0012D20C File Offset: 0x0012B40C
		public Dictionary<string, Action<XmlDataHelper>> XmlNodeHandlers
		{
			get
			{
				return new Dictionary<string, Action<XmlDataHelper>>
				{
					{
						"location",
						delegate(XmlDataHelper xml)
						{
							string attribute = xml.GetAttribute("ID");
							if (!this.startingLocations.TryGetValue(attribute, out this.currentReadingStartingLocationData))
							{
								this.currentReadingStartingLocationData = new StartingLocationData
								{
									Id = attribute
								};
								this.startingLocations.Add(attribute, this.currentReadingStartingLocationData);
							}
							this.currentReadingStartingLocationData.Name = xml.GetAttributeString("Name", this.currentReadingStartingLocationData.Name, false);
							this.currentReadingStartingLocationData.Location = xml.GetAttributeString("Location", this.currentReadingStartingLocationData.Location, false);
							this.currentReadingStartingLocationData.Set = xml.GetAttributeString("Set", this.currentReadingStartingLocationData.Set, false);
							this.currentReadingStartingLocationData.ExcludeFromDaily = xml.GetAttributeString("ExcludeFromDaily", this.currentReadingStartingLocationData.ExcludeFromDaily, false);
							xml.HandleNodes(this.XmlNodeHandlers, null);
							this.currentReadingStartingLocationData = null;
						}
					},
					{
						"description",
						delegate(XmlDataHelper xml)
						{
							this.currentReadingStartingLocationData.Description = xml.GetTextNode();
						}
					},
					{
						"stringgamestate",
						delegate(XmlDataHelper xml)
						{
							this.currentReadingStartingLocationData.stringGameStates[xml.GetAttribute("Name")] = xml.GetAttribute("Value");
							xml.DoneWithElement();
						}
					},
					{
						"item",
						delegate(XmlDataHelper xml)
						{
							this.currentReadingStartingLocationData.items.Add(new StartingLocationItem
							{
								Blueprint = xml.GetAttribute("Blueprint"),
								Number = ((xml.GetAttribute("Number") == null) ? 1 : int.Parse(xml.GetAttribute("Number")))
							});
							xml.DoneWithElement();
						}
					},
					{
						"skill",
						delegate(XmlDataHelper xml)
						{
							this.currentReadingStartingLocationData.skills.Add(new StartingLocationSkill
							{
								Class = xml.GetAttribute("Class")
							});
							xml.DoneWithElement();
						}
					},
					{
						"reputation",
						delegate(XmlDataHelper xml)
						{
							this.currentReadingStartingLocationData.reputations.Add(new StartingLocationReputation
							{
								Faction = xml.GetAttribute("Faction"),
								Modifier = ((xml.GetAttribute("Modifier") == null) ? 0 : int.Parse(xml.GetAttribute("Modifier")))
							});
							xml.DoneWithElement();
						}
					},
					{
						"grid",
						delegate(XmlDataHelper xml)
						{
							this.currentReadingStartingLocationData.grid[xml.GetAttribute("Position")] = new StartingLocationGridElement
							{
								Tile = xml.GetAttribute("Tile"),
								Background = xml.GetAttribute("Background"),
								Detail = xml.GetAttribute("Detail"),
								Foreground = xml.GetAttribute("Foreground")
							};
							xml.DoneWithElement();
						}
					}
				};
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06003D79 RID: 15737 RVA: 0x0012D2BF File Offset: 0x0012B4BF
		public StartingLocationData startingLocation
		{
			get
			{
				Dictionary<string, StartingLocationData> dictionary = this.startingLocations;
				if (dictionary == null)
				{
					return null;
				}
				return dictionary.Values.Where<StartingLocationData>(delegate(StartingLocationData s)
				{
					string id = s.Id;
					QudChooseStartingLocationModuleData data = base.data;
					return id == ((data != null) ? data.StartingLocation : null);
				}).FirstOrDefault<StartingLocationData>();
			}
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x0012D2E8 File Offset: 0x0012B4E8
		public override object handleBootEvent(string id, XRLGame game, EmbarkInfo info, object element)
		{
			if (id == QudGameBootModule.BOOTEVENT_BOOTPLAYEROBJECT)
			{
				GameObject gameObject = element as GameObject;
				foreach (StartingLocationSkill startingLocationSkill in this.startingLocation.skills)
				{
					gameObject.AddSkill(startingLocationSkill.Class, null, null);
				}
				foreach (StartingLocationItem startingLocationItem in this.startingLocation.items)
				{
					AbstractQudEmbarkBuilderModule.AddItem(startingLocationItem.Blueprint, startingLocationItem.Number, gameObject);
				}
				foreach (StartingLocationReputation startingLocationReputation in this.startingLocation.reputations)
				{
					The.Game.PlayerReputation.Modify(startingLocationReputation.Faction, startingLocationReputation.Modifier, "StartingLocation", null, null, true, false);
				}
			}
			if (id == QudGameBootModule.BOOTEVENT_BEFOREINITIALIZEHISTORY)
			{
				string text = "embark";
				QudSubtypeModuleData data = info.getData<QudSubtypeModuleData>();
				string text2;
				if (data == null)
				{
					text2 = null;
				}
				else
				{
					SubtypeEntry entry = data.Entry;
					text2 = ((entry != null) ? entry.StartingLocation : null);
				}
				string text3;
				if ((text3 = text2) == null)
				{
					QudGenotypeModuleData data2 = info.getData<QudGenotypeModuleData>();
					string text4;
					if (data2 == null)
					{
						text4 = null;
					}
					else
					{
						GenotypeEntry entry2 = data2.Entry;
						text4 = ((entry2 != null) ? entry2.StartingLocation : null);
					}
					text3 = text4 ?? base.data.StartingLocation;
				}
				game.SetStringGameState(text, text3);
				foreach (KeyValuePair<string, string> keyValuePair in this.startingLocation.stringGameStates)
				{
					game.SetStringGameState(keyValuePair.Key, keyValuePair.Value);
				}
			}
			if (!(id == QudGameBootModule.BOOTEVENT_BOOTSTARTINGLOCATION))
			{
				return base.handleBootEvent(id, game, info, element);
			}
			QudSubtypeModuleData data3 = info.getData<QudSubtypeModuleData>();
			object obj;
			if (data3 == null)
			{
				obj = null;
			}
			else
			{
				SubtypeEntry entry3 = data3.Entry;
				obj = ((entry3 != null) ? entry3.StartingLocation : null);
			}
			object obj2;
			if ((obj2 = obj) == null)
			{
				QudGenotypeModuleData data4 = info.getData<QudGenotypeModuleData>();
				if (data4 == null)
				{
					obj2 = null;
				}
				else
				{
					GenotypeEntry entry4 = data4.Entry;
					obj2 = ((entry4 != null) ? entry4.StartingLocation : null);
				}
			}
			string text5 = obj2;
			if (!text5.IsNullOrEmpty())
			{
				return new GlobalLocation(text5);
			}
			text5 = this.startingLocation.Location;
			if (!text5.IsNullOrEmpty())
			{
				if (text5.StartsWith("GlobalLocation:"))
				{
					return new GlobalLocation(text5.Split(':', StringSplitOptions.None)[1]);
				}
				if (text5.StartsWith("StringGameState:"))
				{
					return new GlobalLocation(game.GetStringGameState(text5.Split(':', StringSplitOptions.None)[1], "JoppaWorld.11.22.1.1.10@37,22"));
				}
			}
			MetricsManager.LogError("unknown starting location specification:" + text5);
			throw new ArgumentException("Starting location was not properly defined for QudChooseStartingLocationModule.");
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x0012D5CC File Offset: 0x0012B7CC
		public string getSelected()
		{
			QudChooseStartingLocationModuleData data = base.data;
			if (data == null)
			{
				return null;
			}
			return data.StartingLocation;
		}

		// Token: 0x04001E7E RID: 7806
		private StartingLocationData currentReadingStartingLocationData;

		// Token: 0x04001E7F RID: 7807
		public Dictionary<string, StartingLocationData> startingLocations = new Dictionary<string, StartingLocationData>();
	}
}
