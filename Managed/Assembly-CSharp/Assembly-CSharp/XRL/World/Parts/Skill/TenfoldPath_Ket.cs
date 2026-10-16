using System;

namespace XRL.World.Parts.Skill
{
	/// This part is not used in the base game.
	// Token: 0x020013B0 RID: 5040
	[Serializable]
	public class TenfoldPath_Ket : BaseInitiatorySkill
	{
		// Token: 0x0600C2A9 RID: 49833 RVA: 0x00471BEF File Offset: 0x0046FDEF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDieEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetPsionicSifrahSetupEvent>.ID;
		}

		// Token: 0x0600C2AA RID: 49834 RVA: 0x00471C15 File Offset: 0x0046FE15
		public override bool HandleEvent(GetPsionicSifrahSetupEvent E)
		{
			E.Rating++;
			E.Turns++;
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2AB RID: 49835 RVA: 0x00471C3C File Offset: 0x0046FE3C
		public override bool HandleEvent(BeforeDieEvent E)
		{
			if (this.Cooldown <= 0)
			{
				this.Cooldown = 100;
				if (base.Visible())
				{
					if (this.ParentObject.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("You feel a sense of infinite grace flow through your being as you are brought from the brink of death to miraculous health.", "white", true);
					}
					else
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							this.ParentObject.The,
							this.ParentObject.ShortDisplayName,
							"{{white|",
							this.ParentObject.GetVerb("shine", true, false, false, true, false),
							" with a supernal light}} as ",
							this.ParentObject.its,
							" injuries disappear."
						}), null, true);
					}
				}
				this.ParentObject.RestorePristineHealth(true, false);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2AC RID: 49836 RVA: 0x00471D09 File Offset: 0x0046FF09
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.Cooldown > 0)
			{
				this.Cooldown--;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004DA3 RID: 19875
		public const int COOLDOWN = 100;

		// Token: 0x04004DA4 RID: 19876
		public int Cooldown;
	}
}
