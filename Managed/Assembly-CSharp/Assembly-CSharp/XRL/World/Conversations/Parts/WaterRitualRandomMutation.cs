using System;
using System.Collections.Generic;
using System.Linq;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C2A RID: 3114
	public class WaterRitualRandomMutation : IWaterRitualPart
	{
		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06007C88 RID: 31880 RVA: 0x002EC7A7 File Offset: 0x002EA9A7
		public override bool Available
		{
			get
			{
				return this.Mutant && !this.Incompatible;
			}
		}

		// Token: 0x06007C89 RID: 31881 RVA: 0x002EC7BC File Offset: 0x002EA9BC
		public override void Awake()
		{
			if (WaterRitual.Alternative || WaterRitual.RecordFaction.WaterRitualRandomMentalMutation <= 0)
			{
				return;
			}
			if (WaterRitual.Record.Has("SoldRandom" + this.Category + "Mutation"))
			{
				return;
			}
			string text = "Random" + this.Category + "Mutation:";
			string text2;
			if (WaterRitual.Record.TryGetAttribute(text, out text2))
			{
				this.Entry = MutationFactory.GetMutationEntryByName(text2);
				if (this.Entry != null && The.Player.HasPart(this.Entry.Class))
				{
					WaterRitual.Record.attributes.Remove(text + this.Entry.Name);
					this.Entry = null;
				}
			}
			if (this.Entry == null)
			{
				List<MutationEntry> list = new List<MutationEntry>(from e in The.Player.GetPart<Mutations>().GetMutatePool(null, false)
					where e.Category.Name == this.Category
					select e);
				if (list.IsNullOrEmpty<MutationEntry>())
				{
					return;
				}
				this.Entry = list.GetRandomElement<MutationEntry>(null);
				WaterRitual.Record.attributes.Add(text + this.Entry.Name);
			}
			if (The.Player.Property.TryGetValue("MutationLevel", out text2))
			{
				MutationEntry mutationEntryByName = MutationFactory.GetMutationEntryByName(text2);
				this.Incompatible = mutationEntryByName != null && !mutationEntryByName.OkWith(this.Entry, true, false);
			}
			this.Reputation = GetWaterRitualCostEvent.GetFor(The.Player, The.Speaker, "Mutation", WaterRitual.RecordFaction.WaterRitualRandomMentalMutation * this.Entry.Cost);
			this.Mutant = The.Player.IsMutant();
			this.Visible = true;
		}

		// Token: 0x06007C8A RID: 31882 RVA: 0x002EC961 File Offset: 0x002EAB61
		public override void LoadText(string Text)
		{
			this.Message = Text;
		}

		// Token: 0x06007C8B RID: 31883 RVA: 0x002EC96A File Offset: 0x002EAB6A
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007C8C RID: 31884 RVA: 0x002EC988 File Offset: 0x002EAB88
		public override bool HandleEvent(EnteredElementEvent E)
		{
			if (!this.Mutant)
			{
				Popup.ShowFail("You can't be mutated.", true, true, true);
			}
			else if (this.Incompatible)
			{
				Popup.ShowFail("You can't gain " + this.Category.ToLowerInvariant() + " mutations.", true, true, true);
			}
			else if (base.UseReputation("WaterRitualUse"))
			{
				if (!this.Message.IsNullOrEmpty())
				{
					Popup.Show(this.Message.StartReplace().AddObject(The.Speaker, null).AddReplacer("mutation.name", "{{M|" + this.Entry.Name + "}}")
						.StripColors()
						.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				The.Player.GetPart<Mutations>().AddMutation(this.Entry.CreateInstance(), 1, true);
				WaterRitual.Record.attributes.Add("SoldRandom" + this.Category + "Mutation");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007C8D RID: 31885 RVA: 0x002ECA98 File Offset: 0x002EAC98
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = string.Concat(new string[]
			{
				"{{",
				this.Lowlight,
				"|[gain {{M|",
				this.Entry.Name,
				"}}: {{",
				this.Numeric,
				"|",
				base.GetReputationCost("WaterRitualUse").ToString(),
				"}} reputation]}}"
			});
			return false;
		}

		// Token: 0x04003894 RID: 14484
		public MutationEntry Entry;

		// Token: 0x04003895 RID: 14485
		public string Category = "Physical";

		// Token: 0x04003896 RID: 14486
		public string Message;

		// Token: 0x04003897 RID: 14487
		public bool Mutant;

		// Token: 0x04003898 RID: 14488
		public bool Incompatible;
	}
}
