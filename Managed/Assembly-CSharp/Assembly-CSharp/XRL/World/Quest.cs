using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using XRL.Messages;
using XRL.UI;

namespace XRL.World
{
	// Token: 0x02000962 RID: 2402
	[Serializable]
	public class Quest
	{
		// Token: 0x06006311 RID: 25361 RVA: 0x001E7138 File Offset: 0x001E5338
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("[Quest " + this.ID + "]");
			stringBuilder.AppendLine("Name=" + this.Name);
			stringBuilder.AppendLine("Accomplishment=" + this.Accomplishment);
			stringBuilder.AppendLine("Achievement=" + this.Achievement);
			stringBuilder.AppendLine("BonusAtLevel=" + this.BonusAtLevel);
			stringBuilder.AppendLine("Hagiograph=" + this.Hagiograph);
			stringBuilder.AppendLine("HagiographCategory=" + this.HagiographCategory);
			stringBuilder.AppendLine("Factions=" + this.Factions);
			stringBuilder.AppendLine("Reputation=" + this.Reputation);
			stringBuilder.AppendLine("Level=" + this.Level.ToString());
			stringBuilder.AppendLine("Finished=" + this.Finished.ToString());
			StringBuilder stringBuilder2 = stringBuilder;
			string text = "System=";
			Type systemType = this.SystemType;
			stringBuilder2.AppendLine(text + ((systemType != null) ? systemType.FullName : null));
			stringBuilder.AppendLine("QuestGiverName=" + this.QuestGiverName);
			stringBuilder.AppendLine("QuestGiverLocationName=" + this.QuestGiverLocationName);
			stringBuilder.AppendLine("QuestGiverLocationZoneID=" + this.QuestGiverLocationZoneID);
			if (this._dynamicReward == null)
			{
				stringBuilder.Append("DynamicReward=none");
			}
			else
			{
				stringBuilder.Append("DynamicReward=" + this._dynamicReward.ToString());
			}
			stringBuilder.Append("nSteps=" + this.StepsByID.Count.ToString());
			foreach (KeyValuePair<string, QuestStep> keyValuePair in this.StepsByID)
			{
				stringBuilder.Append(" step " + keyValuePair.Key + " = " + keyValuePair.Value.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06006312 RID: 25362 RVA: 0x001E7384 File Offset: 0x001E5584
		// (set) Token: 0x06006313 RID: 25363 RVA: 0x001E738C File Offset: 0x001E558C
		public DynamicQuestReward dynamicReward
		{
			get
			{
				return this._dynamicReward;
			}
			set
			{
				this._dynamicReward = value;
				if (value != null)
				{
					int count = this.StepsByID.Count;
					foreach (QuestStep questStep in this.StepsByID.Values)
					{
						questStep.XP = value.StepXP / count;
					}
				}
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06006314 RID: 25364 RVA: 0x001E7400 File Offset: 0x001E5600
		// (set) Token: 0x06006315 RID: 25365 RVA: 0x001E7433 File Offset: 0x001E5633
		public IQuestSystem System
		{
			get
			{
				if (this._System == null && this.SystemType != null)
				{
					this._System = The.Game.GetSystem(this.SystemType) as IQuestSystem;
				}
				return this._System;
			}
			set
			{
				this._System = value;
				this.SystemType = ((value != null) ? value.GetType() : null);
				if (value != null)
				{
					value.Quest = this;
				}
			}
		}

		// Token: 0x06006316 RID: 25366 RVA: 0x001E7458 File Offset: 0x001E5658
		public GameObject GetInfluencer()
		{
			IQuestSystem system = this.System;
			GameObject gameObject;
			if ((gameObject = ((system != null) ? system.GetInfluencer() : null)) == null)
			{
				QuestManager manager = this.Manager;
				if (manager == null)
				{
					return null;
				}
				gameObject = manager.GetQuestInfluencer();
			}
			return gameObject;
		}

		// Token: 0x06006317 RID: 25367 RVA: 0x001E7484 File Offset: 0x001E5684
		public IQuestSystem InitializeSystem()
		{
			if (this.SystemType == null)
			{
				return null;
			}
			return this.System = The.Game.RequireSystem(this.SystemType) as IQuestSystem;
		}

		// Token: 0x06006318 RID: 25368 RVA: 0x001E74BC File Offset: 0x001E56BC
		public bool ReadyToTurnIn()
		{
			using (Dictionary<string, QuestStep>.ValueCollection.Enumerator enumerator = this.StepsByID.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Finished)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06006319 RID: 25369 RVA: 0x001E751C File Offset: 0x001E571C
		public void FinishPost()
		{
			if (this.dynamicReward != null)
			{
				this.dynamicReward.postaward();
			}
		}

		// Token: 0x0600631A RID: 25370 RVA: 0x001E7531 File Offset: 0x001E5731
		public void ShowStartPopup()
		{
			Popup.Show("You have received a new quest, " + this.DisplayName + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x0600631B RID: 25371 RVA: 0x001E7558 File Offset: 0x001E5758
		public void ShowFailPopup()
		{
			Popup.Show("You have failed the quest " + this.DisplayName + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x0600631C RID: 25372 RVA: 0x001E7580 File Offset: 0x001E5780
		public void ShowFailStepPopup(QuestStep Step)
		{
			Popup.Show(string.Concat(new string[] { "You have failed the step, {{R|", Step.Name, "}}, of the quest ", this.DisplayName, "!" }), null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x0600631D RID: 25373 RVA: 0x001E75D2 File Offset: 0x001E57D2
		public void ShowFinishPopup()
		{
			Popup.Show("You have completed the quest " + this.DisplayName + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x0600631E RID: 25374 RVA: 0x001E75FC File Offset: 0x001E57FC
		public void ShowFinishStepPopup(QuestStep Step)
		{
			string text = string.Concat(new string[] { "You have finished the step, {{G|", Step.Name, "}}, of the quest ", this.DisplayName, "!" });
			if (Step.XP > 0)
			{
				Popup.ShowBlock(text + "\nYou gain {{C|" + Step.XP.ToString() + "}} XP!", null, "Sounds/UI/ui_notification", true, true, true, false, null);
				MessageQueue.AddPlayerMessage(text, null, true);
				return;
			}
			Popup.ShowBlock(text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x0600631F RID: 25375 RVA: 0x001E768F File Offset: 0x001E588F
		public void Fail()
		{
			if (this.System != null)
			{
				this._System.Fail();
				if (this._System.RemoveWithQuest)
				{
					The.Game.FlagSystemForRemoval(this._System);
				}
			}
		}

		// Token: 0x06006320 RID: 25376 RVA: 0x001E76C1 File Offset: 0x001E58C1
		public void FailStep(QuestStep Step)
		{
			IQuestSystem system = this.System;
			if (system == null)
			{
				return;
			}
			system.FailStep(Step);
		}

		// Token: 0x06006321 RID: 25377 RVA: 0x001E76D4 File Offset: 0x001E58D4
		public void Finish()
		{
			this.Finished = true;
			if (this.System != null)
			{
				this._System.Finish();
				if (this._System.RemoveWithQuest)
				{
					The.Game.FlagSystemForRemoval(this._System);
				}
			}
			if (this.dynamicReward != null)
			{
				this.dynamicReward.award();
			}
		}

		// Token: 0x06006322 RID: 25378 RVA: 0x001E772B File Offset: 0x001E592B
		public void FinishStep(QuestStep Step)
		{
			IQuestSystem system = this.System;
			if (system == null)
			{
				return;
			}
			system.FinishStep(Step);
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06006324 RID: 25380 RVA: 0x001E7753 File Offset: 0x001E5953
		// (set) Token: 0x06006323 RID: 25379 RVA: 0x001E773E File Offset: 0x001E593E
		public QuestManager Manager
		{
			get
			{
				return this._Manager;
			}
			set
			{
				this._Manager = value;
				value.MyQuestID = this.ID;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06006325 RID: 25381 RVA: 0x001E775B File Offset: 0x001E595B
		public string DisplayName
		{
			get
			{
				return "{{W|" + this.Name + "}}";
			}
		}

		// Token: 0x06006326 RID: 25382 RVA: 0x001E7774 File Offset: 0x001E5974
		public void Save(SerializationWriter Writer)
		{
			Writer.Write(this.ID);
			Writer.Write(this.Name);
			Writer.Write(this.Level);
			Writer.Write(this.Finished);
			Writer.Write(this.Accomplishment);
			Writer.Write(this.Achievement);
			Writer.Write(this.Hagiograph);
			Writer.Write(this.HagiographCategory);
			Writer.Write(this.Gospel);
			Writer.Write(this.BonusAtLevel);
			Writer.Write(this.Factions);
			Writer.Write(this.Reputation);
			Writer.Write(this.QuestGiverName);
			Writer.Write(this.QuestGiverLocationName);
			Writer.Write(this.QuestGiverLocationZoneID);
			Writer.WriteObject(this._dynamicReward);
			if (this.StepsByID == null)
			{
				Writer.Write(-1);
			}
			else
			{
				Writer.Write(this.StepsByID.Count);
				foreach (KeyValuePair<string, QuestStep> keyValuePair in this.StepsByID)
				{
					keyValuePair.Value.Save(Writer);
				}
			}
			if (this.Manager == null)
			{
				Writer.Write(0);
			}
			else
			{
				Writer.Write(1);
				IPart.Save(this.Manager, Writer);
			}
			Writer.WriteTokenized(this.SystemType);
			Writer.Write<string, object>(this.Properties);
			Writer.Write<string, int>(this.IntProperties);
		}

		// Token: 0x06006327 RID: 25383 RVA: 0x001E78F8 File Offset: 0x001E5AF8
		public static Quest Load(SerializationReader Reader)
		{
			Quest quest = new Quest();
			quest.ID = Reader.ReadString();
			quest.Name = Reader.ReadString();
			quest.Level = Reader.ReadInt32();
			quest.Finished = Reader.ReadBoolean();
			quest.Accomplishment = Reader.ReadString();
			quest.Achievement = Reader.ReadString();
			quest.Hagiograph = Reader.ReadString();
			quest.HagiographCategory = Reader.ReadString();
			quest.Gospel = Reader.ReadString();
			quest.BonusAtLevel = Reader.ReadString();
			quest.Factions = Reader.ReadString();
			quest.Reputation = Reader.ReadString();
			quest.QuestGiverName = Reader.ReadString();
			quest.QuestGiverLocationName = Reader.ReadString();
			quest.QuestGiverLocationZoneID = Reader.ReadString();
			quest._dynamicReward = Reader.ReadObject() as DynamicQuestReward;
			int num = Reader.ReadInt32();
			if (num >= 0)
			{
				quest.StepsByID = new Dictionary<string, QuestStep>(num);
				for (int i = 0; i < num; i++)
				{
					QuestStep questStep = new QuestStep(Reader);
					quest.StepsByID[questStep.ID] = questStep;
				}
			}
			if (Reader.ReadInt32() != 0)
			{
				quest.Manager = (QuestManager)IPart.Load(null, Reader);
			}
			quest.SystemType = Reader.ReadTokenizedType();
			quest.Properties = Reader.ReadDictionary<string, object>();
			quest.IntProperties = Reader.ReadDictionary<string, int>();
			return quest;
		}

		// Token: 0x06006328 RID: 25384 RVA: 0x001E7A4C File Offset: 0x001E5C4C
		public Quest Copy()
		{
			Quest quest = new Quest();
			quest.ID = this.ID;
			quest.Name = this.Name;
			quest.Accomplishment = this.Accomplishment;
			quest.Achievement = this.Achievement;
			quest.Hagiograph = this.Hagiograph;
			quest.HagiographCategory = this.HagiographCategory;
			quest.Gospel = this.Gospel;
			quest.BonusAtLevel = this.BonusAtLevel;
			quest.Factions = this.Factions;
			quest.Reputation = this.Reputation;
			quest.Level = this.Level;
			quest.Finished = this.Finished;
			quest._Manager = this._Manager;
			quest.SystemType = this.SystemType;
			quest._dynamicReward = this._dynamicReward;
			quest.QuestGiverName = this.QuestGiverName;
			quest.QuestGiverLocationName = this.QuestGiverLocationName;
			quest.QuestGiverLocationZoneID = this.QuestGiverLocationZoneID;
			quest.StepsByID = new Dictionary<string, QuestStep>(this.StepsByID.Count);
			foreach (KeyValuePair<string, QuestStep> keyValuePair in this.StepsByID)
			{
				QuestStep value = keyValuePair.Value;
				if (!value.Base)
				{
					quest.StepsByID.Add(keyValuePair.Key, new QuestStep
					{
						Finished = false,
						ID = value.ID,
						Name = value.Name,
						Text = value.Text,
						Value = value.Value,
						XP = value.XP,
						Ordinal = value.Ordinal,
						Flags = value.Flags
					});
				}
			}
			if (!this.Properties.IsNullOrEmpty<KeyValuePair<string, object>>())
			{
				quest.Properties = new Dictionary<string, object>(this.Properties);
				foreach (KeyValuePair<string, object> keyValuePair2 in this.Properties)
				{
					ICollection collection = keyValuePair2.Value as ICollection;
					if (collection != null)
					{
						Type type = collection.GetType();
						quest.Properties[keyValuePair2.Key] = Activator.CreateInstance(type, new object[] { collection });
					}
				}
			}
			if (!this.IntProperties.IsNullOrEmpty<KeyValuePair<string, int>>())
			{
				quest.IntProperties = new Dictionary<string, int>(this.IntProperties);
			}
			return quest;
		}

		// Token: 0x06006329 RID: 25385 RVA: 0x001E7CD0 File Offset: 0x001E5ED0
		public string GetProperty(string Key, string Default = null)
		{
			if (Key == null || this.Properties == null)
			{
				return Default;
			}
			object obj;
			if (!this.Properties.TryGetValue(Key, out obj))
			{
				return Default;
			}
			return obj as string;
		}

		// Token: 0x0600632A RID: 25386 RVA: 0x001E7D04 File Offset: 0x001E5F04
		public int GetProperty(string Key, int Default)
		{
			if (Key == null || this.Properties == null)
			{
				return Default;
			}
			int num;
			if (!this.IntProperties.TryGetValue(Key, out num))
			{
				return Default;
			}
			return num;
		}

		// Token: 0x0600632B RID: 25387 RVA: 0x001E7D34 File Offset: 0x001E5F34
		public float GetProperty(string Key, float Default)
		{
			if (Key == null || this.Properties == null)
			{
				return Default;
			}
			int num;
			if (!this.IntProperties.TryGetValue(Key, out num))
			{
				return Default;
			}
			return BitConverter.Int32BitsToSingle(num);
		}

		// Token: 0x0600632C RID: 25388 RVA: 0x001E7D68 File Offset: 0x001E5F68
		public T GetProperty<T>(string Key, T Default = default(T)) where T : class
		{
			if (Key == null || this.Properties == null)
			{
				return Default;
			}
			object obj;
			if (!this.Properties.TryGetValue(Key, out obj))
			{
				return Default;
			}
			return obj as T;
		}

		// Token: 0x0600632D RID: 25389 RVA: 0x001E7DA0 File Offset: 0x001E5FA0
		public List<string> GetList(string Key)
		{
			if (Key == null || this.Properties == null)
			{
				return null;
			}
			object obj;
			if (!this.Properties.TryGetValue(Key, out obj))
			{
				return null;
			}
			return obj as List<string>;
		}

		// Token: 0x0600632E RID: 25390 RVA: 0x001E7DD4 File Offset: 0x001E5FD4
		public List<T> GetList<T>(string Key)
		{
			if (Key == null || this.Properties == null)
			{
				return null;
			}
			object obj;
			if (!this.Properties.TryGetValue(Key, out obj))
			{
				return null;
			}
			return obj as List<T>;
		}

		// Token: 0x0600632F RID: 25391 RVA: 0x001E7E08 File Offset: 0x001E6008
		public Dictionary<string, string> GetDictionary(string Key)
		{
			if (Key == null || this.Properties == null)
			{
				return null;
			}
			object obj;
			if (!this.Properties.TryGetValue(Key, out obj))
			{
				return null;
			}
			return obj as Dictionary<string, string>;
		}

		// Token: 0x06006330 RID: 25392 RVA: 0x001E7E3C File Offset: 0x001E603C
		public Dictionary<string, T> GetDictionary<T>(string Key)
		{
			if (Key == null || this.Properties == null)
			{
				return null;
			}
			object obj;
			if (!this.Properties.TryGetValue(Key, out obj))
			{
				return null;
			}
			return obj as Dictionary<string, T>;
		}

		// Token: 0x06006331 RID: 25393 RVA: 0x001E7E6E File Offset: 0x001E606E
		public void SetProperty(string Key, object Value)
		{
			if (this.Properties == null)
			{
				this.Properties = new Dictionary<string, object>(3);
			}
			this.Properties[Key] = Value;
		}

		// Token: 0x06006332 RID: 25394 RVA: 0x001E7E91 File Offset: 0x001E6091
		public void SetProperty(string Key, int Value)
		{
			if (this.IntProperties == null)
			{
				this.IntProperties = new Dictionary<string, int>(3);
			}
			this.IntProperties[Key] = Value;
		}

		// Token: 0x06006333 RID: 25395 RVA: 0x001E7EB4 File Offset: 0x001E60B4
		public void SetProperty(string Key, float Value)
		{
			if (this.IntProperties == null)
			{
				this.IntProperties = new Dictionary<string, int>(3);
			}
			this.IntProperties[Key] = BitConverter.SingleToInt32Bits(Value);
		}

		// Token: 0x06006334 RID: 25396 RVA: 0x001E7EDC File Offset: 0x001E60DC
		public bool HasProperty(string Key)
		{
			return (this.Properties != null && this.Properties.ContainsKey(Key)) || (this.IntProperties != null && this.IntProperties.ContainsKey(Key));
		}

		// Token: 0x06006335 RID: 25397 RVA: 0x001E7F0C File Offset: 0x001E610C
		public bool RemoveProperty(string Key)
		{
			return this.Properties != null && this.Properties.Remove(Key);
		}

		// Token: 0x06006336 RID: 25398 RVA: 0x001E7F24 File Offset: 0x001E6124
		public bool RemoveNumericProperty(string Key)
		{
			return this.IntProperties != null && this.IntProperties.Remove(Key);
		}

		// Token: 0x06006337 RID: 25399 RVA: 0x001E7F3C File Offset: 0x001E613C
		public bool IsStepFinished(string StepID)
		{
			QuestStep questStep;
			return this.StepsByID.TryGetValue(StepID, out questStep) && questStep.Finished;
		}

		// Token: 0x06006338 RID: 25400 RVA: 0x001E7F64 File Offset: 0x001E6164
		public static string Consider(Quest Q)
		{
			int num = The.Player.Statistics["Level"].Value - Q.Level;
			if (num <= -15)
			{
				return "[{{R|Impossible}}]";
			}
			if (num <= -10)
			{
				return "[{{r|Very Tough}}]";
			}
			if (num <= -5)
			{
				return "[{{W|Tough}}]";
			}
			if (num < 5)
			{
				return "[{{w|Average}}]";
			}
			if (num <= 10)
			{
				return "[{{g|Easy}}]";
			}
			return "[{{G|Trivial}}]";
		}

		// Token: 0x04002E93 RID: 11923
		public string ID;

		// Token: 0x04002E94 RID: 11924
		public string Name;

		// Token: 0x04002E95 RID: 11925
		public Type SystemType;

		// Token: 0x04002E96 RID: 11926
		public string Accomplishment;

		// Token: 0x04002E97 RID: 11927
		public string Achievement;

		// Token: 0x04002E98 RID: 11928
		public string BonusAtLevel;

		// Token: 0x04002E99 RID: 11929
		public int Level;

		// Token: 0x04002E9A RID: 11930
		public string Factions;

		// Token: 0x04002E9B RID: 11931
		public string Reputation;

		// Token: 0x04002E9C RID: 11932
		public string QuestGiverName;

		// Token: 0x04002E9D RID: 11933
		public string QuestGiverLocationName;

		// Token: 0x04002E9E RID: 11934
		public string QuestGiverLocationZoneID;

		// Token: 0x04002E9F RID: 11935
		public string Hagiograph;

		// Token: 0x04002EA0 RID: 11936
		public string HagiographCategory;

		// Token: 0x04002EA1 RID: 11937
		public string Gospel;

		// Token: 0x04002EA2 RID: 11938
		public bool Finished;

		// Token: 0x04002EA3 RID: 11939
		public DynamicQuestReward _dynamicReward;

		// Token: 0x04002EA4 RID: 11940
		public Dictionary<string, object> Properties;

		// Token: 0x04002EA5 RID: 11941
		public Dictionary<string, int> IntProperties;

		// Token: 0x04002EA6 RID: 11942
		[NonSerialized]
		private IQuestSystem _System;

		// Token: 0x04002EA7 RID: 11943
		public Dictionary<string, QuestStep> StepsByID;

		// Token: 0x04002EA8 RID: 11944
		public QuestManager _Manager;
	}
}
