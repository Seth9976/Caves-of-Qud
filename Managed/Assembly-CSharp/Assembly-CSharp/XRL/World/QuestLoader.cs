using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;

namespace XRL.World
{
	// Token: 0x02000964 RID: 2404
	[HasModSensitiveStaticCache]
	[Serializable]
	public class QuestLoader
	{
		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x0600634D RID: 25421 RVA: 0x001E81FD File Offset: 0x001E63FD
		public static QuestLoader Loader
		{
			get
			{
				QuestLoader.CheckInit();
				return QuestLoader._Loader;
			}
		}

		// Token: 0x0600634E RID: 25422 RVA: 0x001E8209 File Offset: 0x001E6409
		[PreGameCacheInit]
		public static void CheckInit()
		{
			if (QuestLoader._Loader == null)
			{
				QuestLoader._Loader = new QuestLoader();
				Loading.LoadTask("Loading Quests.xml", new Action(QuestLoader._Loader.LoadQuests), true);
			}
		}

		// Token: 0x0600634F RID: 25423 RVA: 0x001E8238 File Offset: 0x001E6438
		public void LoadQuests()
		{
			this.QuestsByID = new Dictionary<string, Quest>();
			foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("Quests", true, true))
			{
				xmlDataHelper.HandleNodes(QuestLoader.parser.rootNodes, null);
			}
		}

		// Token: 0x04002EB7 RID: 11959
		public Dictionary<string, Quest> QuestsByID;

		// Token: 0x04002EB8 RID: 11960
		[ModSensitiveStaticCache(false)]
		private static QuestLoader _Loader = null;

		// Token: 0x04002EB9 RID: 11961
		[ModSensitiveStaticCache(true)]
		public static QuestLoader.XMLParsing parser = new QuestLoader.XMLParsing();

		// Token: 0x02001A6B RID: 6763
		public class XMLParsing
		{
			// Token: 0x17000ECB RID: 3787
			// (get) Token: 0x0600E778 RID: 59256 RVA: 0x00535A59 File Offset: 0x00533C59
			public Dictionary<string, Quest> QuestsByID
			{
				get
				{
					return QuestLoader.Loader.QuestsByID;
				}
			}

			// Token: 0x0600E779 RID: 59257 RVA: 0x00535A68 File Offset: 0x00533C68
			public XMLParsing()
			{
				this.rootNodes = new Dictionary<string, Action<XmlDataHelper>> { 
				{
					"quests",
					new Action<XmlDataHelper>(this.HandleQuests)
				} };
				this.questsNodes = new Dictionary<string, Action<XmlDataHelper>> { 
				{
					"quest",
					new Action<XmlDataHelper>(this.HandleQuest)
				} };
				this.questNodes = new Dictionary<string, Action<XmlDataHelper>>
				{
					{
						"step",
						new Action<XmlDataHelper>(this.HandleStep)
					},
					{
						"property",
						new Action<XmlDataHelper>(this.HandleProperty)
					},
					{
						"intproperty",
						new Action<XmlDataHelper>(this.HandleIntProperty)
					},
					{
						"floatproperty",
						new Action<XmlDataHelper>(this.HandleFloatProperty)
					}
				};
				this.stepNodes = new Dictionary<string, Action<XmlDataHelper>> { 
				{
					"text",
					new Action<XmlDataHelper>(this.HandleStepText)
				} };
				this.propertyNodes = new Dictionary<string, Action<XmlDataHelper>>
				{
					{
						"text",
						new Action<XmlDataHelper>(this.HandlePropertyText)
					},
					{
						"entry",
						new Action<XmlDataHelper>(this.HandlePropertyEntry)
					},
					{
						"value",
						new Action<XmlDataHelper>(this.HandlePropertyValue)
					}
				};
			}

			// Token: 0x0600E77A RID: 59258 RVA: 0x00535B98 File Offset: 0x00533D98
			public void HandleQuests(XmlDataHelper xml)
			{
				xml.HandleNodes(this.questsNodes, null);
			}

			// Token: 0x0600E77B RID: 59259 RVA: 0x00535BA8 File Offset: 0x00533DA8
			public void HandleQuest(XmlDataHelper xml)
			{
				string text = xml.ParseAttribute<string>("ID", null, false, null);
				if (text == null)
				{
					text = xml.ParseAttribute<string>("Name", null, true, null);
				}
				if (text != null)
				{
					if (!this.QuestsByID.TryGetValue(text, out this.currentQuest))
					{
						this.currentQuest = new Quest
						{
							ID = text
						};
					}
				}
				else
				{
					this.currentQuest = new Quest
					{
						ID = ""
					};
				}
				this.currentQuest.Name = xml.ParseAttribute<string>("Name", this.currentQuest.Name, false, null);
				this.currentQuest.Level = xml.ParseAttribute<int>("Level", this.currentQuest.Level, false, null);
				this.currentQuest.Achievement = xml.ParseAttribute<string>("Achievement", this.currentQuest.Achievement, false, null);
				this.currentQuest.Accomplishment = xml.ParseAttribute<string>("Accomplishment", this.currentQuest.Accomplishment, false, null);
				this.currentQuest.Hagiograph = xml.ParseAttribute<string>("Hagiograph", this.currentQuest.Hagiograph, false, null);
				this.currentQuest.HagiographCategory = xml.ParseAttribute<string>("HagiographCategory", this.currentQuest.HagiographCategory, false, null);
				this.currentQuest.Gospel = xml.ParseAttribute<string>("Gospel", this.currentQuest.Gospel, false, null);
				this.currentQuest.BonusAtLevel = xml.ParseAttribute<string>("BonusAtLevel", this.currentQuest.BonusAtLevel, false, null);
				this.currentQuest.Factions = xml.ParseAttribute<string>("Factions", this.currentQuest.Factions, false, null);
				this.currentQuest.Reputation = xml.ParseAttribute<string>("Reputation", this.currentQuest.Reputation, false, null);
				this.currentQuest.QuestGiverName = xml.ParseAttribute<string>("QuestGiverName", null, false, null);
				this.currentQuest.QuestGiverLocationName = xml.ParseAttribute<string>("QuestGiverLocationName", null, false, null);
				this.currentQuest.QuestGiverLocationZoneID = xml.ParseAttribute<string>("QuestGiverLocationZoneID", null, false, null);
				Type type = xml.ParseType("System", "XRL.World.Quests", false);
				if (type != null)
				{
					this.currentQuest.SystemType = type;
				}
				Type type2 = xml.ParseType("Manager", "XRL.World.QuestManagers", false);
				if (type2 != null)
				{
					this.currentQuest.Manager = ModManager.CreateInstance<QuestManager>(type2);
				}
				this.currentQuest.StepsByID = new Dictionary<string, QuestStep>();
				xml.HandleNodes(this.questNodes, null);
				this.QuestsByID[this.currentQuest.ID] = this.currentQuest;
				this.currentQuest = null;
			}

			// Token: 0x0600E77C RID: 59260 RVA: 0x00535E48 File Offset: 0x00534048
			public void HandleStep(XmlDataHelper xml)
			{
				string text = xml.ParseAttribute<string>("ID", null, false, null);
				if (text == null)
				{
					text = xml.ParseAttribute<string>("Name", null, true, null);
				}
				if (!this.currentQuest.StepsByID.TryGetValue(text, out this.currentQuestStep))
				{
					this.currentQuestStep = new QuestStep();
				}
				this.currentQuestStep.ID = xml.ParseAttribute<string>("ID", text, false, null);
				this.currentQuestStep.Name = xml.ParseAttribute<string>("Name", this.currentQuestStep.Name, false, null);
				this.currentQuestStep.Value = xml.ParseAttribute<string>("Value", this.currentQuestStep.Value, false, null);
				this.currentQuestStep.XP = xml.ParseAttribute<int>("XP", this.currentQuestStep.XP, false, null);
				this.currentQuestStep.Optional = xml.ParseAttribute<bool>("Optional", this.currentQuestStep.Optional, false, null);
				this.currentQuestStep.Ordinal = xml.ParseAttribute<int>("Ordinal", this.currentQuest.StepsByID.Count, false, null);
				this.currentQuestStep.Collapse = xml.ParseAttribute<bool>("Collapse", this.currentQuestStep.Collapse, false, null);
				this.currentQuestStep.Awarded = xml.ParseAttribute<bool>("Awarded", this.currentQuestStep.Awarded, false, null);
				this.currentQuestStep.Failed = xml.ParseAttribute<bool>("Failed", this.currentQuestStep.Failed, false, null);
				this.currentQuestStep.Hidden = xml.ParseAttribute<bool>("Hidden", this.currentQuestStep.Hidden, false, null);
				this.currentQuestStep.Base = xml.ParseAttribute<bool>("Base", this.currentQuestStep.Base, false, null);
				xml.HandleNodes(this.stepNodes, null);
				this.currentQuest.StepsByID[this.currentQuestStep.ID] = this.currentQuestStep;
				this.currentQuestStep = null;
			}

			// Token: 0x0600E77D RID: 59261 RVA: 0x0053604B File Offset: 0x0053424B
			public void HandleStepText(XmlDataHelper xml)
			{
				this.currentQuestStep.Text = xml.GetTextNode();
			}

			// Token: 0x0600E77E RID: 59262 RVA: 0x0053605E File Offset: 0x0053425E
			public void SetComplexType(byte Type)
			{
				this.TextValue = null;
				this.ComplexValue = null;
				this.ComplexType = Type;
			}

			// Token: 0x0600E77F RID: 59263 RVA: 0x00536078 File Offset: 0x00534278
			public void HandleProperty(XmlDataHelper xml)
			{
				this.SetComplexType(0);
				string text = xml.ParseAttribute<string>("Name", "", false, null);
				string text2 = xml.ParseAttribute<string>("Value", "", false, null);
				xml.HandleNodes(this.propertyNodes, null);
				Quest quest = this.currentQuest;
				string text3 = text;
				object obj;
				if ((obj = this.ComplexValue) == null)
				{
					obj = this.TextValue ?? text2;
				}
				quest.SetProperty(text3, obj);
			}

			// Token: 0x0600E780 RID: 59264 RVA: 0x005360E0 File Offset: 0x005342E0
			public void HandleIntProperty(XmlDataHelper xml)
			{
				this.SetComplexType(1);
				string text = xml.ParseAttribute<string>("Name", "", false, null);
				int num = xml.ParseAttribute<int>("Value", 0, false, null);
				xml.HandleNodes(this.propertyNodes, null);
				if (this.ComplexValue != null)
				{
					this.currentQuest.SetProperty(text, this.ComplexValue);
					return;
				}
				int num2;
				if (this.TextValue != null && xml.TryParse<int>(this.TextValue, out num2))
				{
					this.currentQuest.SetProperty(text, num2);
					return;
				}
				this.currentQuest.SetProperty(text, num);
			}

			// Token: 0x0600E781 RID: 59265 RVA: 0x00536174 File Offset: 0x00534374
			public void HandleFloatProperty(XmlDataHelper xml)
			{
				this.SetComplexType(2);
				string text = xml.ParseAttribute<string>("Name", "", false, null);
				float num = xml.ParseAttribute<float>("Value", 0f, false, null);
				xml.HandleNodes(this.propertyNodes, null);
				if (this.ComplexValue != null)
				{
					this.currentQuest.SetProperty(text, this.ComplexValue);
					return;
				}
				float num2;
				if (this.TextValue != null && xml.TryParse<float>(this.TextValue, out num2))
				{
					this.currentQuest.SetProperty(text, num2);
					return;
				}
				this.currentQuest.SetProperty(text, num);
			}

			// Token: 0x0600E782 RID: 59266 RVA: 0x00536209 File Offset: 0x00534409
			public void HandlePropertyEntry(XmlDataHelper xml)
			{
				if (this.ComplexType == 0)
				{
					this.HandlePropertyEntry<string>(xml);
					return;
				}
				if (this.ComplexType == 1)
				{
					this.HandlePropertyEntry<int>(xml);
					return;
				}
				if (this.ComplexType == 2)
				{
					this.HandlePropertyEntry<float>(xml);
				}
			}

			// Token: 0x0600E783 RID: 59267 RVA: 0x0053623C File Offset: 0x0053443C
			public void HandlePropertyEntry<T>(XmlDataHelper xml)
			{
				if (this.ComplexValue == null)
				{
					this.ComplexValue = new Dictionary<string, T>();
				}
				Dictionary<string, T> dictionary = this.ComplexValue as Dictionary<string, T>;
				string text = xml.ParseAttribute<string>("Key", "", false, null);
				if (text.IsNullOrEmpty())
				{
					text = xml.ParseAttribute<string>("Name", text, false, null);
				}
				T t = xml.ParseAttribute<T>("Value", default(T), false, null);
				T t2;
				if (xml.TryParseTextNode<T>(out t2, true, true))
				{
					t = t2;
				}
				dictionary[text] = t;
			}

			// Token: 0x0600E784 RID: 59268 RVA: 0x005362BD File Offset: 0x005344BD
			public void HandlePropertyValue(XmlDataHelper xml)
			{
				if (this.ComplexType == 0)
				{
					this.HandlePropertyValue<string>(xml);
					return;
				}
				if (this.ComplexType == 1)
				{
					this.HandlePropertyValue<int>(xml);
					return;
				}
				if (this.ComplexType == 2)
				{
					this.HandlePropertyValue<float>(xml);
				}
			}

			// Token: 0x0600E785 RID: 59269 RVA: 0x005362F0 File Offset: 0x005344F0
			public void HandlePropertyValue<T>(XmlDataHelper xml)
			{
				if (this.ComplexValue == null)
				{
					this.ComplexValue = new List<T>();
				}
				List<T> list = this.ComplexValue as List<T>;
				T t = xml.ParseAttribute<T>("Value", default(T), false, null);
				T t2;
				if (xml.TryParseTextNode<T>(out t2, true, true))
				{
					t = t2;
				}
				list.Add(t);
			}

			// Token: 0x0600E786 RID: 59270 RVA: 0x00536346 File Offset: 0x00534546
			public void HandlePropertyText(XmlDataHelper xml)
			{
				this.TextValue = Strings.SB.Clear().Append(xml.Value).Unindent()
					.ToString();
			}

			// Token: 0x04006942 RID: 26946
			public Dictionary<string, Action<XmlDataHelper>> rootNodes;

			// Token: 0x04006943 RID: 26947
			public Dictionary<string, Action<XmlDataHelper>> questsNodes;

			// Token: 0x04006944 RID: 26948
			public Dictionary<string, Action<XmlDataHelper>> questNodes;

			// Token: 0x04006945 RID: 26949
			public Dictionary<string, Action<XmlDataHelper>> stepNodes;

			// Token: 0x04006946 RID: 26950
			public Dictionary<string, Action<XmlDataHelper>> propertyNodes;

			// Token: 0x04006947 RID: 26951
			public Quest currentQuest;

			// Token: 0x04006948 RID: 26952
			public QuestStep currentQuestStep;

			// Token: 0x04006949 RID: 26953
			private string TextValue;

			// Token: 0x0400694A RID: 26954
			private object ComplexValue;

			// Token: 0x0400694B RID: 26955
			private byte ComplexType;
		}
	}
}
