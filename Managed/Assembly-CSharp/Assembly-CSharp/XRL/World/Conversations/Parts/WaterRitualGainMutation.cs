using System;
using System.Linq;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C25 RID: 3109
	public class WaterRitualGainMutation : IWaterRitualPart
	{
		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06007C63 RID: 31843 RVA: 0x002EB98C File Offset: 0x002E9B8C
		public string DisplayName
		{
			get
			{
				MutationEntry entry = this.Entry;
				return ((entry != null) ? entry.GetDisplayName(false) : null) ?? this.Mutation;
			}
		}

		// Token: 0x06007C64 RID: 31844 RVA: 0x002EB9AB File Offset: 0x002E9BAB
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007C65 RID: 31845 RVA: 0x002EB9CC File Offset: 0x002E9BCC
		public override void Awake()
		{
			if (WaterRitual.Record.Has("SoldMutation"))
			{
				return;
			}
			if (The.Speaker.HasProperty("WaterRitualNoSellMutation"))
			{
				return;
			}
			this.Mutation = The.Speaker.GetStringProperty("WaterRitual_Mutation", null);
			if (this.Mutation == null)
			{
				this.Mutation = The.Speaker.GetxTag("WaterRitual", "SellMutation", null);
			}
			if (this.Mutation.IsNullOrEmpty())
			{
				if (WaterRitual.RecordFaction.WaterRitualMutation.IsNullOrEmpty())
				{
					return;
				}
				this.Mutation = WaterRitual.RecordFaction.WaterRitualMutation;
				this.Entry = MutationFactory.GetMutationEntryByName(this.Mutation);
				if (this.Entry == null)
				{
					this.Entry = MutationFactory.GetMutationEntries(this.Mutation).FirstOrDefault<MutationEntry>();
				}
				this.Reputation = WaterRitual.RecordFaction.WaterRitualMutationCost;
			}
			else
			{
				this.Entry = MutationFactory.GetMutationEntryByName(this.Mutation);
				if (this.Entry == null)
				{
					this.Entry = MutationFactory.GetMutationEntries(this.Mutation).FirstOrDefault<MutationEntry>();
				}
				MutationEntry entry = this.Entry;
				this.Reputation = ((entry != null) ? (entry.Cost * 100) : 100);
			}
			GameObject player = The.Player;
			MutationEntry entry2 = this.Entry;
			if (player.HasPart(((entry2 != null) ? entry2.Class : null) ?? this.Mutation))
			{
				return;
			}
			this.Reputation = GetWaterRitualCostEvent.GetFor(The.Player, The.Speaker, "Mutation", this.Reputation);
			this.Visible = true;
		}

		// Token: 0x06007C66 RID: 31846 RVA: 0x002EBB44 File Offset: 0x002E9D44
		public override bool HandleEvent(EnteredElementEvent E)
		{
			if (base.UseReputation("WaterRitualUse"))
			{
				Popup.Show(string.Concat(new string[]
				{
					"Despite your genetic limitations, ",
					The.Speaker.does("teach", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false),
					" you to improvise {{M|",
					this.DisplayName,
					"}}!"
				}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				Mutations mutations = The.Player.RequirePart<Mutations>(false);
				if (this.Entry != null)
				{
					mutations.AddMutation(this.Entry, 1);
				}
				else
				{
					mutations.AddMutation(this.Mutation, 1);
				}
				WaterRitual.Record.attributes.Add("SoldMutation");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007C67 RID: 31847 RVA: 0x002EBC20 File Offset: 0x002E9E20
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = string.Concat(new string[]
			{
				"{{",
				this.Lowlight,
				"|[gain {{M|",
				this.DisplayName,
				"}}: {{",
				this.Numeric,
				"|",
				base.GetReputationCost("WaterRitualUse").ToString(),
				"}} reputation]}}"
			});
			return false;
		}

		// Token: 0x0400388A RID: 14474
		public string Mutation;

		// Token: 0x0400388B RID: 14475
		public MutationEntry Entry;
	}
}
