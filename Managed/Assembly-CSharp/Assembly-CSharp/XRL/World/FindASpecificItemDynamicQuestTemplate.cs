using System;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x0200091E RID: 2334
	[Serializable]
	public class FindASpecificItemDynamicQuestTemplate : BaseDynamicQuestTemplate
	{
		// Token: 0x060059E8 RID: 23016 RVA: 0x001B459C File Offset: 0x001B279C
		public override void init(DynamicQuestContext context)
		{
			Array values = Enum.GetValues(typeof(QuestStoryType_FindASpecificItem));
			QuestStoryType_FindASpecificItem questStoryType_FindASpecificItem = (QuestStoryType_FindASpecificItem)values.GetValue(Stat.Random(0, values.Length - 1));
			GameObject questDeliveryItem = context.getQuestDeliveryItem();
			questDeliveryItem.AddPart<DynamicQuestTarget>(new DynamicQuestTarget(), true, false);
			questDeliveryItem.SetStringProperty("HasPregeneratedName", "yes", false);
			questDeliveryItem.Render.DisplayName = context.getQuestItemNameMutation(questDeliveryItem.Render.DisplayName);
			string text = base.zoneManager.CacheObject(questDeliveryItem, false, false);
			DynamicQuestDeliveryTarget questDeliveryTarget = context.getQuestDeliveryTarget();
			base.zoneManager.AddZoneBuilder(context.getQuestGiverZone(), 6000, "FindASpecificItemDynamicQuestTemplate_FabricateQuestGiver", "questContext", context, "QST", questStoryType_FindASpecificItem, "deliveryItemID", text, "deliveryTarget", questDeliveryTarget);
			base.zoneManager.AddZoneBuilder(questDeliveryTarget.zoneId, 6000, "FindASpecificItemDynamicQuestTemplate_FabricateQuestItem", "deliveryItemID", text);
		}
	}
}
