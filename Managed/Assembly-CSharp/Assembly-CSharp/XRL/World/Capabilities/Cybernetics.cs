using System;
using System.Text;
using XRL.UI;
using XRL.Wish;
using XRL.World.Anatomy;
using XRL.World.Units;

namespace XRL.World.Capabilities
{
	// Token: 0x020009E2 RID: 2530
	[HasWishCommand]
	public static class Cybernetics
	{
		// Token: 0x06006C00 RID: 27648 RVA: 0x0022DA44 File Offset: 0x0022BC44
		public static StringBuilder GetCreationDetails(GameObjectBlueprint cyber)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (cyber != null)
			{
				string text = cyber.GetTag("CyberneticsCreationDetails", "");
				if (!string.IsNullOrEmpty(text))
				{
					stringBuilder.Append(text);
				}
				else
				{
					stringBuilder.Append(cyber.GetPartParameter<string>("Description", "Short", null));
					text = cyber.GetPartParameter<string>("CyberneticsBaseItem", "BehaviorDescription", null);
					if (!string.IsNullOrEmpty(text))
					{
						stringBuilder.Append("\n\n{{rules|").Append(text).Append("}}");
					}
				}
				stringBuilder.Append("\n\n");
				if (cyber.HasTag("CyberneticsDestroyOnRemoval", false))
				{
					stringBuilder.Append("{{rules|Destroyed when uninstalled}}\n");
				}
				stringBuilder.Append("{{rules|License points: ").Append(cyber.GetPartParameter<int>("CyberneticsBaseItem", "Cost", 0)).Append("}}");
			}
			else
			{
				stringBuilder.Append("{{rules|+1 Toughness}}\n\n").Append("{{R|-2 License Tier (down to 0)}}");
			}
			return stringBuilder;
		}

		// Token: 0x06006C01 RID: 27649 RVA: 0x0022DB3C File Offset: 0x0022BD3C
		[WishCommand("implant", null)]
		private static void WishImplant(string Argument)
		{
			string name;
			string slot;
			Argument.Split(':', out name, out slot);
			GameObjectBlueprint gameObjectBlueprint = GameObjectFactory.Factory.BlueprintList.Find((GameObjectBlueprint x) => x.Name.EqualsNoCase(name)) ?? GameObjectFactory.Factory.BlueprintList.Find((GameObjectBlueprint x) => x.CachedDisplayNameStripped.EqualsNoCase(name));
			if (gameObjectBlueprint == null)
			{
				Popup.ShowFail("No blueprint by the name '" + name + "' could be found.", true, true, true);
				return;
			}
			if (!gameObjectBlueprint.HasPart("CyberneticsBaseItem"))
			{
				Popup.ShowFail("The blueprint '" + gameObjectBlueprint.Name + "' is not a cybernetic.", true, true, true);
				return;
			}
			if (!slot.IsNullOrEmpty())
			{
				BodyPart bodyPart;
				if ((bodyPart = The.Player.Body.GetFirstPart((BodyPart x) => x.Name.EqualsNoCase(slot))) == null)
				{
					bodyPart = The.Player.Body.GetFirstPart((BodyPart x) => x.Description.EqualsNoCase(slot)) ?? The.Player.Body.GetFirstPart((BodyPart x) => x.Type.EqualsNoCase(slot));
				}
				BodyPart bodyPart2 = bodyPart;
				if (bodyPart2 == null)
				{
					Popup.ShowFail("No body part by the name '" + slot + "' could be found.", true, true, true);
					return;
				}
				if (bodyPart2.Cybernetics != null)
				{
					bodyPart2.Unimplant(false);
				}
				slot = bodyPart2.Name;
			}
			GameObject gameObject = new GameObjectCyberneticsUnit
			{
				Blueprint = gameObjectBlueprint.Name,
				Slot = slot
			}.Implant(The.Player);
			if (gameObject != null)
			{
				BodyPart bodyPart3 = The.Player.FindCybernetics(gameObject);
				Popup.Show(string.Concat(new string[]
				{
					"Your ",
					bodyPart3.GetOrdinalName(),
					" ",
					bodyPart3.Plural ? "are" : "is",
					" implanted with ",
					gameObject.DisplayName,
					"!"
				}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}
	}
}
