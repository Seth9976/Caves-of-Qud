using System;

namespace XRL.World.Parts
{
	// Token: 0x02001232 RID: 4658
	[Serializable]
	public class SingleWeaponFighting_Ability : ISupportAbilitySkillPart
	{
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x0600B906 RID: 47366 RVA: 0x004388FC File Offset: 0x00436AFC
		public override string AbilityCommand
		{
			get
			{
				return "CommandSingleWeaponFighting";
			}
		}

		// Token: 0x0600B907 RID: 47367 RVA: 0x00438904 File Offset: 0x00436B04
		public override ActivatedAbilityEntry RequireAbility()
		{
			if (this.AbilityEntry != null)
			{
				return this.AbilityEntry;
			}
			Guid guid = this.ParentObject.AddActivatedAbility("Single Weapon Fighting", this.AbilityCommand, "Skills", null, "ô", null, true, true, false, false, false, true, false, false, true, true, false, false, -1, null, null, null, null, null);
			return this.ParentObject.GetActivatedAbility(guid);
		}

		// Token: 0x0600B908 RID: 47368 RVA: 0x00438961 File Offset: 0x00436B61
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(PooledEvent<GetMeleeAttackChanceEvent>.ID, EventOrder.VERY_EARLY, false);
		}

		// Token: 0x0600B909 RID: 47369 RVA: 0x00438974 File Offset: 0x00436B74
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x0600B90A RID: 47370 RVA: 0x0043898C File Offset: 0x00436B8C
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == this.AbilityCommand)
			{
				this.ParentObject.ToggleActivatedAbility(this.AbilityEntry.ID, false, null);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B90B RID: 47371 RVA: 0x004389D6 File Offset: 0x00436BD6
		public override bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			if (this.AbilityEntry.ToggleState && E.Intrinsic && !E.Primary)
			{
				E.Multiplier = 0.0;
				if (E.Inherited)
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}
	}
}
