using System;
using Qud.API;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008CD RID: 2253
	[Serializable]
	public class RitualSifrahTokenGift : SocialSifrahTokenGift
	{
		// Token: 0x06005773 RID: 22387 RVA: 0x001A8AD0 File Offset: 0x001A6CD0
		public RitualSifrahTokenGift()
		{
			this.Description = "offer an item";
		}

		// Token: 0x06005774 RID: 22388 RVA: 0x001A8AE4 File Offset: 0x001A6CE4
		public RitualSifrahTokenGift(string Blueprint)
			: this()
		{
			this.Blueprint = Blueprint;
			GameObject gameObject = GameObject.CreateSample(Blueprint);
			this.Description = "offer " + gameObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false);
			gameObject.Obliterate(null, false, null);
		}

		// Token: 0x06005775 RID: 22389 RVA: 0x001A8B43 File Offset: 0x001A6D43
		public static bool IsFood(string Blueprint)
		{
			return RitualSifrahTokenGift.IsFood(GameObjectFactory.Factory.GetBlueprintIfExists(Blueprint));
		}

		// Token: 0x06005776 RID: 22390 RVA: 0x001A8B55 File Offset: 0x001A6D55
		public static bool IsFood(GameObjectBlueprint BP)
		{
			return BP != null && BP.HasPart("Food") && !BP.GetPartParameter<bool>("Food", "Gross", false);
		}

		// Token: 0x06005777 RID: 22391 RVA: 0x001A8B80 File Offset: 0x001A6D80
		public new static RitualSifrahTokenGift GetAppropriate(GameObject ContextObject)
		{
			string stringProperty = ContextObject.GetStringProperty("SignatureItemBlueprint", null);
			if (!stringProperty.IsNullOrEmpty())
			{
				return new RitualSifrahTokenGift(stringProperty);
			}
			int tier = ContextObject.GetTier();
			Predicate<GameObjectBlueprint> <>9__0;
			for (int i = 0; i < 10; i++)
			{
				Predicate<GameObjectBlueprint> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = (GameObjectBlueprint pbp) => pbp.HasTagOrProperty("Gift") && !RitualSifrahTokenGift.IsFood(pbp) && !pbp.HasPart("Brain") && pbp.GetPartParameter<bool>("Physics", "Takeable", true) && !pbp.GetPartParameter<string>("Render", "DisplayName", null).Contains("[") && (!pbp.Props.ContainsKey("SparkingQuestBlueprint") || pbp.Name == pbp.Props["SparkingQuestBlueprint"]) && (!pbp.HasTagOrProperty("GiftTrueKinOnly") || ContextObject.IsTrueKin()) && pbp.Tier <= tier);
				}
				GameObjectBlueprint anObjectBlueprintModel = EncountersAPI.GetAnObjectBlueprintModel(predicate);
				if (anObjectBlueprintModel != null)
				{
					string propertyOrTag = anObjectBlueprintModel.GetPropertyOrTag("GiftSkillRestriction");
					if ((propertyOrTag.IsNullOrEmpty() || ContextObject.HasSkill(propertyOrTag)) && anObjectBlueprintModel.GetPartParameter<int>("Examiner", "Complexity", 0) == 0)
					{
						return new RitualSifrahTokenGift(anObjectBlueprintModel.Name);
					}
				}
			}
			return null;
		}
	}
}
