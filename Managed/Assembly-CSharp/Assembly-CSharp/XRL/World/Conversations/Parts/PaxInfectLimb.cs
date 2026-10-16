using System;
using System.Collections.Generic;
using Qud.API;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.QuestManagers;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C0E RID: 3086
	public class PaxInfectLimb : IConversationPart
	{
		// Token: 0x06007BC4 RID: 31684 RVA: 0x002E809C File Offset: 0x002E629C
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == IsElementVisibleEvent.ID || ID == GetChoiceTagEvent.ID || ID == EnterElementEvent.ID;
		}

		// Token: 0x06007BC5 RID: 31685 RVA: 0x002E80C4 File Offset: 0x002E62C4
		public override bool HandleEvent(IsElementVisibleEvent E)
		{
			if (!this.IfQuestActive)
			{
				return base.HandleEvent(E);
			}
			if (The.Player.HasObjectEquipped("PaxInfection", 1))
			{
				return false;
			}
			SpreadPax system = The.Game.GetSystem<SpreadPax>();
			return system != null && !system.Quest.Finished;
		}

		// Token: 0x06007BC6 RID: 31686 RVA: 0x002E8113 File Offset: 0x002E6313
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = "{{g|[Select limb to infect]}}";
			return false;
		}

		// Token: 0x06007BC7 RID: 31687 RVA: 0x002E8121 File Offset: 0x002E6321
		public override bool HandleEvent(EnterElementEvent E)
		{
			return PaxInfectLimb.Infect(The.Player) && base.HandleEvent(E);
		}

		// Token: 0x06007BC8 RID: 31688 RVA: 0x002E8138 File Offset: 0x002E6338
		public static bool Infect(GameObject Object)
		{
			List<BodyPart> parts = Object.Body.GetParts();
			BodyPart bodyPart;
			string text;
			if (!FungalSporeInfection.ChooseLimbForInfection(parts, "Klanq", out bodyPart, out text, true))
			{
				return false;
			}
			if (PaxInfectLimb.InfectLimb(parts, bodyPart, text))
			{
				GameObject equipped = bodyPart.Equipped;
				if (!(((equipped != null) ? equipped.Blueprint : null) != "PaxInfection"))
				{
					return true;
				}
			}
			Popup.Show("The limb rejects the infection!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			return false;
		}

		// Token: 0x06007BC9 RID: 31689 RVA: 0x002E81A8 File Offset: 0x002E63A8
		public static bool InfectLimb(List<BodyPart> Parts, BodyPart Target, string Name)
		{
			if (Target.Equipped != null && !Target.TryUnequip(false, false, false, false))
			{
				return false;
			}
			GameObject gameObject = GameObjectFactory.Factory.CreateObject("PaxInfection");
			gameObject.GetPart<Armor>().AV = ((Target.Type == "Body") ? 3 : 1);
			if (Target.Type == "Hand")
			{
				MeleeWeapon part = gameObject.GetPart<MeleeWeapon>();
				part.BaseDamage = "1d4";
				part.Skill = "Cudgel";
				part.PenBonus = 0;
				part.MaxStrengthBonus = 4;
				gameObject.SetIntProperty("ShowMeleeWeaponStats", 1, false);
			}
			if (Target.SupportsDependent != null)
			{
				foreach (BodyPart bodyPart in Parts)
				{
					if (bodyPart != Target && !(bodyPart.DependsOn != Target.SupportsDependent) && FungalSporeInfection.BodyPartSuitableForFungalInfection(bodyPart, true))
					{
						gameObject.UsesSlots = Target.Type + "," + bodyPart.Type;
						break;
					}
				}
			}
			if (!Target.Equip(gameObject, new int?(0), true, false, false, true))
			{
				gameObject.Destroy(null, false, false, null);
				return false;
			}
			JournalAPI.AddAccomplishment(string.Concat(new string[]
			{
				"You contracted ",
				gameObject.DisplayNameOnly,
				" on your ",
				Name,
				", endearing ",
				The.Player.itself,
				" to fungi across Qud."
			}), string.Concat(new string[]
			{
				"In a show of unprecedented solidarity with fungi, =name= deigned to contract ",
				gameObject.DisplayNameOnly,
				" on ",
				The.Player.GetPronounProvider(false).PossessiveAdjective,
				" ",
				Name,
				"."
			}), string.Concat(new string[]
			{
				"While traveling around ",
				The.Player.CurrentZone.GetTerrainDisplayName(true),
				", =name= discovered ",
				JournalAPI.GetLandmarkNearestPlayer().Text,
				". There ",
				The.Player.GetPronounProvider(false).Subjective,
				" befriened fungi and contracted ",
				gameObject.DisplayNameOnly,
				" on ",
				The.Player.GetPronounProvider(false).PossessiveAdjective,
				" ",
				Name,
				"."
			}), null, "general", MuralCategory.BodyExperienceNeutral, MuralWeight.Medium, null, -1L, true);
			Popup.Show(string.Concat(new string[] { "You've contracted ", gameObject.DisplayNameOnly, " on your ", Name, "." }), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			return true;
		}

		// Token: 0x0400383B RID: 14395
		public bool IfQuestActive;
	}
}
