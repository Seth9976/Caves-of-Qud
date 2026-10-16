using System;
using System.Collections.Generic;
using Qud.API;
using XRL;
using XRL.Core;
using XRL.Language;
using XRL.UI;
using XRL.World;
using XRL.World.Parts;

namespace HistoryKit
{
	// Token: 0x0200036D RID: 877
	[Serializable]
	public class HistoricEvent
	{
		// Token: 0x060020A4 RID: 8356 RVA: 0x000A0424 File Offset: 0x0009E624
		public static HistoricEvent Load(SerializationReader reader)
		{
			HistoricEvent historicEvent = new HistoricEvent();
			historicEvent.id = reader.ReadInt64();
			historicEvent.year = reader.ReadInt64();
			historicEvent.duration = reader.ReadInt64();
			if (reader.ReadBoolean())
			{
				historicEvent.eventProperties = reader.ReadDictionary<string, string>();
			}
			if (reader.ReadBoolean())
			{
				historicEvent.entityProperties = reader.ReadDictionary<string, string>();
			}
			if (reader.ReadBoolean())
			{
				int num = reader.ReadInt32();
				historicEvent.addedListProperties = new Dictionary<string, List<string>>(num);
				for (int i = 0; i < num; i++)
				{
					string text = reader.ReadString();
					List<string> list = reader.ReadStringList();
					historicEvent.addedListProperties.Add(text, list);
				}
			}
			if (reader.ReadBoolean())
			{
				int num2 = reader.ReadInt32();
				historicEvent.removedListProperties = new Dictionary<string, List<string>>(num2);
				for (int j = 0; j < num2; j++)
				{
					string text2 = reader.ReadString();
					List<string> list2 = reader.ReadStringList();
					historicEvent.removedListProperties.Add(text2, list2);
				}
			}
			int num3 = reader.ReadInt32();
			if (num3 > 0)
			{
				historicEvent.perspectives = new Dictionary<string, HistoricPerspective>(num3);
				for (int k = 0; k < num3; k++)
				{
					HistoricPerspective historicPerspective = HistoricPerspective.Load(reader);
					if (historicPerspective.eventId != historicEvent.id)
					{
						throw new Exception("eventId mismatch: " + historicEvent.id.ToString() + " vs. " + historicPerspective.eventId.ToString());
					}
					historicEvent.perspectives.Add(historicPerspective.entityId, historicPerspective);
				}
			}
			return historicEvent;
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000A0598 File Offset: 0x0009E798
		public virtual void Save(SerializationWriter writer)
		{
			writer.Write(this.id);
			writer.Write(this.year);
			writer.Write(this.duration);
			writer.Write(this.eventProperties != null);
			if (this.eventProperties != null)
			{
				writer.Write<string, string>(this.eventProperties);
			}
			writer.Write(this.entityProperties != null);
			if (this.entityProperties != null)
			{
				writer.Write<string, string>(this.entityProperties);
			}
			writer.Write(this.addedListProperties != null);
			if (this.addedListProperties != null)
			{
				writer.Write(this.addedListProperties.Count);
				foreach (KeyValuePair<string, List<string>> keyValuePair in this.addedListProperties)
				{
					writer.Write(keyValuePair.Key);
					writer.Write(keyValuePair.Value);
				}
			}
			writer.Write(this.removedListProperties != null);
			if (this.removedListProperties != null)
			{
				writer.Write(this.removedListProperties.Count);
				foreach (KeyValuePair<string, List<string>> keyValuePair2 in this.removedListProperties)
				{
					writer.Write(keyValuePair2.Key);
					writer.Write(keyValuePair2.Value);
				}
			}
			if (this.perspectives == null)
			{
				writer.Write(0);
				return;
			}
			writer.Write(this.perspectives.Count);
			foreach (string text in this.perspectives.Keys)
			{
				this.perspectives[text].Save(writer);
			}
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x000A0784 File Offset: 0x0009E984
		public void AddListProperty(string Name, List<string> Value)
		{
			if (this.addedListProperties == null)
			{
				this.addedListProperties = new Dictionary<string, List<string>>();
			}
			List<string> list;
			if (this.addedListProperties.TryGetValue(Name, out list))
			{
				list.AddRange(Value);
				return;
			}
			this.addedListProperties[Name] = Value;
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x000A07CC File Offset: 0x0009E9CC
		public void ChangeListProperty(string Name, List<string> Old, List<string> New)
		{
			if (this.addedListProperties == null)
			{
				this.addedListProperties = new Dictionary<string, List<string>>();
			}
			if (this.removedListProperties == null)
			{
				this.removedListProperties = new Dictionary<string, List<string>>();
			}
			this.addedListProperties[Name] = New;
			this.removedListProperties[Name] = Old;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x000A0819 File Offset: 0x0009EA19
		[Obsolete("Use HasEventProperty")]
		public bool hasEventProperty(string key)
		{
			return this.HasEventProperty(key);
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x000A0822 File Offset: 0x0009EA22
		public bool HasEventProperty(string Key)
		{
			return this.eventProperties != null && this.eventProperties.ContainsKey(Key);
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x000A083A File Offset: 0x0009EA3A
		[Obsolete("Use SetEventProperty")]
		public void setEventProperty(string key, string value)
		{
			this.SetEventProperty(key, value);
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x000A0844 File Offset: 0x0009EA44
		public void SetEventProperty(string Key, string Value)
		{
			if (this.eventProperties == null)
			{
				this.eventProperties = new Dictionary<string, string>();
			}
			Value = this.ExpandString(Value);
			this.eventProperties[Key] = Value;
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x000A086F File Offset: 0x0009EA6F
		[Obsolete("Use GetEventProperty")]
		public string getEventProperty(string name, string defaultValue = null)
		{
			return this.GetEventProperty(name, defaultValue);
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x000A087C File Offset: 0x0009EA7C
		public string GetEventProperty(string Key, string Default = null)
		{
			string text;
			if (this.eventProperties != null && this.eventProperties.TryGetValue(Key, out text))
			{
				return text;
			}
			return Default;
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x000A08A4 File Offset: 0x0009EAA4
		public string ExpandString(string s)
		{
			return HistoricStringExpander.ExpandString(s, this.entity.GetSnapshotAtYear(this.year + this.duration), this.history, null, null);
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x000A08CC File Offset: 0x0009EACC
		public string ExpandString(string s, Dictionary<string, string> vars)
		{
			return HistoricStringExpander.ExpandString(s, this.entity.GetSnapshotAtYear(this.year + this.duration), this.history, vars, null);
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000A08F4 File Offset: 0x0009EAF4
		public int Random(int low, int high)
		{
			return this.history.Random(low, high);
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x000A0903 File Offset: 0x0009EB03
		public virtual void SetEntityHistory(HistoricEntity Entity, History History)
		{
			this.entity = Entity;
			this.history = History;
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x000A0913 File Offset: 0x0009EB13
		public virtual void Generate()
		{
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x000A0918 File Offset: 0x0009EB18
		public void AddEntityListItem(string List, string Value, bool Force = false)
		{
			if (this.addedListProperties == null)
			{
				this.addedListProperties = new Dictionary<string, List<string>>();
			}
			Value = this.ExpandString(Value);
			if (!this.addedListProperties.ContainsKey(List))
			{
				this.addedListProperties.Add(List, new List<string>());
			}
			if (Force || !this.addedListProperties[List].Contains(Value))
			{
				this.addedListProperties[List].Add(Value);
			}
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000A098C File Offset: 0x0009EB8C
		public void RemoveEntityListItem(string List, string Value)
		{
			if (this.removedListProperties == null)
			{
				this.removedListProperties = new Dictionary<string, List<string>>();
			}
			Value = this.ExpandString(Value);
			if (!this.removedListProperties.ContainsKey(List))
			{
				this.removedListProperties.Add(List, new List<string>());
			}
			this.removedListProperties[List].Add(Value);
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000A09E8 File Offset: 0x0009EBE8
		public List<string> GetListProperties(string Key)
		{
			List<string> list;
			if (this.addedListProperties != null && this.addedListProperties.TryGetValue(Key, out list))
			{
				return list;
			}
			return null;
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x000A0A10 File Offset: 0x0009EC10
		public bool HasEntityProperty(string Key)
		{
			return this.entityProperties != null && this.entityProperties.ContainsKey(Key);
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x000A0A28 File Offset: 0x0009EC28
		public void SetEntityProperty(string Key, string Value)
		{
			if (this.entityProperties == null)
			{
				this.entityProperties = new Dictionary<string, string>();
			}
			Value = this.ExpandString(Value);
			this.entityProperties[Key] = Value;
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x000A0A54 File Offset: 0x0009EC54
		public string GetEntityProperty(string Key, string Default = null)
		{
			string text;
			if (this.entityProperties != null && this.entityProperties.TryGetValue(Key, out text))
			{
				return text;
			}
			return Default;
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x000A0A7C File Offset: 0x0009EC7C
		public virtual void ApplyToSnapshot(HistoricEntitySnapshot Snapshot)
		{
			if (this.entityProperties != null)
			{
				foreach (string text in this.entityProperties.Keys)
				{
					Snapshot.properties[text] = this.entityProperties[text];
				}
			}
			if (this.removedListProperties != null)
			{
				foreach (string text2 in this.removedListProperties.Keys)
				{
					foreach (string text3 in this.removedListProperties[text2])
					{
						if (Snapshot.listProperties.ContainsKey(text2) && Snapshot.listProperties[text2].Contains(text3))
						{
							Snapshot.listProperties[text2].Remove(text3);
						}
					}
				}
			}
			if (this.addedListProperties != null)
			{
				foreach (string text4 in this.addedListProperties.Keys)
				{
					foreach (string text5 in this.addedListProperties[text4])
					{
						if (!Snapshot.listProperties.ContainsKey(text4))
						{
							Snapshot.listProperties.Add(text4, new List<string>());
						}
						Snapshot.listProperties[text4].Add(text5);
					}
				}
			}
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x000A0C78 File Offset: 0x0009EE78
		public virtual HistoricPerspective GetPerspective(HistoricEntity Entity)
		{
			if (this.perspectives == null)
			{
				return null;
			}
			if (!this.perspectives.ContainsKey(Entity.id))
			{
				return null;
			}
			return this.perspectives[Entity.id];
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x000A0CAA File Offset: 0x0009EEAA
		public virtual HistoricPerspective GetPerspective(HistoricEntitySnapshot Snapshot)
		{
			return this.GetPerspective(Snapshot.entity);
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x000A0CB8 File Offset: 0x0009EEB8
		public virtual HistoricPerspective RequirePerspective(HistoricEntitySnapshot Snapshot, object UseFeeling = null)
		{
			if (this.perspectives == null)
			{
				this.perspectives = new Dictionary<string, HistoricPerspective>(1);
			}
			if (this.perspectives.ContainsKey(Snapshot.entity.id))
			{
				return this.perspectives[Snapshot.entity.id];
			}
			HistoricPerspective historicPerspective = new HistoricPerspective();
			historicPerspective.eventId = this.id;
			historicPerspective.entityId = Snapshot.entity.id;
			Snapshot.supplyPerspectiveColors(historicPerspective);
			if (UseFeeling == null)
			{
				historicPerspective.randomizeFeeling();
			}
			else
			{
				historicPerspective.feeling = (int)UseFeeling;
			}
			this.perspectives[Snapshot.entity.id] = historicPerspective;
			return historicPerspective;
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x000A0D61 File Offset: 0x0009EF61
		public bool HasUnrevealedSecret()
		{
			return this.HasUnrevealedRegion() || this.HasUnrevealedRelicQuest() || JournalAPI.HasUnrevealedSultanEvent(this.id);
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x000A0D88 File Offset: 0x0009EF88
		public string GetUnrevealedSecretSource()
		{
			if (this.HasUnrevealedRegion())
			{
				return "region:" + this.GetEventProperty("revealsRegion", null);
			}
			if (this.HasUnrevealedRelicQuest())
			{
				return "relicquest:" + this.GetEventProperty("revealsItem", null);
			}
			if (JournalAPI.HasUnrevealedSultanEvent(this.id))
			{
				return "sultanevent:" + this.id.ToString();
			}
			return null;
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x000A0DF7 File Offset: 0x0009EFF7
		public JournalSultanNote Reveal(string LearnedFrom = null)
		{
			this.PerformRegionReveal();
			this.PerformRelicQuestReveal();
			return JournalAPI.RevealSultanEvent(this.id, LearnedFrom);
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x000A0E14 File Offset: 0x0009F014
		public bool HasUnrevealedRegion()
		{
			if (this.HasEventProperty("revealsRegion"))
			{
				string eventProperty = this.GetEventProperty("revealsRegion", null);
				if (eventProperty != null && eventProperty != "unknown" && !The.Game.HasIntGameState("sultanRegionReveal_" + eventProperty))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x000A0E68 File Offset: 0x0009F068
		public void PerformRegionReveal()
		{
			if (this.HasEventProperty("revealsRegion"))
			{
				string eventProperty = this.GetEventProperty("revealsRegion", null);
				if (eventProperty != null && eventProperty != "unknown" && !The.Game.HasIntGameState("sultanRegionReveal_" + eventProperty))
				{
					The.Game.SetIntGameState("sultanRegionReveal_" + eventProperty, 1);
					Vector2i vector2i = The.Game.GetObjectGameState("sultanRegionPosition_" + eventProperty) as Vector2i;
					if (vector2i != null)
					{
						Zone zone = XRLCore.Core.Game.ZoneManager.GetZone("JoppaWorld");
						Popup.Show("You discover the location of " + eventProperty + ".", null, "sfx_newLocation_discovered_important", true, true, true, true, null);
						zone.GetCell(vector2i.x, vector2i.y).FireEvent("SultanReveal");
						int zoneTier = The.ZoneManager.GetZoneTier(string.Concat(new string[]
						{
							"JoppaWorld.",
							vector2i.x.ToString(),
							".",
							vector2i.y.ToString(),
							".1.1.10"
						}));
						HistoricEvent.AddQuestsForRegion(eventProperty, zoneTier, null, null, null);
						JournalAPI.AddAccomplishment("You discovered the location of " + eventProperty + ".", "<spice.commonPhrases.intrepid.!random.capitalize> =name= discovered the location of " + eventProperty + ", once thought lost to the sands of time.", string.Concat(new string[]
						{
							"In =year=, =name= appointed a corrupt administrator as minister of ",
							eventProperty,
							". ",
							Grammar.InitCap(Grammar.RandomShePronoun()),
							" mandated the practice of <spice.elements.",
							The.Player.GetMythicDomain(5, null),
							".practices.!random> in ",
							The.Player.GetPronounProvider(false).PossessiveAdjective,
							" name."
						}), null, "general", MuralCategory.VisitsLocation, MuralWeight.Medium, null, -1L, true);
					}
				}
			}
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x000A1044 File Offset: 0x0009F244
		public static void AddQuestsForRegion(string regionName, int zoneTier, string QuestGiverName = null, string QuestGiverLocationName = null, string QuestGiverLocationZoneID = null)
		{
			if (The.Game.IsCoda)
			{
				return;
			}
			QuestStep questStep = new QuestStep();
			questStep.ID = Guid.NewGuid().ToString();
			questStep.Name = "Travel to the historical site of " + regionName;
			questStep.Text = "";
			questStep.XP = 250 * zoneTier;
			Quest quest = new Quest();
			quest.ID = "Visit " + regionName;
			quest.Name = "Visit " + regionName;
			quest.StepsByID = new Dictionary<string, QuestStep>();
			quest.StepsByID.Add(questStep.ID, questStep);
			quest.Level = 1;
			quest._Manager = new VisitSultanDungeonQuestManager();
			(quest._Manager as VisitSultanDungeonQuestManager).Region = regionName;
			The.Game.StartQuest(quest, QuestGiverName, QuestGiverLocationName, QuestGiverLocationZoneID);
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x000A1120 File Offset: 0x0009F320
		public bool HasUnrevealedRelicQuest()
		{
			if (this.HasEventProperty("revealsItem"))
			{
				string eventProperty = this.GetEventProperty("revealsItem", null);
				if (!The.Game.HasIntGameState("sultanItemReveal_" + eventProperty))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000A1164 File Offset: 0x0009F364
		public void PerformRelicQuestReveal()
		{
			if (The.Game.IsCoda)
			{
				return;
			}
			if (this.HasEventProperty("revealsItem"))
			{
				string eventProperty = this.GetEventProperty("revealsItem", null);
				if (!The.Game.HasIntGameState("sultanItemReveal_" + eventProperty))
				{
					The.Game.SetIntGameState("sultanItemReveal_" + eventProperty, 1);
					string eventProperty2 = this.GetEventProperty("revealsItemLocation", null);
					string eventProperty3 = this.GetEventProperty("revealsItemRegion", null);
					this.AddQuestsForRelic(eventProperty, eventProperty2, eventProperty3, null, null, null);
				}
			}
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000A11EC File Offset: 0x0009F3EC
		public void AddQuestsForRelic(string relicName, string locationName, string regionName, string QuestGiverName = null, string QuestGiverLocationName = null, string QuestGiverLocationZoneID = null)
		{
			if (The.Game.IsCoda)
			{
				return;
			}
			string text = relicName.Strip();
			QuestStep questStep = new QuestStep();
			questStep.ID = Guid.NewGuid().ToString();
			questStep.Name = "Locate " + regionName;
			questStep.Text = "Travel to the historical site of " + regionName + ".";
			questStep.XP = 1000;
			QuestStep questStep2 = new QuestStep();
			questStep2.ID = Guid.NewGuid().ToString();
			questStep2.Name = "Recover " + text;
			questStep2.Text = string.Concat(new string[] { "Recover ", relicName, " at ", locationName, "." });
			questStep2.XP = 1000;
			Quest quest = new Quest();
			quest.ID = "Recover " + text;
			quest.Name = "Recover " + text;
			quest.StepsByID = new Dictionary<string, QuestStep>();
			quest.StepsByID.Add(questStep.ID, questStep);
			quest.StepsByID.Add(questStep2.ID, questStep2);
			quest.Level = 1;
			quest._Manager = new LocateRelicQuestManager
			{
				QuestID = quest.ID,
				Relic = relicName
			};
			The.Game.StartQuest(quest, QuestGiverName, QuestGiverLocationName, QuestGiverLocationZoneID);
		}

		// Token: 0x0400108B RID: 4235
		public long id;

		// Token: 0x0400108C RID: 4236
		public long year;

		// Token: 0x0400108D RID: 4237
		public long duration = 1L;

		// Token: 0x0400108E RID: 4238
		[NonSerialized]
		public History history;

		// Token: 0x0400108F RID: 4239
		[NonSerialized]
		public HistoricEntity entity;

		// Token: 0x04001090 RID: 4240
		public Dictionary<string, string> eventProperties;

		// Token: 0x04001091 RID: 4241
		public Dictionary<string, string> entityProperties;

		// Token: 0x04001092 RID: 4242
		public Dictionary<string, List<string>> addedListProperties;

		// Token: 0x04001093 RID: 4243
		public Dictionary<string, List<string>> removedListProperties;

		// Token: 0x04001094 RID: 4244
		public Dictionary<string, HistoricPerspective> perspectives;
	}
}
