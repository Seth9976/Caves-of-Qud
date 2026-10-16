using System;
using Qud.API;
using XRL.Language;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D0A RID: 3338
	[Serializable]
	public class CookingDomainSpecial_UnitCrystalTransform : ProceduralCookingEffectUnit
	{
		// Token: 0x060083C2 RID: 33730 RVA: 0x002FB5F4 File Offset: 0x002F97F4
		public override string GetDescription()
		{
			return "@they became a crystal being permanently.";
		}

		// Token: 0x060083C3 RID: 33731 RVA: 0x002FB5FB File Offset: 0x002F97FB
		public override string GetTemplatedDescription()
		{
			return "?????";
		}

		// Token: 0x060083C4 RID: 33732 RVA: 0x002FB602 File Offset: 0x002F9802
		public override void Init(GameObject target)
		{
		}

		// Token: 0x060083C5 RID: 33733 RVA: 0x002FB604 File Offset: 0x002F9804
		public override void Apply(GameObject Object, Effect parent)
		{
			CookingDomainSpecial_UnitCrystalTransform.ApplyTo(Object);
		}

		// Token: 0x060083C6 RID: 33734 RVA: 0x002FB60C File Offset: 0x002F980C
		public override void Remove(GameObject Object, Effect parent)
		{
		}

		// Token: 0x060083C7 RID: 33735 RVA: 0x002FB610 File Offset: 0x002F9810
		public static void ApplyTo(GameObject Object)
		{
			if (Object.GetPropertyOrTag("AteCrystalDelight", null) == "true")
			{
				Object.ShowFailure("Your genome has already undergone this transformation.");
				return;
			}
			if (Object.IsPlayer())
			{
				Popup.Show("...", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				Popup.Show("You feel an uncomfortable pressure across the length of your body.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				Popup.Show("Your limbs suddenly feel perversely round and bumpy before they shrink back into your body.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				Popup.Show("An abrading force cuts the surfaces of your torso until you feel polished and perfectly smooth.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				Popup.Show("A quartet of prisms shatter out of you and grow. Each quinfurcates at the tip into five finger prisms.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				Popup.Show("You gained the mutation {{C|Crystallinity}}!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				JournalAPI.AddAccomplishment("You ate some Crystal Delight and became a crystalline being.", string.Concat(new string[]
				{
					"Crystalform! Crystalform! On the ",
					Calendar.GetDay(),
					" of ",
					Calendar.GetMonth(),
					", in the year ",
					Calendar.GetYear().ToString(),
					" AR, =name= underwent the divine transformation and became a crystal being."
				}), string.Concat(new string[]
				{
					"<spice.instancesOf.inYear.!random.capitalize> =year=, <spice.instancesOf.afterTumultuousYears.!random>, ",
					Grammar.MakePossessive(The.Player.BaseDisplayNameStripped),
					" counselors suggested ",
					The.Player.GetPronounProvider(false).Subjective,
					" <spice.instancesOf.abdicate.!random> as sultan. Instead, ",
					The.Player.GetPronounProvider(false).Subjective,
					" ate a meal and underwent a transformation into a crystalline being."
				}), null, "general", MuralCategory.BodyExperienceNeutral, MuralWeight.VeryHigh, null, -1L, true);
				Achievement.ATE_CRYSTAL_DELIGHT.Unlock();
			}
			Body body = Object.Body;
			body.Rebuild("HumanoidOctohedron");
			Mutations mutations = Object.RequirePart<Mutations>(false);
			MutationEntry mutationEntry;
			if (MutationFactory.TryGetMutationEntry("Crystallinity", out mutationEntry))
			{
				mutations.AddMutation(mutationEntry, 1);
			}
			body.RegenerateDefaultEquipment();
			Object.Render.RenderString = "µ";
			Object.Render.Tile = "Creatures/sw_crystal_body.png";
			Object.SetStringProperty("AteCrystalDelight", "true", false);
			Object.SetStringProperty("Species", "crystal", false);
		}
	}
}
