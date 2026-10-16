using System;
using System.Collections.Generic;
using XRL.World;

namespace XRL
{
	// Token: 0x020004E5 RID: 1253
	[Serializable]
	public abstract class IQuestSystem : IPlayerSystem
	{
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06003615 RID: 13845 RVA: 0x001047B4 File Offset: 0x001029B4
		// (set) Token: 0x06003616 RID: 13846 RVA: 0x001047E9 File Offset: 0x001029E9
		public Quest Quest
		{
			get
			{
				Quest quest;
				if ((quest = this._Quest) == null)
				{
					quest = (this._Quest = The.Game.Quests.GetValue(this.QuestID));
				}
				return quest;
			}
			set
			{
				this._Quest = value;
				this.QuestID = ((value != null) ? value.ID : null);
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06003617 RID: 13847 RVA: 0x00104804 File Offset: 0x00102A04
		public Quest Blueprint
		{
			get
			{
				Quest quest;
				if ((quest = this._Blueprint) == null)
				{
					quest = (this._Blueprint = QuestLoader.Loader.QuestsByID.GetValue(this.QuestID, null));
				}
				return quest;
			}
		}

		/// <summary>Removes the system from the game when the quest concludes.</summary>
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06003618 RID: 13848 RVA: 0x0010483A File Offset: 0x00102A3A
		public virtual bool RemoveWithQuest
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x0010483D File Offset: 0x00102A3D
		public virtual void ShowStartPopup()
		{
			this.Quest.ShowStartPopup();
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x0010484A File Offset: 0x00102A4A
		public virtual void Start()
		{
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x0010484C File Offset: 0x00102A4C
		public virtual void ShowFailPopup()
		{
			this.Quest.ShowFailPopup();
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x00104859 File Offset: 0x00102A59
		public virtual void Fail()
		{
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x0010485B File Offset: 0x00102A5B
		public virtual void ShowFailStepPopup(QuestStep Step)
		{
			this.Quest.ShowFailStepPopup(Step);
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x00104869 File Offset: 0x00102A69
		public virtual void FailStep(QuestStep Step)
		{
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x0010486B File Offset: 0x00102A6B
		public virtual void ShowFinishPopup()
		{
			this.Quest.ShowFinishPopup();
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x00104878 File Offset: 0x00102A78
		public virtual void Finish()
		{
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x0010487A File Offset: 0x00102A7A
		public virtual void ShowFinishStepPopup(QuestStep Step)
		{
			this.Quest.ShowFinishStepPopup(Step);
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x00104888 File Offset: 0x00102A88
		public virtual void FinishStep(QuestStep Step)
		{
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x0010488A File Offset: 0x00102A8A
		public virtual GameObject GetInfluencer()
		{
			return null;
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x0010488D File Offset: 0x00102A8D
		public string GetProperty(string Key, string Default = null)
		{
			Quest quest = this.Quest;
			return ((quest != null) ? quest.GetProperty(Key, Default) : null) ?? this.Blueprint.GetProperty(Key, Default);
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x001048B4 File Offset: 0x00102AB4
		public int GetProperty(string Key, int Default)
		{
			Quest quest = this.Quest;
			if (quest == null)
			{
				return this.Blueprint.GetProperty(Key, Default);
			}
			return quest.GetProperty(Key, Default);
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x001048D5 File Offset: 0x00102AD5
		public float GetProperty(string Key, float Default)
		{
			Quest quest = this.Quest;
			if (quest == null)
			{
				return this.Blueprint.GetProperty(Key, Default);
			}
			return quest.GetProperty(Key, Default);
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x001048F8 File Offset: 0x00102AF8
		public T GetProperty<T>(string Key, T Default = default(T)) where T : class
		{
			Quest quest = this.Quest;
			T t;
			if ((t = ((quest != null) ? quest.GetProperty<T>(Key, Default) : default(T))) == null)
			{
				t = this.Blueprint.GetProperty<T>(Key, Default);
			}
			return t;
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x00104937 File Offset: 0x00102B37
		public List<string> GetList(string Key)
		{
			Quest quest = this.Quest;
			return ((quest != null) ? quest.GetList(Key) : null) ?? this.Blueprint.GetList(Key);
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x0010495C File Offset: 0x00102B5C
		public List<T> GetList<T>(string Key)
		{
			Quest quest = this.Quest;
			return ((quest != null) ? quest.GetList<T>(Key) : null) ?? this.Blueprint.GetList<T>(Key);
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x00104981 File Offset: 0x00102B81
		public Dictionary<string, string> GetDictionary(string Key)
		{
			Quest quest = this.Quest;
			return ((quest != null) ? quest.GetDictionary(Key) : null) ?? this.Blueprint.GetDictionary(Key);
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x001049A6 File Offset: 0x00102BA6
		public Dictionary<string, T> GetDictionary<T>(string Key)
		{
			Quest quest = this.Quest;
			return ((quest != null) ? quest.GetDictionary<T>(Key) : null) ?? this.Blueprint.GetDictionary<T>(Key);
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x001049CB File Offset: 0x00102BCB
		public void SetProperty(string Key, object Value)
		{
			Quest quest = this.Quest;
			if (quest == null)
			{
				return;
			}
			quest.SetProperty(Key, Value);
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x001049DF File Offset: 0x00102BDF
		public void SetProperty(string Key, int Value)
		{
			Quest quest = this.Quest;
			if (quest == null)
			{
				return;
			}
			quest.SetProperty(Key, Value);
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x001049F3 File Offset: 0x00102BF3
		public void SetProperty(string Key, float Value)
		{
			Quest quest = this.Quest;
			if (quest == null)
			{
				return;
			}
			quest.SetProperty(Key, Value);
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x00104A07 File Offset: 0x00102C07
		public bool RemoveProperty(string Key)
		{
			Quest quest = this.Quest;
			return quest != null && quest.RemoveProperty(Key);
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x00104A1B File Offset: 0x00102C1B
		public bool RemoveNumericProperty(string Key)
		{
			Quest quest = this.Quest;
			return quest != null && quest.RemoveNumericProperty(Key);
		}

		// Token: 0x04001B2C RID: 6956
		public string QuestID;

		// Token: 0x04001B2D RID: 6957
		[NonSerialized]
		private Quest _Quest;

		// Token: 0x04001B2E RID: 6958
		[NonSerialized]
		private Quest _Blueprint;
	}
}
