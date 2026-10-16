using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001327 RID: 4903
	[Serializable]
	public class CookingAndGathering_Harvestry : BaseSkill
	{
		// Token: 0x0600BFBD RID: 49085 RVA: 0x004637BA File Offset: 0x004619BA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AutoexploreObjectEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600BFBE RID: 49086 RVA: 0x004637D8 File Offset: 0x004619D8
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("salt", 3);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BFBF RID: 49087 RVA: 0x004637FC File Offset: 0x004619FC
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			Harvestable harvestable;
			if (!E.AutogetOnlyMode && E.Command == null && base.IsMyActivatedAbilityToggledOn(this.ActivatedAbilityID, null) && E.Item.TryGetPart<Harvestable>(out harvestable) && harvestable.IsHarvestable() && !E.Item.IsImportant())
			{
				E.Command = "Harvest";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BFC0 RID: 49088 RVA: 0x0046385E File Offset: 0x00461A5E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandHarvestToggle");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BFC1 RID: 49089 RVA: 0x00463874 File Offset: 0x00461A74
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandHarvestToggle")
			{
				base.ToggleMyActivatedAbility(this.ActivatedAbilityID, null, false, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BFC2 RID: 49090 RVA: 0x004638B4 File Offset: 0x00461AB4
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Harvest Plants", "CommandHarvestToggle", "Skills", "Toggle to enable or disable the harvesting of plants", "h", null, true, true, false, false, false, true, false, false, true, true, false, -1, null, null, null, null, null, null);
			return true;
		}

		// Token: 0x0600BFC3 RID: 49091 RVA: 0x004638FA File Offset: 0x00461AFA
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return true;
		}

		// Token: 0x04004D2A RID: 19754
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
