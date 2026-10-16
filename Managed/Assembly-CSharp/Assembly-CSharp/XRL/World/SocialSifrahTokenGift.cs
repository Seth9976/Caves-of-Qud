using System;
using Qud.API;
using XRL.UI;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008F8 RID: 2296
	[Serializable]
	public class SocialSifrahTokenGift : SifrahPrioritizableToken
	{
		// Token: 0x06005806 RID: 22534 RVA: 0x001AAD6C File Offset: 0x001A8F6C
		public SocialSifrahTokenGift()
		{
			this.Description = "gift an item";
			this.Tile = "Items/sw_gadget.bmp";
			this.RenderString = "\n";
			this.ColorString = "&M";
			this.DetailColor = 'W';
		}

		// Token: 0x06005807 RID: 22535 RVA: 0x001AADA8 File Offset: 0x001A8FA8
		public SocialSifrahTokenGift(string Blueprint)
			: this()
		{
			this.Blueprint = Blueprint;
			GameObject gameObject = GameObject.CreateSample(Blueprint);
			this.Description = "gift " + gameObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false);
			gameObject.Obliterate(null, false, null);
		}

		// Token: 0x06005808 RID: 22536 RVA: 0x001AAE08 File Offset: 0x001A9008
		public static SocialSifrahTokenGift GetAppropriate(GameObject ContextObject)
		{
			string stringProperty = ContextObject.GetStringProperty("SignatureItemBlueprint", null);
			if (!stringProperty.IsNullOrEmpty())
			{
				return new SocialSifrahTokenGift(stringProperty);
			}
			int tier = ContextObject.GetTier();
			Predicate<GameObjectBlueprint> <>9__0;
			for (int i = 0; i < 10; i++)
			{
				Predicate<GameObjectBlueprint> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = (GameObjectBlueprint pbp) => pbp.HasTagOrProperty("Gift") && !pbp.HasPart("Brain") && pbp.GetPartParameter<bool>("Physics", "Takeable", true) && !pbp.GetPartParameter<string>("Render", "DisplayName", null).Contains("[") && (!pbp.Props.ContainsKey("SparkingQuestBlueprint") || pbp.Name == pbp.Props["SparkingQuestBlueprint"]) && (!pbp.HasTagOrProperty("GiftTrueKinOnly") || ContextObject.IsTrueKin()) && pbp.Tier <= tier);
				}
				GameObjectBlueprint anObjectBlueprintModel = EncountersAPI.GetAnObjectBlueprintModel(predicate);
				if (anObjectBlueprintModel != null)
				{
					string propertyOrTag = anObjectBlueprintModel.GetPropertyOrTag("GiftSkillRestriction");
					if ((propertyOrTag.IsNullOrEmpty() || ContextObject.HasSkill(propertyOrTag)) && anObjectBlueprintModel.GetPartParameter<int>("Examiner", "Complexity", 0) == 0)
					{
						return new SocialSifrahTokenGift(anObjectBlueprintModel.Name);
					}
				}
			}
			return null;
		}

		// Token: 0x06005809 RID: 22537 RVA: 0x001AAED0 File Offset: 0x001A90D0
		public override int GetPriority()
		{
			return this.GetNumberAvailable(0);
		}

		// Token: 0x0600580A RID: 22538 RVA: 0x001AAED9 File Offset: 0x001A90D9
		public override int GetTiebreakerPriority()
		{
			return 0;
		}

		// Token: 0x0600580B RID: 22539 RVA: 0x001AAEDC File Offset: 0x001A90DC
		public int GetNumberAvailable(int Chosen = 0)
		{
			int num = -Chosen;
			foreach (GameObject gameObject in The.Player.GetInventoryAndEquipment())
			{
				if (gameObject.Blueprint == this.Blueprint || gameObject.GetBlueprint(true).DescendsFrom(this.Blueprint))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600580C RID: 22540 RVA: 0x001AAF5C File Offset: 0x001A915C
		public bool IsAvailable(int Chosen = 0)
		{
			int num = 0;
			foreach (GameObject gameObject in The.Player.GetInventoryAndEquipment())
			{
				if (gameObject.Blueprint == this.Blueprint || gameObject.GetBlueprint(true).DescendsFrom(this.Blueprint))
				{
					num++;
					if (num > Chosen)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600580D RID: 22541 RVA: 0x001AAFE4 File Offset: 0x001A91E4
		public override string GetDescription(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return this.Description + " [have {{C|" + this.GetNumberAvailable(Game.GetTimesChosen(this, Slot)).ToString() + "}}]";
		}

		// Token: 0x0600580E RID: 22542 RVA: 0x001AB01C File Offset: 0x001A921C
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return !this.IsAvailable(Game.GetTimesChosen(this, Slot)) || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x0600580F RID: 22543 RVA: 0x001AB03C File Offset: 0x001A923C
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

		// Token: 0x06005810 RID: 22544 RVA: 0x001AB0E9 File Offset: 0x001A92E9
		public override int GetPowerup(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (Slot.CurrentMove == Slot.Token)
			{
				return 1;
			}
			return base.GetPowerup(Game, Slot, ContextObject);
		}

		// Token: 0x06005811 RID: 22545 RVA: 0x001AB104 File Offset: 0x001A9304
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			foreach (GameObject gameObject in The.Player.GetInventoryAndEquipment())
			{
				if (gameObject.Blueprint == this.Blueprint || gameObject.GetBlueprint(true).DescendsFrom(this.Blueprint))
				{
					gameObject.Destroy(null, false, false, null);
					break;
				}
			}
			base.UseToken(Game, Slot, ContextObject);
		}

		// Token: 0x04002C15 RID: 11285
		public string Blueprint;
	}
}
