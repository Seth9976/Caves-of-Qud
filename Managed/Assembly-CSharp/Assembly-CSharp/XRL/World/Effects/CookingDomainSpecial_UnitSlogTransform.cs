using System;
using Qud.API;
using XRL.Language;
using XRL.UI;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D09 RID: 3337
	[Serializable]
	public class CookingDomainSpecial_UnitSlogTransform : ProceduralCookingEffectUnit
	{
		// Token: 0x060083BB RID: 33723 RVA: 0x002FB3E9 File Offset: 0x002F95E9
		public override string GetDescription()
		{
			return "@they became a slug permanently.";
		}

		// Token: 0x060083BC RID: 33724 RVA: 0x002FB3F0 File Offset: 0x002F95F0
		public override string GetTemplatedDescription()
		{
			return "?????";
		}

		// Token: 0x060083BD RID: 33725 RVA: 0x002FB3F7 File Offset: 0x002F95F7
		public override void Init(GameObject target)
		{
		}

		// Token: 0x060083BE RID: 33726 RVA: 0x002FB3F9 File Offset: 0x002F95F9
		public override void Apply(GameObject Object, Effect parent)
		{
			CookingDomainSpecial_UnitSlogTransform.ApplyTo(Object);
		}

		// Token: 0x060083BF RID: 33727 RVA: 0x002FB401 File Offset: 0x002F9601
		public override void Remove(GameObject Object, Effect parent)
		{
		}

		// Token: 0x060083C0 RID: 33728 RVA: 0x002FB404 File Offset: 0x002F9604
		public static void ApplyTo(GameObject Object)
		{
			if (Object.GetPropertyOrTag("AteCloacaSurprise", null) == "true")
			{
				Object.ShowFailure("Your genome has already undergone this transformation.");
				return;
			}
			if (Object.IsPlayer())
			{
				Popup.Show("...", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				Popup.Show("You feel an uncomfortable pressure across the length of your body.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				Popup.Show("Feelers rip through your scalp and shudder with curiosity.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				Popup.Show("Your arms shrink into your torso.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				Popup.Show("A bilge hose painted with mucus undulates out of your lower body. It spews the amniotic broth of its birth from its sputtering mouth.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				Popup.Show("You gained the mutation {{w|Bilge Sphincter}}!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				JournalAPI.AddAccomplishment("You ate the Cloaca Surprise.", string.Concat(new string[]
				{
					"Slugform! Slugform! On the ",
					Calendar.GetDay(),
					" of ",
					Calendar.GetMonth(),
					", in the year ",
					Calendar.GetYear().ToString(),
					" AR, =name= underwent the divine transformation and assumed the Slugform."
				}), string.Concat(new string[]
				{
					"Deep in the bowels of Golgotha, =name= came upon a giant slug performing a secret ritual. Because of ",
					Grammar.MakePossessive(The.Player.BaseDisplayNameStripped),
					" <spice.elements.",
					The.Player.GetMythicDomain(5, null),
					".quality.!random>, the slug taught ",
					The.Player.BaseDisplayNameStripped,
					" the secret to being a slug."
				}), null, "general", MuralCategory.BodyExperienceNeutral, MuralWeight.VeryHigh, null, -1L, true);
				Achievement.ATE_SURPRISE.Unlock();
			}
			Object.Body.Rebuild("SlugWithHands");
			Object.RequirePart<Mutations>(false).AddMutation(new SlogGlands(), 1, true);
			Object.Render.RenderString = "Q";
			Object.Render.Tile = "Creatures/sw_slog.bmp";
			Object.SetStringProperty("AteCloacaSurprise", "true", false);
			Object.SetStringProperty("Species", "slug", false);
		}
	}
}
