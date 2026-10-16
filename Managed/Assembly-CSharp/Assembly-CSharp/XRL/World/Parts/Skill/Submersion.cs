using System;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200132C RID: 4908
	[Serializable]
	public class Submersion : BaseSkill
	{
		// Token: 0x0600BFE4 RID: 49124 RVA: 0x0046495D File Offset: 0x00462B5D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x0600BFE5 RID: 49125 RVA: 0x0046497C File Offset: 0x00462B7C
		public override bool HandleEvent(AIBoredEvent E)
		{
			if (!this.ParentObject.HasEffect<Submerged>() && Submerged.CanSubmerge(this.ParentObject) && this.ParentObject.CanChangeBodyPosition("Submerge", false, false, false, false) && base.IsMyActivatedAbilityVoluntarilyUsable(this.ActivatedAbilityID, null))
			{
				this.ParentObject.ApplyEffect(new Submerged(), null);
				this.ParentObject.UseEnergy(1000, "Skill Submersion Submerge", null, null, false);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BFE6 RID: 49126 RVA: 0x00464A04 File Offset: 0x00462C04
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "CommandSubmerge")
			{
				if (this.ParentObject.HasEffect<Submerged>())
				{
					if (!this.ParentObject.CanChangeBodyPosition("Surface", true, false, false, false))
					{
						return false;
					}
					this.ParentObject.RemoveEffect<Submerged>();
					this.ParentObject.UseEnergy(1000, "Skill Submersion Submerge", null, null, false);
				}
				else
				{
					if (!this.ParentObject.CanChangeBodyPosition("Submerge", true, false, false, false))
					{
						return false;
					}
					if (!Submerged.CanSubmerge(this.ParentObject))
					{
						if (this.ParentObject.IsPlayer())
						{
							Cell currentCell = this.ParentObject.CurrentCell;
							GameObject gameObject = ((currentCell != null) ? currentCell.GetOpenLiquidVolume() : null);
							if (gameObject == null)
							{
								Popup.ShowFail("There is nothing for you to submerge in here.", true, true, true);
							}
							else
							{
								Popup.ShowFail(gameObject.The + gameObject.ShortDisplayName + gameObject.Is + " too shallow for you to submerge in.", true, true, true);
							}
						}
						return false;
					}
					if (!base.IsMyActivatedAbilityVoluntarilyUsable(this.ActivatedAbilityID, null))
					{
						if (this.ParentObject.IsPlayer())
						{
							Popup.ShowFail("You cannot do that right now.", true, true, true);
						}
						return false;
					}
					this.ParentObject.ApplyEffect(new Submerged(), null);
					this.ParentObject.UseEnergy(1000, "Skill Submersion Submerge", null, null, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BFE7 RID: 49127 RVA: 0x00464B64 File Offset: 0x00462D64
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Submerge", "CommandSubmerge", "Maneuvers", null, "÷", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600BFE8 RID: 49128 RVA: 0x00464BAC File Offset: 0x00462DAC
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x04004D3A RID: 19770
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
