using System;
using HistoryKit;
using XRL.Annals;

namespace XRL.World.Parts
{
	// Token: 0x02000E64 RID: 3684
	public static class TombCultistTemplate
	{
		// Token: 0x060094F2 RID: 38130 RVA: 0x0035DDB4 File Offset: 0x0035BFB4
		public static void Apply(GameObject GO, HistoricEntitySnapshot SultanSnapshot)
		{
			GO.DisplayName = GO.Render.DisplayName + " and death pilgrim of the {{Y|" + SultanSnapshot.GetProperty("cultName", "unknown") + "}}";
			ConversationScript part = GO.GetPart<ConversationScript>();
			if (part != null)
			{
				part.Append = "\n\nTread in peace at the tomb of " + QudHistoryHelpers.GetRandomCognomen(SultanSnapshot) + ".";
			}
		}
	}
}
