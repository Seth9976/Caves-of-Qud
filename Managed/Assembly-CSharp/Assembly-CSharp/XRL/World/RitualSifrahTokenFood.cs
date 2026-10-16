using System;
using XRL.World.Parts.Mutation;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008CE RID: 2254
	[Serializable]
	public class RitualSifrahTokenFood : SocialSifrahTokenItem
	{
		// Token: 0x06005778 RID: 22392 RVA: 0x001A8C48 File Offset: 0x001A6E48
		public RitualSifrahTokenFood()
		{
			this.Description = "offer food";
			this.Tile = "Items/sw_rawmeat1.bmp";
			this.RenderString = "%";
			this.ColorString = "&r";
			this.DetailColor = 'w';
		}

		// Token: 0x06005779 RID: 22393 RVA: 0x001A8C84 File Offset: 0x001A6E84
		public RitualSifrahTokenFood(string Blueprint)
			: this()
		{
			this.Blueprint = Blueprint;
			GameObject gameObject = GameObject.CreateSample(Blueprint);
			this.Description = "offer " + gameObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false);
			gameObject.Obliterate(null, false, null);
		}

		// Token: 0x0600577A RID: 22394 RVA: 0x001A8CE3 File Offset: 0x001A6EE3
		public static bool IsFood(string Blueprint)
		{
			return RitualSifrahTokenFood.IsFood(GameObjectFactory.Factory.GetBlueprintIfExists(Blueprint));
		}

		// Token: 0x0600577B RID: 22395 RVA: 0x001A8CF5 File Offset: 0x001A6EF5
		public static bool IsFood(GameObjectBlueprint BP)
		{
			return BP != null && BP.HasPart("Food") && !BP.GetPartParameter<bool>("Food", "Gross", false);
		}

		// Token: 0x0600577C RID: 22396 RVA: 0x001A8D20 File Offset: 0x001A6F20
		public new static RitualSifrahTokenFood GetAppropriate(GameObject ContextObject)
		{
			string stringProperty = ContextObject.GetStringProperty("SignatureItemBlueprint", null);
			if (!stringProperty.IsNullOrEmpty() && RitualSifrahTokenFood.IsFood(stringProperty))
			{
				return new RitualSifrahTokenFood(stringProperty);
			}
			int tier = ContextObject.GetTier();
			for (int i = 0; i < 30; i++)
			{
				GameObjectBlueprint randomElement = GameObjectFactory.Factory.BlueprintList.GetRandomElement<GameObjectBlueprint>(null);
				if (!randomElement.HasPart("Brain") && randomElement.HasPart("Physics") && randomElement.HasPart("Render") && RitualSifrahTokenFood.IsFood(randomElement) && (!ContextObject.HasPart<Carnivorous>() || randomElement.Tags.ContainsKey("Meat")) && !randomElement.Tags.ContainsKey("NoSparkingQuest") && !randomElement.Tags.ContainsKey("BaseObject") && !randomElement.Tags.ContainsKey("ExcludeFromDynamicEncounters") && randomElement.GetPartParameter<bool>("Physics", "Takeable", true) && randomElement.GetPartParameter<bool>("Physics", "IsReal", true) && !randomElement.GetPartParameter<string>("Render", "DisplayName", null).Contains("[") && (!randomElement.Props.ContainsKey("SparkingQuestBlueprint") || randomElement.Name == randomElement.Props["SparkingQuestBlueprint"]) && randomElement.Tier <= tier && randomElement.GetPartParameter<int>("Examiner", "Complexity", 0) == 0)
				{
					return new RitualSifrahTokenFood(randomElement.Name);
				}
			}
			return null;
		}
	}
}
