using System;
using Qud.API;
using XRL.Language;
using XRL.UI;
using XRL.World.Encounters;

namespace XRL.World.Parts
{
	// Token: 0x02000E47 RID: 3655
	[Serializable]
	public class AbsorbablePsyche : IPart
	{
		// Token: 0x06009423 RID: 37923 RVA: 0x00355479 File Offset: 0x00353679
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009424 RID: 37924 RVA: 0x0035547C File Offset: 0x0035367C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDeathRemovalEvent.ID || ID == KilledEvent.ID || ID == ReplicaCreatedEvent.ID;
		}

		// Token: 0x06009425 RID: 37925 RVA: 0x003554A2 File Offset: 0x003536A2
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.WantToRemove(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009426 RID: 37926 RVA: 0x003554C0 File Offset: 0x003536C0
		public override bool HandleEvent(KilledEvent E)
		{
			if (E.Dying.GetPsychicGlimmer(null) >= DimensionManager.GLIMMER_FLOOR)
			{
				if (this.ParentObject.GetPrimaryFaction(false) == "Seekers")
				{
					E.Reason = "You were resorbed into the Mass Mind.";
					E.ThirdPersonReason = E.Dying.Does("were", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + " @@resorbed into the Mass Mind.";
				}
				else if (AbsorbablePsyche.ABSORB_CHANCE.in100())
				{
					E.Reason = "Your psyche exploded, and its psionic bits were encoded on the holographic boundary surrounding the psyche of " + this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false) + ".";
					E.ThirdPersonReason = E.Dying.Its + " psyche exploded, and its psionic bits were encoded on the holographic boundary surrounding the psyche of " + Grammar.MakePossessive(this.ParentObject.BaseDisplayName) + ".";
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009427 RID: 37927 RVA: 0x003555C0 File Offset: 0x003537C0
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (this.EgoBonus > 0)
			{
				GameObject killer = E.Killer;
				if (killer != null && killer.IsPlayer())
				{
					int egoBonus = this.EgoBonus;
					this.EgoBonus = 0;
					if (AbsorbablePsyche.ABSORB_CHANCE.in100())
					{
						if (Popup.ShowYesNo("At the moment of victory, your swelling ego curves the psychic aether and causes the psyche of " + this.ParentObject.GetReferenceDisplayName(2147483647, null, null, false, false, false, false, false, false, false, true, null, false) + " to collide with your own. As the weaker of the two, its binding energy is exceeded and it explodes. Would you like to encode its psionic bits on the holographic boundary of your own psyche?\n\n(+1 Ego permanently)", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
						{
							IComponent<GameObject>.ThePlayer.GetStat("Ego").BaseValue += egoBonus;
							Popup.Show("You encode the psyche of " + this.ParentObject.BaseDisplayName + " and gain +{{C|1}} {{Y|Ego}}!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							JournalAPI.AddAccomplishment("You slew " + this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, true, null, false) + " and encoded their psyche's psionic bits on the holographic boundary of your own psyche.", string.Concat(new string[]
							{
								"After a climactic battle of wills, =name= slew ",
								this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, true, null, false),
								" and absorbed ",
								this.ParentObject.its,
								" psyche, thickening toward Godhood."
							}), string.Format("In the month of {0} of {1}, =name= was challenged by <spice.commonPhrases.pretender.!random.article> to a duel over the rights of {2}. =name= won and had the pretender's psyche kibbled and absorbed into {3} own.", new object[]
							{
								Calendar.GetMonth(),
								Calendar.GetYear(),
								Factions.GetMostLikedFormattedName(true),
								The.Player.GetPronounProvider(false).PossessiveAdjective
							}), null, "general", MuralCategory.Slays, MuralWeight.High, null, -1L, true);
							Achievement.ABSORB_PSYCHE.Unlock();
						}
						else
						{
							Popup.Show("You pause as the psyche of " + this.ParentObject.BaseDisplayName + " radiates into nothingness.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							JournalAPI.AddAccomplishment("You slew " + this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false) + " and watched their psyche radiate into nothingness.", string.Concat(new string[]
							{
								"After a climactic battle of wills, =name= slew ",
								this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, true, null, false),
								" and watched ",
								this.ParentObject.its,
								" psyche radiate into nothingness."
							}), string.Format("In the month of {0} of {1}, =name= was challenged by <spice.commonPhrases.pretender.!random.article> to a duel over the rights of {2}. =name= won and had the pretender's psyche kibbled and radiated into nothingness.", Calendar.GetMonth(), Calendar.GetYear(), Factions.GetMostLikedFormattedName(true)), null, "general", MuralCategory.Slays, MuralWeight.Medium, null, -1L, true);
						}
					}
					else
					{
						JournalAPI.AddAccomplishment("You slew " + this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, true, null, false) + ".", "After a climactic battle of wills, =name= slew " + this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, true, null, false) + ".", string.Format("In the month of {0} of {1}, =name= was challenged by <spice.commonPhrases.pretender.!random.article> to a duel over the rights of {2}. =name= won and slew the pretender, charitably not radiating <spice.pronouns.possessive.!random> psyche into nothingness.", Calendar.GetMonth(), Calendar.GetYear(), Factions.GetMostLikedFormattedName(true)), null, "general", MuralCategory.Slays, MuralWeight.Medium, null, -1L, true);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003D45 RID: 15685
		public static readonly int ABSORB_CHANCE = 10;

		// Token: 0x04003D46 RID: 15686
		public int EgoBonus = 1;
	}
}
