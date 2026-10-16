using System;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000921 RID: 2337
	[Serializable]
	public class InteractWithAnObjectDynamicQuestTemplate : BaseDynamicQuestTemplate
	{
		// Token: 0x060059EC RID: 23020 RVA: 0x001B469C File Offset: 0x001B289C
		public override void init(DynamicQuestContext context)
		{
			Array values = Enum.GetValues(typeof(QuestStoryType_InteractWithAnObject));
			QuestStoryType_InteractWithAnObject questStoryType_InteractWithAnObject = (QuestStoryType_InteractWithAnObject)values.GetValue(Stat.Random(0, values.Length - 1));
			GameObject gameObject;
			if (questStoryType_InteractWithAnObject == QuestStoryType_InteractWithAnObject.StrangePlan)
			{
				gameObject = context.getQuestGenericRemoteInteractable();
			}
			else
			{
				gameObject = context.getQuestRemoteInteractable();
			}
			gameObject.AddPart<DynamicQuestTarget>(new DynamicQuestTarget(), true, false);
			gameObject.AddPart<InteractQuestTarget>(new InteractQuestTarget
			{
				EventID = gameObject.GetStringProperty("QuestEvent", null)
			}, true, false);
			gameObject.SetIntProperty("NoAIEquip", 1, false);
			if (!gameObject.HasPart<Shrine>())
			{
				gameObject.GiveProperName(context.getQuestItemNameMutation(gameObject.Render.DisplayName), true, null, false, null, null, null, null);
				gameObject.SetStringProperty("HasPregeneratedName", "yes", false);
				gameObject.SetStringProperty("DefiniteArticle", "the", false);
				gameObject.SetStringProperty("IndefiniteArticle", "the", false);
			}
			gameObject.RequirePart<Interesting>(false).Radius = 1;
			if (gameObject.IsTakeable())
			{
				gameObject.RequirePart<RemoveInterestingOnTake>(false);
			}
			gameObject.FireEvent("SpecialInit");
			string text = base.zoneManager.CacheObject(gameObject, false, false);
			DynamicQuestDeliveryTarget questDeliveryTarget = context.getQuestDeliveryTarget();
			base.zoneManager.AddZoneBuilder(context.getQuestGiverZone(), 6000, "InteractWithAnObjectDynamicQuestTemplate_FabricateQuestGiver", "questContext", context, "QST", questStoryType_InteractWithAnObject, "deliveryItemID", text, "deliveryTarget", questDeliveryTarget);
			base.zoneManager.AddZoneBuilder(questDeliveryTarget.zoneId, 6000, "InteractWithAnObjectDynamicQuestTemplate_FabricateQuestItem", "deliveryItemID", text);
		}
	}
}
