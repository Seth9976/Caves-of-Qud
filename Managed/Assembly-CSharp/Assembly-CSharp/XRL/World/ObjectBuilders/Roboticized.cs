using System;
using ConsoleLib.Console;
using XRL.Wish;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A1B RID: 2587
	[HasWishCommand]
	[Serializable]
	public class Roboticized : IObjectBuilder
	{
		// Token: 0x06006D3D RID: 27965 RVA: 0x00249708 File Offset: 0x00247908
		public override void Initialize()
		{
			this.NamePrefix = "{{c|mechanical}}";
			this.DescriptionPostfix = "There is a low, persistent hum emanating outward.";
		}

		// Token: 0x06006D3E RID: 27966 RVA: 0x00249720 File Offset: 0x00247920
		public override void Apply(GameObject Object, string Context)
		{
			Roboticized.Roboticize(Object, this.NamePrefix, this.DescriptionPostfix);
		}

		// Token: 0x06006D3F RID: 27967 RVA: 0x00249734 File Offset: 0x00247934
		public static void Roboticize(GameObject Object, string NamePrefix = "{{c|mechanical}}", string DescriptionPostfix = "There is a low, persistent hum emanating outward.")
		{
			if (Object.HasPart<Robot>())
			{
				return;
			}
			Object.AddPart<Robot>(new Robot(), true, false);
			Object.RequirePart<MentalShield>(false);
			Object.RequirePart<Metal>(false);
			Object.RequirePart<MaintenanceSystems>(false);
			Object.RemovePart<Springy>();
			Object.RemovePart<Stomach>();
			if (!Object.HasPart<DarkVision>())
			{
				Object.GetPart<Mutations>().AddMutation(new DarkVision(), 12, true);
			}
			Corpse corpse;
			if (Object.TryGetPart<Corpse>(out corpse))
			{
				corpse.CorpseChance = 0;
				corpse.BurntCorpseChance = 0;
				corpse.VaporizedCorpseChance = 0;
			}
			Object.GetStat("ElectricResistance").BaseValue = -50;
			Object.GetStat("HeatResistance").BaseValue = 25;
			Object.GetStat("ColdResistance").BaseValue = 25;
			Object.IsOrganic = false;
			Object.SetStringProperty("SeveredLimbBlueprint", "RobotLimb", false);
			Object.SetStringProperty("SeveredHeadBlueprint", "RobotHead1", false);
			Object.SetStringProperty("SeveredFaceBlueprint", "RobotFace", false);
			Object.SetStringProperty("SeveredArmBlueprint", "RobotArm", false);
			Object.SetStringProperty("SeveredHandBlueprint", "RobotHand", false);
			Object.SetStringProperty("SeveredLegBlueprint", "RobotLeg", false);
			Object.SetStringProperty("SeveredFootBlueprint", "RobotFoot", false);
			Object.SetStringProperty("SeveredFeetBlueprint", "RobotFeet", false);
			Object.SetStringProperty("SeveredTailBlueprint", "RobotTail", false);
			Object.SetStringProperty("SeveredRootsBlueprint", "RobotRoots", false);
			Object.SetStringProperty("SeveredFinBlueprint", "RobotFin", false);
			Object.SetIntProperty("Bleeds", 1, false);
			Object.SetStringProperty("BleedLiquid", "oil-1000", false);
			Object.SetStringProperty("BleedColor", "&K", false);
			Object.SetStringProperty("BleedPrefix", "&Koily", false);
			Render render = Object.Render;
			string text = (render.TileColor.IsNullOrEmpty() ? render.ColorString : render.TileColor);
			render.ColorString = "&c";
			render.TileColor = "&c";
			if (render.DetailColor == "c")
			{
				char? c;
				render.DetailColor = ((ColorUtility.FindLastForeground(text) != null) ? c.GetValueOrDefault().ToString() : null) ?? Crayons.GetRandomColor(null);
			}
			if (!NamePrefix.IsNullOrEmpty())
			{
				Object.RequirePart<DisplayNameAdjectives>(false).AddAdjective(NamePrefix);
			}
			Body body = Object.Body;
			if (body != null)
			{
				body.CategorizeAll(7);
			}
			if (!DescriptionPostfix.IsNullOrEmpty() && Object.HasPart<Description>())
			{
				if (Object.HasTag("VerseDescription"))
				{
					Description part = Object.GetPart<Description>();
					part._Short = part._Short + "\n\n" + DescriptionPostfix;
					return;
				}
				Description part2 = Object.GetPart<Description>();
				part2._Short = part2._Short + " " + DescriptionPostfix;
			}
		}

		// Token: 0x06006D40 RID: 27968 RVA: 0x002499F0 File Offset: 0x00247BF0
		[WishCommand("mechanical", null)]
		public static void Wish(string Blueprint)
		{
			WishResult wishResult = WishSearcher.SearchForBlueprint(Blueprint);
			GameObject gameObject = GameObjectFactory.Factory.CreateObject(wishResult.Result, 0, 0, null, null, null, "Wish", null);
			Roboticized.Roboticize(gameObject, "{{c|mechanical}}", "There is a low, persistent hum emanating outward.");
			The.PlayerCell.getClosestEmptyCell().AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
		}

		// Token: 0x040031E8 RID: 12776
		public const string PREFIX_NAME = "{{c|mechanical}}";

		// Token: 0x040031E9 RID: 12777
		public const string POSTFIX_DESC = "There is a low, persistent hum emanating outward.";

		// Token: 0x040031EA RID: 12778
		public string NamePrefix;

		// Token: 0x040031EB RID: 12779
		public string DescriptionPostfix;
	}
}
