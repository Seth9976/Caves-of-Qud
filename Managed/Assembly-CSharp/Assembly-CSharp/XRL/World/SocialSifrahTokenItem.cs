using System;
using System.Collections.Generic;
using XRL.UI;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008F7 RID: 2295
	[Serializable]
	public class SocialSifrahTokenItem : SifrahPrioritizableToken
	{
		// Token: 0x060057FA RID: 22522 RVA: 0x001AA8E3 File Offset: 0x001A8AE3
		public SocialSifrahTokenItem()
		{
			this.Description = "gift an item";
			this.Tile = "Items/sw_gadget.bmp";
			this.RenderString = "\n";
			this.ColorString = "&M";
			this.DetailColor = 'W';
		}

		// Token: 0x060057FB RID: 22523 RVA: 0x001AA920 File Offset: 0x001A8B20
		public SocialSifrahTokenItem(string Blueprint)
			: this()
		{
			this.Blueprint = Blueprint;
			GameObject gameObject = GameObject.CreateSample(Blueprint);
			this.Description = "gift " + gameObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false);
			gameObject.Obliterate(null, false, null);
		}

		// Token: 0x060057FC RID: 22524 RVA: 0x001AA980 File Offset: 0x001A8B80
		public static SocialSifrahTokenItem GetAppropriate(GameObject ContextObject)
		{
			string stringProperty = ContextObject.GetStringProperty("SignatureItemBlueprint", null);
			if (!stringProperty.IsNullOrEmpty())
			{
				return new SocialSifrahTokenItem(stringProperty);
			}
			int tier = ContextObject.GetTier();
			for (int i = 0; i < 20; i++)
			{
				GameObjectBlueprint randomElement = GameObjectFactory.Factory.BlueprintList.GetRandomElement<GameObjectBlueprint>(null);
				if (!randomElement.HasPart("Brain") && randomElement.HasPart("Physics") && randomElement.HasPart("Render") && !randomElement.Tags.ContainsKey("NoSparkingQuest") && !randomElement.Tags.ContainsKey("BaseObject") && !randomElement.Tags.ContainsKey("ExcludeFromDynamicEncounters") && randomElement.GetPartParameter<bool>("Physics", "Takeable", true) && randomElement.GetPartParameter<bool>("Physics", "IsReal", true) && !randomElement.GetPartParameter<string>("Render", "DisplayName", null).Contains("[") && (!randomElement.Props.ContainsKey("SparkingQuestBlueprint") || randomElement.Name == randomElement.Props["SparkingQuestBlueprint"]) && randomElement.Tier <= tier && randomElement.GetPartParameter<int>("Examiner", "Complexity", 0) == 0)
				{
					return new SocialSifrahTokenItem(randomElement.Name);
				}
			}
			return null;
		}

		// Token: 0x060057FD RID: 22525 RVA: 0x001AAAE1 File Offset: 0x001A8CE1
		public override int GetPriority()
		{
			return this.GetNumberAvailable(0);
		}

		// Token: 0x060057FE RID: 22526 RVA: 0x001AAAEA File Offset: 0x001A8CEA
		public override int GetTiebreakerPriority()
		{
			return 0;
		}

		// Token: 0x060057FF RID: 22527 RVA: 0x001AAAF0 File Offset: 0x001A8CF0
		public int GetNumberAvailable(int Chosen = 0)
		{
			int num = -Chosen;
			using (List<GameObject>.Enumerator enumerator = The.Player.GetInventoryAndEquipment().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Blueprint == this.Blueprint)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06005800 RID: 22528 RVA: 0x001AAB5C File Offset: 0x001A8D5C
		public bool IsAvailable(int Chosen = 0)
		{
			int num = 0;
			using (List<GameObject>.Enumerator enumerator = The.Player.GetInventoryAndEquipment().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Blueprint == this.Blueprint)
					{
						num++;
						if (num > Chosen)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06005801 RID: 22529 RVA: 0x001AABD0 File Offset: 0x001A8DD0
		public override string GetDescription(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return this.Description + " [have {{C|" + this.GetNumberAvailable(Game.GetTimesChosen(this, Slot)).ToString() + "}}]";
		}

		// Token: 0x06005802 RID: 22530 RVA: 0x001AAC08 File Offset: 0x001A8E08
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return !this.IsAvailable(Game.GetTimesChosen(this, Slot)) || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x06005803 RID: 22531 RVA: 0x001AAC28 File Offset: 0x001A8E28
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			int timesChosen = Game.GetTimesChosen(this, null);
			if (!this.IsAvailable(timesChosen))
			{
				GameObject gameObject = GameObject.CreateSample(this.Blueprint);
				if (gameObject == null)
				{
					Popup.ShowFail("You do not have any more of that kind of item.", true, true, true);
				}
				else if (timesChosen > 0)
				{
					Popup.ShowFail("You do not have any more " + gameObject.GetPluralName(false, false, false, false) + ".", true, true, true);
				}
				else
				{
					Popup.ShowFail("You do not have " + gameObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".", true, true, true);
				}
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}

		// Token: 0x06005804 RID: 22532 RVA: 0x001AACD5 File Offset: 0x001A8ED5
		public override int GetPowerup(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (Slot.CurrentMove == Slot.Token)
			{
				return 1;
			}
			return base.GetPowerup(Game, Slot, ContextObject);
		}

		// Token: 0x06005805 RID: 22533 RVA: 0x001AACF0 File Offset: 0x001A8EF0
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			foreach (GameObject gameObject in The.Player.GetInventoryAndEquipment())
			{
				if (gameObject.Blueprint == this.Blueprint)
				{
					gameObject.Destroy(null, false, false, null);
					break;
				}
			}
			base.UseToken(Game, Slot, ContextObject);
		}

		// Token: 0x04002C14 RID: 11284
		public string Blueprint;
	}
}
