using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008CC RID: 2252
	[Serializable]
	public class RitualSifrahTokenItem : SocialSifrahTokenItem
	{
		// Token: 0x06005770 RID: 22384 RVA: 0x001A88E7 File Offset: 0x001A6AE7
		public RitualSifrahTokenItem()
		{
			this.Description = "offer an item";
		}

		// Token: 0x06005771 RID: 22385 RVA: 0x001A88FC File Offset: 0x001A6AFC
		public RitualSifrahTokenItem(string Blueprint)
			: this()
		{
			this.Blueprint = Blueprint;
			GameObject gameObject = GameObject.CreateSample(Blueprint);
			this.Description = "offer " + gameObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false);
			gameObject.Obliterate(null, false, null);
		}

		// Token: 0x06005772 RID: 22386 RVA: 0x001A895C File Offset: 0x001A6B5C
		public new static RitualSifrahTokenItem GetAppropriate(GameObject ContextObject)
		{
			string stringProperty = ContextObject.GetStringProperty("SignatureItemBlueprint", null);
			if (!stringProperty.IsNullOrEmpty() && !RitualSifrahTokenFood.IsFood(stringProperty))
			{
				return new RitualSifrahTokenItem(stringProperty);
			}
			int tier = ContextObject.GetTier();
			for (int i = 0; i < 30; i++)
			{
				GameObjectBlueprint randomElement = GameObjectFactory.Factory.BlueprintList.GetRandomElement<GameObjectBlueprint>(null);
				if (!randomElement.HasPart("Brain") && randomElement.HasPart("Physics") && randomElement.HasPart("Render") && !RitualSifrahTokenFood.IsFood(randomElement) && !randomElement.Tags.ContainsKey("NoSparkingQuest") && !randomElement.Tags.ContainsKey("BaseObject") && !randomElement.Tags.ContainsKey("ExcludeFromDynamicEncounters") && randomElement.GetPartParameter<bool>("Physics", "Takeable", true) && randomElement.GetPartParameter<bool>("Physics", "IsReal", true) && !randomElement.GetPartParameter<string>("Render", "DisplayName", null).Contains("[") && (!randomElement.Props.ContainsKey("SparkingQuestBlueprint") || randomElement.Name == randomElement.Props["SparkingQuestBlueprint"]) && randomElement.Tier <= tier && randomElement.GetPartParameter<int>("Examiner", "Complexity", 0) == 0)
				{
					return new RitualSifrahTokenItem(randomElement.Name);
				}
			}
			return null;
		}
	}
}
