using System;
using XRL.Rules;

namespace XRL.World
{
	// Token: 0x0200091C RID: 2332
	[Serializable]
	public class FindASiteDynamicQuestTemplate : BaseDynamicQuestTemplate
	{
		// Token: 0x060059E6 RID: 23014 RVA: 0x001B4530 File Offset: 0x001B2730
		public override void init(DynamicQuestContext context)
		{
			Array values = Enum.GetValues(typeof(QuestStoryType_FindASpecificItem));
			QuestStoryType_FindASite questStoryType_FindASite = (QuestStoryType_FindASite)values.GetValue(Stat.Random(0, values.Length - 1));
			base.zoneManager.AddZoneBuilder(context.getQuestGiverZone(), 6000, "FindASpecificSiteDynamicQuestTemplate_FabricateQuestGiver", "questContext", context, "QST", questStoryType_FindASite);
		}
	}
}
