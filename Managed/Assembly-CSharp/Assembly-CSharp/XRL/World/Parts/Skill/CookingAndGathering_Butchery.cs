using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001324 RID: 4900
	[Serializable]
	public class CookingAndGathering_Butchery : BaseSkill
	{
		// Token: 0x0600BFAB RID: 49067 RVA: 0x0046352E File Offset: 0x0046172E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AutoexploreObjectEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600BFAC RID: 49068 RVA: 0x0046354C File Offset: 0x0046174C
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("salt", 3);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BFAD RID: 49069 RVA: 0x00463570 File Offset: 0x00461770
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (!E.AutogetOnlyMode && E.Command == null && base.IsMyActivatedAbilityToggledOn(this.ActivatedAbilityID, null))
			{
				Butcherable part = E.Item.GetPart<Butcherable>();
				CyberneticsButcherableCybernetic part2 = E.Item.GetPart<CyberneticsButcherableCybernetic>();
				if (((part != null && part.IsButcherable()) || (part2 != null && part2.IsButcherable())) && !E.Item.IsImportant())
				{
					E.Command = "Butcher";
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BFAE RID: 49070 RVA: 0x004635E9 File Offset: 0x004617E9
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandButcherToggle");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BFAF RID: 49071 RVA: 0x00463600 File Offset: 0x00461800
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandButcherToggle")
			{
				base.ToggleMyActivatedAbility(this.ActivatedAbilityID, null, false, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BFB0 RID: 49072 RVA: 0x00463640 File Offset: 0x00461840
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Butcher Corpses", "CommandButcherToggle", "Skills", null, "b", null, true, true, true, false, false, true, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600BFB1 RID: 49073 RVA: 0x00463688 File Offset: 0x00461888
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x04004D29 RID: 19753
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
