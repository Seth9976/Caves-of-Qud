using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x0200117F RID: 4479
	[Serializable]
	public abstract class ModImprovedMutationBase<T> : IModification where T : BaseMutation, new()
	{
		// Token: 0x0600B220 RID: 45600 RVA: 0x004133F8 File Offset: 0x004115F8
		public ModImprovedMutationBase()
		{
		}

		// Token: 0x0600B221 RID: 45601 RVA: 0x00413400 File Offset: 0x00411600
		public ModImprovedMutationBase(int Tier)
			: base(Tier)
		{
			this.Tier = Tier;
		}

		// Token: 0x0600B222 RID: 45602 RVA: 0x00413410 File Offset: 0x00411610
		public override int GetModificationSlotUsage()
		{
			return 0;
		}

		// Token: 0x0600B223 RID: 45603 RVA: 0x00413414 File Offset: 0x00411614
		public override void Configure()
		{
			T t = new T();
			this.MutationDisplayName = t.GetDisplayName(true);
			this.ClassName = t.Name;
			this.TrackingProperty = "Equipped" + this.ClassName;
			this.WorksOnEquipper = true;
			this.NameForStatus = this.ClassName + "Amp";
		}

		// Token: 0x0600B224 RID: 45604 RVA: 0x0041347D File Offset: 0x0041167D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == ImplantedEvent.ID || ID == GetShortDescriptionEvent.ID || ID == UnequippedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x0600B225 RID: 45605 RVA: 0x004134B4 File Offset: 0x004116B4
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(string.Concat(new string[]
			{
				"Grants you ",
				this.MutationDisplayName,
				" at level ",
				this.Tier.ToString(),
				". If you already have ",
				this.MutationDisplayName,
				", its level is increased by ",
				this.Tier.ToString(),
				"."
			}));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B226 RID: 45606 RVA: 0x00413538 File Offset: 0x00411738
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.ParentObject.IsEquippedProperly(null))
			{
				this.mutationMod = E.Actor.RequirePart<Mutations>(false).AddMutationMod(typeof(T), null, this.Tier, Mutations.MutationModifierTracker.SourceType.Equipment, this.ParentObject.DisplayName);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B227 RID: 45607 RVA: 0x0041358E File Offset: 0x0041178E
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.RequirePart<Mutations>(false).RemoveMutationMod(this.mutationMod);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B228 RID: 45608 RVA: 0x004135B0 File Offset: 0x004117B0
		public override bool HandleEvent(ImplantedEvent E)
		{
			if (this.ParentObject.IsEquippedProperly(null))
			{
				this.mutationMod = E.Implantee.RequirePart<Mutations>(false).AddMutationMod(typeof(T).Name, null, this.Tier, Mutations.MutationModifierTracker.SourceType.Equipment, this.ParentObject.DisplayName);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B229 RID: 45609 RVA: 0x0041360B File Offset: 0x0041180B
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RequirePart<Mutations>(false).RemoveMutationMod(this.mutationMod);
			return base.HandleEvent(E);
		}

		// Token: 0x0400482E RID: 18478
		public string MutationDisplayName;

		// Token: 0x0400482F RID: 18479
		public string ClassName;

		// Token: 0x04004830 RID: 18480
		public string TrackingProperty;

		// Token: 0x04004831 RID: 18481
		public int moddedAmount;

		// Token: 0x04004832 RID: 18482
		public Guid mutationMod;
	}
}
