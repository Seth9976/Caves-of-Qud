using System;
using XRL.Collections;
using XRL.Language;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000BFE RID: 3070
	public class HaveItem : IConversationPart
	{
		// Token: 0x06007B57 RID: 31575 RVA: 0x002E5743 File Offset: 0x002E3943
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || (this.Require && ID == EnterElementEvent.ID) || (!this.Require && ID == IsElementVisibleEvent.ID);
		}

		// Token: 0x06007B58 RID: 31576 RVA: 0x002E5773 File Offset: 0x002E3973
		public override bool HandleEvent(IsElementVisibleEvent E)
		{
			return this.PlayerHasAmount() && base.HandleEvent(E);
		}

		// Token: 0x06007B59 RID: 31577 RVA: 0x002E5786 File Offset: 0x002E3986
		public override bool HandleEvent(EnterElementEvent E)
		{
			return this.PlayerHasAmount() && base.HandleEvent(E);
		}

		// Token: 0x06007B5A RID: 31578 RVA: 0x002E579C File Offset: 0x002E399C
		public bool PlayerHasAmount()
		{
			bool flag;
			using (ScopeDisposedList<GameObject> fromPool = ScopeDisposedList<GameObject>.GetFromPool())
			{
				The.Player.GetContents(fromPool);
				string blueprints = this.Blueprints;
				string[] array = ((blueprints != null) ? blueprints.Split(',', StringSplitOptions.None) : null) ?? new string[0];
				string ids = this.IDs;
				string[] array2 = ((ids != null) ? ids.Split(',', StringSplitOptions.None) : null) ?? new string[0];
				int num = this.Amount;
				int num2 = 0;
				int count = fromPool.Count;
				while (num2 < count && num > 0)
				{
					GameObject gameObject = fromPool[num2];
					if ((array.Contains(gameObject.Blueprint) || array2.Contains(gameObject.GetStringProperty("id", null))) && (this.AllowTemporary || !gameObject.IsTemporary))
					{
						num -= gameObject.Count;
						if (num <= 0)
						{
							return true;
						}
					}
					num2++;
				}
				if (this.Require && array.Length != 0)
				{
					GameObject gameObject2 = GameObjectFactory.Factory.CreateSampleObject(array[0], null);
					The.Player.ShowFailure((this.Amount > 1) ? ("You do not have enough " + Grammar.Pluralize(gameObject2.DisplayNameOnlyDirectAndStripped) + ".") : ("You do not have " + gameObject2.an(int.MaxValue, null, null, false, true, false, false, true, false, true, false, false, true, false, null, false, null, false) + "."));
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x04003821 RID: 14369
		public string Blueprints;

		// Token: 0x04003822 RID: 14370
		public string IDs;

		// Token: 0x04003823 RID: 14371
		public int Amount = 1;

		// Token: 0x04003824 RID: 14372
		public bool AllowTemporary;

		// Token: 0x04003825 RID: 14373
		public bool Require;
	}
}
