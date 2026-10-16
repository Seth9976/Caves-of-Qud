using System;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C21 RID: 3105
	public class WaterRitualBuyItem : IWaterRitualPart
	{
		// Token: 0x170009EC RID: 2540
		// (set) Token: 0x06007C42 RID: 31810 RVA: 0x002EABF8 File Offset: 0x002E8DF8
		public string Source
		{
			set
			{
				string text = value.ToLowerInvariant();
				int num;
				if (!(text == "faction"))
				{
					if (!(text == "valued"))
					{
						num = 0;
					}
					else
					{
						num = 2;
					}
				}
				else
				{
					num = 1;
				}
				this.Type = num;
			}
		}

		// Token: 0x06007C43 RID: 31811 RVA: 0x002EAC39 File Offset: 0x002E8E39
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == PrepareTextEvent.ID || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007C44 RID: 31812 RVA: 0x002EAC60 File Offset: 0x002E8E60
		public bool SetFactionItem()
		{
			string text = (WaterRitual.Alternative ? WaterRitual.RecordFaction.WaterRitualAltItemBlueprint : WaterRitual.RecordFaction.WaterRitualItemBlueprint);
			if (text.IsNullOrEmpty() || WaterRitual.Record.numItems < 1)
			{
				return false;
			}
			this.Item = The.Speaker.HasItemWithBlueprint(text);
			if (this.Item == null && WaterRitual.Record.canGenerateItem)
			{
				The.Speaker.ReceiveObject(text, false, 0, 0, null, null, null, null);
				this.Item = The.Speaker.HasItemWithBlueprint(text);
			}
			this.Reputation = (WaterRitual.Alternative ? WaterRitual.RecordFaction.WaterRitualAltItemCost : WaterRitual.RecordFaction.WaterRitualItemCost);
			WaterRitual.Record.canGenerateItem = false;
			return this.Item != null;
		}

		// Token: 0x06007C45 RID: 31813 RVA: 0x002EAD24 File Offset: 0x002E8F24
		public override void Awake()
		{
			if (this.Type == 1)
			{
				if (!this.SetFactionItem())
				{
					return;
				}
			}
			else if (this.Type == 2)
			{
				if (WaterRitual.Record.numGifts < 1 || !WaterRitual.RecordFaction.WaterRitualBuyMostValuableItem)
				{
					return;
				}
				this.Item = The.Speaker.GetMostValuableItem();
				if (!GameObject.Validate(ref this.Item))
				{
					return;
				}
			}
			if (this.Reputation <= 0)
			{
				this.Reputation = 5;
				Commerce commerce;
				if (this.Item.TryGetPart<Commerce>(out commerce))
				{
					this.Reputation = Math.Max(5, (int)(commerce.Value / 4.0));
				}
			}
			this.Reputation = GetWaterRitualCostEvent.GetFor(The.Player, The.Speaker, "Item", this.Reputation);
			this.Visible = true;
		}

		// Token: 0x06007C46 RID: 31814 RVA: 0x002EADE7 File Offset: 0x002E8FE7
		public override bool HandleEvent(PrepareTextEvent E)
		{
			E.Object = this.Item;
			return base.HandleEvent(E);
		}

		// Token: 0x06007C47 RID: 31815 RVA: 0x002EADFC File Offset: 0x002E8FFC
		public override bool HandleEvent(EnteredElementEvent E)
		{
			if (base.UseReputation("WaterRitualUse"))
			{
				this.Item = this.Item.RemoveOne();
				this.Item.UnequipAndRemove();
				Popup.Show(The.Speaker.Does("gift", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " you " + this.Item.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				The.Player.Inventory.AddObject(this.Item, null, false, false, true, null);
				if (this.Type == 1)
				{
					WaterRitual.Record.numItems--;
				}
				else if (this.Type == 2)
				{
					WaterRitual.Record.numGifts--;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007C48 RID: 31816 RVA: 0x002EAF0C File Offset: 0x002E910C
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = string.Concat(new string[]
			{
				"{{",
				this.Lowlight,
				"|[{{",
				this.Numeric,
				"|",
				base.GetReputationCost("WaterRitualUse").ToString(),
				"}} reputation]}}"
			});
			return false;
		}

		// Token: 0x04003880 RID: 14464
		public const int TYPE_FACTION = 1;

		// Token: 0x04003881 RID: 14465
		public const int TYPE_VALUED = 2;

		// Token: 0x04003882 RID: 14466
		public GameObject Item;

		// Token: 0x04003883 RID: 14467
		public int Type;
	}
}
