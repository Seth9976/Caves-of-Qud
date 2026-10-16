using System;
using XRL.Collections;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C18 RID: 3096
	public class TakeItem : IConversationPart
	{
		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06007BF7 RID: 31735 RVA: 0x002E93F9 File Offset: 0x002E75F9
		// (set) Token: 0x06007BF8 RID: 31736 RVA: 0x002E9401 File Offset: 0x002E7601
		public bool Destroy
		{
			get
			{
				return this.Remove;
			}
			set
			{
				this.Remove = value;
			}
		}

		// Token: 0x06007BF9 RID: 31737 RVA: 0x002E940A File Offset: 0x002E760A
		public TakeItem()
		{
			this.Priority = -1000;
		}

		// Token: 0x06007BFA RID: 31738 RVA: 0x002E9436 File Offset: 0x002E7636
		public TakeItem(string Blueprints)
		{
			this.Blueprints = Blueprints;
		}

		// Token: 0x06007BFB RID: 31739 RVA: 0x002E945E File Offset: 0x002E765E
		public override void LoadText(string Text)
		{
			this.Message = Text;
		}

		// Token: 0x06007BFC RID: 31740 RVA: 0x002E9467 File Offset: 0x002E7667
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || (this.Require && ID == EnterElementEvent.ID) || (!this.Require && ID == EnteredElementEvent.ID);
		}

		// Token: 0x06007BFD RID: 31741 RVA: 0x002E9498 File Offset: 0x002E7698
		public override bool HandleEvent(EnterElementEvent E)
		{
			if (!this.Execute())
			{
				try
				{
					GameObject gameObject = (this.FromSpeaker ? The.Speaker : The.Player);
					GameObject gameObject2 = null;
					if (!this.Blueprints.IsNullOrEmpty())
					{
						gameObject2 = GameObject.CreateSample(this.Blueprints.GetRandomSubstring(',', false, null));
					}
					string text = this.Message.Coalesce("=subject.T= =verb:do= not have =object.an=.");
					if (gameObject2 != null || !text.Contains("=object"))
					{
						Popup.ShowFail(GameText.VariableReplace(text, gameObject, gameObject2, false), true, true, true);
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Require conversation item", ex, "game_exception");
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007BFE RID: 31742 RVA: 0x002E954C File Offset: 0x002E774C
		public override bool HandleEvent(EnteredElementEvent E)
		{
			this.Execute();
			return base.HandleEvent(E);
		}

		// Token: 0x06007BFF RID: 31743 RVA: 0x002E955C File Offset: 0x002E775C
		public bool Execute()
		{
			GameObject gameObject = (this.FromSpeaker ? The.Speaker : The.Player);
			GameObject gameObject2 = (this.FromSpeaker ? The.Player : The.Speaker);
			bool flag2;
			using (ScopeDisposedList<GameObject> fromPool = ScopeDisposedList<GameObject>.GetFromPool())
			{
				gameObject.GetContents(fromPool);
				string blueprints = this.Blueprints;
				string[] array = ((blueprints != null) ? blueprints.Split(',', StringSplitOptions.None) : null) ?? Array.Empty<string>();
				string ids = this.IDs;
				string[] array2 = ((ids != null) ? ids.Split(',', StringSplitOptions.None) : null) ?? Array.Empty<string>();
				bool flag = this.Amount == "*" || this.Amount.EqualsNoCase("all");
				int num = (flag ? int.MaxValue : this.Amount.RollCached());
				int num2 = 0;
				int count = fromPool.Count;
				while (num2 < count && num > 0)
				{
					GameObject gameObject3 = fromPool[num2];
					if ((array.Contains(gameObject3.Blueprint) || array2.Contains(gameObject3.GetStringProperty("id", null))) && (this.AllowTemporary || !gameObject3.IsTemporary))
					{
						int num3 = 1;
						Stacker stacker = gameObject3.Stacker;
						if (stacker != null && stacker != null)
						{
							num3 = stacker.Number;
							if (num3 > num)
							{
								stacker.SplitStack(num, The.Player, false);
								num3 = num;
							}
						}
						if (this.Remove)
						{
							if (!gameObject3.TryRemoveFromContext(null))
							{
								Popup.ShowFail("You cannot give " + gameObject3.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + "!", true, true, true);
								goto IL_02DF;
							}
						}
						else
						{
							if (!gameObject2.ReceiveObject(gameObject3, false, null))
							{
								Popup.ShowFail("You cannot give " + gameObject3.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + "!", true, true, true);
								gameObject.ReceiveObject(gameObject3, false, null);
								goto IL_02DF;
							}
							Popup.Show(gameObject2.Does("take", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " " + gameObject3.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, false, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							if (this.Unsellable)
							{
								gameObject3.SetIntProperty("WontSell", 1, false);
							}
							if (this.ClearQuest)
							{
								gameObject3.Physics.Category = gameObject3.GetStringProperty("OriginalCategory", null) ?? gameObject3.GetBlueprint(true).GetPartParameter<string>("Physics", "Category", "Miscellaneous");
								gameObject3.RemoveProperty("QuestItem");
								gameObject3.RemoveProperty("NoAIEquip");
							}
						}
						num -= num3;
					}
					IL_02DF:
					num2++;
				}
				flag2 = flag || num <= 0;
			}
			return flag2;
		}

		// Token: 0x04003856 RID: 14422
		public string Blueprints;

		// Token: 0x04003857 RID: 14423
		public string IDs;

		// Token: 0x04003858 RID: 14424
		public string Amount = "1";

		// Token: 0x04003859 RID: 14425
		public string Message;

		// Token: 0x0400385A RID: 14426
		public bool Unsellable = true;

		// Token: 0x0400385B RID: 14427
		public bool ClearQuest;

		// Token: 0x0400385C RID: 14428
		public bool Remove;

		// Token: 0x0400385D RID: 14429
		public bool AllowTemporary;

		// Token: 0x0400385E RID: 14430
		public bool Require = true;

		// Token: 0x0400385F RID: 14431
		public bool FromSpeaker;
	}
}
