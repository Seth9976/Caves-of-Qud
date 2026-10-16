using System;
using System.Collections.Generic;
using System.Text;
using XRL.World.Parts;

namespace XRL.World.Quests.GolemQuest
{
	// Token: 0x02000B34 RID: 2868
	[Serializable]
	public class GolemQuestSystem : IQuestSystem
	{
		// Token: 0x060073BE RID: 29630 RVA: 0x002BA504 File Offset: 0x002B8704
		public static GolemQuestSystem Get()
		{
			return The.Game.GetSystem<GolemQuestSystem>();
		}

		// Token: 0x060073BF RID: 29631 RVA: 0x002BA510 File Offset: 0x002B8710
		public static GolemQuestSystem Require()
		{
			return The.Game.RequireSystem<GolemQuestSystem>(() => new GolemQuestSystem());
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060073C0 RID: 29632 RVA: 0x002BA53B File Offset: 0x002B873B
		public GolemBodySelection Body
		{
			get
			{
				return this.Selections.GetValue("Body", null) as GolemBodySelection;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060073C1 RID: 29633 RVA: 0x002BA553 File Offset: 0x002B8753
		public GolemCatalystSelection Catalyst
		{
			get
			{
				return this.Selections.GetValue("Catalyst", null) as GolemCatalystSelection;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060073C2 RID: 29634 RVA: 0x002BA56B File Offset: 0x002B876B
		public GolemAtzmusSelection Atzmus
		{
			get
			{
				return this.Selections.GetValue("Atzmus", null) as GolemAtzmusSelection;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060073C3 RID: 29635 RVA: 0x002BA583 File Offset: 0x002B8783
		public GolemArmamentSelection Armament
		{
			get
			{
				return this.Selections.GetValue("Armament", null) as GolemArmamentSelection;
			}
		}

		// Token: 0x060073C4 RID: 29636 RVA: 0x002BA59B File Offset: 0x002B879B
		public override void Start()
		{
			this.UpdateQuest();
		}

		// Token: 0x060073C5 RID: 29637 RVA: 0x002BA5A4 File Offset: 0x002B87A4
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Selections.Count);
			foreach (KeyValuePair<string, GolemQuestSelection> keyValuePair in this.Selections)
			{
				Writer.Write(keyValuePair.Key);
				Writer.Write(keyValuePair.Value.GetType().FullName);
				keyValuePair.Value.Save(Writer);
			}
		}

		// Token: 0x060073C6 RID: 29638 RVA: 0x002BA634 File Offset: 0x002B8834
		public override void Read(SerializationReader Reader)
		{
			GolemQuestSystem golemQuestSystem = null;
			if (this.Selections == null)
			{
				this.Selections = new Dictionary<string, GolemQuestSelection>();
			}
			int num = Reader.ReadInt32();
			int i = 0;
			while (i < num)
			{
				string text = Reader.ReadString();
				Type type = ModManager.ResolveType(Reader.ReadString(), false, false, true);
				try
				{
					if (type != null)
					{
						GolemQuestSelection golemQuestSelection = Activator.CreateInstance(type) as GolemQuestSelection;
						if (golemQuestSelection != null)
						{
							golemQuestSelection.Load(Reader);
							this.Selections[text] = golemQuestSelection;
							goto IL_00AC;
						}
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Error deserializing golem selection", ex, "game_exception");
				}
				goto IL_0085;
				IL_00AC:
				i++;
				continue;
				IL_0085:
				if (golemQuestSystem == null)
				{
					golemQuestSystem = new GolemQuestSystem();
				}
				GolemQuestSelection golemQuestSelection2;
				if (golemQuestSystem.Selections.TryGetValue(text, out golemQuestSelection2))
				{
					this.Selections[text] = golemQuestSelection2;
					goto IL_00AC;
				}
				goto IL_00AC;
			}
		}

		// Token: 0x060073C7 RID: 29639 RVA: 0x002BA708 File Offset: 0x002B8908
		public static bool IsValidHolder(GameObject Object)
		{
			if (((Object != null) ? Object.Inventory : null) == null || Object.Physics == null || !Object.Physics.IsReal)
			{
				return false;
			}
			if (Object.Brain != null)
			{
				return Object.IsPlayerControlled();
			}
			return Object.HasPart(typeof(Container)) && Object.Physics.Owner.IsNullOrEmpty();
		}

		// Token: 0x060073C8 RID: 29640 RVA: 0x002BA770 File Offset: 0x002B8970
		public List<GameObject> GetValidHolders()
		{
			this._ValidHolders.Clear();
			GolemQuestMound mound = this.Mound;
			Cell cell;
			if (mound == null)
			{
				cell = null;
			}
			else
			{
				GameObject parentObject = mound.ParentObject;
				cell = ((parentObject != null) ? parentObject.CurrentCell : null);
			}
			Cell cell2 = cell;
			if (cell2 != null)
			{
				Predicate<GameObject> predicate = new Predicate<GameObject>(GolemQuestSystem.IsValidHolder);
				foreach (Cell cell3 in cell2.IterateAdjacent(2, false, false, true))
				{
					GameObject firstObject = cell3.GetFirstObject(predicate);
					if (firstObject != null)
					{
						this._ValidHolders.Add(firstObject);
					}
				}
			}
			else
			{
				this._ValidHolders.Add(The.Player);
			}
			return this._ValidHolders;
		}

		// Token: 0x060073C9 RID: 29641 RVA: 0x002BA810 File Offset: 0x002B8A10
		public bool AllValid()
		{
			foreach (KeyValuePair<string, GolemQuestSelection> keyValuePair in this.Selections)
			{
				if (!keyValuePair.Value.IsValid())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060073CA RID: 29642 RVA: 0x002BA874 File Offset: 0x002B8A74
		public void UpdateQuest()
		{
			Quest quest;
			if (base.Quest == null && The.Game.TryGetQuest("The Golem", out quest))
			{
				base.Quest = quest;
			}
			if (base.Quest == null || base.Quest.Finished)
			{
				return;
			}
			StringBuilder stringBuilder = null;
			Quest value = QuestLoader.Loader.QuestsByID.GetValue("The Golem", null);
			foreach (KeyValuePair<string, QuestStep> keyValuePair in base.Quest.StepsByID)
			{
				GolemQuestSelection golemQuestSelection;
				if (this.Selections.TryGetValue(keyValuePair.Key, out golemQuestSelection))
				{
					if (golemQuestSelection.IsValid())
					{
						if (stringBuilder == null)
						{
							stringBuilder = Event.NewStringBuilder(null);
						}
						stringBuilder.Clear().Append(golemQuestSelection.GetOptionChoice());
						golemQuestSelection.AppendEffects(stringBuilder, 0);
						keyValuePair.Value.Text = stringBuilder.ToString();
						keyValuePair.Value.Collapse = false;
						The.Game.FinishQuestStep("The Golem", keyValuePair.Key, -1, false, null);
					}
					else if (keyValuePair.Value.Finished)
					{
						keyValuePair.Value.XP = 0;
						keyValuePair.Value.Finished = false;
						keyValuePair.Value.Text = value.StepsByID.GetValue(keyValuePair.Key, null).Text;
					}
				}
			}
		}

		// Token: 0x04003549 RID: 13641
		public const string QUEST_ID = "The Golem";

		// Token: 0x0400354A RID: 13642
		[NonSerialized]
		public Dictionary<string, GolemQuestSelection> Selections = new Dictionary<string, GolemQuestSelection>
		{
			{
				"Body",
				new GolemBodySelection()
			},
			{
				"Catalyst",
				new GolemCatalystSelection()
			},
			{
				"Atzmus",
				new GolemAtzmusSelection()
			},
			{
				"Armament",
				new GolemArmamentSelection()
			},
			{
				"Incantation",
				new GolemIncantationSelection()
			},
			{
				"Hamsa",
				new GolemHamsaSelection()
			},
			{
				"Power",
				new GolemAscensionSelection()
			}
		};

		// Token: 0x0400354B RID: 13643
		[NonSerialized]
		public GolemQuestMound Mound;

		// Token: 0x0400354C RID: 13644
		[NonSerialized]
		private List<GameObject> _ValidHolders = new List<GameObject>(8);
	}
}
