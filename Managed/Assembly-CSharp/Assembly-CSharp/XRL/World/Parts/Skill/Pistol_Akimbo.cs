using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001366 RID: 4966
	[Serializable]
	public class Pistol_Akimbo : BaseSkill
	{
		// Token: 0x0600C144 RID: 49476 RVA: 0x0046B5B6 File Offset: 0x004697B6
		public override void Attach()
		{
			if (this.ActivatedAbilityID == Guid.Empty)
			{
				this.AddAbility();
			}
			base.Attach();
		}

		// Token: 0x0600C145 RID: 49477 RVA: 0x0046B5D6 File Offset: 0x004697D6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanFireAllMissileWeaponsEvent>.ID || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x0600C146 RID: 49478 RVA: 0x0046B5F4 File Offset: 0x004697F4
		public override bool HandleEvent(CanFireAllMissileWeaponsEvent E)
		{
			return !base.IsMyActivatedAbilityToggledOn(this.ActivatedAbilityID, null) && base.HandleEvent(E);
		}

		// Token: 0x0600C147 RID: 49479 RVA: 0x0046B610 File Offset: 0x00469810
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "CommandToggleAkimbo")
			{
				base.ToggleMyActivatedAbility(this.ActivatedAbilityID, null, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C148 RID: 49480 RVA: 0x0046B64E File Offset: 0x0046984E
		public override bool AddSkill(GameObject Object)
		{
			this.AddAbility();
			return base.AddSkill(Object);
		}

		// Token: 0x0600C149 RID: 49481 RVA: 0x0046B65D File Offset: 0x0046985D
		public override bool RemoveSkill(GameObject Object)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(Object);
		}

		// Token: 0x0600C14A RID: 49482 RVA: 0x0046B674 File Offset: 0x00469874
		public void AddAbility()
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Akimbo", "CommandToggleAkimbo", "Skills", null, "\u001d", null, true, true, false, false, false, true, false, false, true, true, false, -1, null, null, null, null, null, null);
		}

		// Token: 0x04004D71 RID: 19825
		public Guid ActivatedAbilityID;
	}
}
